using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeApi.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int RecepiesCount { get; set; }
    }
}