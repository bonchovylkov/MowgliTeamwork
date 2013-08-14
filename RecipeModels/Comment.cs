using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeModels
{
   public class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }

        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
