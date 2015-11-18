using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace find_files
{

    public class Find
    {
        public static FileInfo f = new FileInfo("search_result.dat");
        public StreamWriter sw = f.CreateText();
        public void close_stream() { sw.Close(); }
        public void FindInDir(DirectoryInfo dir, string pattern)
        {
           try
            {
              
                foreach (var file in dir.GetFiles(pattern))
                {
                    sw.WriteLine(file.FullName);
                }
            }
           catch (UnauthorizedAccessException)
           {
           }

          
                DirectoryInfo[] subdir = dir.GetDirectories();
                int i;
                int l = subdir.Length;
                for (i = 1; i < l; i++)
                {
                    try
                    {
                        this.FindInDir(subdir[i], pattern);
                    }
                    catch (UnauthorizedAccessException)
                    {
                       
                        sw.WriteLine("Access error to " + subdir[i].Name+" folder");
                    }
                }
         

        }
        public void FindFiles(string dir, string pattern)
        {
            this.FindInDir(new DirectoryInfo(dir), pattern);
        }

    }
 
    class Program
    {
        static void Main(string[] args)
        {
           DriveInfo[] allDrives = DriveInfo.GetDrives();
           Find f = new Find();
           
           Console.WriteLine("Enter mask:");
           string mask = Console.ReadLine();       
          foreach (DriveInfo d in allDrives)
           {
               
               string aa=d.DriveType.ToString();
               if (aa == "Fixed")
               {
                   string a = d.Name;
                   DirectoryInfo b = new DirectoryInfo(a);
                   try
                   {
                       DirectoryInfo[] test = b.GetDirectories();
                   }
                   catch (UnauthorizedAccessException)
                   {
                       Console.Out.WriteLine("Access error to folder");
                       Console.ReadKey();
                    }

                   
                   f.FindFiles(a,"*"+mask);
               }

           }
          f.close_stream();
               Console.Out.WriteLine("Search is end....");
        }
    }
}
