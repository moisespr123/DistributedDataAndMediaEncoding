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
            string album = "";
            string source = "";
            string commandline = "";
            //starts parsing parameters
            // NOTE: \\\"\\\"\" is needed so the BOINC wrapper can understand what a " is...
            for (int arg = 0; arg < argv.Count(); arg++)
            {
                if (argv[arg] == "-b")
                {
                    int number = 64;
                    Int32.TryParse(argv[arg + 1], out number);
                    commandline += " --bitrate " + number.ToString ();
                    arg++;
                }
                if (argv[arg] == "-artist")
                {
                    commandline += " --artist \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-title")
                {
                    commandline += " --title \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                    trackname = argv[arg + 1].Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('\"', '_').Replace('?', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_');
                    arg++;
                }
                else if (argv[arg] == "-album")
                {
                    album = argv[arg + 1];
                    commandline += " --album \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-year")
                {
                    commandline += " --date \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-tn")
                {
                    tracknumber = argv[arg + 1];
                    commandline += " --tracknumber \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-g")
                {
                    commandline += " --genre \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-comm")
                {
                    commandline += " --comment \\\"\\\"\"\"COMMENT=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-p")
                {
                    commandline += " --picture \\\"\\\"\"\"" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-s")
                {
                    source = argv[arg + 1];
                    arg++;
                }

            }
            string[] return_values = { commandline, trackname, tracknumber, album, source };
            return return_values;
        }
    }
}
