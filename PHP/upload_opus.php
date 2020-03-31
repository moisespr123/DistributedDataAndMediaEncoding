<?php
/**
 * Created by PhpStorm.
 * User: cardo
 * Date: 10/1/2018
 * Time: 1:51 PM
 */
require_once("../inc/util.inc");
require_once("../inc/user.inc");
require_once("scripts_config.php");
require_once("custom_functions.php");
global $mysqli;

$user = get_logged_in_user();
page_head(tra("Upload Media Files"));
echo "Use the following form to encode files to Opus.</br></br>";
?>
<form method="post" enctype="multipart/form-data">
    <p>
        <input name="MAX_FILE_SIZE" value="268435456" type="hidden"/>
        Browse for a file or files to encode to Opus:<br/>
        <input name="files[]" type="file" multiple><br/><br/>
        Choose a Bitrate:<br/>
        <select title="Choose a Bitrate:" name='bitrate'>
            <option value='32'>32</option>
            <option value='64' selected="selected">64</option>
            <option value='96'>96</option>
            <option value='128'>128</option>
            <option value='256'>256</option>
            <option value='320'>320</option>
        </select> kbit/s<br/><br/>
        Choose the encoder to use:<br/>
        <input name="encoder" type="radio" value='opus_encoder'>opus-tools. Only compatible with FLAC or .WAV files<br/>
        <input name="encoder" type="radio" value='ffmpeg_encoder' checked>ffmpeg libopus<br/>
        Category: <input type="text" name="category" required><br/><br/>
        <input name="upload" type="submit" value="Upload"/>
    </p>
</form>
<?php
if (filter_input(INPUT_POST, 'upload')) {
    for ($i = 0; $i < count($_FILES['files']['name']); $i++) {
        $category_hash = bin2hex(random_bytes(16));
        $random_token = bin2hex(random_bytes(16));
        $file_name = $_FILES["files"]["name"][$i];
        $file_tmp = $_FILES["files"]["tmp_name"][$i];
        $ext = pathinfo($file_name, PATHINFO_EXTENSION);
        $filename = $random_token . "." . $ext;
        $output_filename = pathinfo($file_name, PATHINFO_FILENAME) . '.opus';
        $encoder = filter_input(INPUT_POST, 'encoder');
        if (!in_array($ext, Array('flac', 'wav')) && $encoder == 'opus_encoder') {
            echo("File " . $file_name . " is not a WAV or FLAC file. Opus-tools is only compatible with WAV and FLAC files.</br>");
        }
        else{
            if (move_uploaded_file($file_tmp = $_FILES["files"]["tmp_name"][$i], $download_folder . $filename)) {
                chdir($templates_folder);
                $wu_template = fopen($random_token . "_wu", "w");
                fwrite($wu_template, generate_opus_wu_template($filename, filter_input(INPUT_POST, 'bitrate'), $encoder, $random_token));
                fclose($wu_template);
                $result_template = fopen($random_token . "_result", "w");
                fwrite($result_template, generate_generic_result_template($random_token . "-out.opus", "500"));
                fclose($result_template);
                chdir($root_folder);
                exec(return_job_string($encoder, $random_token, $filename));
                insertUserFile($mysqli, $user->id, $category_hash, filter_input(INPUT_POST, 'category'), $random_token, $filename, $random_token . "-out.opus", $output_filename, $encoder, 1);
                rename($download_folder . $filename, $move_folder . $filename);
                echo("Workunit for file " . $file_name . " generated</br>");
            } else {
                echo("File " . $file_name . " failed to upload</br>");
            }
        }
    }
}
page_tail();
