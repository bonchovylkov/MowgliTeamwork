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
            this.data = data;
        }

        public LikesController()
        {
            this.data = new DbRepositoryEF<Like>(new RecipeContext());
        }

        // GET api/likes
        public IEnumerable<LikesModel> Get()
        {
            var likes = this.data.All();
            var convertLikes = ConvertLikes(likes);
            return convertLikes;
        }

        // GET api/likes/5
        public LikesModel Get(int id)
        {
            var like = this.data.Get(id);
            var convertLike = ConvertLike(like);
            return convertLike;
        }

        // POST api/likes
        public HttpResponseMessage Post([FromBody]Like model)
        {
            //Like like = DeserializeFromModel(model);
            this.data.Add(model);

            var message = this.Request.CreateResponse(HttpStatusCode.Created);
            message.Headers.Location = new Uri(this.Request.RequestUri + model.LikeId.ToString(CultureInfo.InvariantCulture));
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
