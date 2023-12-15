using System;
using System.Windows.Forms;
using System.Drawing;

namespace Alterai_óra
{
    public partial class Form1 : Form
    {
        private const float DefaultFontSize = 36;
        private const int MinimumFormWidth = 380;
        private const int MinimumFormHeight = 280;
        private const int SpacingBetweenLabels = 5; // Csökkentett érték a két label közötti térközre

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

            lblDate.AutoSize = false;
            lblDate.Dock = DockStyle.Bottom;
            lblDate.TextAlign = ContentAlignment.MiddleCenter;

            // A két label közötti térköz beállítása
            lblDate.Margin = new Padding(0, SpacingBetweenLabels, 0, 0);
            lblTime.Margin = new Padding(0, SpacingBetweenLabels, 0, 0);

            this.Controls.Add(lblTime);
            this.Controls.Add(lblDate);

            SetInitialFontSize();
            SetDoubleBuffered(this);
            SetDoubleBuffered(lblTime);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            colonVisible = DateTime.Now.Second % 2 == 0;
            lblTime.Text = GetFormattedTime();
            lblDate.Text = DateTime.Now.ToString("yyyy.MM.dd");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            SetFontSize();
            CheckWindowSize();
        }

        private void SetInitialFontSize()
        {
            float fontSize = Math.Max(DefaultFontSize, Math.Min(this.Width, this.Height) / 3.6f);
            lblTime.Font = new Font(lblTime.Font.FontFamily, fontSize, FontStyle.Bold);
            lblDate.Font = new Font(lblTime.Font.FontFamily, fontSize / 2, FontStyle.Bold);
        }

        private void SetFontSize()
        {
            float fontSize = Math.Max(DefaultFontSize, Math.Min(this.Width, this.Height) / 3.6f);
            lblTime.Font = new Font(lblTime.Font.FontFamily, fontSize, FontStyle.Bold);
            lblDate.Font = new Font(lblTime.Font.FontFamily, fontSize / 2, FontStyle.Bold);
        }

        private void SetDoubleBuffered(Control control)
        {
            control.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?.SetValue(control, true, null);
        }

        private void CheckWindowSize()
        {
            int formWidth = Math.Max(this.Width, MinimumFormWidth);
            int formHeight = Math.Max(this.Height, MinimumFormHeight);
            this.Size = new Size(formWidth, formHeight);
        }

        private string GetFormattedTime()
        {
            string timeText = DateTime.Now.ToString("HH:mm:ss");
            if (!colonVisible)
            {
                timeText = timeText.Replace(":", " ");
            }
            return timeText;
        }
    }
}
