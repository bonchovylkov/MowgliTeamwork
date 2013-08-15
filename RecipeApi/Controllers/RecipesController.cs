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
using System.IO;

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
        public void Post([FromBody]Recipe model)
        {
            var recipe = this.recipeRepository.Add(model);
        }

        // PUT api/recipes/5
        public void Put(int id, [FromBody]RecipiesModelFull value)
        {
        }

        // DELETE api/recipes/5
        public void Delete(int id)
        {
        }

        private IEnumerable<RecepiesModel> ConvertRecipesToRecipesModel(IQueryable<Recipe> allRecipes)
        {
            var recipes = (from r in allRecipes
                           select new RecepiesModel
                           {
                               RecipeId = r.RecipeId,
                               RecipeName = r.RecipeName,
                               FromUser = r.User.UserName,
                               PictureLink = r.PictureLink,
                               Products = r.Products
                           }).AsEnumerable();
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
                Steps = (ICollection<Step>)
                    (from s in model.Steps
                    select new Step
                    {

                    }),
            };

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
            recipeModel.Steps = (from s in recipe.Steps
                                 select new StepModel
                                     {
                                         StepId = s.StepId,
                                         StepText = s.StepText,
                                         ForRecipe = s.Recipe.RecipeName,
                                     }).AsEnumerable();
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
