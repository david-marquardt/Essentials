using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Essentials
{
    public static class FileExtensions
    {
        public static readonly string LocalFolder;


        static FileExtensions()
        {
            // Gets the target platform's valid save location
            LocalFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        // Byte[] extension methods

        public static string GetLocalFilePath(string fileName)
        {
            return Path.Combine(LocalFolder, fileName);
        }

        public static async Task<string> SaveToLocalFolderAsync(this byte[] dataBytes, string fileName)
        {
            return await Task.Run(() =>
            {
                // Use Combine so that the correct file path slashes are used
                var filePath = Path.Combine(LocalFolder, fileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                File.WriteAllBytes(filePath, dataBytes);

                return filePath;
            });
        }

        public static async Task<byte[]> LoadFileBytesAsync(string filePath)
        {
            return await Task.Run(() => File.ReadAllBytes(filePath));
        }

        //####################################################################
        // Stream extension methods
        //####################################################################

        public static async Task<string> SaveToLocalFolderAsync(this Stream dataStream, string fileName)
        {
            // Use Combine so that the correct file path slashes are used
            var filePath = Path.Combine(LocalFolder, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var fileStream = File.OpenWrite(filePath))
            {
                if (dataStream.CanSeek)
                    dataStream.Position = 0;

                await dataStream.CopyToAsync(fileStream);

                return filePath;
            }
        }

        public static async Task<Stream> LoadFileStreamAsync(string fileName)
        {
            return await Task.Run(() =>
            {
                var filePath = Path.Combine(LocalFolder, fileName);

                if (File.Exists(filePath))
                {
                    using (var fileStream = File.OpenRead(filePath))
                    {
                        return fileStream;
                    }
                }
                else
                {
                    return null;
                }

            });
        }

        public static Stream GetFileStream(string fileName)
        {

            var filePath = Path.Combine(LocalFolder, fileName);



            if (File.Exists(filePath))
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    return fileStream;
                }
            }
            else
            {
                return null;
            }

        }

        public static string GetFileString(string fileName)
        {

            var filePath = Path.Combine(LocalFolder, fileName);

            if (File.Exists(filePath))
            {
                string fileStream = File.ReadAllText(filePath);

                return fileStream;

            }
            else
            {
                return null;
            }

        }


        public static async Task<Stream> LoadFileStreamFromPathAsync(string fullPath)
        {
            return await Task.Run(() =>
            {
                if (File.Exists(fullPath))
                {
                    using (var fileStream = File.OpenRead(fullPath))
                    {
                        return fileStream;
                    }
                }
                else
                {
                    return null;
                }

            });
        }


        //####################################################################
        // Directory manipulation
        //####################################################################

        /// <summary>
        /// Checks if Directory exists
        /// </summary>
        /// <param name="dirName">Name of directory, which should be checked if existend</param>
        /// <returns>true if exists, false if not</returns>
        public static bool DirectoryExists(string dirName)
        {
            string folder = Path.Combine(LocalFolder, dirName);

            return File.Exists(folder);
        }

        public static bool FilePathExists(string filePath)
        {

            return File.Exists(filePath);
        }

        /// <summary>
        /// Checks if file exists in directory
        /// </summary>
        /// <param name="dirName">Name of directory, which should be checked if existend</param>
        /// <param name="fileName"></param>
        /// <returns>true if exists, false if not</returns>


        public static bool FileInDirExists(string dirName, string fileName)
        {
            string folder = Path.Combine(LocalFolder, dirName);
            string file = Path.Combine(folder, fileName);

            return File.Exists(file);
        }



        /// <summary>
        /// Creates a directory
        /// </summary>
        /// <param name="dirName"></param>

        public static void CreateDirectory(string dirName)
        {
            if (!DirectoryExists(dirName))
            {
                var dir = Directory.CreateDirectory(LocalFolder + "/" + dirName);
            }
        }



        public static async Task<string> SaveToFolderAsync(this Stream dataStream, string dirName, string fileName)
        {
            // Use Combine so that the correct file path slashes are used
            var dirPath = Path.Combine(LocalFolder, dirName);
            var filePath = Path.Combine(dirPath, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var fileStream = File.OpenWrite(filePath))
            {
                if (dataStream.CanSeek)
                    dataStream.Position = 0;

                await dataStream.CopyToAsync(fileStream);

                return filePath;
            }
        }

        public static List<string> GetFiles(string dirName = "")
        {
            string folder;
            if (dirName != "")
            {
                folder = Path.Combine(LocalFolder, dirName);

            }
            else
            {
                folder = LocalFolder;
            }

            if (Directory.Exists(folder))
            {

                string[] files = Directory.GetFiles(folder);

                return files.OfType<string>().ToList();
            }
            else
            {
                return null;
            }

        }



    }
}
