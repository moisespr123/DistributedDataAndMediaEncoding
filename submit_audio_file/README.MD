# Audio Submitter

This is a comman-line tool designed to submit audio tracks to the BOINC server for processing.

It was tested with Exact Audio Copy.

The command line is the following:
## FLAC
`boinc_weak_key flac -artist "%artist%" -title "%title%" -album "%albumtitle%" -year "%year%" -tn %tracknr1% -g %genre% -comm %comment% -aa %albuminterpret% -comp %composer% -cnd %cdnumber% -cdt %totalcds% -tt %numtracks% %hascover%-p "%coverfile%" %hascover% -s "%source%"`

## Opus
`boinc_weak_key opus -b bitrate -artist "%artist%" -title "%title%" -album "%albumtitle%" -year "%year%" -tn %tracknr1% -g %genre% -comm %comment% %hascover%-p "%coverfile%" %hascover% -s "%source%"`

* Change `boinc_weak_key with your account weak_key as found in http://boinc.moisescardona.me/weak_auth.php