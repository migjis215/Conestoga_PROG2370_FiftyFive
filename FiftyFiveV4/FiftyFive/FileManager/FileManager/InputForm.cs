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

namespace FileManager
{
    public partial class InputForm : Form
    {
        private Score newScore;
        private int blocks;

        public Score NewScore { get => newScore; set => newScore = value; }

        public InputForm(int blocks)
        {
            InitializeComponent();

            this.blocks = blocks;
        }

        public InputForm()
        {
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            //newScore = new Score();
            //lblBlocks.Text = PlayerBlock.getEnabledBlocks()
            lblBlocks.Text = blocks.ToString();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            newScore = new Score();
            newScore.Name = txtName.Text;
            newScore.Blocks = blocks;

            List<Score> scores = Score.getRecords();
            scores.Add(newScore);
            Score.addRecord(scores);
            //Score.addRecord(Score.getRecords().Add(newScore));

            this.Close();
        }



        //private string extractRecord()
        //{
        //    record = "";

        //}
    }
}
