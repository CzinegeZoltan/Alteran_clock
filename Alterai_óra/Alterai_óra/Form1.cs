using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alterai_óra
{
    public partial class Form1 : Form
    {
        private const float DefaultFontSize = 36;
        private const int MinimumFormWidth = 382;
        private const int MinimumFormHeight = 289;
        private Timer timer;
        private bool colonVisible;

        public Form1()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            InitializeComponent();
            this.Load += Form1_Load;
            this.Resize += Form1_Resize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            lblTime.AutoSize = false;
            lblTime.Dock = DockStyle.Fill;
            lblTime.TextAlign = ContentAlignment.MiddleCenter;

            SetInitialFontSize();
            SetDoubleBuffered(this);
            SetDoubleBuffered(lblTime);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            colonVisible = DateTime.Now.Second % 2 == 0;
            lblTime.Text = GetFormattedTime();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            SetFontSize();
        }
        //betűméret -- fontsize
        private void SetInitialFontSize()
        {
            float fontSize = Math.Max(DefaultFontSize, Math.Min(this.Width, this.Height) / 3.9f);
            lblTime.Font = new Font(lblTime.Font.FontFamily, fontSize, FontStyle.Bold);
        }

        private void SetFontSize()
        {
            float fontSize = Math.Max(DefaultFontSize, Math.Min(this.Width, this.Height) / 3.9f);
            lblTime.Font = new Font(lblTime.Font.FontFamily, fontSize, FontStyle.Bold);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void SetDoubleBuffered(Control control)
        {
            control.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?.SetValue(control, true, null);
        }
        //kiírás formázás -- text format
        private string GetFormattedTime()
        {
            string timeText = DateTime.Now.ToString("HH:mm:ss\nyyyy.MM.dd");
            if (!colonVisible)
            {
                timeText = timeText.Replace(":", " ");
            }
            return timeText;
        }
    }
}
