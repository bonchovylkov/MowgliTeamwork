using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeModels
{
   public class Recipe
    {
       public Recipe()
       {
           this.Steps = new HashSet<Step>();
           this.Likes = new HashSet<Like>();
           this.Comments = new HashSet<Comment>();
       }
        public int RecipeId{ get; set; }
        public virtual User User { get; set; }
        public string PictureLink { get; set; }
        public string Products { get; set; }
        public string RecipeName { get; set; }

        public virtual ICollection<Step> Steps { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
       
    }
}
