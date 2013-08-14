using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeModels
{
    public class Step
    {
        public int StepId { get; set; }
        public string StepText { get; set; }
        public Recipe Recipe { get; set; }
    }
}
