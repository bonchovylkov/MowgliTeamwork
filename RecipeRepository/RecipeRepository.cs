﻿using RecipeData;
using RecipeModels;
using RecipeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeDropbox;

namespace RecipeRepositories
{
    public class RecipeRepositoryyy : IRepository<Recipe>
    {
        

        public RecipeRepositoryyy(RecipeContext context)
        {
            
        }

        public IQueryable<Recipe> GetRecipiesByUser(int userId)
        {
            var context = new RecipeContext();
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user!=null)
            {
                return user.Recipes as IQueryable<Recipe>;
            }
            else
            {
                throw new ArgumentException("The user is null aperantly!");
            }

        }

        public IQueryable<Recipe> GetAllRecipies()
        {
            var context = new RecipeContext();
            return context.Recipies;
        }

        public Recipe AddRecipe(int userId, Recipe recipe)
        {
            var context = new RecipeContext();
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user!=null)
            {
                recipe.User = user;
                context.Recipies.Add(recipe);
                context.SaveChanges();
                return recipe;
            }
            else
            {
                throw new ArgumentException("The user is null aperantly!");
            }
        }



        public Recipe Add(Recipe item)
        {
            throw new NotImplementedException();
        }

        public Recipe Update(int id, Recipe item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Recipe item)
        {
            throw new NotImplementedException();
        }

        public Recipe Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Recipe> All()
        {
            throw new NotImplementedException();
        }
    }
}
