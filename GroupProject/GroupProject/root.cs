using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject
{
    class root
    {
        public string question { get; set; }
        public int question_num { get; set; }
        public List<answers> Answers { get; set; }



        public override string ToString()
        {
            return $"{question}, {question_num}";
        }
    }
}
   
