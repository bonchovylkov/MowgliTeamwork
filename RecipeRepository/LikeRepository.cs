using RecipeData;
using RecipeModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeRepositories
{
    public class LikeRepository : IRepository<Like>
    {
        private readonly RecipeContext context;

        public LikeRepository(RecipeContext context)
        {
            this.context = context;
        }

        public Like AddLike(int userId, int recipeId, Like like)
        {
            using(context)
            {
                User user = context.Users.FirstOrDefault(x => x.UserId == userId);
                Recipe recipe = context.Recipies.FirstOrDefault(x => x.RecipeId == recipeId);

                if (user != null && recipe != null)
                {
                    like.User = user;
                    like.Recipe = recipe;
                    context.Likes.Add(like);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Either user or recipe is null.");
                }
            }
            return like;
        }

        public IQueryable<Like> GetLikesFromUser(int userId)
        {
            using (context)
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == userId);
                var likes = (from c in user.Likes
                                select c).AsQueryable<Like>();
                return likes;
            }
            
        }

        public IQueryable<Like> GetLikesForRecipe(int recipeId)
        {
            using (context)
            {
                var recipe = context.Recipies.FirstOrDefault(r => r.RecipeId == recipeId);
                var likes = (from c in recipe.Likes
                             select c).AsQueryable<Like>();
                return likes;
            }

        }

        public Like Add(Like like)
        {
            using(context)
            {
                context.Likes.Add(like);
                context.SaveChanges();
            }
            return like;
        }

        public Like Update(int id, Like item)
        {
            using(context)
            {
                
            }
            return item;
        }

        public void Delete(int id)
        {
            using(context)
            {
                Like like = context.Likes.Find(id);
                if (like != null)
                {
                    context.Likes.Remove(like);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(Like like)
        {
            using(context)
            {
                if (context.Likes.Contains(like))
                {
                    context.Likes.Remove(like);
                    context.SaveChanges();
                }
            }
        }

        public Like Get(int id)
        {
            Like like = context.Likes.Find(id);
            if (like != null)
            {
                return like;
            }
            return null;
        }

        public IQueryable<Like> All()
        {
            using(context)
            {
                return context.Likes;
            }
        }
    }
}
