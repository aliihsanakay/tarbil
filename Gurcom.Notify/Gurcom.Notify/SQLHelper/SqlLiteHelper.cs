using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gurcom.Notify.SQLHelper
{
  public  class SqlLiteHelper
    {
        public static string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public static string path = System.IO.Path.Combine(folderPath, "GurKopDb.db3");

        public static bool CreateTable<T>()
        {
            try
            {
                using (var connection = new SQLiteConnection(path))
                {                    
                    connection.CreateTable<T>();

                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                addLog(ex);

                return false;
            }
        }
        public static bool Insert<T>(T item)
        {
            try
            {
               if(!CreateTable<T>())                
                    addLog("Insert Create Table Error");
                

                using (var connection = new SQLiteConnection(path))
                {
                    connection.Insert(item);

                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                addLog(ex);

                return false;
            }
        }
        public static bool DeleteTable<T>()
        {
            try
            {
                if (!CreateTable<T>())
                    addLog("Insert Create Table Error");
                using (var connection = new SQLiteConnection(path))
                {
                    connection.DeleteAll<T>();
                   
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                addLog(ex);

                return false;
            }
        }
     
        public static bool DeleteRecord<T>(int id)
        {
            try
            {
                if (!CreateTable<T>())
                    addLog("Insert Create Table Error");
                using (var connection = new SQLiteConnection(path))
                {
                    connection.Delete<T>(id);

                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                addLog(ex);

                return false;
            }
        }
        public static T GetById<T>(int id) where T :  new()
        {
            try
            {
                if (!CreateTable<T>())
                    addLog("Insert Create Table Error");
                using (var connection = new SQLiteConnection(path))
                {
                    return connection.Find<T>(id);
                }
            }
            catch (SQLiteException ex)
            {

                addLog(ex);
                return new T();
            }
           
        }
        public static List<T> GetAll<T>() where T:new()
        {
            try
            {
                if (!CreateTable<T>())
                    addLog("Insert Create Table Error");
                using (var connection = new SQLiteConnection(path))
                {
                    return connection.Table<T>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                addLog(ex);

                return null;
            }
        }

        private static void addLog(SQLiteException ex)
        {
            //loglama yazılacak
        }
        private static void addLog(string ex)
        {
            //loglama yazılacak
        }
    }
}
