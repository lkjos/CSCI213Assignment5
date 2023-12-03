using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KjosAssignment5.Models;

namespace KjosAssignment5.Data
{
    public class KjosAssignment5Context : DbContext
    {
        public KjosAssignment5Context (DbContextOptions<KjosAssignment5Context> options)
            : base(options)
        {
        }

        public DbSet<KjosAssignment5.Models.Song> Song { get; set; } = default!;
    }
}
