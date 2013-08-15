using RecipeApi.Models;
using RecipeData;
using RecipeModels;
using RecipeRepositories;
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
            this.data = data as CommentRepository;
        }

        public CommentsController()
        {
            this.data = new CommentRepository(new RecipeContext());
        }

        // GET api/comments
        [HttpGet]
        [ActionName("usercomments")]
        public IEnumerable<CommentsModel> GetCommentsFromUser(string sessionKey)
        {
            var UserRep =new UserRepository(new RecipeContext());
            var userId = UserRep.LoginUser(sessionKey);
            var comments = (this.data as CommentRepository).GetCommentsFromUser(userId);
            var convertComments = ConvertComments(comments);
            return convertComments;
        }


        // GET api/comments/5
        [HttpGet]
        [ActionName("recipecomments")]
        public IEnumerable<CommentsModel> GetCommentsFromRecipe(string sessionKey,int recipeId)
        {
            var comments = (this.data as CommentRepository).GetCommentsFromRecipe(recipeId);
            var convertComment = ConvertComments(comments);
            return convertComment;
        }

        // POST api/comments\
        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage AddComment(int recipeId,string sessionKey,[FromBody]Comment comment)
        {
            var UserRep =new UserRepository(new RecipeContext());
            var userId = UserRep.LoginUser(sessionKey);
            (this.data as CommentRepository).AddComment(userId,recipeId, comment);

            var message = this.Request.CreateResponse(HttpStatusCode.Created);
            message.Headers.Location = new Uri(this.Request.RequestUri + comment.CommentId.ToString(CultureInfo.InvariantCulture));
            return message;
        }

        // PUT api/comments/5
        //public void Put(int id, [FromBody]string model)
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
