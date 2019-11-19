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
                    var question = JsonConvert.DeserializeObject<Results>(content);


                    foreach (var result in question.results)
                    {
                        questionTB.AppendText(Convert.ToString(result.question));
                        AnswerLB.Items.Add(Convert.ToString(result.correct_answer));
                    }

                    foreach (var result in question.results)
                    {
                        List<string> incorrect = new List<string>();
                        incorrect.Add(Convert.ToString(result.incorrect_answer));

                        foreach (var item in incorrect)
                        {
                            AnswerLB.Items.Add(incorrect);
                        }
                    }

                }


            }
        }

        private void ChooseBt_Click(object sender, RoutedEventArgs e)
        {
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
