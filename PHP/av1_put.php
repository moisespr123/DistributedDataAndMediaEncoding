<?php
/**
 * Created by PhpStorm.
 * User: cardo
 * Date: 10/1/2018
 * Time: 5:54 PM
 *
 * Post parameters:
 * k = user key
 * f = the file
 * c = command line arguments
 * n = name
 */
require_once("scripts_config.php");
require_once("custom_functions.php");
global $mysqli;
if (isset($_POST['k'])) {
    $user_id = getUser($mysqli, $_POST['k']);
    if ($user_id == 1) {
        chdir($templates_folder);
        $filename = $_POST['f'];
        $wu_template = fopen($filename . "_wu", "w");
        $result_template = fopen($filename . "_result", "w");
        $app = "rav1e_encoder";
        fwrite($wu_template, generate_wu_rav1e_template_cmd($filename . ".y4m", $_POST['c']));
        fwrite($result_template, generate_put_result_template($filename . ".ivf"));
        $job_creation_command = return_job_string($app, $filename, $filename . ".y4m");
        fclose($wu_template);
        fclose($result_template);
        chdir($root_folder);
        exec($job_creation_command);
        insertY4mFile($mysqli, $user_id, $_POST['n'] , $filename, $app);
        rename($download_folder .  $filename . ".y4m", $move_folder .  $filename . ".y4m");
        echo "Done";
    } else {
        echo "Incorrect User Key";
    }
}
