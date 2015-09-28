using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Logical_SCH__ATESTAT___TRY_
{
    public partial class Form3 : Form
    {
        const int MF_BYPOSITION = 0x400;

        [DllImport("User32")]

        private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("User32")]

        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("User32")]

        private static extern int GetMenuItemCount(IntPtr hWnd);


        private Panel[] panouriVariabile = new Panel[20];
        
        private RadioButton[] radButonInt = new RadioButton[20];
        private RadioButton[] radButonFloat = new RadioButton[20];
        private RadioButton[] radButonDouble = new RadioButton[20];

        private CheckBox[] checkTablou = new CheckBox[20];

        private TextBox[] numeVariabila = new TextBox[20];
        private TextBox[] dimensiuneTablou = new TextBox[20];
        private TextBox[] elementeTablou = new TextBox[20];

        private int CONTOR = 0;


        generareSchema originalForm;
        
        public Form3(generareSchema _passedForm)
        {
            originalForm = _passedForm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mathBox.Checked)
            {
                originalForm.TextHeaders += "#include<cmath>\r\n";
 
            }
            if (charBox.Checked)
            {
                originalForm.TextHeaders += "#include<cstring>\r\n";
            }
            
            for (int i = 0; i < CONTOR; i++)
            {
                if (radButonInt[i].Checked == true && checkTablou[i].Checked == false)
                {
                    originalForm.textDeclarareVariabile += "int " + numeVariabila[i].Text + ";" + "\r\n";
                }

                else if (radButonInt[i].Checked == true && checkTablou[i].Checked == true)
                {
                    originalForm.textDeclarareVariabile += "int " + numeVariabila[i].Text;

                    for (int j = 1; j <= int.Parse(dimensiuneTablou[i].Text); j++)
                    {

                        originalForm.textDeclarareVariabile += "[" + elementeTablou[i].Text + "]";
                    
                    }

                    originalForm.textDeclarareVariabile += ";\r\n";
                }

                if (radButonFloat[i].Checked == true && checkTablou[i].Checked == false)
                {

                    originalForm.textDeclarareVariabile += "float " + numeVariabila[i].Text + ";" + "\r\n";
                
                }
                
                else if (radButonFloat[i].Checked == true && checkTablou[i].Checked == true)
                {
                    originalForm.textDeclarareVariabile += "float " + numeVariabila[i].Text;
                    
                    for (int j = 1; j <= int.Parse(dimensiuneTablou[i].Text); j++)
                    {
                       
                        originalForm.textDeclarareVariabile += "[" + elementeTablou[i].Text + "]";
                    
                    }
                    
                    originalForm.textDeclarareVariabile += ";\r\n";
                }

                if (radButonDouble[i].Checked == true && checkTablou[i].Checked == false)
                {
                    
                    originalForm.textDeclarareVariabile += "double " + numeVariabila[i].Text + ";" + "\r\n";
                
                }
                
                else if (radButonDouble[i].Checked == true && checkTablou[i].Checked == true)
                {
                    
                    originalForm.textDeclarareVariabile += "double " + numeVariabila[i].Text;
                    
                    for (int j = 1; j <= int.Parse(dimensiuneTablou[i].Text); j++)
                    {
                        
                        originalForm.textDeclarareVariabile += "[" + elementeTablou[i].Text + "]";
                    
                    }
                    
                    originalForm.textDeclarareVariabile += ";\r\n";
                
                }
            
            }
            
            //this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            generarePanouri();

            button2.Location = new Point(12, panouriVariabile[CONTOR].Location.Y + 22 + 3);

            generareNumeVariabila();

            generareButonInt();

            generareButonFloat();

            generareButonDouble();

            generareCheckTablou();

            creareEventClickCheckTablou();

            creareLabelDimensiune();

            generareDimensiuneTablou();
            
            creareLabelNrElemente();

            generareElementeTablou();

            
            this.Controls.Add(panouriVariabile[CONTOR]);

            CONTOR++;


        }

        private void generareElementeTablou()
        {
            elementeTablou[CONTOR] = new TextBox();

            elementeTablou[CONTOR].Parent = panouriVariabile[CONTOR];

            elementeTablou[CONTOR].Size = new Size(35, 20);

            elementeTablou[CONTOR].Location = new Point(815, 2);

            elementeTablou[CONTOR].Enabled = false;

            panouriVariabile[CONTOR].Controls.Add(elementeTablou[CONTOR]);
        }

        private void creareLabelNrElemente()
        {
            Label nr_el = new Label();

            nr_el.Parent = panouriVariabile[CONTOR];

            nr_el.Text = "Numar elemente:";

            nr_el.Size = new Size(114, 13);

            nr_el.Location = new Point(693, 5);
        }

        private void generareDimensiuneTablou()
        {
            dimensiuneTablou[CONTOR] = new TextBox();

            dimensiuneTablou[CONTOR].Parent = panouriVariabile[CONTOR];

            dimensiuneTablou[CONTOR].Size = new Size(35, 20);

            dimensiuneTablou[CONTOR].Location = new Point(650, 1);

            dimensiuneTablou[CONTOR].Enabled = false;

            panouriVariabile[CONTOR].Controls.Add(dimensiuneTablou[CONTOR]);
        }

        private void creareLabelDimensiune()
        {
            Label Dimensiune = new Label();

            Dimensiune.Text = "Dimensiune:";

            Dimensiune.Parent = panouriVariabile[CONTOR];

            Dimensiune.Size = new Size(87, 13);

            Dimensiune.Location = new Point(557, 4);
        }

        private void creareEventClickCheckTablou()
        {
            CheckBox CheckB_Tablou_EVENT = checkTablou[CONTOR];

            CheckB_Tablou_EVENT.Click += new System.EventHandler(Check_Tablou_event);
        }

        private void generareCheckTablou()
        {
            checkTablou[CONTOR] = new CheckBox();

            checkTablou[CONTOR].Parent = panouriVariabile[CONTOR];

            checkTablou[CONTOR].Size = new Size(71, 20);

            checkTablou[CONTOR].Location = new Point(487, 2);

            checkTablou[CONTOR].Text = "Tablou";

            panouriVariabile[CONTOR].Controls.Add(checkTablou[CONTOR]);
        }

        private void generareButonDouble()
        {
            radButonDouble[CONTOR] = new RadioButton();

            radButonDouble[CONTOR].Parent = panouriVariabile[CONTOR];

            radButonDouble[CONTOR].Size = new Size(180, 17);

            radButonDouble[CONTOR].Location = new Point(303, 2);

            radButonDouble[CONTOR].Text = "Real cu virgula mobila";

            panouriVariabile[CONTOR].Controls.Add(radButonDouble[CONTOR]);
        }

        private void generareButonFloat()
        {
            radButonFloat[CONTOR] = new RadioButton();

            radButonFloat[CONTOR].Parent = panouriVariabile[CONTOR];

            radButonFloat[CONTOR].Size = new Size(54, 17);

            radButonFloat[CONTOR].Location = new Point(242, 1);

            radButonFloat[CONTOR].Text = "Real";

            panouriVariabile[CONTOR].Controls.Add(radButonFloat[CONTOR]);
        }

        private void generareButonInt()
        {
            radButonInt[CONTOR] = new RadioButton();

            radButonInt[CONTOR].Parent = panouriVariabile[CONTOR];

            radButonInt[CONTOR].Size = new Size(69, 17);

            radButonInt[CONTOR].Location = new Point(173, 2);

            radButonInt[CONTOR].Text = "Intreg";

            panouriVariabile[CONTOR].Controls.Add(radButonInt[CONTOR]);
        }

        private void generareNumeVariabila()
        {
            numeVariabila[CONTOR] = new TextBox();

            numeVariabila[CONTOR].Parent = panouriVariabile[CONTOR];
            
            numeVariabila[CONTOR].Size = new Size(152, 20);
            
            numeVariabila[CONTOR].Location = new Point(4, 1);

            panouriVariabile[CONTOR].Controls.Add(numeVariabila[CONTOR]);
        }


        private void generarePanouri()
        {
            panouriVariabile[CONTOR] = new Panel();

            panouriVariabile[CONTOR].Parent = this;

            panouriVariabile[CONTOR].Size = new Size(863, 22);

            panouriVariabile[CONTOR].Location = new Point(12, CONTOR * 22 + 25);  // 25 este spatiul de sus + spatiul dintre panouri
        }

        private void Check_Tablou_event(object sender, EventArgs e)
        {
            elementeTablou[CONTOR - 1].Enabled = true;

            dimensiuneTablou[CONTOR - 1].Enabled = true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = (IntPtr)GetSystemMenu(this.Handle, false);

            int menuItemCount = GetMenuItemCount(hMenu);

            RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);
        }

        private void Închidere_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
