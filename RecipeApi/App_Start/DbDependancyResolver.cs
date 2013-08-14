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

namespace RecipeApi.App_Start
{
    public class DbDependencyResolver : IDependencyResolver
    {
        static IRepository<RecepiesModel> recipesRepository = 
            new DbRepositoryEF<RecepiesModel>(new RecipeContext());
        static IRepository<LikesModel> likesRepository =
            new DbRepositoryEF<LikesModel>(new RecipeContext());
        static IRepository<UserModel> usersRepository =
            new DbRepositoryEF<UserModel>(new RecipeContext());
        static IRepository<CommentsModel> commentsRepository =
            new DbRepositoryEF<CommentsModel>(new RecipeContext());
        static IRepository<StepsModel> stepsRepository =
            new DbRepositoryEF<StepsModel>(new RecipeContext());

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
