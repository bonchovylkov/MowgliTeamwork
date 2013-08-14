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
        private const string DropboxAppKey = "yasr6seigaasi40";
        private const string DropboxAppSecret = "ukvg7gpncbtefgy";

        //private const string DropboxAppKey = "voqbdcv2l4s628w";
        //private const string DropboxAppSecret = "is2id7hf4evzwn4";

        private const string OAuthTokenFileName = "OAuthTokenFileName.txt";

        static void Main(string[] args)
        {
            //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<RecipeContext, Configuration>());

            //RecipeContext db = new RecipeContext();
            //Step step = new Step
            //{
            //    StepText="test step 5",

            //};

            //var allSteps = db.Steps;

            //foreach (var s in allSteps)
            //{
            //    Console.WriteLine(s.StepText);
            //}

            //db.Steps.Add(step);
            //db.SaveChanges();

            Console.WriteLine("test Dropbox");

            DropboxServiceProvider dropboxServiceProvider =
                new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

            // Authenticate the application (if not authenticated) and load the OAuth token
            if (!File.Exists(OAuthTokenFileName))
            {
                RecipeDropboxStore.AuthorizeAppOAuth(dropboxServiceProvider);
            }
            OAuthToken oauthAccessToken = RecipeDropboxStore.LoadOAuthToken(OAuthTokenFileName);

            // Login in Dropbox
            IDropbox dropbox = dropboxServiceProvider.GetApi(oauthAccessToken.Value, oauthAccessToken.Secret);

            // Display user name (from his profile)
            DropboxProfile profile = dropbox.GetUserProfileAsync().Result;
            Console.WriteLine("Hi " + profile.DisplayName + "!");

            // Create new folder
            string newFolderName = "New_Folder_" + DateTime.Now.Ticks;
            Entry createFolderEntry = dropbox.CreateFolderAsync(newFolderName).Result;
            Console.WriteLine("Created folder: {0}", createFolderEntry.Path);

            // Upload a file
            Entry uploadFileEntry = dropbox.UploadFileAsync(
                new FileResource("../../springFlowers.jpg"),
                "/" + newFolderName + "/springFlowers.jpg").Result;   
                //new FileResource("../../Program.cs"),
                //"/" + newFolderName + "/Program.cs").Result;
            Console.WriteLine("Uploaded a file: {0}", uploadFileEntry.Path);

            // Share a file
            DropboxLink sharedUrl = dropbox.GetShareableLinkAsync(uploadFileEntry.Path).Result;
            Process.Start(sharedUrl.Url);
        }
    }
}
