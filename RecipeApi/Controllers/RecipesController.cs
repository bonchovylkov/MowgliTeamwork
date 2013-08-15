﻿using System;
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
        public IEnumerable<RecepiesModel> GetRecipiesByUser(string sessionKey)
        {
            var UserRep = new UserRepository(new RecipeContext());
            var userId = UserRep.LoginUser(sessionKey);
            var recipiesByUser = (this.recipeRepository as RecipeRepositoryyy).GetRecipiesByUser(userId);
            var allRecipesModel = ConvertRecipesToRecipesModel(recipiesByUser);
            return allRecipesModel.AsEnumerable();
        }

        [HttpPost]
        [ActionName("addrecipe")]
        public HttpResponseMessage AddRecipe(string sessionKey, [FromBody] Recipe recipe)
        {
            var UserRep = new UserRepository(new RecipeContext());
            var userId = UserRep.LoginUser(sessionKey);
            var recipeToReturn = (this.recipeRepository as RecipeRepositoryyy).AddRecipe(userId, recipe);
            var message = this.Request.CreateResponse(HttpStatusCode.Created, recipeToReturn);
            message.Headers.Location = new Uri(this.Request.RequestUri + recipeToReturn.RecipeId.ToString(CultureInfo.InvariantCulture));
            return message;
        }
        //// GET api/recipes/5
        //public RecipiesModelFull Get(int id)
        //{
        //    var recipe = this.recipeRepository.Get(id);
        //    var recipeModel = ConverRecipeToRecipeModelFull(recipe);
        //    return recipeModel;
        //}

        //// POST api/recipes
        //[HttpPost]
        //public void Post([FromBody]RecipiesModelFull model)
        //{
        //    var recipe = DeserializeRecipeFromModelFull(model);
        //    this.recipeRepository.Add(recipe);
        //}

        //// PUT api/recipes/5
        //public void Put(int id, [FromBody]RecipiesModelFull value)
        //{
        //}

        //// DELETE api/recipes/5
        //public void Delete(int id)
        //{
        //}

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