using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skelet
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool CreateCaret(IntPtr hWnd, IntPtr hBmp, int w, int h);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetCaretPos(int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowCaret(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyCaret();

        string openedFile = "";
        bool saved = false;
        public Form1()
        {
            InitializeComponent();
            this.textBox1.GotFocus += OnFocus;
            this.textBox1.LostFocus += OnDefocus;
            this.GotFocus += FormGotFocus;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void OnFocus(object sender, EventArgs e)
        {
            CreateCaret(textBox1.Handle, IntPtr.Zero, 8, Int32.Parse(((textBox1.Font.Size - 2)*2).ToString()));
            SetCaretPos(2, 1);
            ShowCaret(textBox1.Handle);
            base.OnGotFocus(e);
        }

        private void OnDefocus(object sender, EventArgs e)
        {
            DestroyCaret();
            base.OnLostFocus(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void SaveFile()
        {
            if (openedFile == "")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = ".txt";
                sfd.Filter = "Text file(*.txt)|*.txt|Any(*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    openedFile = sfd.FileName;
                    saven();
                    saved = true;
                }
            }
            else
            {
                saven();
                saved = true;
            }
            SetTitle();
        }

        void saven()
        {
            using (StreamWriter sw = new StreamWriter(openedFile))
            {
                foreach (string line in textBox1.Lines)
                {
                    sw.WriteLine(line);
                }
            }
        }
        
        void OpenFile()
        {
            if (saved || textBox1.Text == "")
            {
                openen();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Your file has not been saved! Save?", "Unsaved", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    SaveFile();
                    openen();
                }
                else if (dr == DialogResult.No)
                {
                    openen();
                }
            }
        }

        void openen()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.Filter = "Text file(*.txt)|*.txt|Any(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = "";
                openedFile = ofd.FileName;
                using (StreamReader sr = new StreamReader(openedFile))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        textBox1.Text += line+Environment.NewLine;
                    }
                }
                saved = true;
            }
            SetTitle();
        }

        void SetTitle()
        {
            string title = "Skelet | ";
            if (openedFile != "")
            {
                title += openedFile;
            }
            if (!saved)
            {
                title += "*";
            }
            this.Text = title;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                SaveFile();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.N))
            {
                var info = new System.Diagnostics.ProcessStartInfo(Application.ExecutablePath);
                System.Diagnostics.Process.Start(info);
                return true;
            }
            else if (keyData == (Keys.Control | Keys.O))
            {
                OpenFile();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Back))
            {
                SendKeys.SendWait("^+{LEFT}{BACKSPACE}");
                return true;
            }
            else if (keyData == (Keys.Tab))
            {
                textBox1.SelectedText = "    ";
            }
            else if (keyData == (Keys.Alt | Keys.Q))
            {
                Application.Exit();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void FormGotFocus(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            saved = false;
            linesLabel.Text = "Lines: "+textBox1.Lines.Length.ToString();
            SetTitle();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox1.SelectAll();
                e.SuppressKeyPress = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saved || textBox1.Text == "")
            {
                e.Cancel = false;
            }
            else
            {
                DialogResult dr = MessageBox.Show("Your file has not been saved! Save?", "Unsaved", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    SaveFile();
                    e.Cancel = false;
                }
                else if (dr == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}