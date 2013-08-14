using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecipeModels;
using RecipeData;
using RecipeApi.Models;
using RecipeRepository;

namespace RecipeApi.Controllers
{
    public class UsersController : ApiController
    {

        private readonly IRepository<User> data;

        public UsersController(IRepository<User> data)
        {
            this.data = data;
        }

        public UsersController()
        {
            this.data = new DbRepositoryEF<User>(new RecipeContext());
        }


        public IQueryable<UserModel> Get()
        {
            IQueryable<User> users = this.data.All();
            var getUserModel = GetUserModel(users);
            return getUserModel;
        }

        //public UserModelFull Get(int id)
        //{
        //    User user = this.data.Get(id);
        //    UserModelFull getUser = GetUserFull(user);
        //    return getUser;
        //}

        //private UserModelFull GetUserFull(RecipeModels.User user)
        //{
        //    UserModelFull userModelFull = new UserModelFull
        //    {
        //        UserId = a.UserId,
        //        UserName = a.UserName,
        //        Recepies = from r in user.Recipes
        //                   select new RecepiesModel
        //                   {
        //                       FromUser = r.User.UserName,
        //                       PictureLink = r.PictureLink,
        //                       Products = r.Products,
        //                   },
        //        Likes = from l in user.Likes
        //                select new LikesModel {
        //                    FromUser = l.User.UserName,
        //                    ForRecipe = l.Recipe.
        //                }
        //    };
        //}


        private IQueryable<UserModel> GetUserModel(IQueryable<User> user)
        {
            IQueryable<UserModel> userModels = from a in user
                                                  select new UserModel
                                                  {
                                                      UserId = a.UserId,
                                                      UserName=a.UserName,
                                                      RecepiesCount=a.Recipes.Count,
                                                      LikesCount = a.Likes.Count,
                                                      CommentsCount = a.Comments.Count
                                                  };

            return userModels;
        }


    }
}
