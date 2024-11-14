using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment7_1
{
    public partial class Form1 : Form
    {
        int questionAmount = 20;
        char[] correctAnswers = {'b', 'd', 'a', 'a', 'c',
                                'a', 'b', 'a', 'c', 'd',
                                'b', 'c', 'd', 'a', 'd',
                                'c', 'c', 'b', 'd', 'a'};

        Dictionary<string, char[]> studentAnswers = new Dictionary<string, char[]>();

        public Form1()
        {
            InitializeComponent();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "studentExamResults.txt");
            ExamResultFileRecord(path);
        }

        // read and record from student result text file
        // the name of the student and each answer start on new line
        private void ExamResultFileRecord(string filePath)
        {
            string line;
            int index = 0;
            string studentName = "";

            try
            {
                StreamReader readLine = new StreamReader(filePath);
                line = readLine.ReadLine();
                while (line != null)
                {
                    if (line.Length > 1)
                    {
                        studentName = line;
                        comboBox1.Items.Add(studentName);
                        studentAnswers[studentName] = new char[questionAmount];
                        index = 0;
                    } else
                    {
                        studentAnswers[studentName][index] = char.Parse(line);
                        index++;
                    }
                    line = readLine.ReadLine();
                }
                readLine.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.Message);
            }
        }

        // calculate results for a student
        private void EvaluateGrade(string studentName)
        {
            char[] currentStudentResults = studentAnswers[studentName];
            int correctAnswerAmount = 0;
            string incorrectAnswers = "";

            for (int i = 0; i < questionAmount; i++)
            {
                if (currentStudentResults[i] != correctAnswers[i])
                {
                    incorrectAnswers += (i + 1) + " ";
                } else
                {
                    correctAnswerAmount++;
                }
            }

            label2.Text = correctAnswerAmount >= 15 ? "Exam passed" : "Exam failed";
            label4.Text = correctAnswerAmount.ToString();
            label5.Text = (questionAmount - correctAnswerAmount).ToString();
            label9.Text = incorrectAnswers;
        }

        // exit program button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // selected student change
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Text = comboBox1.Text;
            EvaluateGrade(comboBox1.Text);
        }
    }
}
