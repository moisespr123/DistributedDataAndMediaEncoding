# Distributed Data and Media Encoding

This repository contains custom PHP code that interacts with the BOINC server to generate tasks. It also contains a data retriever script to download result data from user-submitted tasks.

## Apps
Contains XML files required for the apps.

See README.MD in the folder for more information and executable information.

## Assimilators
This folder includes custom Python assimilator scripts

See README.MD in the folder for more information and usage.

## PHP
This folder includes modified and custom PHP code

See README.MD in the folder for more information and usage.

## Scripts

Contains cmd scripts

## submit_audio_file
This is a Command-Line software written in C# that acts as a file submitter. It communicates with a custom PHP file that receives an audio file and generates the workunit.

It can be combined with Exact Audio Copy to rip Audio CD's and perform encoding tasks.

Currently it supports submitting tasks to encode to FLAC and Opus using the apps `flac_encoder` and `opus_encoder`, respectively.

See README.MD in the folder for more information and usage.

## retrieve_files
This is a Windows Forms software written in C# that allows you to download the result files from your submitted tasks. It lists the categories (if you added one) and your files. Simply, select a category or files to download them.

See README.MD in the folder for more information and usage.

## av1_task_generator
This is a Windows Forms software written in Visual Basic .NET that will create rav1e tasks to distribute on a BOINC Server.