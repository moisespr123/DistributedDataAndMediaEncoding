<?php
if (isset($_GET['f'])) {
    require_once("../inc/util.inc");
    require_once("../inc/user.inc");
    require_once("scripts_config.php");
    global $mysqli;
    $user = get_logged_in_user();
    function getFile($mysqli, $user)
    {
        $stmt = $mysqli->prepare("SELECT * FROM user_media_files WHERE user_id=? AND random_token=? ORDER BY id");
        $stmt->bind_param("ds", $user->id, $_GET['f']);
        $stmt->execute();
        $results = $stmt->get_result();
        $stmt->close();
        return $results;
    }

    function set_link_expired($mysqli, $user)
    {
        $stmt = $mysqli->prepare("UPDATE user_media_files SET expired=1 WHERE user_id=? AND random_token=?");
        $stmt->bind_param("ds", $user->id, $_GET['f']);
        $stmt->execute();
        $stmt->close();
    }

    $result = getFile($mysqli, $user);
    $row = mysqli_fetch_assoc($result);
    if ($row['expired'] == 0) {
        if ($user->id == $row['user_id']) {
            $filename = basename(pathinfo($row['filename'], PATHINFO_BASENAME), "." . pathinfo($row['filename'], PATHINFO_EXTENSION));
            if ($row['app'] == 'flac_encoder') {
                $file_path = $flac_encoder_file_path . $row['random_token'];
                rename($file_path, $flac_encoder_file_path . "downloaded/" . $row['random_token']);
                $file_path = $flac_encoder_file_path . "downloaded/" . $row['random_token'];
                $filename = $filename . ".flac";
            } else if ($row['app'] = 'opus_encoder') {
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
                set_link_expired($mysqli, $user);
                readfile($file_path);
                exit;
            }
        }
    } else {
        echo "Download Link Expired";
    }
}