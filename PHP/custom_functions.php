<?php
/**
 * Created by PhpStorm.
 * User: cardo
 * Date: 9/30/2018
 * Time: 9:32 PM
 */
function return_wu_template($filename, $command_line, $picture_file)
{
    if ($picture_file != true)
        return
            "<file_info>
    <number>0</number>
</file_info>
<workunit>
    <file_ref>
        <file_number>0</file_number>
        <open_name>$filename</open_name>
        <copy_file/>
    </file_ref>
    <min_quorum>1</min_quorum>
    <target_nresults>1</target_nresults>
    <command_line>$command_line</command_line>
</workunit>";
    else
        return
            "<file_info>
    <number>0</number>
</file_info>
<file_info>
    <number>1</number>
</file_info>
<workunit>
    <file_ref>
        <file_number>0</file_number>
        <open_name>$filename</open_name>
        <copy_file/>
    </file_ref>
    <file_ref>
        <file_number>1</file_number>
        <open_name>$filename.imgfile</open_name>
        <copy_file/>
    </file_ref>
    <min_quorum>1</min_quorum>
    <target_nresults>1</target_nresults>
    <command_line>" . str_replace("input_image_file.imgfile", $filename . ".imgfile", $command_line) . "</command_line>
</workunit>";
}

function return_result_template($filename)
{
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
</result>\n
";
}

function getUser($mysqli, $key)
{
    $splitted_string = explode("_", $key);
    $stmt = $mysqli->prepare("SELECT id, authenticator, passwd_hash FROM user WHERE id=?");
    $stmt->bind_param("d", $splitted_string[0]);
    $stmt->execute();
    $result = $stmt->get_result();
    $stmt->close();
    try {
        $row = mysqli_fetch_assoc($result);
        if (md5($row['authenticator'] . $row['passwd_hash']) == $splitted_string[1])
            return $row['id'];
        else
            return 0;
    } catch (Exception $e) {
        return 0;
    }
}

function insertAudioTrack($mysqli, $user, $hash, $name, $app)
{
    $stmt = $mysqli->prepare("INSERT INTO user_media_files (user_id, random_token, filename, app) VALUES ($user, ?, ?, ?)");
    $stmt->bind_param("sss", $hash, $name, $app);
    $stmt->execute();
    $stmt->close();
}

function getFile($mysqli, $user, $token)
{
    $stmt = $mysqli->prepare("SELECT * FROM user_media_files WHERE user_id=? AND random_token=? ORDER BY id");
    $stmt->bind_param("ds", $user, $token);
    $stmt->execute();
    $results = $stmt->get_result();
    $stmt->close();
    return $results;
}

function getFileCategory($mysqli, $user, $token)
{
    $stmt = $mysqli->prepare("SELECT album FROM user_albums WHERE user_id=? AND hash=? ORDER BY id");
    $stmt->bind_param("ds", $user, $token);
    $stmt->execute();
    $result = $stmt->get_result();
    $stmt->close();
    while($row = mysqli_fetch_assoc($result)){
        return $row['album'];
    }
    return "None";
}

function getUserFiles($mysqli, $user)
{
    $stmt = $mysqli->prepare("SELECT * FROM user_media_files WHERE user_id=? AND expired=0 ORDER BY id");
    $stmt->bind_param("d", $user);
    $stmt->execute();
    $results = $stmt->get_result();
    $stmt->close();
    return $results;
}

function getExtensionBasedOnApp($app)
{
    if ($app == 'flac_encoder')
        return ".flac";
    else if ($app == 'opus_encoder')
        return ".opus";
    else
        return ".flac";
}

function set_link_expired($mysqli, $user, $token)
{
    $stmt = $mysqli->prepare("UPDATE user_media_files SET expired=1 WHERE user_id=? AND random_token=?");
    $stmt->bind_param("ds", $user, $token);
    $stmt->execute();
    $stmt->close();
}
function insertAlbum($mysqli, $user, $album, $filename)
{
    $stmt = $mysqli->prepare("INSERT INTO user_albums (user_id, album, hash) VALUES ($user, ?, ?)");
    $stmt->bind_param("ss", $album, $filename);
    $stmt->execute();
    $stmt->close();
}

function return_job_string($appname, $hash, $filename)
{
    return "./bin/create_work -appname " . $appname . " -wu_name " . $appname . "_" . $hash . " -wu_template templates/" . $hash . "_wu -result_template templates/" . $hash . "_result \"" . $filename . "\"";
}

function return_job_string_multiple_files($appname, $hash, $filenames)
{
    $job_string = "./bin/create_work -appname " . $appname . " -wu_name " . $appname . "_" . $hash . " -wu_template templates/" . $hash . "_wu -result_template templates/" . $hash . "_result ";
    foreach ($filenames as $file)
        $job_string .= "\"" . $file . "\" ";
    return $job_string;
}

function generate_flac_wu_template($random_hash, $out)
{
    return return_wu_template($random_hash, "--lax -l 32 -b 4096 -e -m -p -r 0,15 -A bartlett -A bartlett_hann -A blackman -A blackman_harris_4term_92db -A connes -A flattop -A gauss(0.5) -A hamming -A hann -A kaiser_bessel -A nuttall -A rectangle -A triangle -A tukey(0.5) -A partial_tukey(2) -A punchout_tukey(3) -A welch " . $random_hash . " -o " . $out . "-out.flac");
}

function generate_flac_wu_template_with_cmd($random_hash, $cmd, $out, $picture)
{
    return return_wu_template($random_hash, $cmd . " " . $random_hash . " -o " . $out, $picture);
}

function generate_flac_result_template($random_hash)
{
    return return_result_template($random_hash . "-out.flac");
}

function generate_opus_wu_template($random_hash, $bitrate, $out)
{
    return return_wu_template($random_hash, "--music --bitrate $bitrate " . $random_hash . " " . $out . "-out.opus");
}

function generate_opus_wu_template_with_cmd($random_hash, $command_line, $out)
{
    return return_wu_template($random_hash, $command_line . " " . $random_hash . " " . $out);
}

function generate_opus_result_template($random_hash)
{
    return return_result_template($random_hash . "-out.opus");
}

function generate_put_result_template($random_hash)
{
    return return_result_template($random_hash);
}