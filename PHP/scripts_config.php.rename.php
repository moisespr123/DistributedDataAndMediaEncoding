<?php

global $mysqli;
$mysqli = new mysqli('host', 'username', 'password', 'project_database');
if ($mysqli->connect_error) {
    exit('Error connecting to database');
}
$mysqli->set_charset("utf8");

$download_folder = '/location/to/download/folder';
$templates_folder = "/location/to/templates/folder";
$root_folder = "/location/to/root/project/folder";
$move_folder = "/location/to/store/raw/files/";
$temp_folder = "/location/to/temp/folder";
$windows_temp_folder = "C:/temp";

$flac_encoder_file_path = "/path";
$mp3packer_file_path = "/path";
$opus_encoder_file_path = "/path";
$ffmpeg_encoder_file_path = "/path";
$paq_file_path = "/path";

$other_file_path = "/path";