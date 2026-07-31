using System.Security;

namespace DS3BackupApp.util {
    internal static class FileSystemHelper {
        internal static bool CopyFile(string sourceFile, string destPath) {
            try {
                File.Copy(sourceFile, destPath, true);
                return true;
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
            return false;
        }

        // フォルダを再帰的にコピーするメソッドを追加
        internal static bool CopyDirectory(string sourceDir, string destDir, bool recursive = true, bool overwrite = true) {
            try {
                if (!Directory.Exists(sourceDir)) {
                    MessageHepler.Error(string.Format(Properties.Resources.Error_DirectoryNotFound, sourceDir));
                    return false;
                }

                if (!Directory.Exists(destDir)) {
                    Directory.CreateDirectory(destDir);
                }

                // ファイルをコピー
                foreach (var file in Directory.GetFiles(sourceDir)) {
                    var destFile = Path.Combine(destDir, Path.GetFileName(file));
                    File.Copy(file, destFile, overwrite);
                }

                // サブディレクトリを再帰的にコピー
                if (recursive) {
                    foreach (var dir in Directory.GetDirectories(sourceDir)) {
                        var destSubDir = Path.Combine(destDir, Path.GetFileName(dir));
                        if (!CopyDirectory(dir, destSubDir, true, overwrite)) {
                            throw new Exception();
                        }
                    }
                }

                // 最終更新日時をコピー（可能なら）
                try {
                    Directory.SetLastWriteTime(destDir, Directory.GetLastWriteTime(sourceDir));
                } catch {
                    // 個別にメッセージを出さず無視（元のスタイルに合わせる）
                }

                return true;
            } catch (UnauthorizedAccessException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_UnauthorizedAccess, ex.Message));
                DeleteDirectory(destDir, true); // コピーに失敗した場合、作成したディレクトリを削除
            } catch (ArgumentException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_ArgumentPath, ex.Message));
                DeleteDirectory(destDir, true);
            } catch (PathTooLongException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_PathTooLong, ex.Message));
                DeleteDirectory(destDir, true);
            } catch (DirectoryNotFoundException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_DirectoryNotFound, ex.Message));
                DeleteDirectory(destDir, true);
            } catch (IOException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_IO, ex.Message));
                DeleteDirectory(destDir, true);
            } catch (NotSupportedException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_NotSupportedPath, ex.Message));
                DeleteDirectory(destDir, true);
            } catch (SecurityException ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_Security, ex.Message));
                DeleteDirectory(destDir, true);
            } catch (Exception ex) {
                MessageHepler.Error(string.Format(Properties.Resources.Error_CopyEx, ex.Message));
                DeleteDirectory(destDir, true);
            }
            return false;
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
            return []; // エラー時は空の配列を返す
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
            return []; // エラー時は空の配列を返す
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
