<?php
header("Content-type: application/json; charset=utf-8");

require_once("scripts_config.php");
require_once("custom_functions.php");
global $mysqli;

class File
{
    public $name = "";
    public $key = "";
    public $category = "";
}
if(filter_input(INPUT_POST, 'k')) {
    $user_id = getUser($mysqli, filter_input(INPUT_POST, 'k'));
    $results = getUserFiles($mysqli, $user_id);
    $json_array = array();
    while ($row = mysqli_fetch_assoc($results)) {
        if ($row['processed'] == 1) {
            $file = new File();
            $file->name = $row['filename'];
            $file->key = $row['random_token'];
            $file->category = $row['category'];
            $json_array[] = $file;
        }
    }
    echo json_encode($json_array);
}