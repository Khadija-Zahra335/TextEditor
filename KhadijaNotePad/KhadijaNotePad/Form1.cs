using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
using System.Drawing.Printing;

namespace KhadijaNotePad
{
    public partial class Form1 : Form
    {
        int printLine = 0;
        public Form1()
        {
            InitializeComponent();
        }


        // New File
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtEditor.Modified)
            {
                DialogResult ask = MessageBox.Show("Do you want to save changes ?",
                    "New Document",
                    MessageBoxButtons.YesNoCancel);


                if (ask == DialogResult.Yes)
                {
                    saveFileDialog1.ShowDialog();
                    File.WriteAllText(saveFileDialog1.FileName, txtEditor.Text);
                    txtEditor.Clear();


                }
                else if (ask == DialogResult.No)
                {
                    txtEditor.Clear();

                }
            }
            else { txtEditor.Clear(); }

        }

        // Opening File
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtEditor.Modified)
            {
                DialogResult ask = MessageBox.Show("Do you want to save changes ?",
                    "New Document",
                    MessageBoxButtons.YesNoCancel);


                if (ask == DialogResult.Yes)
                {
                    saveFileDialog1.ShowDialog();
                    File.WriteAllText(saveFileDialog1.FileName, txtEditor.Text);
                    txtEditor.Clear();


                }
                else if (ask == DialogResult.Cancel)
                {
                    return;

                }

            }
            openFileDialog1.ShowDialog();
           
            txtEditor.Text = File.ReadAllText(openFileDialog1.FileName);

        }

        // Saving file 
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (File.Exists(saveFileDialog1.FileName))
            {
                DialogResult ask = MessageBox.Show("File exists . Replace ?",
                    "File Exists",
                    MessageBoxButtons.YesNo

                    );

                if (ask == DialogResult.Yes) {

                    File.WriteAllText(saveFileDialog1.FileName, txtEditor.Text);
                }
                else
                {

                    File.WriteAllText(saveFileDialog1.FileName, txtEditor.Text);
                }
            }
        }

        // Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtEditor.CanUndo)
                txtEditor.Undo();

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtEditor.SelectionLength > 0) { 
                txtEditor.Cut();
            
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtEditor.SelectionLength > 0)
            {
                txtEditor.Copy();

            }

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                txtEditor.Paste();

            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.SelectAll();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string textFind = Microsoft.VisualBasic.Interaction.InputBox("Enter Text to find");
            int index = txtEditor.Text.IndexOf(textFind);
            if (index >= 0)
            {
                txtEditor.SelectionStart = index;
                txtEditor.SelectionLength = textFind.Length;
                txtEditor.Focus();

            }
            else {
                MessageBox.Show("Text not found");
            
            }


        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            txtEditor.Font = fontDialog1.Font;
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            txtEditor.ForeColor = colorDialog1.Color;
        }

        private void textAllignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.TextAlign = HorizontalAlignment.Left;

        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.TextAlign = HorizontalAlignment.Center;


        }

        private void rightToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtEditor.TextAlign = HorizontalAlignment.Right;

        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            txtEditor.BackColor = colorDialog1.Color;

        }

        

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "Khadija Zahra Notepad\nVersion 1.0\n© 2026 Zahra Khadija",
        "About");

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font printFont = txtEditor.Font;
            float lineheight = printFont.GetHeight(e.Graphics);
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            string[] lines = txtEditor.Text.Split('\n');
            while (printLine < lines.Length)
            {
                y += lineheight;
                e.Graphics.DrawString(lines[printLine], printFont, Brushes.Black, x, y);
                printLine++;
                if (y + lineheight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;



                }
            }
            printLine = 0;

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }


        }
    }

}
