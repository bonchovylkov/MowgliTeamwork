using RecipeData;
using RecipeModels;
using RecipeRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeRepositories
{
    public class StepRepository : IRepository<Step>
    {
        private readonly RecipeContext context;

        public StepRepository(RecipeContext context)
        {
            this.context = context;
        }

        public Step AddStep(int recipeId, Step step)
        {
            using(context)
            {
                Recipe recipe = context.Recipies.FirstOrDefault(r => r.RecipeId == recipeId);
                if (recipe != null)
                {
                    step.Recipe = recipe;
                    context.Steps.Add(step);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Recipe do not exist.");
                }
            }
            return step;
        }

        public IQueryable<Step> GetSteps(int recipeId)
        {
            using (context)
            {
                var recipe = context.Recipies.FirstOrDefault(r => r.RecipeId == recipeId);
                var steps = (from c in recipe.Steps
                             select c).AsQueryable<Step>();
                return steps;
            }
        }

        public Step Add(Step item)
        {
            throw new NotImplementedException();
        }

        public Step Update(int id, Step item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (context)
            {
                Step step = context.Steps.Find(id);
                if (step != null)
                {
                    context.Steps.Remove(step);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(Step step)
        {
            throw new NotImplementedException();
        }

        public Step Get(int id)
        {
            Step step = context.Steps.Find(id);
            if (step != null)
            {
                return step;
            }
            return null;
        }

        public IQueryable<Step> All()
        {
            using (context)
            {
                return context.Steps;
            }
        }
    }
}
