using AnimationCollectionAPI.DAL.DBO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationCollectionAPI.DAL
{
    public class AnimationCollectionDbContext : DbContext
    {
        public AnimationCollectionDbContext(DbContextOptions<AnimationCollectionDbContext> options) : base(options)
        {
        }

        public DbSet<Animation> Animations { get; set; }
    }
}
