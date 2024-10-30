using M_Bytes.Core.Messages;
using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace M_Bytes.Core.EngineCL
{
    internal class EngineCL
    {
        public static Task DeleteDuplicateFilesAsync(string folderPath)
        {
            return Task.Run(() =>
            {
                var fileHashes = new ConcurrentDictionary<string, List<string>>();

                try
                {
                    var files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

                    Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = 10 }, filePath =>
                    {
                        try
                        {
                            string fileHash = ComputeFileHash(filePath);
                            fileHashes.AddOrUpdate(fileHash,
                                new List<string> { filePath },
                                (key, existingList) =>
                                {
                                    lock (existingList) { existingList.Add(filePath); }
                                    return existingList;
                                });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error hashing file '{filePath}': {ex.Message}");
                        }
                    });

                    Parallel.ForEach(fileHashes, new ParallelOptions { MaxDegreeOfParallelism = 10 }, hashEntry =>
                    {
                        var duplicateFiles = hashEntry.Value;
                        if (duplicateFiles.Count > 1)
                        {
                            for (int i = 1; i < duplicateFiles.Count; i++)
                            {
                                try
                                {
                                    File.Delete(duplicateFiles[i]);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error deleting file '{duplicateFiles[i]}': {ex.Message}");
                                }
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing folder '{folderPath}': {ex.Message}");
                }
            });
        }

        public static Task DELETE_ALL_DEBLUCATE_FILES()
        {
            return Task.Run(async () =>
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady && drive.DriveType == DriveType.Fixed)
                    {
                        try
                        {
                            await DeleteDuplicateFilesAsync(drive.RootDirectory.FullName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error scanning drive '{drive.Name}': {ex.Message}");
                        }
                    }
                }
            });
        }

        private static string ComputeFileHash(string filePath)
        {
            try
            {
                using (var md5 = MD5.Create())
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error computing hash for '{filePath}': {ex.Message}");
                return null;
            }
        }

        public static async Task CleanTemp()
        {
            string tempPath = Path.GetTempPath();
            await RemoveAllFiles(tempPath);
        }

        public static async Task CleanPerfetch()
        {
            string perfetchPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            await RemoveAllFiles(perfetchPath);
        }

        public static async Task RemoveStartUpFiles()
        {
            try
            {
                string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                await RemoveAllFiles(startupPath);

                using (RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", writable: true))
                {
                    if (startupKey != null)
                    {
                        string[] valueNames = startupKey.GetValueNames();
                        foreach (string valueName in valueNames)
                        {
                            startupKey.DeleteValue(valueName, throwOnMissingValue: false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing startup files: {ex.Message}");
            }
        }

        public static async Task RemoveAllFiles(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);

                    var deleteTasks = files.Select(file => Task.Run(() =>
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error deleting file '{file}': {ex.Message}");
                        }
                    }));

                    await Task.WhenAll(deleteTasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error removing all files in '{path}': {ex.Message}");
                }
            }
            else
            {
                MSG.SH("Warning", "Directory does not exist: " + path);
            }
        }
    }
}
