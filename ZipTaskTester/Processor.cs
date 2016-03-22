using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Threading;

namespace ZipTaskTester
{
    public class Processor
    {
        public Processor()
        {
            _archive = @"C:\Temp\xxx\Archive\";
            _processed = @"C:\Temp\xxx\Processed\";
            _results = @"C:\Temp\xxx\Results\";
        }
        private string _archive;
        private string _processed;
        private string _results;

        public static async Task Process()
        {
            await CreateZip(@"C:\Temp\xxx\Archive\", "P_", @"C:\Temp\xxx\Processed\");
            await CreateZip(@"C:\Temp\xxx\Archive\", "R_", @"C:\Temp\xxx\Results\");
        }

        public static async Task CreateZip(string archivepath, string prefix, string processedpath)
        {
            await Task.Run(() =>
            {
                Thread thread = Thread.CurrentThread;
                Console.WriteLine("Task thread: " + thread.ManagedThreadId.ToString());

                // want to ensure avoid directorynotfoundexception
                if (Directory.Exists(processedpath))
                {
                    // count to make sure further processing worth it
                    DirectoryInfo info = new DirectoryInfo(processedpath);
                    int count = info.EnumerateFiles().Count();

                    if(count > 0)
                    {
                        // need to ensure that the archive directory exists, if not, create it
                        if (!Directory.Exists(@"C:\Temp\xxx\Archive\"))
                        {
                            Directory.CreateDirectory(@"C:\Temp\xxx\Archive\");
                        }
                        // create zip name
                        string name = archivepath + prefix + DateTimeOffset.Now.ToString("yyyy -MM-dd_hh-mm-ss") +".zip";
                        // create the zip file
                        ZipFile.CreateFromDirectory(processedpath, name);
                        //
                        // want to read back contents and ensure same as directory
                        ZipArchive archive = ZipFile.OpenRead(name);
                        if (count == archive.Entries.Count)
                        {
                            Console.WriteLine("all good mother fucker");
                            foreach (var file in Directory.GetFiles(processedpath))
                            {
                                File.Delete(file);
                            }
                        }
                    }
                }
                else
                {
                    // TODO - log - no processed directory at all!!
                    Console.WriteLine("Cannot find directory \"Processed\"");
                }
            });
        }

        public static void MoveFileAndTimeStamp()
        {
            // testing my timezone rename stuff
            DateTime timeNow = DateTime.Now;
            try
            {
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");

                // getting a timezone not found exception for "AUS Eastern Daylight Time' - lets just cheat and add 1 hour 
                string currentName = easternZone.IsDaylightSavingTime(timeNow) ? easternZone.DaylightName : easternZone.StandardName;

                DateTime easternTimeNow;
                if (currentName == "AUS Eastern Daylight Time")
                {
                    easternTimeNow = TimeZoneInfo.ConvertTime(timeNow, TimeZoneInfo.Local,
                                                       easternZone);
                    easternTimeNow.AddHours(1);
                }
                else
                {
                    easternTimeNow = TimeZoneInfo.ConvertTime(timeNow, TimeZoneInfo.Local,
                                                       easternZone);
                }
                
                Console.WriteLine(easternTimeNow.ToString("yyyy-MM-dd hh:mm:ss"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            string ben = Console.ReadLine();
            Console.WriteLine("Winning");

        }
    }
}
