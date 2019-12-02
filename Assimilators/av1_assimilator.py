from database_functions import *
import assimilator_functions
import os
import subprocess

path_dict = {'rav1e_encoder': {'output_path': assimilator_config.rav1e_output_path,
                               'ffmpeg_concatenate_path': assimilator_config.rav1e_ffmpeg_concatenate_path_prefix},
             'svt_av1_encoder': {'output_path': assimilator_config.svt_av1_output_path,
                                 'ffmpeg_concatenate_path': assimilator_config.svt_av1_ffmpeg_concatenate_path_prefix}
             }


def generate_2pass_wu(wu_basename, app):
    input_file, output_file = database_select_multi(wu_basename, "SELECT input_file, output_file FROM user_media_files "
                                                                 "WHERE random_token='%s'")[0]
    random_token = database_select(input_file, "SELECT random_token FROM user_media_files "
                                               "WHERE input_file='%s' AND type=4")

    # Move RAW y4m back to BOINC download folder
    os.rename(assimilator_config.raw_files_path + '/' + input_file,
              assimilator_config.boinc_download_path + '/' + input_file)

    # Move First Pass file to BOINC download folder
    os.rename(path_dict[app]['output_path'] + '/' + output_file,
              assimilator_config.boinc_download_path + '/' + output_file)

    current_dir = os.getcwd()
    os.chdir(assimilator_config.boinc_server_path)
    # Create workunit using the already generated templates
    subprocess.call(['./bin/create_work -appname ' + app + ' -wu_name ' + app + '_' + random_token +
                     ' -wu_template templates/' + random_token + '_wu -result_template templates/' + random_token +
                     '_result "' + input_file + '" "' + output_file + '"'], shell=True)

    # Move back RAW file
    os.rename(assimilator_config.boinc_download_path + '/' + input_file,
              assimilator_config.raw_files_path + '/' + input_file)

    # Delete the first pass file
    os.remove(assimilator_config.boinc_download_path + '/' + output_file)
    os.chdir(current_dir)


def av1_assimilator(wu_basename):
    update_database(wu_basename, "UPDATE user_media_files SET processed=1 WHERE random_token='%s'")
    category_hash, app, wu_type = database_select_multi(wu_basename,
                                                        "SELECT category_hash, app, type FROM user_media_files "
                                                        "WHERE random_token='%s'")[0]

    # Check if the file is the first pass
    if wu_type == 5:
        generate_2pass_wu(wu_basename, app)
        return

    # Delete RAW file:
    if app in assimilator_config.delete_raw_files:
        assimilator_functions.delete_raw_files(wu_basename)

    # If we made it here, it means the file is either a single-pass encode, or the file is the second-pass.
    files = database_select_multi(category_hash,
                                  "SELECT processed, output_file FROM user_media_files "
                                  "WHERE category_hash='%s'")
    processed_list = []
    output_filenames = []
    audio_file = None
    for file in files:
        if not str(file[1]).endswith(".webm"):
            processed_list.append(file[0])
            output_filenames.append(file[1])

    # Check that all files are processed
    if all(processed_list):
        # We'll write a text file with the filenames to concatenate
        merge_list = open(path_dict[app]["output_path"] + '/' +
                          category_hash + ".txt", "w")
        for output_filename in output_filenames:
            if str(output_filename).endswith(".ivf"):
                merge_list.write("file '" + output_filename + "'\n")
            elif str(output_filename).endswith(".opus"):
                audio_file = output_filename
        merge_list.close()

        # We concatenate the files
        subprocess.call(['/mnt/c/Windows/system32/cmd.exe', '/c', 'ffmpeg', '-f', 'concat', '-safe', '0', '-i',
                         path_dict[app]["ffmpeg_concatenate_path"] + '/' +
                         category_hash + ".txt", '-c', 'copy',
                         path_dict[app]["ffmpeg_concatenate_path"] + '/' +
                         category_hash + '.ivf', '-y'])

        # Now, we'll merge the concatenated file with the audio file.
        subprocess.call(['/mnt/c/Windows/system32/cmd.exe', '/c', 'ffmpeg', '-i',
                         path_dict[app]["ffmpeg_concatenate_path"] + '/' +
                         category_hash + '.ivf', '-i',
                         assimilator_config.ffmpeg_output_path_prefix + '/' +
                         audio_file, '-c:v', 'copy', '-c:a', 'copy',
                         path_dict[app]["ffmpeg_concatenate_path"] + '/output/' +
                         category_hash + ".webm", '-y'])

        if os.path.exists(path_dict[app]["output_path"] + '/output/' +
                          category_hash + ".webm"):
            # Finally, we'll mark te file as processed in the table if the file exist.
            update_database(category_hash,
                            "UPDATE user_media_files SET processed=1 WHERE random_token='%s'")

            # Time for cleanup!
            for output_filename in output_filenames:
                if str(output_filename).endswith(".ivf"):
                    file_to_remove = path_dict[app]["output_path"] + "/" + output_filename
                    if os.path.exists(file_to_remove):
                        os.remove(file_to_remove)
            os.remove(assimilator_config.ffmpeg_output_path + '/' + audio_file)
            os.remove(path_dict[app]["output_path"] + "/" + category_hash + '.txt')
            os.remove(path_dict[app]["output_path"] + "/" + category_hash + '.ivf')
