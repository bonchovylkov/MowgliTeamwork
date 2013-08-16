using System;
using System.IO;
using System.Diagnostics;

using Spring.Social.OAuth1;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using Spring.IO;

namespace RecipeDropbox
{
    public class RecipeDropboxStore
    {
        private const string DropboxAppKey = "avebpvpe2pr4o85";
        private const string DropboxAppSecret = "bz5ysp3dsw0xh6l";
        private const string OAuthTokenFileName = "OAuthTokenFileName.txt";

        public static string UploadToDropBox(byte[] fileBytes, string fileName)
        {
            DropboxServiceProvider dropboxServiceProvider =
                new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

            using (StreamWriter writer = new StreamWriter(OAuthTokenFileName))
            {
                writer.WriteLine("mpwnerxk5xb4p54b");
                writer.WriteLine("i1ehqrnox9o1cpt");
            }

            if (!File.Exists(OAuthTokenFileName))
            {
                AuthorizeAppOAuth(dropboxServiceProvider);
            }
            OAuthToken oauthAccessToken = LoadOAuthToken(OAuthTokenFileName);

            IDropbox dropbox = dropboxServiceProvider.GetApi(oauthAccessToken.Value, oauthAccessToken.Secret);
            DropboxProfile profile = dropbox.GetUserProfileAsync().Result;
            string newFolderName = "Recipe_" + DateTime.Now.Ticks;
            Entry createFolderEntry = dropbox.CreateFolderAsync(newFolderName).Result;

            Entry uploadFileEntry = dropbox.UploadFileAsync(
                new ByteArrayResource(fileBytes),
                "/" + newFolderName + "/" + fileName).Result;

            DropboxLink sharedUrl = dropbox.GetMediaLinkAsync(uploadFileEntry.Path).Result;
            //Process.Start(sharedUrl.Url);

            return sharedUrl.Url.ToString();
        }

        public static OAuthToken LoadOAuthToken(string oAuthTokenFileName)
        {
            string[] lines = File.ReadAllLines(oAuthTokenFileName);
            OAuthToken oauthAccessToken = new OAuthToken(lines[0], lines[1]);
            return oauthAccessToken;
        }

        public static void AuthorizeAppOAuth(DropboxServiceProvider dropboxServiceProvider)
        {
            OAuthToken oauthToken = dropboxServiceProvider.OAuthOperations.FetchRequestTokenAsync(null, null).Result;
            OAuth1Parameters parameters = new OAuth1Parameters();
            string authenticateUrl = dropboxServiceProvider.OAuthOperations.BuildAuthorizeUrl(
                oauthToken.Value, parameters);
            Process.Start(authenticateUrl);
            AuthorizedRequestToken requestToken = new AuthorizedRequestToken(oauthToken, null);
            OAuthToken oauthAccessToken =
                dropboxServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;
            string[] oauthData = new string[] { oauthAccessToken.Value, oauthAccessToken.Secret };
            File.WriteAllLines(OAuthTokenFileName, oauthData);
        }
    }
}
