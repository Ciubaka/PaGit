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
using System.Reflection;
//using Microsoft.SqlServer.Dts.Runtime;
using System.Runtime.InteropServices;
using System.Reflection.Metadata;
//using Mono.Cecil;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using PeNet;
using System.Text;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaGit.Controllers
{
    public class TenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public string Pliky()

        public IActionResult Pliky()
        {
            //sciezka do pobranych plikow
            var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "upload");

            //info o folderze
            var dirInfo = new DirectoryInfo(dirPath);
            FileInfo[] files = dirInfo.GetFiles();

            List<string> listaPlikow = Directory.EnumerateFiles(dirPath, "*.*").ToList();


            List<string> nazwy = new List<string>();
            List<string> rozszerzenia = new List<string>();
            List<string> rozmiary = new List<string>();
            List<string> datyOstatniegoDostepu = new List<string>();
            List<string> atrybuty = new List<string>();


            foreach (FileInfo file in files)
            {
                nazwy.Add(file.Name.ToString());
                rozszerzenia.Add(file.Extension.ToString());
                rozmiary.Add(file.Length.ToString());

                datyOstatniegoDostepu.Add(file.LastAccessTime.ToShortDateString());
                atrybuty.Add(file.Attributes.ToString());

            }


            List<string> shasha = new List<string>();
            List<string> md5md5 = new List<string>();
            List<string> sha256sha256 = new List<string>();

            List<string> magicBytes = new List<string>();

            List<string> OriginalFilename = new List<string>();
            List<string> InternalFilename = new List<string>();
            List<string> Language = new List<string>();
            List<string> CompanyName = new List<string>();
            List<string> FileVersion = new List<string>();
            List<string> versionsProduct = new List<string>();
            List<string> versionBuild = new List<string>();
            List<string> importLib = new List<string>();
            List<DateTime> timeStamp = new List<DateTime>();
            List<string> imphash = new List<string>();

            


            string output_penet = null;
            string inne = null;



            foreach (string plik in listaPlikow)
            {

                //sha1
                using (FileStream fop = System.IO.File.OpenRead(plik))
                {
                    shasha.Add(BitConverter.ToString(SHA1.Create().ComputeHash(fop)).Replace(" - ", "").ToLowerInvariant());
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

                //sha256
                using (FileStream filestream = System.IO.File.OpenRead(plik))
                {
                    SHA256 mySHA256 = SHA256Managed.Create();

                    filestream.Position = 0;

                    byte[] hashValue = mySHA256.ComputeHash(filestream);

                    sha256sha256.Add(BitConverter.ToString(hashValue).Replace("-", "").ToLowerInvariant());

                }


                //rozpoznawanie 
                using (BinaryReader reader = new BinaryReader(
                    new FileStream(Convert.ToString(plik), FileMode.Open, FileAccess.Read, FileShare.None)))
                {
                    reader.BaseStream.Position = 0x0;
                    byte[] data = reader.ReadBytes(0x04);
                    string data_as_hex = BitConverter.ToString(data);
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

                        case var someVal when new Regex(@"42-4D.*").IsMatch(someVal):
                            output = " => bmp";
                            break;

                        case var someVal when new Regex(@"4D-5A.*").IsMatch(someVal):

                            //var versionInfo = FileVersionInfo.GetVersionInfo(plik); //dodaaaC!
                            //OriginalFilename.Add( versionInfo.OriginalFilename);
                            //Language.Add(versionInfo.Language);
                            //CompanyName.Add(versionInfo.CompanyName);
                            //FileVersion.Add(versionInfo.FileVersion);
                            //versionProduct.Add(versionInfo.ProductVersion);
                            //versionBuild.Add(versionInfo.FileBuildPart.ToString());
                            //string CompanyName = versionInfo;


                            //var peFile = new PeFile(@"C:\Windows\WinSxS\msil_microsoft.workflow.compiler_31bf3856ad364e35_4.0.15671.0_none_2f5b1249df4df665\Microsoft.Workflow.Compiler.exe");
                            //foreach (var dll in peFile.ImportedFunctions.Select(funkcja => funkcja.DLL).Distinct())
                            //{
                            //    // tutaj wyświetl `dll'
                            //    ViewBag.dlll = dll;


                            //}
                   



                                reader.BaseStream.Position = 0x3C;
                            byte[] datas = reader.ReadBytes(0x04);

                            int i = BitConverter.ToInt32(datas, 0);
                            reader.BaseStream.Position = i;
                            byte[] myyyczek = reader.ReadBytes(0x04);
                            string data_as_he = BitConverter.ToString(myyyczek);

                            string wzor = data_as_he.Substring(0, 11);
                            switch (wzor)
                            {
                                case "50-45-00-00":
                                    output = "EXE => PE32";
                                    break;
                                default:
                                    output =  "EXE => DOS";
                                    break;
                            }



                            break;

                        case "null":

                            output = "file type is not matches with array";

                            break;



                    }



                    if (output != String.Empty)
                    {
                        magicBytes.Add(output);
                        ViewBag.out_put = "Typ pliku to: " + output;
                    }
                    else
                    {
                        ViewBag.out_put = "Dodaj plik z orginalnym rozszerzeniem!";

                    }

                }


                //importowane biblioteki


                PeFile p = null;

                try
                {
                    p = new PeFile(plik);
                if(p.IsEXE)
                {
                        output_penet = "isExe, ";
                        //czas z timestamp

                        const int peHeaderOffset = 60;
                        const int linkerTimestampOffset = 8;
                        var b = new byte[2048];
                        System.IO.FileStream s = null;
                        try
                        {
                            s = new FileStream(listaPlikow.First(), FileMode.Open, FileAccess.Read);
                            s.Read(b, 0, 2048);
                        }
                        finally
                        {
                            if (s != null)
                                s.Close();
                        }
                        var dt = new DateTime(1970, 1, 1, 0, 0, 0).
                        AddSeconds(BitConverter.ToInt32(b, BitConverter.ToInt32(b, peHeaderOffset) + linkerTimestampOffset));
                        timeStamp.Add(dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours));

                        if (p.Is64Bit)
                            {
                                output_penet += "PE64 ";
                            }
                            else if (p.Is32Bit)
                            {
                                output_penet += "PE32 ";
                            }
                }

                else if(p.IsDLL)
                {
                        output_penet += "isDLL ";
                    }
                else if (p.IsDriver)
                {
                        output_penet += "isDriver ";
                    }
                else
                {
                        output_penet += "";
                }

                    foreach (var dll in p.ImportedFunctions.Select(funkcja => funkcja.DLL).Distinct())
                    {
                        importLib.Add(dll);
                    }

                    imphash.Add(p.ImpHash);


                    //do oGARNIECIA!!
                    //var cos = p.ImageSectionHeaders.ToList();
                    //ViewBag.coss = cos;



                }
                catch(Exception e)
                {
                    ViewBag.exception = "WYJĄTEK W PENECIE Y+BYKU";
                }
                

                var versionInfo = FileVersionInfo.GetVersionInfo(plik); //dodaaaC!
                OriginalFilename.Add(versionInfo.OriginalFilename);
                InternalFilename.Add(versionInfo.InternalName);
                Language.Add(versionInfo.Language);
                CompanyName.Add(versionInfo.CompanyName);
                FileVersion.Add(versionInfo.FileVersion);
                versionsProduct.Add(versionInfo.ProductVersion);
                versionBuild.Add(versionInfo.FileBuildPart.ToString());
            }


            //czas z timestamp

            //const int peHeaderOffset = 60;


            ViewBag.outputcik = output_penet;
            ViewBag.inne = inne;
           



            ViewBag.name = nazwy;
            ViewBag.extension = rozszerzenia;
            ViewBag.fileSize = rozmiary;
            ViewBag.magic = magicBytes;
            ViewBag.dateOfCompilation = timeStamp;


            ViewBag.md5md5 = md5md5;
            ViewBag.sha256 = sha256sha256;
            ViewBag.sha1 = shasha;
            ViewBag.imphash = imphash;


            ViewBag.data = importLib;

            ViewBag.lastAccess = datyOstatniegoDostepu;
            ViewBag.attributes = atrybuty;
            ViewBag.orginalFilename = OriginalFilename;
            ViewBag.internalFilename = InternalFilename;
            ViewBag.orginalLanguage = Language;
            ViewBag.companyName = CompanyName;
            ViewBag.fileVersion = FileVersion;
            ViewBag.versionProduct = versionsProduct;
            ViewBag.versionBuild = versionBuild;


            foreach (FileInfo file in dirInfo.EnumerateFiles())
            {
                file.Delete();
            }

            return View();

        }


    }


    }

