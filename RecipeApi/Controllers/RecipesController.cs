using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecipeRepository;
using RecipeApi.Models;

namespace RecipeApi.Controllers
{
    public class RecipesController : ApiController
    {
        public RecipesController()
        {

        }

        public RecipesController(IRepository<RecepiesModel> repository)
        {

        }

        // GET api/recipes
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/recipes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/recipes
        public void Post([FromBody]string value)
        {
        }

        // PUT api/recipes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/recipes/5
        public void Delete(int id)
        {
        }
    }
}
