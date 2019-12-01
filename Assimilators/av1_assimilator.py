from database_functions import *
import os
import subprocess

path_dict = {'rav1e_encoder': {'output_path': assimilator_config.rav1e_output_path,
                               'ffmpeg_concatenate_path': assimilator_config.rav1e_ffmpeg_concatenate_path_prefix},
             'svt_av1_encoder': {'output_path': assimilator_config.svt_av1_output_path,
                                 'ffmpeg_concatenate_path': assimilator_config.svt_av1_ffmpeg_concatenate_path_prefix}
             }


def av1_assimilator(wu_basename):
    update_database(wu_basename, "UPDATE user_media_files SET processed=1 WHERE random_token='%s'")
    category_hash, app = database_select_multi(wu_basename,
                                               "SELECT category_hash, app FROM user_media_files "
                                               "WHERE random_token='%s'")[0]
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

        if os.path.exists(path_dict[app]["ffmpeg_concatenate_path"] + '/output/' +
                          category_hash + ".webm"):
            # Finally, we'll mark te file as processed in the table if the file exist.
            update_database(category_hash,
                            "UPDATE user_media_files SET processed=1 WHERE random_token='%s'")

            # Time for cleanup!
            for output_filename in output_filenames:
                os.remove(path_dict[app]["ffmpeg_concatenate_path"] + "/" + output_filename)
            os.remove(assimilator_config.ffmpeg_output_path_prefix + '/' + audio_file)
