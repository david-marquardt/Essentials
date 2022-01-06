using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

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

            var fileStream = File.OpenWrite(filePath);
            if (dataStream.CanSeek)
                dataStream.Position = 0;

            await dataStream.CopyToAsync(fileStream);

            dataStream.Close();

            return filePath;
        }


        public static async Task<string> CopyFileToFolder(this string originFile, string dirName, string fileName)
        {
            // Use Combine so that the correct file path slashes are used
            string dirPath = Path.Combine(LocalFolder, dirName);
            string filePath = Path.Combine(dirPath, fileName);

            if (!FilePathExists(dirName))
            {
                CreateDirectory(dirName);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (FileStream fileStream = File.OpenWrite(filePath))
            {
                Stream dataStream = File.OpenRead(originFile);

                if (dataStream.CanSeek)
                {
                    dataStream.Position = 0;
                    dataStream.Seek(0, SeekOrigin.Begin);
                }

                await dataStream.CopyToAsync(fileStream);

                dataStream.Dispose();
                dataStream.Close();
                fileStream.Close();
            }
            return filePath;
        }

        public static async Task<Stream> LoadFileStreamAsync(string fileName)
        {
            return await Task.Run(() =>
            {
                var filePath = Path.Combine(LocalFolder, fileName);

                if (File.Exists(filePath))
                {
                    var fileStream = File.OpenRead(filePath);
                    return fileStream;
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
                    var fileStream = File.OpenRead(fullPath);
                    return fileStream;
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
            string dirPath = Path.Combine(LocalFolder, dirName);
            string filePath = Path.Combine(dirPath, fileName);

            if (!FilePathExists(dirName))
            {
                CreateDirectory(dirName);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (FileStream fileStream = File.OpenWrite(filePath))
            {
                if (dataStream.CanSeek)
                {
                    dataStream.Position = 0;
                    dataStream.Seek(0, SeekOrigin.Begin);
                }

                await dataStream.CopyToAsync(fileStream);

                dataStream.Dispose();
                dataStream.Close();
                fileStream.Close();
            }
            return filePath;
        }


        public static string SaveToFolder(this Stream dataStream, string dirName, string fileName)
        {
            // Use Combine so that the correct file path slashes are used
            string dirPath = Path.Combine(LocalFolder, dirName);
            string filePath = Path.Combine(dirPath, fileName);

            if (!FilePathExists(dirName))
            {
                CreateDirectory(dirName);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (FileStream fileStream = File.OpenWrite(filePath))
            {
                if (dataStream.CanSeek)
                {
                    dataStream.Position = 0;
                    dataStream.Seek(0, SeekOrigin.Begin);
                }

                dataStream.CopyTo(fileStream);

                dataStream.Dispose();
                dataStream.Close();
                fileStream.Close();
            }
            return filePath;
        }

        public static List<string> GetFiles(string dirName = "")
        {
            string folder = dirName != "" ? Path.Combine(LocalFolder, dirName) : LocalFolder;

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

        public static byte[] ImageSourceToBytes(ImageSource imageSource)
        {
            StreamImageSource streamImageSource = (StreamImageSource)imageSource;
            System.Threading.CancellationToken cancellationToken =
            System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;
            byte[] bytesAvailable = new byte[stream.Length];
            stream.Read(bytesAvailable, 0, bytesAvailable.Length);
            return bytesAvailable;
        }

        public static Stream ImageSourceToStream(ImageSource imageSource)
        {
            StreamImageSource streamImageSource = (StreamImageSource)imageSource;
            System.Threading.CancellationToken cancellationToken =
            System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;
            return stream;
        }

        public static string FileToBase64(Stream stream)
        {
            string image64String = "";

            if (stream != null)
            {
                //image.Source = ImageSource.FromStream(() => stream);
                byte[] imageBytes = ReadStreamToEnd(stream);
                //byte[] imageBytes = stream.ToArray();
                image64String = Convert.ToBase64String(imageBytes);

            }

            return image64String;
        }

        public static async Task<string> GetBase64(string filePath)
        {
            byte[] bytes = await LoadFileBytesAsync(filePath);


            string image64String = Convert.ToBase64String(bytes);


            return image64String;
        }

        public static Stream GetStreamFromBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            Stream stream = new MemoryStream(bytes);

            return stream;
        }

        public static byte[] ReadStreamToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static FileSize GetFileSize(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            FileSize fileSize = new FileSize();


            fileSize.B = fi.Length;
            fileSize.KB = fileSize.B / 1024.0;
            fileSize.MB = fileSize.KB / 1024.0;
            fileSize.GB = fileSize.MB / 1024.0;

            return fileSize;
        }

        public static void DeleteDirectory(string dirName)
        {
            string path = GetLocalFilePath(dirName);

            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists)
            {
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }
            }
        }

        public static void DeleteFileInDirectory(string dirName, string fileName)
        {
            string path = GetLocalFilePath(dirName);

            path = Path.Combine(path, fileName);

            FileInfo file = new FileInfo(path);

            file.Delete();
        }


        public static void CombineMP3(string filepath1, string filepath2)
        {
            using (var fs = File.OpenWrite(Path.Combine(LocalFolder, "metronom.mp3")))
            {
                var buffer = File.ReadAllBytes(filepath1);
                fs.Write(buffer, 0, buffer.Length);
                buffer = File.ReadAllBytes(filepath2);
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush();
            }
        }


    }
}
