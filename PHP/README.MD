# PHP Files
This directory contains the PHP files the project uses.

* `custom_functions.php` contains functions used by other PHP files.
* `data_retriever_get_files.php` is used by the `retrieve_files` C# project.
* `download_file.php` is used by `user_files.php` to download result files.
* `download_file_post.php` is used by the `retrieve_files` C# project to download result files.
* `media_put.php` is used by the `submit_audio_file` C# project to upload and generate workunits.
* `scripts_config.php.rename.php` is where you configure paths and the MySQL credentials. Needs to be renamed to just `scripts_config.php`.
* `upload_flac.php` is used to upload and generate tasks for the `flac_encoder` app.
* `upload_opus.php` is used to upload and generate tasks for the `opus_encoder` app.
* `user.inc` is a modified version of the BOINC `user.inc` file. This goes in the `inc` folder.
* `user_files.php` is a script to list user-submitted tasks.

`*.inc` files should go into the `inc` folder.