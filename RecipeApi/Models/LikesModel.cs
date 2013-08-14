using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeApi.Models
{
    public class LikesModel
    {
        public int LikeId { get; set; }
        public string LikeStatus { get; set; }

        public string FromUser { get; set; }
        public string ForRecipe { get; set; }
    }
}

