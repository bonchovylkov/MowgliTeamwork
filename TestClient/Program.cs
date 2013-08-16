using RecipeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeData;
using System.Data.Entity;
using RecipeData.Migrations;
using RecipeDropbox;

using Spring.Social.OAuth1;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using Spring.IO;
using System.Diagnostics;
using System.IO;

namespace TestClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<RecipeContext, Configuration>());

            //RecipeContext db = new RecipeContext();
            //Step step = new Step
            //{
            //    StepText = "test step 7",

            //};

            //var allSteps = db.Steps;

            //foreach (var s in allSteps)
            //{
            //    Console.WriteLine(s.StepText);
            //}

            //db.Steps.Add(step);
            //db.SaveChanges();

            
            //Console.WriteLine("test Dropbox");

            //string recipePictureDropBoxePath = RecipeDropboxStore.UploadToDropBox("../../springFlowers.jpg", "springFlowers.jpg");
            //Console.WriteLine(recipePictureDropBoxePath);
        
        }
    }
}
