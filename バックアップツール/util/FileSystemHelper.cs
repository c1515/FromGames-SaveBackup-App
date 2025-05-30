using System.Security;

namespace DS3BackupApp.util {
    internal static class FileSystemHelper {
        internal static void CopyFile(string filePath, string destPath) {
            try {
                File.Copy(filePath, destPath, true);
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (DirectoryNotFoundException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_DirectoryNotFound, ex.Message));
            } catch (FileNotFoundException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_FileNotFound, ex.Message));
            } catch (IOException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_IO, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_CopyEx, ex.Message));
            }
        }

        internal static void CreateDirectory(string path) {
            try {
                Directory.CreateDirectory(path);
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (IOException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_IO, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (SecurityException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_Security, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_CreateDirectoryEx, ex.Message));
            }
        }

        internal static bool DeleteDirectory(string path, bool recursive) {
            try {
                Directory.Delete(path, recursive);
                return true;
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (IOException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_IO, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (SecurityException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_Security, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_DeleteEx, ex.Message));
            }
            return false;
        }

        internal static void SetLastWriteTime(string path, DateTime lastWriteTime) {
            try {
                Directory.SetLastWriteTime(path, lastWriteTime);
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_SetLastWriteTimeEx, ex.Message));
            }
        }

        internal static bool GetLastWriteTime(string path, out DateTime lastWriteTime) {
            try {
                lastWriteTime = Directory.GetLastWriteTime(path);
                return true;
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_GetLastWriteTimeEx, ex.Message));
            }
            lastWriteTime = DateTime.MinValue;
            return false;
        }

        internal static bool MoveDirectory(string sourceDir, string destDir) {
            try {
                Directory.Move(sourceDir, destDir);
                return true;
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (IOException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_IO, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (SecurityException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_Security, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_MoveEx, ex.Message));
            }
            return false;
        }

        internal static string[] GetDirectories(string path) {
            try {
                return Directory.GetDirectories(path);
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (DirectoryNotFoundException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_DirectoryNotFound, ex.Message));
            } catch (IOException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_IO, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (SecurityException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_Security, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_GetDirectoriesEx, ex.Message));
            }
            return Array.Empty<string>(); // エラー時は空の配列を返す
        }

        internal static string[] GetFiles(string path) {
            try {
                return Directory.GetFiles(path);
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (DirectoryNotFoundException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_DirectoryNotFound, ex.Message));
            } catch (IOException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_IO, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (SecurityException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_Security, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_GetDirectoriesEx, ex.Message));
            }
            return Array.Empty<string>(); // エラー時は空の配列を返す
        }

        internal static bool CreateFile(string path) {
            try {
                File.Create(path).Close();
                return true;
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (DirectoryNotFoundException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_DirectoryNotFound, ex.Message));
            } catch (IOException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_IO, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (SecurityException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_Security, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_CreateFileEx, ex.Message));
            }
            return false;
        }

        internal static bool WriteAllText(string path, string content) {
            try {
                File.WriteAllText(path, content);
                return true;
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (IOException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_IO, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (SecurityException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_Security, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_MemoWriteEx, ex.Message));
            }
            return false;
        }

        internal static string ReadAllText(string path) {
            try {
                return File.ReadAllText(path);
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
            } catch (IOException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_IO, ex.Message));
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
            } catch (SecurityException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_Security, ex.Message));
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_MemoReadEx, ex.Message));
            }
            return "";
        }
    }
}
