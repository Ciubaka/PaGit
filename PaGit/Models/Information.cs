using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaGit.Models
{
    public class Information
    {
        public int ID { get; set; }

        public string SHA256 { get; set; }

        public string SHA1 { get; set; }

        public string MD5 { get; set; }
        public string Nazwa { get; set; }
        public string Rozszerzenie { get; set; }
        public string Rozmiar { get; set; }
        public string DataOstatniegoDostepu { get; set; }
        public string Atrybuty { get; set; }
        public string Magic { get; set; }
        public string OrginalnaNazwa { get; set; }
        public string Jezyk { get; set; }
        public string CompanyName { get; set; }
        public string FileVersion { get; set; }
        public string VersionProduct { get; set; }
        public string ImportowaneBiblioteki { get; set; }

        [Display(Name = "TimeStamp")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
  

    }
}
