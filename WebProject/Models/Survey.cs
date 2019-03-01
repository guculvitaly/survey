using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    
    public class Survey
    {
        
        [Key]
        public int? SurveyId { get; set; }
        public string UserNameCreator { get; set; }
        public string NameOfSurvey { get; set; }
        public string BodyTextSurvey { get; set; }
        public string CreatedTime { get; set; }

        
        public  IList<Question> Questions { get; set; }




        public Survey()
        {
            Questions = new List<Question>();
        }
    }
}
