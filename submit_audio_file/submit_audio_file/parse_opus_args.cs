using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace submit_audio_file
{
    class parse_opus_args
    {
        public static string[] parse_args(string[] argv)
        {
            string trackname = "";
            string tracknumber = "";
            string artist = "";
            string album = "";
            string source = "";
            string picturefile = "";
            string commandline = "";
            //starts parsing parameters
            // NOTE: \\\"\\\"\" is needed so the BOINC wrapper can understand what a " is...
            for (int arg = 0; arg < argv.Count(); arg++)
            {
                if (argv[arg] == "-b")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        int number = 64;
                        Int32.TryParse(argv[arg + 1], out number);
                        commandline += " --bitrate " + number.ToString();
                        arg++;
                    }
                }
                if (argv[arg] == "-artist")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " --artist \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-title")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " --title \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                        trackname = argv[arg + 1].Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('\"', '_').Replace('?', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_');
                        arg++;
                    }
                }
                else if (argv[arg] == "-album")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        album = argv[arg + 1];
                        commandline += " --album \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-category")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        album = argv[arg + 1];
                        arg++;
                    }
                }
                else if (argv[arg] == "-year")
                {
                    commandline += " --date \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-tn")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        tracknumber = argv[arg + 1];
                        commandline += " --tracknumber \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-g")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " --genre \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-aa")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        artist = argv[arg + 1];
                        arg++;
                    }
                }
                else if (argv[arg] == "-comm")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        commandline += " --comment \\\"\\\"\"\"COMMENT=" + argv[arg + 1] + "\\\"\\\"\"\"";
                        arg++;
                    }
                }
                else if (argv[arg] == "-p")
                {
                    if (!Program.checkArg(argv[arg + 1]))
                    {
                        picturefile = argv[arg + 1];
                        commandline += " --picture \\\"\\\"\"\"input_image_file.imgfile\\\"\\\"\"\"";
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
