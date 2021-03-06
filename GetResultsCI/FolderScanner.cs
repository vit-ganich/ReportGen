﻿using System;
using System.Collections.Generic;
using System.IO;

namespace GetResultsCI
{
    class FolderScanner
    {
        public static List<string> ScanFolder()
        {
            var filesInFolder = new List<string>();

            string resultsCIfolder = GetTheLastResultsFolder();

            if (resultsCIfolder == null)
            {
                return null;
            }
            Logger.Log.Info($"CI results folder: {resultsCIfolder}");

            var extension = string.Format($"*.{ConfigReader.FilesExtension}");

            Logger.Log.Info("Scanning the folder... Please, wait...");

            filesInFolder.AddRange(Directory.GetFiles(
                 resultsCIfolder, extension, SearchOption.AllDirectories));

            if(filesInFolder.Count == 0)
            {
                throw new Exception("The CI results folder is empty.");
            }
            else
            {
                Logger.Log.Info($"Folder {resultsCIfolder} was successfully scanned.");
            }
            return filesInFolder;
        }

        public static string GetTheLastResultsFolder()
        {
            string parentDir = ConfigReader.TestResultsDir;

            Logger.Log.Info($"Scanning the subfolders of the '{parentDir}' folder");

            if (!Directory.Exists(parentDir))
            {
                throw new DirectoryNotFoundException("Search directory doesn't exist.");
            }

            string dateFormat = ConfigReader.DateTimeFormat;

            int searchRange = 10; // days from today for search, if the results folder for today is absent

            Logger.Log.Info($"Searching the latest CI result folder for the last {searchRange} days from today...");

            for (int i = 0; i > -searchRange; i--)
            {
                try
                {
                    return Directory.GetDirectories(parentDir, DateTime.Now.AddDays(i).ToString(dateFormat))[0];
                }
                catch
                {
                    Logger.Log.Debug($"Folder for {DateTime.Now.AddDays(i).ToString(dateFormat)} not found.");
                }
            }
            throw new Exception("There is no matching subfolders in the search directory");
        }
    }
}
