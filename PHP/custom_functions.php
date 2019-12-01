<?php

$rsc_fpops_est = "3600000000000";
$flac_rsc_fpops_est = "50000000000000";
$rav1e_rsc_fpops_est = "99999999999999999999999999999999";
$rsc_fpops_bound = "99999999999999999999999999999999";
$rsc_memory_bound = "500000000";
$rsc_disk_bound = "1000000000";

function move($source, $dest){
    copy($source, $dest);
    unlink($source);
}

function return_wu_template($filename, $command_line, $picture_file, $fpops_est, $fpops_bound, $memory_bound, $disk_bound) {
    $max_error_results = "10";
    $max_total_results = "40";

    $workunit_template = "
<file_info>
    <number>0</number>
</file_info>";

    if ($picture_file) {
        $workunit_template .= "
<file_info>
    <number>1</number>
</file_info>";
    }

    $workunit_template .= "
<workunit>
    <file_ref>
        <file_number>0</file_number>
        <open_name>$filename</open_name>
        <copy_file/>
    </file_ref>";

    if ($picture_file) {
        $workunit_template .= "
    <file_ref>
        <file_number>1</file_number>
        <open_name>$filename.imgfile</open_name>
        <copy_file/>
    </file_ref>";
    }
    if ($picture_file) {
        $workunit_template .= "<command_line>" . str_replace("input_image_file.imgfile", $filename . ".imgfile", $command_line) . "</command_line>";
    } else {
        $workunit_template .= "<command_line>$command_line</command_line>";
    }

    $workunit_template .= "
    <rsc_fpops_est>$fpops_est</rsc_fpops_est>
    <rsc_fpops_bound>$fpops_bound</rsc_fpops_bound>
    <rsc_memory_bound>$memory_bound</rsc_memory_bound>
    <rsc_disk_bound>$disk_bound</rsc_disk_bound>
    <min_quorum>1</min_quorum>
    <target_nresults>1</target_nresults>
    <max_error_results>$max_error_results</max_error_results>
    <max_total_results>$max_total_results</max_total_results>
</workunit>";
    return $workunit_template;
}

function return_result_template($filename) {
    return
            "<file_info>
    <name><OUTFILE_0/></name>
    <generated_locally/>
    <upload_when_present/>
    <max_nbytes>300000000</max_nbytes>
    <url><UPLOAD_URL/></url>
</file_info>
<result>\n
    <file_ref>
        <file_name><OUTFILE_0/></file_name>
        <open_name>$filename</open_name>
        <copy_file/>
    </file_ref>
</result>\n";
}

function getUser($mysqli, $key) {
    $splitted_string = explode("_", $key);
    $stmt = $mysqli->prepare("SELECT id, authenticator, passwd_hash FROM user WHERE id=?");
    $stmt->bind_param("d", $splitted_string[0]);
    $stmt->execute();
    $result = $stmt->get_result();
    $stmt->close();
    try {
        $row = mysqli_fetch_assoc($result);
        if (md5($row['authenticator'] . $row['passwd_hash']) == $splitted_string[1]) {
            return $row['id'];
        } else {
            return 0;
        }
    } catch (Exception $e) {
        return 0;
    }
}

function insertUserFile($mysqli, $user, $category_hash, $category, $hash, $input_file, $output_file, $output_name, $app, $type) {
    $stmt = $mysqli->prepare("INSERT INTO user_media_files (user_id, category_hash, category, random_token, input_file, output_file, filename, app, type) VALUES ($user, ?, ?, ?, ?, ?, ?, ?, ?)");
    $stmt->bind_param("sssssssi", $category_hash, $category, $hash, $input_file, $output_file, $output_name, $app, $type);
    $stmt->execute();
    $stmt->close();
}

function insertY4mFile($mysqli, $user, $category, $name, $app) {
    $stmt = $mysqli->prepare("INSERT INTO user_av1_files (user_id, category, filename, app) VALUES ($user, ?, ?, ?)");
    $stmt->bind_param("sss", $category, $name, $app);
    $stmt->execute();
    $stmt->close();
}

function getFile($mysqli, $user, $token) {
    $stmt = $mysqli->prepare("SELECT * FROM user_media_files WHERE user_id=? AND random_token=? ORDER BY id");
    $stmt->bind_param("ds", $user, $token);
    $stmt->execute();
    $results = $stmt->get_result();
    $stmt->close();
    return $results;
}

function getUserFiles($mysqli, $user) {
    $stmt = $mysqli->prepare("SELECT * FROM user_media_files WHERE user_id=? AND expired=0 ORDER BY id");
    $stmt->bind_param("d", $user);
    $stmt->execute();
    $results = $stmt->get_result();
    $stmt->close();
    return $results;
}

function set_link_expired($mysqli, $user, $token) {
    $stmt = $mysqli->prepare("UPDATE user_media_files SET expired=1 WHERE user_id=? AND random_token=?");
    $stmt->bind_param("ds", $user, $token);
    $stmt->execute();
    $stmt->close();
}

function return_job_string($appname, $hash, $filename) {
    return "./bin/create_work -appname " . $appname . " -wu_name " . $appname . "_" . $hash . " -wu_template templates/" . $hash . "_wu -result_template templates/" . $hash . "_result \"" . $filename . "\"";
}

function return_job_string_multiple_files($appname, $hash, $filenames) {
    $job_string = "./bin/create_work -appname " . $appname . " -wu_name " . $appname . "_" . $hash . " -wu_template templates/" . $hash . "_wu -result_template templates/" . $hash . "_result ";
    foreach ($filenames as $file) {
        $job_string .= "\"" . $file . "\" ";
    }
    return $job_string;
}

function generate_flac_wu_template($random_hash, $out) {
    global $flac_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound;
    return return_wu_template($random_hash, "--lax -l 32 -b 4096 -e -m -p -r 0,15 -A bartlett -A bartlett_hann -A blackman -A blackman_harris_4term_92db -A connes -A flattop -A gauss(0.5) -A hamming -A hann -A kaiser_bessel -A nuttall -A rectangle -A triangle -A tukey(0.5) -A partial_tukey(2) -A punchout_tukey(3) -A welch " . $random_hash . " -o " . $out . "-out.flac", false, $flac_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound);
}

function generate_flac_wu_template_with_cmd($random_hash, $cmd, $out, $picture) {
    global $flac_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound;
    return return_wu_template($random_hash, $cmd . " " . $random_hash . " -o " . $out, $picture, $flac_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound);
}

function generate_ffmpeg_wu_template_with_cmd($random_hash, $cmd, $out, $picture) {
    global $flac_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound;
    return return_wu_template($random_hash, $cmd . " " . $random_hash . " -o " . $out, $picture, $flac_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound);
}

function generate_generic_result_template($output_filename) {
    return return_result_template($output_filename);
}

function generate_opus_wu_template($random_hash, $bitrate, $enc, $out) {
    global $rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound;
    if ($enc == "opus_encoder") {
        return return_wu_template($random_hash, "--music --bitrate $bitrate " . $random_hash . " " . $out . "-out.opus", false, $rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound);
    } else {
        return return_wu_template($random_hash, "-i " . $random_hash . " -c:a libopus -b:a " . $bitrate . "K " . $out . "-out.opus", false, $rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound);
    }
}

function generate_opus_wu_template_with_cmd($app, $random_hash, $command_line, $out, $picture) {
    global $rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound;
    if ($app == "opus_encoder") {
        return return_wu_template($random_hash, $command_line . " " . $random_hash . " " . $out, $picture, $rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound);
    } else {
        return return_wu_template($random_hash, "-i " . $random_hash . " " . $command_line . " " . $out, false, $rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound);
    }
}

function generate_av1_wu_template($filename, $quantizer, $enc, $out) {
    global $rav1e_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound;
    if ($enc == "rav1e_encoder") {
        return return_wu_template($filename, $filename . " -o " . $out . " --quantizer " . $quantizer . " -s 0 -i 99999 -I 99999" , false, $rav1e_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound);
    } else {
        return return_wu_template($filename, "-enc-mode 0 -q " . $quantizer . " -i " . $filename . " -b " . $out, false, $rav1e_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound);
    }
}

function generate_wu_general_template_cmd($input_file, $command_line) {
    return return_wu_template($input_file, $command_line, false);
}

//I'll deprecate this later
function generate_wu_av1_template_cmd($app, $input_file, $command_line) {
    global $rav1e_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound;
    if ($app == "rav1e_encoder"){
        return return_wu_template($input_file, $command_line, false, $rav1e_rsc_fpops_est, $rsc_fpops_bound, $rsc_memory_bound, $rsc_disk_bound);
    }
}
