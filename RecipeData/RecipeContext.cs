using RecipeModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeData
{
    public class RecipeContext : DbContext
    {
        public RecipeContext()
            : base("RecipeDb")
        {
        }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Recipe> Recipies { get; set; }

        public DbSet<Step> Steps { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

    }
}
