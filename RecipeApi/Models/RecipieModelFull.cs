using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeApi.Models
{
    public class RecipiesModelFull:RecepiesModel
    {
        public RecipiesModelFull()
        {
            this.Users = new HashSet<UserModel>();
            this.Likes = new HashSet<LikesModel>();
            this.Comments=new HashSet<CommentsModel>();
        }
        public IEnumerable<UserModel> Users { get; set; }
        public IEnumerable<LikesModel> Likes { get; set; }
        public IEnumerable<CommentsModel> Comments { get; set; }
    }
}