using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;


namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object convert;

        Results question = new Results();
        string correctAnswer;

        public MainWindow()
        {
         
            InitializeComponent();

            string url = @"https://opentdb.com/api.php?amount=1";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    question = JsonConvert.DeserializeObject<Results>(content);


                    foreach (var result in question.results)
                    {
                        questionTB.AppendText(Convert.ToString(result.question));
                        List<string> Answers = new List<string>();
                        Answers.Add(Convert.ToString(result.correct_answer));
                        Answers.Add(Convert.ToString(result.incorrect_answers[0]));
                        Answers.Add(Convert.ToString(result.incorrect_answers[1]));
                        Answers.Add(Convert.ToString(result.incorrect_answers[2]));

                        correctAnswer = result.correct_answer;
                        for (int i=0; i< Answers.Count; i++)
                        {
                            //Random rnd = new Random();
                            //int r = rnd.Next(Answers.Count);
                            AnswerLB.Items.Add(Answers[i]);
                            //AnswerLB.Items.Add(Answers[1]);
                            //AnswerLB.Items.Add(Answers[2]);
                            //AnswerLB.Items.Add(Answers[3]);
                        }
                    }
                  
                

                    //foreach (var result in question.results)
                    //{
                    //    List<string> incorrect = new List<string>();
                    //    incorrect.Add(Convert.ToString(result.incorrect_answers[0]));
                    //    incorrect.Add(Convert.ToString(result.incorrect_answers[1]));

                    //    incorrect.Add(Convert.ToString(result.incorrect_answers[2]));


                    //foreach (var item in Answers)
                    //{
                    //    AnswerLB.Items.Add(Answers);
                    //}
                    ////}

                }


            }
         
        }

        private void ChooseBt_Click(object sender, RoutedEventArgs e)
        {
            const int scoreCorrect = 10;
            int accumulator = 0;
            int strike = 0;
            int strikeAccum = 0;
            int questionsAsked = 0;
            //using (MainWindow answer = new MainWindow())
            //    answer.ShowDialog;
                do
                {
                    if (AnswerLB.SelectedItem == correctAnswer)
                    {
                        CorrectLB.Items.Add(scoreCorrect + accumulator);
                        questionsAsked++;
                    break;

                    }
                    else
                    {
                        StrikeLB.Items.Add(strike + 1);
                        strikeAccum++;
                        questionsAsked++;
                    
                        if (strikeAccum == 3)
                        {
                            MessageBox.Show($"Game Over... you hit three strikes. Your score is {accumulator}!");
                        }
                    break;
                    }
                } while (questionsAsked < 11 || strike < 3);

            MessageBox.Show($"Great job... you got through all 10 questions. Your end score is {accumulator}.");
            
            //string url = @"https://opentdb.com/api.php?amount=1";
            //using (HttpClient client = new HttpClient())
            //{
            //    HttpResponseMessage response = client.GetAsync(url).Result;
            //    if(response.IsSuccessStatusCode)
            //    {
            //        var content = response.Content.ReadAsStringAsync().Result;
            //        var question = JsonConvert.DeserializeObject<Results>(content);


            //        foreach (var result in question.results)
            //        {
            //            questionTB.AppendText(Convert.ToString(result.question));
            //            AnswerLB.Items.Add(Convert.ToString(result.correct_answer));
            //        }

            //        foreach (var result in question.results)
            //        {
            //            List<string> incorrect = new List<string>();
            //            incorrect.Add(Convert.ToString(result.incorrect_answer));

            //            foreach (var item in incorrect)
            //            {
            //                AnswerLB.Items.Add(incorrect);
            //            }
            //        }

            //    }


            //}
                
            
        }
    }
}
