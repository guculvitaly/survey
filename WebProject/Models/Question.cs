using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    //[Table("Questions")]
    public class Question
    {

        [Key]
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public string QuestionTitlte { get; set; }
        public string QuestionMessage { get; set; }
        public string QuestionAnswer { get; set; }                
        public int SurveyListID { get; set; }
        public   Survey Survey { get; set; }
       
        
    }

    public class QuestionToSurvey
    {

        QuestionToSurvey() {

        }

        public string QuestionTitlte { get; set; }
        public string QuestionMessage { get; set; }
    }
}
