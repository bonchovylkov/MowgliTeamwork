using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipeRepository;
using RecipeApi.Models;
using RecipeData;
using System.Data.Entity;
using RecipeApi.Controllers;
using RecipeModels;

namespace RecipeApi.App_Start
{
    public class DbDependencyResolver : IDependencyResolver
    {
        static IRepository<Recipe> recipesRepository =
            new DbRepositoryEF<Recipe>(new RecipeContext());
        static IRepository<Like> likesRepository =
            new DbRepositoryEF<Like>(new RecipeContext());
        static IRepository<User> usersRepository =
            new DbRepositoryEF<User>(new RecipeContext());
        static IRepository<Comment> commentsRepository =
            new DbRepositoryEF<Comment>(new RecipeContext());
        static IRepository<Step> stepsRepository =
            new DbRepositoryEF<Step>(new RecipeContext());

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(RecipesController))
            {
                return new RecipesController(recipesRepository);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
