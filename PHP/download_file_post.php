<?php
if(isset($_POST['k'])) {
    require_once("../inc/util.inc");
    require_once("../inc/user.inc");
    require_once("scripts_config.php");
    require_once("custom_functions.php");
    global $mysqli;
    $user_id = getUser($mysqli, $_POST['k']);
    $result = getFile($mysqli, $user_id, $_POST['f']);
    $row = mysqli_fetch_assoc($result);
    if ($row['expired'] == 0) {
        if ($user_id == $row['user_id']) {
            $filename = basename(pathinfo($row['filename'], PATHINFO_BASENAME), "." . pathinfo($row['filename'], PATHINFO_EXTENSION));
            if ($row['app'] == 'flac_encoder') {
                $file_path = $flac_encoder_file_path . $row['random_token'];
                rename($file_path, $flac_encoder_file_path . "downloaded/" . $row['random_token']);
                $file_path = $flac_encoder_file_path . "downloaded/" . $row['random_token'];
                $filename = $filename . ".flac";
            } else if ($row['app'] == 'opus_encoder') {
                $file_path = $opus_encoder_file_path . $row['random_token'];
                rename($file_path, $opus_encoder_file_path . "downloaded/" . $row['random_token']);
                $file_path = $opus_encoder_file_path . "downloaded/" . $row['random_token'];
                $filename = $filename . ".opus";
            } else {
                $file_path = $other_file_path . $row['random_token'];
                rename($file_path, $other_file_path . "downloaded/" . $row['random_token']);
                $file_path = $other_file_path . "downloaded/" . $row['random_token'];
                $filename = $filename . ".flac";
            }
            if (file_exists($file_path)) {
                header('Content-Description: File Transfer');
                header('Content-Type: application/octet-stream');
                header('Content-Disposition: attachment; filename="' . $filename . '"');
                header('Expires: 0');
                header('Cache-Control: must-revalidate');
                header('Pragma: public');
                header('Content-Length: ' . filesize($file_path));
                set_link_expired($mysqli, $user_id, $_POST['f']);
                readfile($file_path);
                exit;
            }
        }
    } else {
        echo "Download Link Expired";
    }
}