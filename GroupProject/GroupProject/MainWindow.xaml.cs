﻿using System;
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

        int questionsAsked = 0;
        int strikeAccum = 0;
        int accumulator = 0;
        const int scoreCorrect = 10;
        int strike = 1;


        public MainWindow()
        {
         
            InitializeComponent();

            string url = @"https://opentdb.com/api.php?amount=1&type=multiple";
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

                        //var rand  = new Random();
                        //var res = Answers.OrderBy(item => rand);

                        for (int i = 0; i < Answers.Count; i++)
                        {
                            Random rnd = new Random();
                            int r = rnd.Next(Answers.Count);
                            AnswerLB.Items.Add(Answers[i]);
                        }
                    }
                }


            }
         
        }



        private void ChooseBt_Click(object sender, RoutedEventArgs e)
        {
            

            do
                {
                if ((string)AnswerLB.SelectedValue == correctAnswer)
                    {
                        accumulator = scoreCorrect + accumulator;
                        CorrectLB.Items.Add(accumulator);
                        questionsAsked++;
                        //break;

                    }
                    else
                    {
                        StrikeLB.Items.Add(strike + strikeAccum);
                        strikeAccum++;
                        questionsAsked++;
                    
                        //if (strikeAccum == 3)
                        //{
                        //    MessageBox.Show($"Game Over... you hit three strikes. Your score is {accumulator}!");
                        //}
                        //break;
                    }
                
                questionTB.Text = "";
                    AnswerLB.Items.Clear();

                string url = @"https://opentdb.com/api.php?amount=1&type=multiple";
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
                            for (int i = 0; i < Answers.Count; i++)
                            {
                                //Random rnd = new Random();
                                //int r = rnd.Next(Answers.Count);
                                AnswerLB.Items.Add(Answers[i]);
                            }
                        }
                    }


                }
                break;

            } while (questionsAsked < 11 || strikeAccum < 3);

            if (questionsAsked == 10)
            {
                MessageBox.Show($"Great job... you got through all 10 questions. Your end score is {accumulator}.");
            }
            else if (strikeAccum == 3)
            {
                MessageBox.Show($"Game Over... you hit three strikes. Your score is {accumulator}!");
            }
        }
    }
}
