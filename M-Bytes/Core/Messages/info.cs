using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_Bytes.Core.Messages
{
    public partial class info : Form
    {
        public string Label1Text
        {
            get { return guna2HtmlLabel1.Text; }
            set { guna2HtmlLabel1.Text = value; }
        }

        public string Label2Text
        {
            get { return guna2HtmlLabel2.Text; }
            set { guna2HtmlLabel2.Text = value; }
        }

        public info()
        {
            InitializeComponent();
        }

        private void info_Load(object sender, EventArgs e)
        {
            Visible = true;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            this.Location = new Point(screenWidth - this.Width, screenHeight - this.Height);
        }




        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2HtmlLabel1_Click_1(object sender, EventArgs e)
        {
            // ...
        }

        private void guna2HtmlLabel2_Click_1(object sender, EventArgs e)
        {
            // ...
        }
    }
}
