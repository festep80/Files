using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    public class Files
    {
        public static void FileCopy(string sourceFileName, string destFileName)
        {
            FileInfo fileInf = new FileInfo(sourceFileName);

            if (fileInf.Exists)
            {
                fileInf.CopyTo(destFileName, true);                
            }
            else
            {
                throw new FileNotFoundException(
                    "Source file does not exist or could not be found: "
                    + sourceFileName);
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
                  
            Directory.CreateDirectory(destDirName);
            
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }
            
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        public static void DelFileByName(string fileName, string sourceFileDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceFileDir);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceFileDir);
            }

            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.Name == fileName)
                {
                    file.Delete();
                    continue;
                }
                else
                {
                    throw new FileNotFoundException(
                        fileName 
                        + "does not exist in source directory: " 
                        + sourceFileDir);
                }
            }
        }

        public static void DelFilesByNames(string[] fileNames, string sourceFileDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceFileDir);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceFileDir);
            }

            FileInfo[] files = dir.GetFiles();

            foreach (string item in fileNames)
            {
                foreach (FileInfo file in files)
                {
                    if (file.Name == item)
                    {
                        file.Delete();
                        continue;
                    }
                    else
                    {
                        throw new FileNotFoundException(
                            item
                            + "does not exist in source directory: "
                            + sourceFileDir);
                    }
                }
            }
        }    

        public static void DelFileByMask(string fileExtension, string sourceFileDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceFileDir);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceFileDir);
            }

            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.Extension == fileExtension)
                {
                    file.Delete();
                }                
            }
        }

        public static void MoveFile(string sourceFileName, string destFileName)
        {
            FileInfo fileInf = new FileInfo(sourceFileName);

            if (fileInf.Exists)
            {
                fileInf.MoveTo(destFileName);
            }
            else
            {
                throw new FileNotFoundException(
                    "Source file does not exist or could not be found: "
                    + sourceFileName);
            }
        }
    }
}
