using RecipeApi.Models;
using RecipeData;
using RecipeModels;
using RecipeRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecipeApi.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly IRepository<Comment> data;

        public CommentsController(IRepository<Comment> data)
        {
            this.data = data;
        }

        public CommentsController()
        {
            this.data = new DbRepositoryEF<Comment>(new RecipeContext());
        }

        // GET api/comments
        public IEnumerable<CommentsModel> Get()
        {
            var comments = this.data.All();
            var convertComments = ConvertComments(comments);
            return convertComments;
        }


        // GET api/comments/5
        public CommentsModel Get(int id)
        {
            var comment = this.data.Get(id);
            var convertComment = ConvertComment(comment);
            return convertComment;
        }

        // POST api/comments
        public HttpResponseMessage Post([FromBody]Comment model)
        {
            this.data.Add(model);

            var message = this.Request.CreateResponse(HttpStatusCode.Created);
            message.Headers.Location = new Uri(this.Request.RequestUri + model.CommentId.ToString(CultureInfo.InvariantCulture));
            return message;
        }

        // PUT api/comments/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/comments/5
        public void Delete(int id)
        {
            this.data.Delete(id);
        }

        private IEnumerable<CommentsModel> ConvertComments(IEnumerable<Comment> comments)
        {
            
            IEnumerable<CommentsModel> commentModels = from c in comments
                                                       where c.User!=null && c.Recipe != null
                                                       select new CommentsModel
                                                       {
                                                           CommentId = c.CommentId,
                                                           CommnetTet = c.CommentText,
                                                           FromUser = c.User.UserName,
                                                           ForRecipe = c.Recipe.RecipeName
                                                       };
            return commentModels;
        }

        private CommentsModel ConvertComment(Comment comment)
        {
            CommentsModel likeModel = new CommentsModel()
            {
                CommentId = comment.CommentId,
                CommnetTet = comment.CommentText,
                FromUser = comment.User.UserName,
                ForRecipe = comment.Recipe.RecipeName
            };
            return likeModel;
        }
    }
}
