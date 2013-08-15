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
using RecipeRepositories;
using System.Globalization;

namespace RecipeApi.Controllers
{
    public class StepsController : ApiController
    {
        private readonly IRepository<Step> data;

        public StepsController(IRepository<Step> data)
        {
            this.data = data as StepRepository;
        }

        public StepsController()
        {
            this.data = new StepRepository(new RecipeContext());
        }


        // GET api/recipiesteps
        [HttpGet]
        [ActionName("recipiesteps")]
        public IEnumerable<StepModel> GetStepsForRecipe(string sessionKey, int recipeId)
        {
            var steps = (this.data as StepRepository).GetSteps(recipeId);
            var convertSteps = ConvertStepsToStepsModel(steps);
            return convertSteps;
        }

        // GET api/steps/5
        public StepModel Get(int id)
        {
            var step = this.data.Get(id);
            var stepModel = ConvertStepToStepModel(step);
            return stepModel;
        }

        // POST api/steps
        [HttpGet]
        [ActionName("addStep")]
        public HttpResponseMessage Post(string sessionKey, int recipeId, [FromBody]Step step)
        {
            (this.data as StepRepository).AddStep(recipeId, step);

            var message = this.Request.CreateResponse(HttpStatusCode.Created);
            message.Headers.Location = new Uri(this.Request.RequestUri + step.StepId.ToString(CultureInfo.InvariantCulture));
            return message;
        }

        // PUT api/steps/5
        public void Put(int id, [FromBody]Step value)
        {
            if (id == value.StepId)
            {
                this.data.Update(id, value);
            }
        }

        // DELETE api/steps/5
        public void Delete(int id)
        {
            this.data.Delete(id);
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
