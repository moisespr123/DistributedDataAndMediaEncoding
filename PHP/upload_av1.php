<?php
require_once("../inc/util.inc");
require_once("../inc/user.inc");
require_once("scripts_config.php");
require_once("custom_functions.php");
global $mysqli;

$user = get_logged_in_user();
page_head(tra("Upload Media Files"));
echo "Use the following form to upload a video file to encode to AV1.</br></br>";
?>
<form method="post" enctype="multipart/form-data">
    <p>
        <input name="MAX_FILE_SIZE" value="9999999999" type="hidden"/>
        Browse for a file to encode to AV1:<br/>
        <input name="files[]" type="file" multiple><br/><br/>
        Choose the encoder to use:<br/>
        <input name="encoder" type="radio" value='rav1e_encoder'>rav1e<br/>
        <input name="encoder" type="radio" value='svt_av1_encoder' checked>SVT-AV1<br/><br/>
        Quantizer setting:<br/>
        <input type="text" name="quantizer" required><br/><br/>
        <input type="checkbox" name="twopass" value="enabled" checked> Two Pass Encoding.<br/><br/>
        Enter a name for this job: <input type="text" name="category" required><br/><br/>
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
        $encoder = filter_input(INPUT_POST, 'encoder');
        $av1_output_filename = filter_input(INPUT_POST, 'category');
        $quantizer = intval(filter_input(INPUT_POST, 'quantizer'));
        $twopass = filter_input(INPUT_POST, 'twopass');
        if ($quantizer < 1 && $quantizer > 255 && $encoder == 'rav1e_encoder') {
            echo("Quantizer setting for rav1e should be between 1 and 255.</br>");
        } else if ($quantizer < 1 && $quantizer > 63 && $encoder == 'svt_av1_encoder') {
            echo("Quantizer setting for SVT-AV1 should be between 1 and 63.</br>");
        } else {
            $temp_av1_folder = $temp_folder . $random_token . "-av1";
            mkdir($temp_av1_folder);
            $temp_av1_filename = $temp_av1_folder . "/" . $filename;
            if (move_uploaded_file($file_tmp = $_FILES["files"]["tmp_name"][$i], $temp_av1_filename)) {

                //changing directory to write templates
                chdir($templates_folder);

                //writes opus encoder job template
                $opus_wu_template = fopen($random_token . "_wu", "w");
                fwrite($opus_wu_template, generate_opus_wu_template($filename, "64", "ffmpeg_encoder", $random_token));
                fclose($opus_wu_template);

                //writes opus encoder result template
                $opus_result_template = fopen($random_token . "_result", "w");
                fwrite($opus_result_template, generate_generic_result_template($random_token . "-out.opus"));
                fclose($opus_result_template);
                   
                chdir($root_folder);
                //Now, let's split the video file into 1-second chunks.
                exec("/mnt/c/Windows/system32/cmd.exe /c ffmpeg -i " . $windows_temp_folder . "/" . $random_token . "-av1/" . $filename . " -f segment -segment_time 1 " . $windows_temp_folder . "/" . $random_token . "-av1/" . $random_token . "-part-%7d.y4m -y");
                
                //time to submit each chunk!
                $y4m_files = glob($temp_av1_folder . "/*.y4m");
                $counter = 0;
                if (sizeof($y4m_files) > 0) {
                    foreach ($y4m_files as $y4m_file) {
                        //increase the counter
                        $counter++;

                        //randomize the filename
                        $random_token_2 = bin2hex(random_bytes(16));
                        $random_token_twopass = bin2hex(random_bytes(16));
                        $av1_chunk_filename_no_extension = sprintf('%07d', $counter) . "-" . $random_token_2;
                        $av1_chunk_filename = $av1_chunk_filename_no_extension . ".y4m";
                        $av1_chunk_output_filename = $av1_chunk_filename_no_extension . "-out.ivf";
                        move($y4m_file, $download_folder . $av1_chunk_filename);
                        $result_size_in_mb = "1024";

                        //av1 workunit creation
                      
                        if (empty($twopass)){
                            $av1_wu_template = fopen($templates_folder . "/" . $random_token_2 . "_wu", "w");
                            fwrite($av1_wu_template, generate_av1_wu_template($av1_chunk_filename, $quantizer, $encoder, $av1_chunk_output_filename, false, 0));
                            fclose($av1_wu_template);
                            $av1_result_template = fopen($templates_folder . "/" . $random_token_2 . "_result", "w");
                            fwrite($av1_result_template, generate_generic_result_template($av1_chunk_output_filename, $result_size_in_mb));
                            fclose($av1_result_template);
                            exec(return_job_string($encoder, $random_token_2, $av1_chunk_filename));
                            insertUserFile($mysqli, $user->id, $category_hash, $av1_output_filename, $random_token_2, $av1_chunk_filename, $av1_chunk_output_filename, "Chunk " . strval($counter), $encoder, 4);
                            move($download_folder . $av1_chunk_filename, $move_folder . $av1_chunk_filename);
                        }
                        else{
                            $av1_firstpass_output_filename = $av1_chunk_filename_no_extension . ".firstpass";
                            
                            // WU template for first pass
                            $av1_wu_template = fopen($templates_folder . "/" . $random_token_2 . "_wu", "w");
                            fwrite($av1_wu_template, generate_av1_wu_template($av1_chunk_filename, $quantizer, $encoder, array($av1_firstpass_output_filename, $av1_chunk_output_filename), true, 1));
                            fclose($av1_wu_template);
                            $av1_result_template = fopen($templates_folder . "/" . $random_token_2 . "_result", "w");
                            fwrite($av1_result_template, generate_generic_result_template($av1_firstpass_output_filename, $result_size_in_mb));
                            fclose($av1_result_template);
                            
                            // WU template for second pass
                            $av1_wu_template_2pass = fopen($templates_folder . "/" . $random_token_twopass . "_wu", "w");
                            fwrite($av1_wu_template_2pass, generate_av1_wu_template(array($av1_chunk_filename, $av1_firstpass_output_filename), $quantizer, $encoder, $av1_chunk_output_filename, true, 2));
                            fclose($av1_wu_template_2pass);
                            $av1_result_template_2pass = fopen($templates_folder . "/" . $random_token_twopass . "_result", "w");
                            fwrite($av1_result_template_2pass, generate_generic_result_template($av1_chunk_output_filename, $result_size_in_mb));
                            fclose($av1_result_template_2pass);
                            
                            // Submit first pass WU
                            exec(return_job_string($encoder, $random_token_2, $av1_chunk_filename));
                            
                            // Add entries to database table
                            insertUserFile($mysqli, $user->id, $category_hash, $av1_output_filename, $random_token_2, $av1_chunk_filename, $av1_firstpass_output_filename, "First Pass Chunk " . strval($counter), $encoder, 5);
                            insertUserFile($mysqli, $user->id, $category_hash, $av1_output_filename, $random_token_twopass, $av1_chunk_filename, $av1_chunk_output_filename, "Second Pass Chunk " . strval($counter), $encoder, 4);
                            
                            // Move file to RAW folder
                            move($download_folder . $av1_chunk_filename, $move_folder . $av1_chunk_filename);
                        }
                    }
                    
                    //submit opus job
                    move($temp_av1_filename, $download_folder . $filename);
                    chdir($root_folder);
                    exec(return_job_string("ffmpeg_encoder", $random_token, $filename));
                    insertUserFile($mysqli, $user->id, $category_hash, $av1_output_filename, $random_token, $filename, $random_token . "-out.opus", "Audio Track", "ffmpeg_encoder", 3);
                    move($download_folder . $filename, $move_folder . $filename);
                    
                    // Finally, add the output file to the table
                    insertUserFile($mysqli, $user->id, $category_hash, $av1_output_filename, $category_hash, $category_hash . ".webm",  $category_hash . ".webm", $av1_output_filename . ".webm", "av1_generic", 2);
                    
                    echo("Workunit for file " . $file_name . " generated.</br>");
                    echo("There are " . strval($counter) . " chunks to encode. You can see the progress in the Media Files list");
                }
            } else {
                echo("File " . $file_name . " failed to upload</br>");
            }
        }
    }
}
page_tail();
