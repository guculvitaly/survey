using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class SurveyMessage
    {
        public int Id { get; set; }
        public List<CollectionQuestions> questions { get; set; }

    }

    public class CollectionQuestions
    {
        public string Questions { get; set; }
        public string QuestionTittle { get; set; }
    }
}
