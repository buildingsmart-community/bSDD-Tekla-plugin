using System;
using System.IO;
using System.Linq;

namespace Bsdd.Core.FileManagement
{
    public static class FileValidator
    {
        /// <summary>
        /// Checks if the given path is valid, ensuring it adheres to path length limits and contains no invalid characters.
        /// </summary>
        /// <param name="path">The path to validate.</param>
        /// <returns>True if the path is valid, false otherwise.</returns>
        public static bool IsValidPath(string path)
        {
            // Check if path is null, empty, or exceeds the maximum path length
            if (string.IsNullOrEmpty(path) || path.Length > 260)
            {
                return false;
            }

            // Check if path ends with invalid characters
            char[] invalidEndChars = { '.', ' ', '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
            if (invalidEndChars.Any(c => path.EndsWith(c.ToString())))
            {
                return false;
            }

            // Check for any invalid characters in the path
            var invalidChars = Path.GetInvalidPathChars();
            if (path.IndexOfAny(invalidChars) >= 0)
            {
                return false;
            }

            // You can further enforce more restrictions if needed, such as forbidding certain reserved names like CON, PRN, etc.

            return true;
        }

        /// <summary>
        /// Ensures that the specified directory exists, creating it if necessary.
        /// </summary>
        /// <param name="folderPath">The folder path to ensure.</param>
        /// <returns>True if the directory exists or was successfully created, false otherwise.</returns>
        public static bool EnsureDirectory(string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Failed to create directory: {ex.Message}");
                return false;
            }
        }


        /// <summary>
        /// Validates whether the provided file name is valid. 
        /// A valid file name must not be null or empty, must be within the allowed length, 
        /// and must not contain any invalid characters as defined by the system.
        /// </summary>
        /// <param name="fileName">The file name to validate.</param>
        /// <returns>True if the file name is valid, otherwise false.</returns>
        public static bool IsValidFileName(string fileName)
        {
            // Check if the file name is null, empty, or exceeds the maximum allowed length (255 characters)
            if (string.IsNullOrEmpty(fileName) || fileName.Length > 255) // Max filename length in most file systems
            {
                return false;
            }

            // Check if the file name contains any invalid characters
            var invalidChars = Path.GetInvalidFileNameChars();
            return fileName.IndexOfAny(invalidChars) < 0;
        }



    }
}