using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeModels
{
    public class User
    {
        public User()
        {
            this.Likes = new HashSet<Like>();
            this.Comments = new HashSet<Comment>();
            this.Recipes = new HashSet<Recipe>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
        public string SessionKey { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        
    }
}
