using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecipeRepository;
using RecipeApi.Models;
using RecipeData;
using RecipeModels;

namespace RecipeApi.Controllers
{
    public class RecipesController : ApiController
    {
        private readonly IRepository<Recipe> recipeRepository;

        public RecipesController()
        {
            this.recipeRepository = new DbRepositoryEF<Recipe>(new RecipeContext());
        }

        public RecipesController(IRepository<Recipe> repository)
        {
            this.recipeRepository = repository;
        }

        // GET api/recipes
        public IEnumerable<RecepiesModel> Get()
        {
            var allRecipes = this.recipeRepository.All();
            var allRecipesModel = ConvertRecipesToRecipesModel(allRecipes);
            return allRecipesModel.AsEnumerable();
        }

        // GET api/recipes/5
        public RecipiesModelFull Get(int id)
        {
            var recipe = this.recipeRepository.Get(id);
            var recipeModel = ConverRecipeToRecipeModelFull(recipe);
            return recipeModel;
        }

        // POST api/recipes
        public void Post([FromBody]Recipe value)
        {

        }

        // PUT api/recipes/5
        public void Put(int id, [FromBody]Recipe value)
        {
        }

        // DELETE api/recipes/5
        public void Delete(int id)
        {
        }

        private IEnumerable<RecepiesModel> ConvertRecipesToRecipesModel(IQueryable<Recipe> allRecipes)
        {
            throw new NotImplementedException();
        }

        private RecipiesModelFull ConverRecipeToRecipeModelFull(Recipe recipe)
        {
            var recipeModel = new RecepiesModel();
            recipeModel.RecipeId = recipe.RecipeId;
            recipeModel.RecipeName = recipe.RecipeName;
            recipeModel.PictureLink = recipe.PictureLink;
            recipeModel.Products = recipe.Products;
            recipeModel.FromUser = recipe.User.UserName;
            return null;
        }
    }
}
