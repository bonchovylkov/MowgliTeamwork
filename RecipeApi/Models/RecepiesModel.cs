using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeApi.Models
{
    public class RecepiesModel
    {
        public int RecipeId { get; set; }
        public string FromUser { get; set; }
        public string PictureLink { get; set; }
        public string Products { get; set; }

    }
}
