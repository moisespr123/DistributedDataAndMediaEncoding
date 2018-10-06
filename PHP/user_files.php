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
require_once ("custom_functions.php");
global $mysqli;

$user = get_logged_in_user();
page_head(tra("Your Media Files"));
echo "Below, you'll find your media files. Download links expire once the download has started.</br></br>";
function getFileStatusAndLink($row){
    $status = $row['processed'];
    if ($status == 0)
        return "Queued";
    else if ($status == 1)
        return "<a href=download_file.php?f=".$row['random_token'].">Ready to download</a>";
}
$result  = getUserFiles($mysqli, $user->id);
echo "<table class='table'>
<tr><thead class='bg-primary'><td>File</td><td>Status</td></thead></tr>";
 while($row = mysqli_fetch_assoc($result))
     echo "<tr><td>".$row["filename"]."</td><td>".getFileStatusAndLink($row)."</td></tr>";
echo "</table>";