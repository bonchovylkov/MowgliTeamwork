using RecipeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeApi.Models
{
    public class LikesModelFull
    {
        public LikesModelFull()
        {
            User = new User();
            Recipe = new Recipe();
        }
        public int LikeId { get; set; }
        public string LikeStatus { get; set; }

        public User User { get; set; }
        public Recipe Recipe { get; set; }
    }
}