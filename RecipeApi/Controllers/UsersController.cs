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
using System.Globalization;

namespace RecipeApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IRepository<User> data;

        public UsersController(IRepository<User> data)
        {
            this.data = data as UserRepository;
        }

        public UsersController()
        {
            this.data = new UserRepository(new RecipeContext());
        }

        public IQueryable<UserModel> Get()
        {
            IQueryable<User> users = this.data.All();
            var getUserModel = GetUserModel(users);
            return getUserModel;
        }

        
        public UserModelFull Get(int id)
        {
            User user = this.data.Get(id);
            UserModelFull getUser = GetUserFull(user);
            return getUser;
        }

       

        // POST api/User
        public HttpResponseMessage PostUser(UserModel user)
        {
            if (ModelState.IsValid)
            {
                User userPost = DeserializeUserFromModel(user);
                (this.data as UserRepository).CreateUser(userPost.UserName,userPost.Password);

                var message = this.Request.CreateResponse(HttpStatusCode.Created);
                message.Headers.Location = new Uri(this.Request.RequestUri + userPost.UserId.ToString(CultureInfo.InvariantCulture));
                return message;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        private User DeserializeUserFromModel(UserModel model)
        {

            User user = new User()
            {
               UserId=model.UserId,
               UserName=model.UserName,
               Password=model.Password,
               Picture=model.Picture
            };

            return user;
        }


        private UserModelFull GetUserFull(RecipeModels.User user)
        {
            UserModelFull userModelFull = new UserModelFull
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Recepies = from r in user.Recipes
                           select new RecepiesModel
                           {
                               FromUser = r.User.UserName,
                               PictureLink = r.PictureLink,
                               Products = r.Products,
                           },
                Likes = from l in user.Likes
                        select new LikesModel {
                            FromUser = l.User.UserName,
                            ForRecipe = l.Recipe.RecipeName,
                            LikeStatus = l.LikeStatus
                        },
                Comments = from c in user.Comments
                           select new CommentsModel
                           { 
                                FromUser = c.User.UserName,
                                ForRecipe = c.Recipe.RecipeName,
                                CommnetTet= c.CommentText
                           }
            };

            return userModelFull;
        }

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
