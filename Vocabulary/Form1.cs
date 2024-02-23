using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Vocabulary
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer reTimer;
        int choice;
        int timeelap = 5;

        public Form1()
        {
            InitializeComponent();
            generate_vocab();
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        //methods to generate vocabs
        private void generate_vocab()
        {
            try
            {
                string[] voc = { };
                //get path of document folder and read vocab file
                string customFile = textBox1.Text;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string file = $"{path}\\{customFile}";
                string[] langfile = File.ReadAllLines(file);
                int max = langfile.Length; //set max to amount of lines in array

                //generate first set
                generateRandom(max);
                voc = langfile[choice].Split(':'); //split array at :
                langfile = langfile.Except(new string[] { langfile[choice] }).ToArray(); //re-create array without [choice]
                Vocablbl1.Text = voc[0];
                Englbl1.Text = voc[1];

                //generate second set
                generateRandom(max);
                voc = langfile[choice].Split(':'); //split array at :
                langfile = langfile.Except(new string[] { langfile[choice] }).ToArray(); //re-create array without [choice]
                Vocablbl2.Text = voc[0];
                Englbl2.Text = voc[1];

                //generate third set
                generateRandom(max);
                voc = langfile[choice].Split(':'); //split array at :
                langfile = langfile.Except(new string[] { langfile[choice] }).ToArray(); //re-create array without [choice]
                Vocablbl3.Text = voc[0];
                Englbl3.Text = voc[1];

                //generate fourth set
                generateRandom(max);
                voc = langfile[choice].Split(':'); //split array at :
                langfile = langfile.Except(new string[] { langfile[choice] }).ToArray(); //re-create array without [choice]
                Vocablbl4.Text = voc[0];
                Englbl4.Text = voc[1];

                //generate fifth set
                generateRandom(max);
                voc = langfile[choice].Split(':'); //split array at :
                langfile = langfile.Except(new string[] { langfile[choice] }).ToArray(); //re-create array without [choice]
                Vocablbl5.Text = voc[0];
                Englbl5.Text = voc[1];

                //generate sixth set
                generateRandom(max);
                voc = langfile[choice].Split(':'); //split array at :
                langfile = langfile.Except(new string[] { langfile[choice] }).ToArray(); //re-create array without [choice]
                Vocablbl6.Text = voc[0];
                Englbl6.Text = voc[1];

                //generate seventh set
                generateRandom(max);
                voc = langfile[choice].Split(':'); //split array at :
                langfile = langfile.Except(new string[] { langfile[choice] }).ToArray(); //re-create array without [choice]
                Vocablbl7.Text = voc[0];
                Englbl7.Text = voc[1];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(choice);
            }
        }

        //random selection
        private void generateRandom(int max)
        {
            Random rnd = new Random();
            choice = rnd.Next(0, max);
        }

        //save entered vocabulary in file on button
        private void button1_Click(object sender, EventArgs e)
        {
            //get path of document folder and read vocab file
            string customFile = textBox1.Text;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string file = $"{path}\\{customFile}";
            //jigsaw together vocab for saving
            String totalentry = $"{entry1.Text}:{entry2.Text}";
            //read .txt file to check if already present, save if not
            string[] langfile = File.ReadAllLines(file);
            if (langfile.Contains(totalentry))
            {
                MessageBox.Show("This Vocabulary is already present.");
            }
            else
            {
                File.AppendAllText(file, string.Format("{0}{1}", $"{totalentry}", Environment.NewLine));
            }
        }

        //refresh, disable btn and start cooldown on refresh click
        private void refrshbtn_Click(object sender, EventArgs e)
        {
            generate_vocab();
            refrshbtn.Enabled = false;
            refrh_timer();
            reTimer.Start();
            hideAnswer1.Visible = true;
            hideAnswer2.Visible = true;
            hideAnswer3.Visible = true;
            hideAnswer4.Visible = true;
            hideAnswer5.Visible = true;
            hideAnswer6.Visible = true;
            hideAnswer7.Visible = true;
        }

        //create 1s timer
        private void refrh_timer()
        {
            reTimer = new System.Timers.Timer(1000);
            reTimer.Elapsed += OnTimedEvent;
            reTimer.Enabled = true;
        }

        //change refresh button text on elapsed like a countdown
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            timeelap--;
            switch (timeelap)
            {
                case 5:
                    refrshbtn.Invoke(new MethodInvoker(() => refrshbtn.Text = "Refresh (5s)"));
                    break;
                case 4:
                    refrshbtn.Invoke(new MethodInvoker(() => refrshbtn.Text = "Refresh (4s)"));
                    break;
                case 3:
                    refrshbtn.Invoke(new MethodInvoker(() => refrshbtn.Text = "Refresh (3s)"));
                    break;
                case 2:
                    refrshbtn.Invoke(new MethodInvoker(() => refrshbtn.Text = "Refresh (2s)"));
                    break;
                case 1:
                    refrshbtn.Invoke(new MethodInvoker(() => refrshbtn.Text = "Refresh (1s)"));
                    break;
                case 0:
                    reTimer.Dispose();
                    refrshbtn.Invoke(new MethodInvoker(() => refrshbtn.Text = "Refresh"));
                    refrshbtn.Invoke(new MethodInvoker(() => refrshbtn.Enabled=true));
                    timeelap = 6;
                    break;
            }
        }

        private void onClick(Control c)
        {
            c.Visible = false;
        }

        private void hideAnswer_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            onClick(btn);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            generate_vocab();
        }
    }
}
