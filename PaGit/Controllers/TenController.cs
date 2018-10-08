using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaGit.Controllers
{
    public class TenController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //public string Pliky()
        
        public IActionResult Pliky()

        {
            string[] pliki = Directory.GetFiles(@"C:\Users\User\source\repos\PaGit\PaGit\upload", "*.*");

            var dir = new DirectoryInfo(@"C:\Users\User\source\repos\PaGit\PaGit\upload");
            FileInfo[] files = dir.GetFiles();

            List<string> listaPlikow = pliki.ToList();
            

            List<string> nazwy = new List<string>();
            List<string> rozszerzenia = new List<string>();
            List<string> rozmiary = new List<string>();
            List<string> datyOstatniegoDostepu = new List<string>();
            List<string> datyStworzeniaPliku = new List<string>();
            List<string> datyOstatniejModyfikacji = new List<string>();


            //var versInfo = FileVersionInfo.GetVersionInfo(files[0]);
            ////String fileVersion = versInfo.FileVersion;
            ////String productVersion = versInfo.ProductVersion;

            //ViewBag.wersja = fileVersion.ToString();
            //ViewBag.wersja2 = productVersion.ToString();



            foreach (FileInfo file in files)
            {
                nazwy.Add(file.Name.ToString());
                rozszerzenia.Add(file.Extension.ToString());
                rozmiary.Add(file.Length.ToString());

                datyOstatniegoDostepu.Add(file.LastAccessTime.ToShortDateString());
                datyStworzeniaPliku.Add(file.CreationTime.ToShortDateString());
                datyOstatniejModyfikacji.Add(file.LastAccessTime.ToShortDateString());
            }

            ViewBag.nazwa = "Nazwa pliku: " + nazwy.First();
            ViewBag.rozszerzenia = "Rozszerzenie: " + rozszerzenia.First();
            ViewBag.rozmiary = "Rozmiar w bajtach: " + rozmiary.First();
            ViewBag.datyOstatniegoDostepu = "Data ostatniego dotępu: " + datyOstatniegoDostepu.First();
            ViewBag.datyStworzeniaPliku = "Data stworzenia pliku: " + datyStworzeniaPliku.First();
            ViewBag.datyOstatniejModyfikacji = "Data ostatniej modyfikacji: " + datyOstatniejModyfikacji.First();




            List<string> shasha = new List<string>();
            List<string> md5md5 = new List<string>();

            

            foreach (string plik in listaPlikow)
            {
                //byte[] bytes = Encoding.ASCII.GetBytes(plik);

                //sha1
                using (FileStream fop = System.IO.File.OpenRead(plik))
                {
                    shasha.Add(BitConverter.ToString(SHA1.Create().ComputeHash(fop)).Replace("-", "").ToLowerInvariant());
                }

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

            //ViewBag.md5md5 = "MD5: " + md5md5.ToString();
            //ViewBag.shasha = "SHA-1: " + shasha.ToString();




            //rozpoznawanie 
            BinaryReader reader = new BinaryReader(new FileStream(Convert.ToString(listaPlikow.Last()), FileMode.Open, FileAccess.Read, FileShare.None));

            reader.BaseStream.Position = 0x0; // The offset you are reading the data from  

            byte[] data = reader.ReadBytes(0x10); // Read 16 bytes into an array  

            string data_as_hex = BitConverter.ToString(data);

            reader.Close();

            // substring to select first 11 characters from hexadecimal array  

            string my = data_as_hex.Substring(0, 11);

            string output = null;

            switch (my)

            {

                case "38-42-50-53":

                    output = " => psd";

                    break;

                case "25-50-44-46":

                    output = " => pdf";

                    break;

                case "49-49-2A-00":

                    output = " => tif";

                    break;

                case "4D-4D-00-2A":

                    output = " => tif";

                    break;

                case "FF-D8-FF-E0":
                        output = " => jpg";
                    break;


                case "47-49-46-38":
                    output = " => gif";
                    break;

                case "47-4B-53-4D":
                    output = " => gks";
                    break;


                case "49-49-4E-31":
                    output = " => nif";
                    break;


                case "56-49-45-57":
                    output = " => pm";
                    break;



                case "89-50-4E-47":
                    output = " => png";
                    break;


                case "59-A6-6A-95":
                    output = " => ras";
                    break;

                case "23-46-49-47":
                    output = " => fig";
                    break;

                case "50-4B-03-04":
                    output = " => zip";
                    break;
                  

                case "7F-45-4C-46":
                    output = " => elf";
                    break;


                case var someVal when new Regex(@"37-7A-BC-AF-27-1C.*").IsMatch(someVal):

                    output = " => 7z";
                    break;

                  



                   case var someVal when new Regex(@"52-61-72-21-1A-07-00.*").IsMatch(someVal):

                    output = " => rar";
                    break;

                   

                   case var someVal when new Regex(@"52-61-72-21-1A-07-01-00.*").IsMatch(someVal):

                    output = " => rar";
                    break;








                case var someVal when new Regex(@"42-4D.*").IsMatch(someVal):

                    output = " => bmp";
                    break;

                case var someVal when new Regex(@"4D-5A.*").IsMatch(someVal):

                    output = " => exe";
                    break;

                case "null":

                    output = "file type is not matches with array";

                    break;

                

            }

          

            if (output != String.Empty)
            {
                ViewBag.Message = "Plik w hexa: " + data_as_hex;
                ViewBag.out_put = "Typ pliku to: " + output;
            }
            else
            {
                ViewBag.out_put = "Dodaj plik z orginalnym rozszerzeniem!";
            }




            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }

            //foreach (FileInfo file in di.EnumerateFiles())
            //{
            //    file.Delete();
            //}


            return View(md5md5);
        }
    



    }
}
