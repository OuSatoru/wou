using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wou
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int w = Size.Width;
            richTextBox1.Width = (w - 22) / 2;
            webBrowser1.Width = (w - 22) / 2;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            richTextBox1.Width = (Size.Width - 22) / 2;
            webBrowser1.Width = (Size.Width - 22) / 2;
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            int ori = richTextBox1.SelectionStart;
            richTextBox1.TextChanged -= richTextBox1_TextChanged;
            richTextBox1.Rtf = fmt.rtfmt(richTextBox1.Rtf);
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            //Enabled = true;
            richTextBox1.SelectionStart = ori;
            richTextBox1.SelectionFont = new Font("Consolas", (float)11.25, FontStyle.Regular);
            richTextBox1.SelectionColor = Color.Black;
            textBox1.Text = richTextBox1.Rtf;
            /*int ori = richTextBox1.SelectionStart;
            richTextBox1.Select(0, richTextBox1.TextLength);
            richTextBox1.SelectionFont = new Font("宋体", 10, FontStyle.Italic);
            richTextBox1.SelectionStart = ori;
            webBrowser1.DocumentText = html.head + 
                richTextBox1.Text + 
                html.end;*/
            
        }

        
    }
}
