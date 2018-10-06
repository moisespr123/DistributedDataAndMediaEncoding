<?php
header("Content-type: application/json; charset=utf-8");
/**
 * Created by PhpStorm.
 * User: cardo
 * Date: 10/6/2018
 * Time: 4:20 PM
 */
require_once("scripts_config.php");
require_once("custom_functions.php");
global $mysqli;

class File
{
    public $name = "";
    public $key = "";
    public $category = "";
}

$user_id = getUser($mysqli, $_POST['k']);
$results = getUserFiles($mysqli, $user_id);
$json_array = array();
while ($row = mysqli_fetch_assoc($results)) {
    if ($row['processed'] == 1) {
        $file = new File();
        $filename = basename(pathinfo($row['filename'], PATHINFO_BASENAME), "." . pathinfo($row['filename'], PATHINFO_EXTENSION));
        $file->name = $filename . getExtensionBasedOnApp($row['app']);
        $file->key = $row['random_token'];
        $file->category = getFileCategory($mysqli, $user_id, $row['random_token']);
        $json_array[] = $file;
    }
}
echo json_encode($json_array);