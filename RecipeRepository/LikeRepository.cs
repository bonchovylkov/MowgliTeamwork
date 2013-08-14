using RecipeData;
using RecipeModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeRepository
{
    public class LikeRepository : IRepository<Like>
    {
        private readonly RecipeContext context;

        public LikeRepository(RecipeContext context)
        {
            this.context = context;
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
