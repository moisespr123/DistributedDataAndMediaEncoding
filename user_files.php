<?php
/**
 * Created by PhpStorm.
 * User: cardo
 * Date: 10/1/2018
 * Time: 8:38 AM
 */
require_once("../inc/util.inc");
require_once("../inc/user.inc");
require_once("scripts_config.php");
global $mysqli;

$user = get_logged_in_user();
page_head(tra("Your Media Files"));
echo "Below, you'll find your media files. Download links expire once the download has started.</br></br>";
function getUserFiles($mysqli, $user){
    $stmt = $mysqli->prepare("SELECT * FROM user_media_files WHERE user_id=? AND expired=0 ORDER BY id");
    $stmt->bind_param("d", $user->id);
    $stmt->execute();
    $results = $stmt->get_result();
    $stmt->close();
    return $results;
}
function getFileStatusAndLink($row){
    $status = $row['processed'];
    if ($status == 0)
        return "Queued";
    else if ($status == 1)
        return "<a href=download_file.php?f=".$row['random_token'].">Ready to download</a>";
}
$result  = getUserFiles($mysqli, $user);
echo "<table class='table'>
<tr><thead class='bg-primary'><td>File</td><td>Status</td></thead></tr>";
 while($row = mysqli_fetch_assoc($result))
     echo "<tr><td>".$row["filename"]."</td><td>".getFileStatusAndLink($row)."</td></tr>";
echo "</table>";