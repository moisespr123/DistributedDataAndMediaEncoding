using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace submit_audio_file
{
    class parse_flac_args
    {
        public static string[] parse_args(string[] argv)
        {
            string trackname = "";
            string tracknumber = "";
            string artist = "";
            string album = "";
            string source = "";
            string commandline = "";
            string picturefile = "";
            //starts parsing parameters
            // NOTE: \\\"\\\"\" is needed so the BOINC wrapper can understand what a " is...
            for (int arg = 0; arg < argv.Count(); arg++)
            {
                if (argv[arg] == "-artist")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " -T \\\"\\\"\"\"ARTIST=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-title")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " -T \\\"\\\"\"\"TITLE=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        trackname = argv[arg + 1].Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('\"', '_').Replace('?', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_');
                        arg++;
                    }
                }
                else if (argv[arg] == "-album")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        album = argv[arg + 1];
                        commandline += " -T \\\"\\\"\"\"ALBUM=" + album + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-year")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " -T \\\"\\\"\"\"DATE=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-tn")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        tracknumber = argv[arg + 1];
                        commandline += " -T \\\"\\\"\"\"TRACKNUMBER=" + tracknumber + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-g")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " -T \\\"\\\"\"\"GENRE=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-aa")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        artist = argv[arg + 1];
                        commandline += " -T \\\"\\\"\"\"BAND=" + artist + "\\\"\\\"\"\" -T \\\"\\\"\"\"ALBUMARTIST=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-comm")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " -T \\\"\\\"\"\"COMMENT=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-comp")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " -T \\\"\\\"\"\"COMPOSER=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-cdn")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " -T \\\"\\\"\"\"DISCNUMBER=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-cdt")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " -T \\\"\\\"\"\"TOTALDISCS=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-tt")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " -T \\\"\\\"\"\"TOTALTRACKS=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-p")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        picturefile = argv[arg + 1];
                        commandline += " --picture input_image_file.imgfile";
                        arg++;
                    }
                }
                else if (argv[arg] == "-s")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        source = argv[arg + 1];
                        arg++;
                    }
                }

            }
            string[] return_values = { commandline, trackname, tracknumber, album, artist, picturefile, source };
            return return_values;
        }
    }
}
