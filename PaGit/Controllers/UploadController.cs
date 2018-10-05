//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.FileProviders;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PaGit.Models.Startowa;
using System;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography;
using System.Text;
using System.Linq;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaGit.Controllers
{
    public class UploadController : Controller
    {
        //private readonly IFileProvider fileProvider;

        //public UploadController(IFileProvider fileProvider)
        //{
        //    this.fileProvider = fileProvider;
        //}

        public IActionResult Index()
        {
            return View();
        }

        //public string UploadFiles()
        //{
        //    return "jest";
        //}


        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Content("files not selected");

            foreach (var file in files)
            {

                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "upload",
                        file.GetFilename());

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

            }
            return RedirectToAction("Files");
        }



        public List<string> Files()
        {
            string[] pliki = Directory.GetFiles(@"C:\Users\User\source\repos\PaGit\PaGit\upload", "*.*");

            //var names = new List<string>();

            //foreach (string file in pliki)
            //{
            //    names.Add(file);

            //}
            //Path.GetFileName(pliki[1]);

            for (int i = 0; i < pliki.Length; i++)
            {

            }

            DirectoryInfo directory = new DirectoryInfo(@"C:\Users\User\source\repos\PaGit\PaGit\upload");

            string po = directory.LastAccessTime.ToString();

           // FileInfo F = new FileInfo(pliki[0]);
            //string zoba = pliki[0];
            //byte[] bytes = Encoding.ASCII.GetBytes(zoba);

            List<string> shasha = new List<string>();
            List<string> md5md5 = new List<string>();

            List<string> listaPlikow = pliki.ToList();
            
            foreach (string plik in listaPlikow)
            {
                //byte[] bytes = Encoding.ASCII.GetBytes(plik);

                //sha1
                FileStream fop = System.IO.File.OpenRead(plik);
                shasha.Add(BitConverter.ToString(SHA1.Create().ComputeHash(fop)).Replace("-", "").ToLowerInvariant());

                //md5
                using (var md5 = MD5.Create())
                {
                    using (var stream = System.IO.File.OpenRead(plik))
                    {
                        var hash = md5.ComputeHash(stream);
                        md5md5.Add(BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
                    }
                }

            }


            //md5
            //using (var md5 = MD5.Create())
            //{
            //    using (var stream = System.IO.File.OpenRead(zoba))
            //    {
            //        var hash = md5.ComputeHash(stream);
            //        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            //    }
            //}
            //sha1

            //FileStream fop = System.IO.File.OpenRead(zoba);
            //string chksum = BitConverter.ToString(SHA1.Create().ComputeHash(fop));
            //return chksum;


            return md5md5; 



            //using (var md5 = MD5.Create())
            //{
            //    using (var stream = System.IO.File.OpenRead(zoba))
            //    {
            //        return Encoding.Default.GetString(md5.ComputeHash(stream));
            //    }
            //}
        }

        //string localDirectoryUpload = @"C:\Users\User\source\repos\PaGit\PaGit\upload";
        // string localPatternUpload = "*.*";
        // string[] pliki = Directory.GetFiles(@"C:\Users\User\source\repos\PaGit\PaGit\upload");
       // string[] pliki = Directory.GetFiles(@"C:\Users\User\source\repos\PaGit\PaGit\upload", "*.*");
        
       
      


        //    SftpClient client_sftp = new SftpClient("10.6.219.22", "Szymon", "qwerty");
        //    SshClient client_ssh= new SshClient("10.6.219.22", "Szymon", "qwerty");
        //    client_sftp.Connect();
        //    string localDirectoryUpload = @"C:\Users\szymo\Desktop\WebApplication1\WebApplication1\upload";
        //    string localDirectoryDownload = @"C:\Users\szymo\Desktop\WebApplication1\tmp\Scan1.txt";
        //    string localPatternUpload = "*.mp3";
        //    string uploadDirectory = "/C:/Users/Szymon/Desktop/Upload/";
        //    string downloadDirectory= "/C:/Users/Szymon/Desktop/Scan/Scan.txt";
        //    string[] filesUpload = Directory.GetFiles(localDirectoryUpload, localPatternUpload);
        //    foreach (string file in filesUpload)
        //    {
        //        using (Stream inputStream = new FileStream(file, FileMode.Open))
        //        {
        //            client_sftp.UploadFile(inputStream, uploadDirectory + Path.GetFileName(file));
        //        }
        //    }
        //    System.IO.DirectoryInfo di = new DirectoryInfo(localDirectoryUpload);

        //    foreach (FileInfo file in di.GetFiles())
        //    {
        //        file.Delete();
        //    }
        //    client_ssh.Connect();
        //    client_ssh.RunCommand("C:/Users/Szymon/Desktop/Scan.bat");
        //    using (Stream fileStream = System.IO.File.OpenWrite(localDirectoryDownload))
        //    {
        //        client_sftp.DownloadFile(downloadDirectory, fileStream);
        //    }

        //    return RedirectToAction("Files");
        //}
        //public string Files()
        //{

        //    string readtext = system.io.file.readalltext(@"c:\users\szymo\desktop\webapplication1\tmp\scan1.txt");
        //    string readtext2 = system.io.file.readalltext(@"c:\users\szymo\desktop\webapplication1\tmp\scan2.txt");
        //    return (readtext) + (readtext2);


        //}



    }
}
