using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecipeRepository;
using RecipeModels;
using RecipeData;
using RecipeApi.Models;
using System.Globalization;

namespace RecipeApi.Controllers
{
    public class LikesController : ApiController
    {
        private readonly IRepository<Like> data;

        public LikesController(IRepository<Like> data)
        {
            this.data = data as LikeRepository;
        }

        public LikesController()
        {
            this.data = new LikeRepository(new RecipeContext());
        }

        // GET api/userlikes
        [HttpGet]
        [ActionName("userlikes")]
        public IEnumerable<LikesModel> GetLikesFromUser(string sessionKey)
        {
            var userRep = new UserRepository(new RecipeContext());
            var userId = userRep.LoginUser(sessionKey);
            var likes = (this.data as LikeRepository).GetLikesFromUser(userId);
            var convertLikes = ConvertLikes(likes);
            return convertLikes;
        }

        // GET api/recipelikes
        [HttpGet]
        [ActionName("recipelikes")]
        public IEnumerable<LikesModel> GetLikesForRecipe(string sessionKey, int recipeId)
        {
            var likes = (this.data as LikeRepository).GetLikesForRecipe(recipeId);
            var convertLikes = ConvertLikes(likes);
            return convertLikes;
        }

        // POST api/addlike
        [HttpGet]
        [ActionName("addlike")]
        public HttpResponseMessage Post(string sessionKey, int recipeId, [FromBody]Like like)
        {
            //Like like = DeserializeFromModel(model);
            var userRep = new UserRepository(new RecipeContext());
            var userId = userRep.LoginUser(sessionKey);
            
            (this.data as LikeRepository).AddLike(userId, recipeId, like);

            var message = this.Request.CreateResponse(HttpStatusCode.Created);
            message.Headers.Location = new Uri(this.Request.RequestUri + like.LikeId.ToString(CultureInfo.InvariantCulture));
            return message;
        }

        // PUT api/likes/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/likes/5
        public void Delete(int id)
        {
            this.data.Delete(id);
        }

        private IEnumerable<LikesModel> ConvertLikes(IEnumerable<Like> likes)
        {
            IEnumerable<LikesModel> likeModels = from l in likes
                                                 select new LikesModel
                                                 {
                                                     LikeId = l.LikeId,
                                                     LikeStatus = l.LikeStatus,
                                                     FromUser = l.User.UserName,
                                                     ForRecipe = l.Recipe.RecipeName
                                                 };
            return likeModels;
        }

        private LikesModel ConvertLike(Like like)
        {
            LikesModel likeModel = new LikesModel()
            {
                LikeId = like.LikeId,
                LikeStatus = like.LikeStatus,
                FromUser = like.User.UserName,
                ForRecipe = like.Recipe.RecipeName
            };
            return likeModel;
        }

        //private Like DeserializeFromModel(LikesModelFull model)
        //{
        //    Like like = new Like()
        //    {
        //        LikeId = model.LikeId,
        //        LikeStatus = model.LikeStatus,
        //        User = model.User,
        //        Recipe = model.Recipe
        //    };
        //    return like;
        //}
    }
}
