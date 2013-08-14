using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeApi.Models
{
    public class StepModel
    {
        public int StepId { get; set; }
        public string StepText { get; set; }

        public string ForRecipe { get; set; }
    }
}