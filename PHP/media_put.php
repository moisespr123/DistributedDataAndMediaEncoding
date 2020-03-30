<?php

/**
 * Post parameters:
 * k = user key
 * filedata = the file
 * a = album/category
 * c = command line arguments
 * n = name
 * f = format
 */
require_once("scripts_config.php");
require_once("custom_functions.php");
global $mysqli;
if (filter_input(INPUT_POST, 'k')) {
    $user_id = getUser($mysqli, filter_input(INPUT_POST, 'k'));
    if ($user_id != 0) {
        $category_hash = bin2hex(random_bytes(16));
        $random_token = bin2hex(random_bytes(16));
        $ext = (new SplFileInfo($_FILES['filedata']['name']))->getExtension();
        $input_file = $random_token . "." . $ext;
        $filenames[] = $input_file;
        $isset_picture = isset($_FILES["picture"]);
        if (move_uploaded_file($_FILES["filedata"]["tmp_name"], $download_folder . $input_file)) {
            if ($isset_picture) {
                move_uploaded_file($_FILES["picture"]["tmp_name"], $download_folder . $input_file . ".imgfile");
                $filenames[] = $input_file . ".imgfile";
            }
            chmod($download_folder . $input_file, 777);
            $format = filter_input(INPUT_POST, 'f');
            chdir($templates_folder);
            $filename = $random_token;
            $wu_template = fopen($random_token . "_wu", "w");
            $result_template = fopen($random_token . "_result", "w");
            if ($format == "mp3packer") {
                $filename .= "-out.mp3";
                $app = "mp3packer";
                fwrite($wu_template, generate_mp3packer_wu_template_with_cmd($input_file, filter_input(INPUT_POST, 'c'), $filename));
            } else if ($format == "opus") {
                $filename .= "-out.opus";
                $app = "opus_encoder";
                fwrite($wu_template, generate_opus_wu_template_with_cmd($app, $input_file, filter_input(INPUT_POST, 'c'), $filename, $isset_picture));
            } else if ($format == "opus_ffmpeg_libopus") {
                $filename .= "-out.opus";
                $app = "ffmpeg_encoder";
                fwrite($wu_template, generate_opus_wu_template_with_cmd($app, $input_file, filter_input(INPUT_POST, 'c'), $filename, $isset_picture));
            } else if ($format == "flac") {
                $filename .= "-out.flac";
                $app = "flac_encoder";
                fwrite($wu_template, generate_flac_wu_template_with_cmd($input_file, filter_input(INPUT_POST, 'c'), $filename, $isset_picture));
            } else if ($format == "paq8px_v185") {
                $app = "paq8px_v185";
                $commandLineArgs = filter_input(INPUT_POST, 'c');
                if ($commandLineArgs == '-d') {
                    $filename = (new SplFileInfo($input_file))->getBasename($ext) . "extracted";
                    fwrite($wu_template, generate_paq8px_wu_template_with_cmd($input_file, $commandLineArgs, $filename, $isset_picture));
                } else {
                    $filename .= "-out.paq8px185";
                    fwrite($wu_template, generate_paq8px_wu_template_with_cmd($input_file, $commandLineArgs, $filename, $isset_picture));
                }
            }
            fwrite($result_template, generate_generic_result_template($filename));
            $job_creation_command = return_job_string_multiple_files($app, $random_token, $filenames);
            fclose($wu_template);
            fclose($result_template);
            chdir($root_folder);
            exec($job_creation_command);
            $category = "";
            insertUserFile($mysqli, $user_id, $category_hash, filter_input(INPUT_POST, 'a'), $random_token, $input_file, $filename, filter_input(INPUT_POST, 'n'), $app, 1);
            rename($download_folder . $input_file, $move_folder . $input_file);
            if ($isset_picture) {
                rename($download_folder . $input_file . ".imgfile", $move_folder . $input_file . ".imgfile");
            }
            echo "Done";
        } else {
            echo "Error";
        }
    } else {
        echo "Incorrect User Key";
    }
} else {
    echo "Invalid Request";
}
