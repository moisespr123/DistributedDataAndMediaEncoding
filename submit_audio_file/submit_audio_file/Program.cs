using System;
using System.IO;
using System.Linq;
using System.Net.Http;
namespace submit_audio_file
{
    class Program
    {
        public static bool checkArg(string arg)
        {
            string[] args = { "-b", "-artist", "-title", "-album", "-category", "-year", "-tn", "-g", "-aa", "-ai", "-comm", "-comp", "-cdn", "-cdt", "-tt", "-p", "-s" };
            if (args.All(arg.Contains))
                return true;
            else
                return false;
        }
        public static string CreateBase64Image(string picturePath)
        {
            return Convert.ToBase64String(File.ReadAllBytes(picturePath), Base64FormattingOptions.None);
        }
        static void Main(string[] argv)
        {
            string commandline = "";
            string format = argv[1];
            string artist = "";
            string album = "";
            string picturefile = "";
            string source = "";
            string filename = "";
            string tracknumber;
            string trackname;
            if (format == "flac")
            {
                commandline = "--lax -l 32 -b 4096 -e -m -p -r 15 -A bartlett -A bartlett_hann -A blackman -A blackman_harris_4term_92db -A connes -A flattop -A gauss(0.5) -A hamming -A hann -A kaiser_bessel -A nuttall -A rectangle -A triangle -A tukey(0.5) -A partial_tukey(2) -A punchout_tukey(3) -A welch -V";
                string[] parsed_args = parse_flac_args.parse_args(argv);
                commandline += parsed_args[0];
                trackname = parsed_args[1];
                tracknumber = parsed_args[2];
                album = parsed_args[3];
                artist = parsed_args[4];
                picturefile = parsed_args[5];
                source = parsed_args[6];
                if (!string.IsNullOrEmpty(tracknumber) && !string.IsNullOrEmpty(trackname))
                    filename = string.Format("{0:00}", Convert.ToInt32(tracknumber)) + " - " + trackname + ".flac";
                else
                    filename = Path.GetFileNameWithoutExtension(source) + ".flac";
            }
            else if (format == "opus")
            {
                commandline = "--music";
                string[] parsed_args = parse_opus_args.parse_args(argv);
                commandline += parsed_args[0];
                trackname = parsed_args[1];
                tracknumber = parsed_args[2];
                album = parsed_args[3];
                artist = parsed_args[4];
                picturefile = parsed_args[5];
                source = parsed_args[6];
                if (!string.IsNullOrEmpty(tracknumber) && !string.IsNullOrEmpty(trackname))
                    filename = string.Format("{0:00}", Convert.ToInt32(tracknumber)) + " - " + trackname + ".opus";
                else
                    filename = Path.GetFileNameWithoutExtension(source) + ".opus";
            }
            if (File.Exists(source))
            {
                string result = Upload(argv[0], format, commandline, filename, artist, album, picturefile, source);
                if (result.Contains("Done"))
                {
                    Console.WriteLine("Job submitted :)");
                    Console.WriteLine("----------------");
                    Console.WriteLine("User key: " + argv[0]);
                    Console.WriteLine("Encoding Format: " + format);
                    Console.WriteLine("Command Line Arguments: " + commandline);
                    Console.WriteLine("Filename for output file: " + filename);
                    Console.WriteLine("Album name: " + album);
                    if (!string.IsNullOrEmpty(artist))
                        Console.WriteLine("Album artist: " + artist);
                    Console.WriteLine("Input file: " + source);
                    Console.WriteLine("-----------------");
                    Console.WriteLine("List of your Media Files: http://boinc.moisescardona.me/user_files.php");
                }
                else
                {
                    Console.WriteLine("An error occurred submitting the work :(");
                }
                Console.WriteLine("Done :)");
            }
            else
                Console.WriteLine("The file doesn't exists");
        }
        private static string Upload(string key, string format, string commandline, string filename, string artist, string album, string picturefile, string file)
        {
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                {
                    formData.Add(new StringContent(key), "k");
                    formData.Add(new StreamContent(new FileStream(file, FileMode.Open)), "filedata", Path.GetFileName(file));
                    if (string.IsNullOrEmpty(artist) && !string.IsNullOrEmpty(album))
                        formData.Add(new StringContent(album), "a");
                    else
                        formData.Add(new StringContent(artist + " - " + album), "a");
                    formData.Add(new StringContent(format), "f");
                    formData.Add(new StringContent(commandline.Replace('\'', '`')), "c");
                    if (picturefile != "")
                        formData.Add(new StreamContent(new FileStream(picturefile, FileMode.Open)), "picture", "picture.img");
                    formData.Add(new StringContent(filename), "n");
                    Uri uri = new Uri("http://boinc.moisescardona.me/media_put.php");
                    client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en-US;q=0.8,en;q=0.6,ru;q=0.4");
                    HttpResponseMessage response = client.PostAsync(uri, formData).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Error");
                    }
                    StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result);
                    return reader.ReadToEnd();
                }
            }
        }
    }
}