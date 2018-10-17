ffmpeg -f concat -i %1 -c copy temp.ivf
ffmpeg -i temp.ivf -i %2 -c:v copy -c:a copy %3
del temp.ivf