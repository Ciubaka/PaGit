using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

        //public List<string> Pliky()
        public IActionResult Pliky()
        {
            string[] pliki = Directory.GetFiles(@"C:\Users\User\source\repos\PaGit\PaGit\upload", "*.*");
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
            return View(shasha);
        }
    



    }
}
