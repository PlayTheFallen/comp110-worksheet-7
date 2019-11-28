﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7
{
	public static class DirectoryUtils
	{
		// Return the size, in bytes, of the given file
		public static long GetFileSize(string filePath)
		{
			return new FileInfo(filePath).Length;
		}

		// Return true if the given path points to a directory, false if it points to a file
		public static bool IsDirectory(string path)
		{
			return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
		}

		// Return the total size, in bytes, of all the files below the given directory
		public static long GetTotalSize(string directory)
		{
            long total = 0;
            foreach (FileInfo file in new DirectoryInfo(directory).EnumerateFiles())
            {
                total += file.Length;
            }
            return total;
        }

		// Return the number of files (not counting directories) below the given directory
		public static int CountFiles(string directory)
		{
            return new DirectoryInfo(directory).GetFiles().Length;
		}

		// Return the nesting depth of the given directory. A directory containing only files (no subdirectories) has a depth of 0.
		public static int GetDepth(string directory)
		{
            int depth = 0;
            string[] directories = Directory.GetDirectories(directory);
            if (directories.Length == 0) return 0;
            foreach(string dir in directories)
            {
                int _depth = GetDepth(dir);
                if (_depth > depth) depth = _depth;
            }
            return depth;
		}

		// Get the path and size (in bytes) of the smallest file below the given directory
		public static Tuple<string, long> GetSmallestFile(string directory)
		{
            string fileName = null;
            long fileSize = -1;
            foreach(FileInfo file in new DirectoryInfo(directory).EnumerateFiles())
            {
                if(fileName == null || fileSize == -1)
                {
                    fileName = file.Name;
                    fileSize = file.Length;
                    continue;
                }
                if(file.Length < fileSize)
                {
                    fileName = file.Name;
                    fileSize = file.Length;
                }
            }
            return new Tuple<string, long>(fileName, fileSize);
		}

		// Get the path and size (in bytes) of the largest file below the given directory
		public static Tuple<string, long> GetLargestFile(string directory)
		{
            string fileName = null;
            long fileSize = -1;
            foreach (FileInfo file in new DirectoryInfo(directory).EnumerateFiles())
            {
                if (fileName == null || fileSize == -1)
                {
                    fileName = file.Name;
                    fileSize = file.Length;
                    continue;
                }
                if (file.Length > fileSize)
                {
                    fileName = file.Name;
                    fileSize = file.Length;
                }
            }
            return new Tuple<string, long>(fileName, fileSize);
		}

		// Get all files whose size is equal to the given value (in bytes) below the given directory
		public static IEnumerable<string> GetFilesOfSize(string directory, long size)
		{
            List<string> list = new List<string>();
            foreach (FileInfo file in new DirectoryInfo(directory).GetFiles())
            {
                if (file.Length == size)
                    list.Add(file.FullName);
            }

            return list;
		}
	}
}
