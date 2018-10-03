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
            string album = "";
            string source = "";
            string commandline = "";
            //starts parsing parameters
            // NOTE: \\\"\\\"\" is needed so the BOINC wrapper can understand what a " is...
            for (int arg = 0; arg < argv.Count(); arg++)
            {
                if (argv[arg] == "-artist")
                {
                    commandline += " -T \\\"\\\"\"\"ARTIST=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-title")
                {
                    commandline += " -T \\\"\\\"\"\"TITLE=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    trackname = argv[arg + 1].Replace('\\', '_').Replace('/', '_').Replace(':', '_').Replace('*', '_').Replace('\"', '_').Replace('?', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_'); 
                    arg++;
                }
                else if (argv[arg] == "-album")
                {
                    album = argv[arg + 1];
                    commandline += " -T \\\"\\\"\"\"ALBUM=" + album + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-year")
                {
                    commandline += " -T \\\"\\\"\"\"DATE=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-tn")
                {
                    tracknumber = argv[arg + 1];
                    commandline += " -T \\\"\\\"\"\"TRACKNUMBER=" + tracknumber + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-g")
                {
                    commandline += " -T \\\"\\\"\"\"GENRE=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-aa")
                {
                    commandline += " -T \\\"\\\"\"\"BAND=" + argv[arg + 1] + "\\\"\\\"\"\" -T \\\"\\\"\"\"ALBUMARTIST=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-comm")
                {
                    commandline += " -T \\\"\\\"\"\"COMMENT=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-comp")
                {
                    commandline += " -T \\\"\\\"\"\"COMPOSER=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-cdn")
                {
                    commandline += " -T \\\"\\\"\"\"DISCNUMBER=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-cdt")
                {
                    commandline += " -T \\\"\\\"\"\"TOTALDISCS=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-tt")
                {
                    commandline += " -T \\\"\\\"\"\"TOTALTRACKS=" + argv[arg + 1] + "\\\"\\\"\"\"";
                    arg++;
                }
                else if (argv[arg] == "-p")
                {
                    commandline += " -T \\\"\\\"\"\"picture=" + argv[arg + 1] + "\\\"\\\"\"\"";
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
