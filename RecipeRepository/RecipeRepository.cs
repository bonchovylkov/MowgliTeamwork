using RecipeData;
using RecipeModels;
using RecipeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeDropbox;

namespace RecipeRepositories
{
    public class RecipeRepository : IRepository<Recipe>
    {
        private RecipeContext context;

        public RecipeRepository(RecipeContext context)
        {
            this.context = context;
        }

        public Recipe Add(Recipe item)
        {
            using (context)
            {
                
                context.Recipies.Add(item);
                context.SaveChanges();
            }

            return null;
        }

        public Recipe Update(int id, Recipe item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Recipe item)
        {
            throw new NotImplementedException();
        }

        public Recipe Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Recipe> All()
        {
            throw new NotImplementedException();
        }
    }
}
