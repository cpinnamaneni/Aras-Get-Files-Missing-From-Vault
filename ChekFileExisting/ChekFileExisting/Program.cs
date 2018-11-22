using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChekFileExisting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Vault Path:");
            //string vaultPath = @"C:\Aras\Vault\Aras11SP14\Aras11SP14";//Console.ReadLine();
            string vaultPath =Console.ReadLine();

            Console.WriteLine("Data File:");
            //string dataFilePath = @"D:\Files_List.csv";//Console.ReadLine();
            string dataFilePath = Console.ReadLine();

            Console.WriteLine("Log File:");
            //string LogfilePath = @"C:\Users\Chaitanya P\Documents\Files_List.txt";//Console.ReadLine();
            string LogfilePath = Console.ReadLine();

            char delimiter ;

            string[] lines = System.IO.File.ReadAllLines(dataFilePath);
            var Logfile = File.AppendText(LogfilePath);
            int missing_filecnt = 0;

            foreach (string line in lines)
            {

                
                delimiter = line[32];
                string[] linedetails = line.Split(delimiter);
                string fileid = linedetails[0];
                string fileName = linedetails[1];
                char fstchar = fileid[0];
                string nxttwochars = fileid.Substring(1, 2);

                try
                {
                    string folderpath = Path.Combine(vaultPath, fileid[0].ToString(), nxttwochars, fileid.Substring(3, 29));

                    string filepath = Path.Combine(folderpath, fileName);

                    if (!File.Exists(filepath))
                    {
                        Console.WriteLine(fileName + "File Does not Exist......");
                        Logfile.WriteLine(filepath + delimiter + line);
                        missing_filecnt++;
                    }
                    else
                    {
                        //Console.WriteLine(fileName + "Exist .....");
                    }
                }
                catch (Exception ex)
                {
                    Logfile.WriteLine("" + delimiter + line);
                }

            }
            Console.WriteLine("no of Missing file -- " + missing_filecnt);
            try
            {
                if (missing_filecnt <= 0)
                {
                    if (File.Exists(LogfilePath))
                    {
                        Console.WriteLine("Deleteing the Log file");
                        Logfile.Close();
                        File.Delete(LogfilePath);
                    }
                }
                else
                {
                    Console.WriteLine("Closing the Log file");
                    Logfile.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
