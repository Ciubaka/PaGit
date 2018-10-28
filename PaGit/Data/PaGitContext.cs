using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PaGit.Models
{
    public class PaGitContext : DbContext
    {
        public PaGitContext (DbContextOptions<PaGitContext> options)
            : base(options)
        {
        }

        public DbSet<PaGit.Models.Information> Information { get; set; }
    }
}
