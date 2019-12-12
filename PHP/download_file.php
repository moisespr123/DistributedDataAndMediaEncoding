<?php

$type = 0;

//Check to see if it is a GET or POST request.
if (filter_input(INPUT_GET, 'f')){
    $type = 1;
} else if (filter_input(INPUT_POST, 'k') && filter_input(INPUT_POST, 'f')) {
    $type = 2;
}

//If it is a valid request, proceed to download.
if ($type != 0) {
    require_once("../inc/util.inc");
    require_once("../inc/user.inc");
    require_once("scripts_config.php");
    require_once("custom_functions.php");
    global $mysqli;
    if ($type == 1) {
        $user = get_logged_in_user()->id;
        $file_to_request = filter_input(INPUT_GET, 'f');
    } else {
        $user = getUser($mysqli, filter_input(INPUT_POST, 'k'));
        $file_to_request = filter_input(INPUT_POST, 'f');
    }
    $result = getFile($mysqli, $user, $file_to_request);
    $row = mysqli_fetch_assoc($result);
    if ($row['expired'] == 0) {
        if ($user == $row['user_id']) {
            $filename = $row['filename'];
            $result_filename = $row['output_file'];
            if ($row['app'] == 'flac_encoder') {
                $file_path = $flac_encoder_file_path . "downloaded/" . $row['output_file'];
                rename($flac_encoder_file_path . $result_filename, $file_path);
            } else if ($row['app'] == 'ffmpeg_encoder') {
                $file_path = $ffmpeg_encoder_file_path . "downloaded/" . $row['output_file'];
                rename($ffmpeg_encoder_file_path . $result_filename, $file_path);
            } else if ($row['app'] == 'mp3packer') {
                $file_path = $mp3packer_file_path . "downloaded/" . $row['output_file'];
                rename($mp3packer_file_path . $result_filename, $file_path);
            } else if ($row['app'] == 'opus_encoder') {
                $file_path = $opus_encoder_file_path . "downloaded/" . $row['output_file'];
                rename($opus_encoder_file_path . $result_filename, $file_path);
           
            } else {
                $file_path = $other_file_path . "downloaded/" . $row['output_file'];
                rename($other_file_path . $result_filename, $file_path);
            }
            if (file_exists($file_path)) {
                header('Content-Description: File Transfer');
                header('Content-Type: application/octet-stream');
                header('Content-Disposition: attachment; filename="' . $filename . '"');
                header('Expires: 0');
                header('Cache-Control: must-revalidate');
                header('Pragma: public');
                header('Content-Length: ' . filesize($file_path));
                set_link_expired($mysqli, $user, $file_to_request);
                readfile($file_path);
                exit;
            }
        }
    } else {
        echo "Download Link Expired";
    }
}
else{
    echo "Invalid Request";
}
