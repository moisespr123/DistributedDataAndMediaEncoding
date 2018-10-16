<?php
/**
 * Created by PhpStorm.
 * User: cardo
 * Date: 10/1/2018
 * Time: 5:54 PM
 *
 * Post parameters:
 * k = user key
 * filedata = the file
 * a = album to use
 * c = command line arguments
 * n = name
 * f = format
 */
require_once("scripts_config.php");
require_once("custom_functions.php");
global $mysqli;
if (isset($_POST['k'])) {
    $user_id = getUser($mysqli, $_POST['k']);
    if ($user_id != 0) {
        $random_token = bin2hex(random_bytes(16));
        $ext = pathinfo($_FILES['filedata']['name']);
        $ext = $ext['extension'];
        $input_file = $random_token . "." . $ext;
        $filenames[] = $input_file;
        if (move_uploaded_file($_FILES["filedata"]["tmp_name"], $download_folder . $input_file)) {
            if (isset($_FILES["picture"])) {
                move_uploaded_file($_FILES["picture"]["tmp_name"], $download_folder . $input_file . ".imgfile");
                $filenames[] = $input_file . ".imgfile";
            }
            chmod($download_folder . $input_file, 777);
            $format = $_POST["f"];
            chdir($templates_folder);
            $filename = $random_token;
            $wu_template = fopen($random_token . "_wu", "w");
            $result_template = fopen($random_token . "_result", "w");
            if ($format == "opus") {
                $filename .= "-out.opus";
                $app = "opus_encoder";
                fwrite($wu_template, generate_opus_wu_template_with_cmd($input_file, $_POST['c'], $filename, isset($_FILES["picture"])));
            } else if ($format == "flac") {
                $filename .= "-out.flac";
                $app = "flac_encoder";
                fwrite($wu_template, generate_flac_wu_template_with_cmd($input_file, $_POST['c'], $filename, isset($_FILES["picture"])));
            }
            fwrite($result_template, generate_put_result_template($filename));
            $job_creation_command = return_job_string_multiple_files($app, $random_token, $filenames);
            fclose($wu_template);
            fclose($result_template);
            chdir($root_folder);
            exec($job_creation_command);
            insertAudioTrack($mysqli, $user_id, $filename, $_POST['n'], $app);
            if (isset($_POST['a'])) {
                insertAlbum($mysqli, $user_id, $_POST['a'], $filename);
            }
            rename($download_folder . $input_file, $move_folder . $input_file);
            if (isset($_FILES['picture']))
                rename($download_folder . $input_file . ".imgfile", $move_folder . $input_file . ".imgfile");
            echo "Done";
        } else {
            echo "Error";
        }
    } else {
        echo "Incorrect User Key";
    }
}
