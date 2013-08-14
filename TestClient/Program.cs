using RecipeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeData;
using System.Data.Entity;
using RecipeData.Migrations;
namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
              Database.SetInitializer(new MigrateDatabaseToLatestVersion<RecipeContext, Configuration>());

            RecipeContext db = new RecipeContext();
            Step step = new Step
            {
                StepText="test step",
            };
            db.Steps.Add(step);
            db.SaveChanges();
        }
    }
}
