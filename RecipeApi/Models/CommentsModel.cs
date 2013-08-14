using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeApi.Models
{
    public class CommentsModel
    {
        public int CommentId { get; set; }
        public string CommnetTet { get; set; }

        public string FromUser { get; set; }
        public string ForRecipe { get; set; }
    }
}

