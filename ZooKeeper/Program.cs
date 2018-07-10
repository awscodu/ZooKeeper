using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper
{
    class Program
    {
        public static List<string> GetAllZipFiles(string directory)
        {
            return Directory.GetFiles(directory, "*.zip", SearchOption.AllDirectories).ToList();
        }

        static void UnzipAll(string path, string targetPath, string password)
        {
            List<string> zipFiles = GetAllZipFiles(path);
            foreach(string zipPath in zipFiles)
            {
                using (ZipFile archive = new ZipFile(zipPath))
                {
                    archive.Password = password;
                    archive.Encryption = EncryptionAlgorithm.PkzipWeak; // the default: you might need to select the proper value here
                    archive.StatusMessageTextWriter = Console.Out;
                    archive.ExtractAll(targetPath, ExtractExistingFileAction.Throw);
                }
            }
        }

        static void Main(string[] args)
        {
            if(args[0] == "help" || args[0] == "?")
            {
                Console.WriteLine("Unzips all password protected zip files");
                Console.WriteLine("Usage: C:/user/folder C:/user/targetfolder password");
                return;
            }
            if(args.Length < 3 || args.Length > 3)
            {
                Console.WriteLine("Usage: path targetPath password");
                return;
            }
            UnzipAll(args[0], args[1], args[2]);
            Console.WriteLine("Done.");
            return;
        }

    }
}
