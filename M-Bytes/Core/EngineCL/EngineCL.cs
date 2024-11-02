using Guna.UI2.WinForms;
using M_Bytes.Core.Messages;
using Microsoft.Win32;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_Bytes.Core.EngineCL
{
    internal class EngineCL
    {
        public static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static async Task CREAT_RECOVERY_POINT(string description)
        {
            try
            {
                string command = $"Checkpoint-Computer -Description \"{description}\" -RestorePointType \"MODIFY_SETTINGS\"";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = command,
                    Verb = "runas",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                await Task.Run(() =>
                {
                    using (Process process = Process.Start(psi))
                    {
                        process.WaitForExit();
                    }
                });

                Console.WriteLine("Recovery point created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating recovery point: {ex.Message}");
            }
        }

        public async static Task BLOCK_FIREWALL_APP(string PATH)
        {
            
            if (string.IsNullOrWhiteSpace(PATH))
            {
                MSG.SH("Error Path", "Please Enter App Path");
                return;
            }

            await Task.Run(() =>
            {
                try
                {
                    string command = $"New-NetFirewallRule -DisplayName \"Block_{PATH}\" -Direction Outbound -Program \"{PATH}\" -Action Block";

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = command,
                        Verb = "runas",  /// ADMIN PRIV ...
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    Process process = Process.Start(psi);
                    process.WaitForExit();


                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"ENGINE.CL: Error Blocking");
                }
                finally
                {
                    MSG.SH("Success", $"Blocked");
                    logger.Info($"ENGINE.CL: Blocked {PATH}");
                }
            });
        }




        public async static Task UNBLOCK_FIREWALL_APP(string PATH)
        {
            if (string.IsNullOrWhiteSpace(PATH))
            {
                MSG.SH("Error Path", "Please Enter App Path");
                return;
            }

            await Task.Run(() =>
            {
                try
                {
                    string command = $"Remove-NetFirewallRule -DisplayName \"Block_{PATH}\"";

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = command,
                        Verb = "runas",
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

                    using (Process process = Process.Start(psi))
                    {
                        process.WaitForExit();
                    }

                    logger.Info($"ENGINE.CL: Unblocked {PATH}");
                    MSG.SH("Success", $"Unblocked {PATH}");
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"ENGINE.CL: Error Unblocking {PATH}");
                }
            });
        }



        public static Task MemoryClean()
        {
            return Task.Run(() =>
            {
                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    logger.Info("ENGINE.CL: Memory cleaned successfully.");
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "ENGINE.CL: Error during memory clean.");
                }
            });
        }

        public static Task RegistryClean()
        {
            return Task.Run(() =>
            {
                try
                {
                    using (RegistryKey currentUserKey = Registry.CurrentUser.OpenSubKey(@"Software\YourApplication", writable: true))
                    {
                        if (currentUserKey != null)
                        {
                            string[] valueNames = currentUserKey.GetValueNames();
                            foreach (string valueName in valueNames)
                            {
                                currentUserKey.DeleteValue(valueName, throwOnMissingValue: false);
                                logger.Info($"ENGINE.CL: Deleted registry value '{valueName}' from HKEY_CURRENT_USER\\Software\\YourApplication");
                            }
                        }
                    }
                    logger.Info("ENGINE.CL: Registry cleaned successfully.");
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "ENGINE.CL: Error during registry clean.");
                }
            });
        }



        public static Task CleanAllBrowserCaches()
        {
            return Task.Run(async () =>
            {
                string userDataPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Google\\Chrome\\User Data");

                var profileDirectories = Directory.GetDirectories(userDataPath);

                foreach (var profileDir in profileDirectories)
                {
                    string cachePath = Path.Combine(profileDir, "Cache");
                    await RemoveAllFiles(cachePath);
                }
            });
        }

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
                            logger.Error(ex, $"ENGINE.CL: Error hashing file '{filePath}'");
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
                                    logger.Info($"ENGINE.CL: Deleted duplicate file '{duplicateFiles[i]}'");
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex, $"ENGINE.CL: Error deleting file '{duplicateFiles[i]}'");
                                }
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"ENGINE.CL: Error processing folder '{folderPath}'");
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
            catch (FileNotFoundException ex)
            {
                logger.Error(ex, $"ENGINE.CL: File not found '{filePath}'.");
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.Error(ex, $"ENGINE.CL: Access denied to file '{filePath}'.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"ENGINE.CL: Error computing hash for '{filePath}'.");
                return null;
            }
        }


        public static async Task CleanTemp()
        {
            string tempPath = Path.GetTempPath();
            await RemoveAllFiles(tempPath);
        }

        public static async Task CleanPrefetch()
        {
            string prefetchPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            await RemoveAllFiles(prefetchPath);
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
                            logger.Info($"ENGINE.CL: Deleted startup entry '{valueName}'");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "ENGINE.CL: Error removing startup files");
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
                            logger.Info($"ENGINE.CL: Deleted file '{file}'");
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, $"ENGINE.CL: Error deleting file '{file}'");
                        }
                    }));

                    await Task.WhenAll(deleteTasks);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"ENGINE.CL: Error removing all files in '{path}'");
                }
            }
            else
            {
                logger.Warn($"ENGINE.CL: Directory does not exist: {path}");
            }
        }
    }
}
