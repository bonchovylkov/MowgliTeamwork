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
            var convertLikes = ConvertLike(likes);
            return convertLikes;
        }

        // GET api/likes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/likes
        public void Post([FromBody]string value)
        {
        }

        // PUT api/likes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/likes/5
        public void Delete(int id)
        {
        }

        private IEnumerable<LikesModel> ConvertLike(IEnumerable<Like> likes)
        {
            IEnumerable<LikesModel> likeModels = from l in likes
                                                 select new LikesModel
                                                 {
                                                     LikeId = l.LikeId,
                                                     LikeStatus = l.LikeStatus,
                                                     FromUser = l.User.UserName,
                                                     ForRecipe = l.Recipe.Name
                                                 };
            return likeModels;
        }
    }
}
