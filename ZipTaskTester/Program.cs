using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipTaskTester
{
    class Program
    {
        static void Main(string[] args)
        {
            // what we want here is to call a method that is either void or bool - 'Process()'
            // that inside rus two tasks or calls two methods Task<ResultT> / Task (same method twice!!)
            // 1) tests if directory has any files
            // 1a) creates zip from the 'Processed Directory'
            // 1b) checkes the contents of zip and original directory the same
            // 1c) deletes contents of original directory

            // C:\Temp\xxx\Processed
            // C:\Temp\xxx\Results
            // C:\Temp\xxx\Archive

            // need to ensure that the tasks finish before the method completes - as can't reset our timer 
            //Processor.Process().Wait();
            //
            //Console.WriteLine("we are done");
            //string finished = Console.ReadLine();

            Processor.MoveFileAndTimeStamp();
        }
    }
}
