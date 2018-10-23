if %3==opus (
	for %%f in (%1\*.flac) do submit_audio_file.exe boinc_key %3 -b 64 -category %2 -s "%%f"
 )
if %3==flac (
	for %%f in (%1\*.flac) do submit_audio_file.exe boinc_key %3 -category %2 -s "%%f"
)