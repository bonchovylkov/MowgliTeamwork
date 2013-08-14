using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecipeRepositories;
using RecipeModels;
using RecipeData;
using RecipeApi.Models;

namespace RecipeApi.Controllers
{
    public class StepsController : ApiController
    {
        IRepository<Step> stepRepository;

        public StepsController()
        {
            this.stepRepository = new DbRepositoryEF<Step>(new RecipeContext());
        }

        public StepsController(IRepository<Step> repository)
        {
            this.stepRepository = repository;
        }

        // GET api/steps
        public IEnumerable<StepModel> Get()
        {
            var allSteps = this.stepRepository.All();
            var allStepsModel = ConvertStepsToStepsModel(allSteps);
            return allStepsModel;
        }

        // GET api/steps/5
        public StepModel Get(int id)
        {
            var step = this.stepRepository.Get(id);
            var stepModel = ConvertStepToStepModel(step);
            return stepModel;
        }

        // POST api/steps
        public void Post([FromBody]Step value)
        {
            this.stepRepository.Add(value);
        }

        // PUT api/steps/5
        public void Put(int id, [FromBody]Step value)
        {
            if (id == value.StepId)
            {
                this.stepRepository.Update(id, value);
            }
        }

        // DELETE api/steps/5
        public void Delete(int id)
        {
            this.stepRepository.Delete(id);
        }

        private IEnumerable<StepModel> ConvertStepsToStepsModel(IQueryable<Step> allSteps)
        {
            var steps = (from s in allSteps
                        select new StepModel
                        {
                            StepId = s.StepId,
                            StepText = s.StepText,
                            ForRecipe = s.Recipe.RecipeName,
                        }).AsEnumerable();

            return steps;
        }

        private StepModel ConvertStepToStepModel(Step step)
        {
            var stepModel = new StepModel();
            stepModel.StepId = step.StepId;
            stepModel.StepText = step.StepText;
            stepModel.ForRecipe = step.Recipe.RecipeName;

            return stepModel;
        }
    }
}
