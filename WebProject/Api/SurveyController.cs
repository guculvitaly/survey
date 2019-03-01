using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebProject.Context;
using WebProject.Models;

namespace WebProject.Api
{
    [Route("api/[controller]")]
    //[Route("api/survey")]
    public class SurveyController : Controller
    {
        private readonly ApplicationContext _context;

        public SurveyController(ApplicationContext context)
        {
            _context = context;

           
        }


        /// <summary>
        /// Create survey
        /// </summary>
        /// <param name="model"></param>
        [Route("survey")]
        [HttpPost]
        public IActionResult CreateSurvey([FromBody]Survey survey)
        {
                
            if (survey == null)
            {
                return BadRequest();
            }

            var date = DateTime.Now.ToString("yyyy-MM-dd h:mm tt");

           
            survey.CreatedTime = date;


           var addTo =  _context.Surveys.Add(survey);
          
            _context.SaveChanges();


            return Ok(survey);
       
        }

        /// <summary>
        /// List all surveys
        /// </summary>
        /// <returns></returns>
        [Route("surveys")]
        [HttpGet]
        public IEnumerable<Survey> ListSurveys()
        {
            return _context.Surveys.ToList();
        }

        /// <summary>
        /// Get info for a single survey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("survey/{id}")]
        [HttpGet]
        public IActionResult GetInfoOfSingleSurvey(int id)
        {
            Survey model = _context.Surveys.FirstOrDefault(x => x.SurveyId == id);

            if (model == null)
            {
                return NotFound();
            }
            return new ObjectResult(model);
        }

        /// <summary>
        /// Delete Survey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("survey/{id}")]
        [HttpDelete]
        public IActionResult DeleteSurvey(int id)
        {
            Survey model = _context.Surveys.FirstOrDefault(x => x.SurveyId == id);

            if (model == null)
            {
                return NotFound();
            }
            _context.Surveys.Remove(model);
            _context.SaveChanges();
            return Ok(model);
        }

        /// <summary>
        /// Edit Survey
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("survey/{id}")]
        [HttpPut]
        public IActionResult EditSurvey( Survey model)
        {
            if (model == null)
            {
                return NotFound();
            }

            if (!_context.Surveys.Any(x => x.SurveyId == model.SurveyId))
            {
                return NotFound();
            }
            _context.Update(model);
            _context.SaveChanges();
            return Ok(model);
        }


    }
}
