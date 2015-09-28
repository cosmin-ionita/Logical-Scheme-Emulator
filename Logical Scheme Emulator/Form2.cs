using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Logical_SCH__ATESTAT___TRY_
{
    public partial class Form2 : Form
    {
        generareSchema _initForm = new generareSchema();
        public Form2(generareSchema _passedForm)
        {
            _initForm = _passedForm;
            InitializeComponent();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            _initForm.textInterpretat = null;
            _initForm.textHeaders = null;
            _initForm.textDeclarareVariabile = null;
            _initForm.CONTOR_INDENT = 0;
        }

        

        private void deschidereCodeBlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream file = File.Create(@"D:\L_S_Generated_code.cpp");

            StreamWriter outStream = new StreamWriter(file);

            outStream.WriteLine(richTextBox1.Text);

            outStream.Flush();

            outStream.Close();

            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = @"C:\Program Files (x86)\CodeBlocks\codeblocks.exe";
            startInfo.Arguments = @"D:\L_S_Generated_code.cpp";

            Process.Start(startInfo);
        }

        private void salvareFisierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "*.cpp";
            saveFileDialog1.Filter = "Extensia de salvare " + "(*.cpp)|*.cpp";
            saveFileDialog1.Title = "L_S_Salvare fisier";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);

            }
        }
    }
}
