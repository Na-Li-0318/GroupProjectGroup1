using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject
{
    class Results
    {
        public int response_code { get; set; }

        public List<Result> results { get; set; }

    }

    class Result
    {
        public string category { get; set; }

        public string type { get; set; }

        public string difficulty { get; set; }
        public string question { get; set; }
        public string correct_answer { get; set; }
        public string[] incorrect_answers { get; set; }

        //public override string ToString()
        //{
        //    return name;
        //}
    }
}
