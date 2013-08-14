using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeApi.Models
{
    public class UserModelFull :UserModel
    {
        public UserModelFull()
        {
            this.Likes = new HashSet<LikesModel>();
            this.Comments = new HashSet<CommentsModel>();
            this.Recepies = new HashSet<RecepiesModel>();
        }
        public IEnumerable<LikesModel> Likes { get; set; }
        public IEnumerable<CommentsModel> Comments { get; set; }
        public IEnumerable<RecepiesModel> Recepies { get; set; }
    }
}