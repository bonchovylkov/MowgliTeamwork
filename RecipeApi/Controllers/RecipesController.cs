using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecipeRepositories;
using RecipeApi.Models;
using RecipeData;
using RecipeModels;
using System.IO;
using System.Web.Http.Cors;
using RecipeRepository;
using System.Globalization;
using RecipeDropbox;
using System.Text;
using System.Web;

namespace RecipeApi.Controllers
{
    public class RecipesController : ApiController
    {
        private readonly IRepository<Recipe> recipeRepository;

        public RecipesController()
        {
            this.recipeRepository = new RecipeRepositoryyy(new RecipeContext());
        }

        public RecipesController(IRepository<Recipe> repository)
        {
            this.recipeRepository = repository as RecipeRepositoryyy;
        }

        // GET api/recipes
        [HttpGet]
        [ActionName("getall")]
        public IEnumerable<RecepiesModel> GetAllRecipies(string sessionKey)
        {
            var allRecipes = (this.recipeRepository as RecipeRepositoryyy).GetAllRecipies();
            var allRecipesModel = ConvertRecipesToRecipesModel(allRecipes);
            return allRecipesModel.AsEnumerable();
        }

        [HttpGet]
        [ActionName("getbyuser")]
        public HttpResponseMessage GetRecipiesByUser(string sessionKey)
        {
            try{
            var UserRep = new UserRepository(new RecipeContext());
            var userId = UserRep.LoginUser(sessionKey);
            var recipiesByUser = (this.recipeRepository as RecipeRepositoryyy).GetRecipiesByUser(userId);
            var allRecipesModel = ConvertRecipesToRecipesModel(recipiesByUser);
            var message = this.Request.CreateResponse(HttpStatusCode.Created, allRecipesModel);
            return message;
            }
            catch (Exception ex)
            {
                var response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message + sessionKey);
                return response;
            }
        }

        [HttpPost]
        [ActionName("addrecipe")]
        public HttpResponseMessage AddRecipe(string sessionKey, [FromBody] RecepiesModel recipeModel)
        {
            try
            {
                var UserRep = new UserRepository(new RecipeContext());
                var userId = UserRep.LoginUser(sessionKey);
                var recipe = ConvertFromModelToDbRecipe(recipeModel);
                var recipeToReturn = (this.recipeRepository as RecipeRepositoryyy).AddRecipe(userId, recipe);
                RecipiesModelFull rep = ConverRecipeToRecipeModelFull(recipeToReturn);
                var message = this.Request.CreateResponse(HttpStatusCode.Created, rep);
                message.Headers.Location = new Uri(this.Request.RequestUri + recipeToReturn.RecipeId.ToString(CultureInfo.InvariantCulture));
                return message;
            }
            catch (Exception ex)
            {
                var response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        [HttpPost]
        [ActionName("upload")]
        public HttpResponseMessage UploadImage()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                List<string> imagesLinks = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(postedFile.InputStream))
                    {
                        fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                    }
                    var filePath = RecipeDropbox.RecipeDropboxStore.UploadToDropBox(fileData, postedFile.FileName);

                    imagesLinks.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, imagesLinks[0]);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }

        private Recipe ConvertFromModelToDbRecipe(RecepiesModel recipeModel)
        {
            Recipe rep = new Recipe()
            {
                RecipeName = recipeModel.RecipeName,
                Products = recipeModel.Products,
                // PictureLink = recipeModel.PictureLink
            };
            return rep;
        }


        private IQueryable<RecepiesModel> ConvertRecipesToRecipesModel(IQueryable<Recipe> allRecipes)
        {
            var recipes = (from r in allRecipes
                           select new RecepiesModel
                           {
                               RecipeId = r.RecipeId,
                               RecipeName = r.RecipeName,
                               FromUser = r.User.UserName,
                               PictureLink = r.PictureLink,
                               Products = r.Products
                           });
            return recipes;
        }

        private Recipe DeserializeRecipeFromModelFull(RecipiesModelFull model)
        {
            Recipe recipe = new Recipe
            {
                RecipeId = model.RecipeId,
                RecipeName = model.RecipeName,
                Products = model.Products,
                PictureLink = model.PictureLink,
            };

            if (model.Steps != null)
            {
                recipe.Steps = (ICollection<Step>)
                    (from s in model.Steps
                     select new Step
                     {
                         StepId = s.StepId,
                         StepText = s.StepText,
                     });
            }

            return recipe;
        }

        private RecipiesModelFull ConverRecipeToRecipeModelFull(Recipe recipe)
        {
            var recipeModel = new RecipiesModelFull();
            recipeModel.RecipeId = recipe.RecipeId;
            recipeModel.RecipeName = recipe.RecipeName;
            recipeModel.PictureLink = recipe.PictureLink;
            recipeModel.Products = recipe.Products;
            recipeModel.FromUser = recipe.User.UserName;
            if (recipe.Steps != null)
            {
                recipeModel.Steps = (from s in recipe.Steps
                                     select new StepModel
                                     {
                                         StepId = s.StepId,
                                         StepText = s.StepText,
                                         ForRecipe = s.Recipe.RecipeName,
                                     }).AsEnumerable();
            }
            recipeModel.User = new UserModel()
            {
                UserName = recipe.User.UserName,
                SessionKey = recipe.User.SessionKey
            };

            return recipeModel;
        }

        //private Recipe ConvertRecipeModelFullToRecipe(RecipiesModelFull recipeModel)
        //{
        //    Recipe recipe = new Recipe();
        //    recipe.RecipeName = recipeModel.RecipeName;
        //    recipe.Products = recipeModel.Products;
        //    recipe.PictureLink = recipeModel.PictureLink;
        //    recipe.User = new User();

        //    return null;
        //}
    }
}