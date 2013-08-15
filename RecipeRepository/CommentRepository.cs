using RecipeData;
using RecipeModels;
using RecipeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;

namespace RecipeRepositories
{
    public class CommentRepository :IRepository<Comment>
    {
        public CommentRepository(RecipeContext context)
        {
        }

        public Comment AddComment(int userId,int recipeId, Comment comment)
        {
            var context = new RecipeContext();
            using(context)
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == userId);
                var recipe = context.Recipies.FirstOrDefault(r => r.RecipeId == recipeId);
                if (user!=null && recipe!=null)
                {
                    comment.User = user;
                    comment.Recipe = recipe;
                    context.Comments.Add(comment);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Eigher user or recipe is null(not found)");
                }

                return comment;
            }
        }

        public IQueryable<Comment> GetCommentsFromUser(int userId)
        {
            var context = new RecipeContext();
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);
            var comments = (from c in user.Comments
                           select c).AsQueryable<Comment>();
            return comments;
        }

        public IQueryable<Comment> GetCommentsFromRecipe(int recipeId)
        {
            var context = new RecipeContext();
            var recipe = context.Recipies.FirstOrDefault(u => u.RecipeId == recipeId);
            var comments = (from c in recipe.Comments
                            select c).AsQueryable<Comment>();
            return comments;
        }


        public Comment Add(Comment item)
        {
            throw new NotImplementedException();
        }

        public Comment Update(int id, Comment item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Comment item)
        {
            throw new NotImplementedException();
        }

        public Comment Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> All()
        {
            throw new NotImplementedException();
        }
    }
}
