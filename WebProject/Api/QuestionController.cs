﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Context;
using WebProject.Models;

namespace WebProject.Api
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly ApplicationContext _context;

        public QuestionController(ApplicationContext context)
        {
            
            _context = context;
        }

        /// <summary>
        /// Create question
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("question")]
        [HttpPost]
        public IActionResult CreateQuestion( Question model)
        {
            if (model == null)
            {
                return BadRequest();
            }

           var ef =   _context.Question.Add(model);
           
           _context.SaveChanges();
                      
                     

            return Ok(ef);
        }

        /// <summary>
        /// List of questions
        /// </summary>
        /// <returns></returns>
        [Route("questions")]
        [HttpGet]
        public IEnumerable<Question> ListQuestions()
        {
            return _context.Question.ToList();
        }

        /// <summary>
        /// Get info for a single question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("question/{id}")]
        [HttpGet]
        public IActionResult GetInfoOfSingleQuestion(int id)
        {
            Question model = _context.Question.FirstOrDefault(x => x.QuestionId == id);

            if (model == null)
            {
                return NotFound();
            }
            return new ObjectResult(model);
        }

        /// <summary>
        /// Edit question
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("question/{id}")]
        [HttpPut]
        public IActionResult EditQuestion(Question model)
        {
            if (model == null)
            {
                return NotFound();
            }

            if (!_context.Question.Any(x => x.QuestionId == model.QuestionId))
            {
                return NotFound();
            }
            _context.Update(model);
            _context.SaveChanges();
            return Ok(model);
        }

        /// <summary>
        /// Delete question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("question/{id}")]
        [HttpDelete]
        public IActionResult DeleteQuestion(int id)
        {
            Question model = _context.Question.FirstOrDefault(x => x.QuestionId == id);

            if (model == null)
            {
                return NotFound();
            }
            _context.Question.Remove(model);
            _context.SaveChanges();
            return Ok(model);
        }

        /// <summary>
        /// Add question to list of the questions in survey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("survey/{id}")]
        
        [HttpPost]
        public IActionResult AddQuestionToList([FromBody]QuestionToSurvey model, int id)
        {
          
            //find Survey by Id
            var survey = _context.Surveys.FirstOrDefault(x => x.SurveyId == id);
           
           
            if (survey == null)
            {
                return NotFound();
            }
            var ini = new Survey();


            //add new message to Survey
            ini.Questions.Add(new Question()
            {
                QuestionTitlte = model.QuestionTitlte,
                QuestionMessage = model.QuestionMessage

            });

           
            _context.Surveys.Include(t => t.Questions).GroupBy(x => x.SurveyId == id);

            _context.Surveys.AddRange(ini);
            _context.SaveChanges();
            return Ok(survey);
        }

        /// <summary>
        /// List all questions for a survey
        /// </summary>
        [Route("survey/{questionId}")]
        [HttpGet]
        public IActionResult ListAllQuestionsOfSurvay(int questionId)
        {

            var impl = _context.Surveys.Include(p => p.Questions).ToList().Where(t => t.SurveyId == questionId);

            if (impl == null)
            {
                return NotFound();
            }

            return new JsonResult(impl);
        }

        /// <summary>
        /// Remove questions from question list in survey
        /// </summary>
        /// <returns></returns>
        [Route("surveyquestions/{questionId}")]
        [HttpDelete]
        public IActionResult Remove_Questions_From_List_Survey(int questionId)
        {
            
            var er = _context.Surveys.FirstOrDefault();

            foreach (var m in er.Questions)
            {
                if (m.QuestionId == questionId)
                {
                    _context.Question.RemoveRange(m);
                    _context.SaveChanges();
                }
            }


            
            return new ObjectResult(er);
        }
    }
}