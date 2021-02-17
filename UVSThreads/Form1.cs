using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace UVSThreads
{
    public partial class Form1 : Form
    {

        Random rnd = new Random();
        private Thread InterFace;
        private Thread[] ThreadsPool;
        private bool Stop = true;
        Repository repository = new Repository();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StopButton.Enabled = false;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            int ThreadsNumber = comboBox1.SelectedIndex + 2;
            if (ThreadsNumber > 1 && ThreadsNumber < 16)
            {
                ThreadsPool = new Thread[ThreadsNumber];
                listBox1.Items.Clear();
                //MessageBox.Show(ThreadsPool.Length.ToString());
                Stop = false;
                //Paleidimas
                InterFace = new Thread(() => StartThreadsPool(ThreadsNumber));
                InterFace.Start();
                //Mygtukai
                comboBox1.Enabled = false;
                StartButton.Enabled = false;
                StopButton.Enabled = true;
            }

        }

        private void StartThreadsPool(int ThreadsNumber)
        {
            for (int i = 0; i < ThreadsNumber; i++)
            {
                Thread t = new Thread(() => GenerateRandomSymbolsLyneWithTime());
                t.Name = "No." + (i+1).ToString();
                ThreadsPool[i] = t;
                ThreadsPool[i].IsBackground = true;
            }

            for (int i = 0; i < ThreadsNumber; i++)
            {
                ThreadsPool[i].Start();
            }
        }

        private void  GenerateRandomSymbolsLyneWithTime()
        {
            while(Stop == false){
                int Time = rnd.Next(500, 2000);
                Thread.Sleep(Time);
                string ThreadID = Thread.CurrentThread.Name;
                //MessageBox.Show(ThreadID);
                AddToList(GenerateRandomSymbolsLyne(), ThreadID);
            }
        }

        private void AddToList(string GeneratesSymbols, string ThreadID)
        {

            this.Invoke((MethodInvoker)delegate
            {
                int ListCount = listBox1.Items.Count;
                if (ListCount >= 20)
                {
                    listBox1.Items.RemoveAt(0);
                }

                listBox1.Items.Add("ID: " + ThreadID + " | " + GeneratesSymbols);
                repository.AddNewValue(ThreadID, GeneratesSymbols);
            });
        }

        private string GenerateRandomSymbolsLyne()
        {
            string SymbolsLine = "";
            int SybolsLineLengh = rnd.Next(5, 10);
            for (int i = 0; i < SybolsLineLengh; i++)
            {
                SymbolsLine = SymbolsLine + GenerateSymbol(); // + " "; 
            }
            return SymbolsLine;
        }

        private string GenerateSymbol()
        {
            int symbolNumber = rnd.Next(35, 254);
            if (symbolNumber == 127)
            {
                symbolNumber = 33;
            }
            if (symbolNumber == 160)
            {
                symbolNumber = 33;
            }
            return ((char)symbolNumber).ToString();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop = true;

            for (int i = 0; i < ThreadsPool.Length; i++)
            {
                ThreadsPool[i].Abort();
            }
            //Mygtukai
            comboBox1.Enabled = true;
            StartButton.Enabled = true;
            StopButton.Enabled = false;
        }
    }
}
