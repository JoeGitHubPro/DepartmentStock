using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DepartmentStock.SystemsApproaches.FileSystem
{
    public static class Paths
    {
        static Paths()
        {
            logFilePath = "Files/LoggingArea/Log.txt";         
           
            DatabaseBackupFilePath = "Files/DatabaseBackup/DepartmentStockDB.bak";
        }


        private static string _fullLogFilePath;
        public static string logFilePath
        {
            get { return _fullLogFilePath; }
            set { _fullLogFilePath = CurrentFullPath(value); }
        }
    

        private static string _databaseBackupFilePath;
        public static string DatabaseBackupFilePath 
        {
            get { return _databaseBackupFilePath; }
            set { _databaseBackupFilePath = CurrentFullPath(value); }
        }



        public static string CurrentFullPath(string FilePath) 
        {

           
            string CurrentServerPath =$"{HttpRuntime.AppDomainAppPath}{FilePath}";

            return CurrentServerPath;

        }
        
    }
}
