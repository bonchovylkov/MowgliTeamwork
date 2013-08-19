using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecipeModels;
using RecipeData;
using RecipeApi.Models;
using RecipeRepositories;
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
        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage PostUser(UserModel user)
        {
            if (ModelState.IsValid)
            {
                User userPost = DeserializeUserFromModel(user);
                (this.data as UserRepository).CreateUser(userPost.UserName, userPost.Password);
                var userDb = (this.data as UserRepository).LoginUser(user.UserName, user.Password);
                var userModel = GetUserModelOne(userDb);

                var message = this.Request.CreateResponse(HttpStatusCode.Created, userModel);
                message.Headers.Location = new Uri(this.Request.RequestUri + userPost.UserId.ToString(CultureInfo.InvariantCulture));
                return message;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPost]
        [ActionName("login")]
        public HttpResponseMessage LoginUser(UserLoginModel userModel)
        {

            var user = (this.data as UserRepository).LoginUser(userModel.Username, userModel.Password);
            var loggedUser = new UserModel
            {
                UserName = userModel.Username,
                SessionKey = user.SessionKey
            };

            var message = this.Request.CreateResponse(HttpStatusCode.Created, loggedUser);
            message.Headers.Location = new Uri(this.Request.RequestUri + loggedUser.UserId.ToString(CultureInfo.InvariantCulture));
            return message;
        }

        [HttpGet]
        [ActionName("logout")]
        public HttpResponseMessage LogoutUser([FromUri]string sessionKey)
        {
            (this.data as UserRepository).LogoutUser(sessionKey);
            var message = this.Request.CreateResponse(HttpStatusCode.OK);
            return message;
        }

        private object GetUserModelOne(RecipeModels.User userDb)
        {
            UserModel user = new UserModel();
            user.UserId = userDb.UserId;
            user.UserName = userDb.UserName;
            user.SessionKey = userDb.SessionKey;

            return user;
        }

        private User DeserializeUserFromModel(UserModel model)
        {

            User user = new User()
            {
                UserId = model.UserId,
                UserName = model.UserName,
                Password = model.Password,
                Picture = model.Picture
            };

            return user;
        }

        private UserModelFull GetUserFull(RecipeModels.User user)
        {
            UserModelFull userModelFull = new UserModelFull
            {
                UserId = user.UserId,
                UserName = user.UserName,
                SessionKey = user.SessionKey,
                Recepies = from r in user.Recipes
                           select new RecepiesModel
                           {
                               FromUser = r.User.UserName,
                               PictureLink = r.PictureLink,
                               Products = r.Products,
                           },
                Likes = from l in user.Likes
                        select new LikesModel
                        {
                            FromUser = l.User.UserName,
                            ForRecipe = l.Recipe.RecipeName,
                            LikeStatus = l.LikeStatus
                        },
                Comments = from c in user.Comments
                           select new CommentsModel
                           {
                               FromUser = c.User.UserName,
                               ForRecipe = c.Recipe.RecipeName,
                               CommnetTet = c.CommentText
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
                                                   UserName = a.UserName,
                                                   RecepiesCount = a.Recipes.Count,
                                                   LikesCount = a.Likes.Count,
                                                   CommentsCount = a.Comments.Count,
                                                   SessionKey = a.SessionKey,
                                               };

            return userModels;
        }
    }
}
