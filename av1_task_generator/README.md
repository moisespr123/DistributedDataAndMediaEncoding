# av1_task_generator
A GUI to generate rav1e tasks on a BOINC server

## Dependencies:
* You need my build of `opusenc.exe`. You can get it here: https://moisescardona.me/opusenc-libopus%201.3-rc2-2-ga8e2a2b1-dirty-resample-10
* You need ffmpeg as found here: https://ffmpeg.zeranoe.com/builds/. Use the nightly builds. [Instructions here.](https://moisescardona.me/downloading_ffmpeg_rav1e_gui)

The software will split a video file into parts and store it in the BOINC server download folder. It will also extract and encode the audio separately using opusenc.

Written in Visual Basic .NET using Visual Studio 2017.
