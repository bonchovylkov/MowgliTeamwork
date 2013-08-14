using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeModels
{
    public class Like
    {
        public int LikeId { get; set; }
        public string LikeStatus { get; set; }

        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }

    }
}
