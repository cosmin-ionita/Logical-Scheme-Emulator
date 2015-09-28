using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;                                     
using System.Threading;                                           
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;  
                        
namespace Logical_SCH__ATESTAT___TRY_
{
    public partial class generareSchema : Form
    {
        public generareSchema()
        {
            Form4 logo = new Form4();

            logo.Show();
            Thread.Sleep(2000);
            logo.Close();

            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            
        }

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        #region DECLARARI_VARIABILE
        // declarari variabile

        Form3 winDeclVar;

        TextBox[] textBoxControl = new TextBox[500];

        //_______________________________________________________________________________________
        PictureBox[] pbArray = new PictureBox[500];                // imaginile care apar pe forma 
        PictureBox[,] liniiInchidere = new PictureBox[30,30];      // liniile de inchidere ale structurilor de control
        UInt32 globalContor = 0, liniiInchidereContor = 0;       // contorul care acceseaza imaginile si editoarele precum si liniile de inchidere
        int i=0;
        public int CONTOR_INDENT = 0;
        //_______________________________________________________________________________________
        
        // flag-uri butoane
        
        bool button4Flag = false, button1Flag = false, button2Flag = false, button3Flag = false, 
             button5Flag = false, button6Flag = false, button7Flag = false, button8Flag = false;

       // bool ifDA = false, ifNU = false, ifDaNu = false;

        Point coordonateMouse;

        ContextMenuStrip meniuStergere = new ContextMenuStrip();
        ContextMenuStrip meniuDeclarareVariabile = new ContextMenuStrip();

        int AUX = 0;

        bool isEndif = false, isEndwhile = false;
        Int32 NR_EL_PE_DA, NR_EL_PE_NU, WH_INDEX = 0;
        int startPos = 0;

        public string textInterpretat = null;                        //  text-ul de interpretare
        public string[] spatiiIndentare = new String[30];
        public string textDeclarareVariabile = null;
        public string textHeaders = null;

        const int latimeControl = 138;
        const int inaltimeControl = 85;

        int controlX, controlY;     // coordonate de pozitionare ale controlului (care ies prin parametrii out ai metodei de determinare)
        int ifIndent;                // coordonata de aliniere IF (I-ul if-ului)

        public struct matriceCoordonateControl
        {
            public Int32 x, y;                  // coordonatele fiecarui element al matricei (pozitia din colt stanga sus)
            public bool activ;                  // verifica daca un element este sau nu activ (poate retine un control)
            public bool ocupat;

        };

        matriceCoordonateControl[,] matriceCoordonate = new matriceCoordonateControl[100,100];
        Int32 I, J;                            // variabile de parcurgere


        /*
         *         Structura definita mai jos reprezinta principala metoda de memorare a datelor care definesc un control.
         *         Este de fapt un arbore de structuri care are 5 campuri cu urmatoarea semnificatie:
         *          --- Campul ID reprezinta un numar unic asociat fiecarui control, astfel:
         *                  -- Start are ID-ul 1
         *                  -- Citeste are ID-ul 2
         *                  -- Scrie are ID-u 3
         *                  -- Conditie (if) are ID-ul 4
         *                  -- Prelucrare are ID-ul 5
         *                  -- Cat timp are ID-ul 6
         *                  -- Terminatorul de if si while are ID-ul 0
         *                  -- Stop are ID-ul -1
         **/
        private struct arborePrincipalControl
        {
            public Int32 ID;
            //public string controlText;
            //public Int32 textBoxId;
            public Int32 X, Y;
            public Int32 coefAdancime;

            public bool DA;
            public bool ifInchis;
            public bool whileInchis;
            public Int32 endifParentIndex;
        }
        arborePrincipalControl[] arborePrincipal = new arborePrincipalControl[100];

        private Int32 contorArbore;

        #endregion

        #region INITIALIZARE_MATRICE_DE_COORDONATE
        private void Initializare_Matrice_Principala()    // initializeaza campurile structurii de matrice
        {
            

            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    matriceCoordonate[i, j].x = latimeControl * i;
                    matriceCoordonate[i, j].y = inaltimeControl * j;
                    matriceCoordonate[i, j].activ = false;
                    matriceCoordonate[i, j].ocupat = false;
                }
            }
        }
        #endregion

        #region CALCULARE_COORDONATE_POZITIONARE_CONTROL
        /* Functia de mai jos calculeaza coordonatele la care controlul va fi pozitionat. In corpul acesteia se executa urmatoarele
         * operatii: 1 -- se determina coordonatele elementelor active din matrice
         *           2 -- se determina coordonatele mouse-ului, acolo unde a plasat utilizatorul controlul
         *           3 -- se determina distantele dintre pozitia mouse-ului si punctele elementelor active
         *           4 -- se determina distanta minima 
         *           5 -- se revine la coordonatele elementului corespunzatoare distantei minime
         *           6 -- se returneaza aceste coordonate prin parametrii out ai acestei functii
         *
         */

       
        private void DET_Coordonate_Control(out int XX, out int YY)
        {

            int[] X = new int[10];
            int[] Y = new int[10];
            decimal[] vDist = new decimal[10];
            
            int CNT = 0;
            
            XX = 0; YY = 0;

            //________*1____________


            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (matriceCoordonate[i, j].activ == true)
                    {
                        X[CNT] = matriceCoordonate[i, j].x;
                        Y[CNT] = matriceCoordonate[i, j].y;

                        CNT++;
                    }
                }
            }
            
            //___________*2 - _____________

            
            coordonateMouse = panel4.PointToClient(Cursor.Position);
            

            //___________*3_____________
            
            for (int i = 0; i < CNT; i++)
            {
                vDist[i] = (decimal)Math.Sqrt((X[i] - coordonateMouse.X) * (X[i] - coordonateMouse.X) + (Y[i] - coordonateMouse.Y) * (Y[i] - coordonateMouse.Y));
            }

            //___________*4______________

            decimal MIN = vDist[0];

            for (int i = 1; i < CNT; i++)
            {
                if (vDist[i] < MIN)
                {
                    MIN = vDist[i];

                }
            }

            //___________*5______________

            for (int i = 0; i < CNT; i++)
            {
                if (MIN == vDist[i])
                {
                    XX = X[i];
                    YY = Y[i];
                    break;
                }
            }
            
        }

        //_______________________________________________________________________________________
        #endregion

        #region VERIFICARE_CONTROL_IN_IF
       
        /*          Functie care verifica daca un anumit control se afla pe una din ramurile unui IF. 
         *          Functia parcurge arborele principal, si identifica primul IF neinchis (cu campul IF_INCHIS setat pe false)
         *          (daca exista un IF cu aceasta proprietate). Prin parametrul out IF_INDENT, se returneaza indentarea IF-ului
         *          de care apartine controlul ce urmeaza a fi pozitionat (X-ul din arbore, alias I-ul din matrice).
         * 
         */
        private bool CONTROL_AFLAT_IN_IF(out int IF_INDENT)
        {
            for(int i = contorArbore - 1; i >= 0; i--)
            {
                if(arborePrincipal[i].ID == 4 && arborePrincipal[i].ifInchis == false)
                {
                    IF_INDENT = arborePrincipal[i].X;
                    return true;
                }
            }
            IF_INDENT = 0;
            return false;
        }
        #endregion

        private bool existaElementPeDa(int i)
        {
            for (int k = i; arborePrincipal[k].ID != 4; k--)
            {
                if (arborePrincipal[k].DA == true)
                {
                    return true;
                }
            }

            textInterpretat += spatiiIndentare[CONTOR_INDENT] + ";";

            return false;
        }

        protected override void OnLoad(EventArgs e)                        // Pozitionare bara de scroll orizontala
        {
            base.OnLoad(e);
            this.AutoScroll = false;
            this.AutoScrollMinSize = new Size(panel4.Width, 0);
            this.AutoScrollPosition = new Point((this.AutoScrollMinSize.Width -
                                                 this.ClientSize.Width) / 2, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initializare_Matrice_Principala();
            contorArbore = 0;

            activareButoanePanou(start);
            dezactivareButoanePanou(citeste, scrie, prelucrare, interpretare, cat_timp, end, daca, stop);

            winDeclVar = new Form3(this);
            
       }

        private void activareButoanePanou(params Button[] listaButoane)
        {
            for (int i = 0; i < listaButoane.Length; i++)
            {
                listaButoane[i].Enabled = true;

                switch (listaButoane[i].Name)
                {
                    case "start": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.start_activ; break; }
                    case "stop": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.stop_activ; break; }
                    case "citeste": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.citeste_activ; break; }
                    case "scrie": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.scrie_activ; break; }
                    case "cat_timp": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.cat_timp_activ; break; }
                    case "daca": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.daca_activ; break; }
                    case "interpretare": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.interpretare_activ; break; }
                    case "prelucrare": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.prelucrare_activ; break; }
                    case "end": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.end_activ; break; }

                }

            }
        }
        
        private void dezactivareButoanePanou(params Button[] listaButoane)
        {
            for (int i = 0; i < listaButoane.Length; i++)
            {
                listaButoane[i].Enabled = false;

                switch (listaButoane[i].Name)
                {
                    case "start": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.start_inactiv; break; }
                    case "stop": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.stop_inactiv; break; }
                    case "citeste": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.citeste_inactiv; break; }
                    case "scrie": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.scrie_inactiv; break; }
                    case "cat_timp": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.cat_timp_inactiv; break; }
                    case "daca": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.daca_inactiv; break; }
                    case "interpretare": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.interpretare_inactiv; break; }
                    case "prelucrare": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.prelucrare_inactiv; break; }
                    case "end": { listaButoane[i].Image = Logical_SCH__ATESTAT___TRY_.Properties.Resources.end_inactiv; break; }

                }

            }
        }
        
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
             
        }

        #region PLASARE_CONTROL_PE_FORMA_(METODA_DE_CONTROL)
        //_____________Metoda principala de drag/drop  [este invocata de evenimentul de drop pe forma]______________________

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            start.FlatAppearance.BorderSize = 0;         // setez bordurile la 0 pentru ca atunci cand
            stop.FlatAppearance.BorderSize = 0;
            end.FlatAppearance.BorderSize = 0;         // am dat drag/drop pe forma, conturul albastru 
            citeste.FlatAppearance.BorderSize = 0;         // din toolbox sa dispara
            scrie.FlatAppearance.BorderSize = 0;
            cat_timp.FlatAppearance.BorderSize = 0;
            daca.FlatAppearance.BorderSize = 0;
            prelucrare.FlatAppearance.BorderSize = 0;
            
            
            

            pbArray[globalContor] = new PictureBox();              // instantiez obiectele de tip PictureBox si TextBox
            textBoxControl[globalContor] = new TextBox();


            TextBox TEXT_BOX_EVENT = textBoxControl[globalContor]; // creez un obiect neindexabil pentru ca nu pot invoca evenimentul ca element al matricei de picture-box-uri

            PictureBox PB_EVENT = pbArray[globalContor];

            pbArray[globalContor].Image = (Bitmap)e.Data.GetData(typeof(Bitmap));
            pbArray[globalContor].Size = new Size(pbArray[globalContor].Image.Width - 2, pbArray[globalContor].Image.Height - 5);
           
            if (button1Flag == true)          // flag buton start
            {

                activareButoanePanou(citeste, scrie, prelucrare, daca, cat_timp, stop);
                dezactivareButoanePanou(start, end, interpretare);

                meniuDeclarareVariabile.Items.Add("Declarare Variabile", null, DECLARARE_VARIABILE);
                pbArray[globalContor].ContextMenuStrip = meniuDeclarareVariabile;
                butonStart();

            }

            if (button2Flag == true)                           // flag buton stop
            {
                activareButoanePanou(interpretare);
                butonStop();
            }

            if (button3Flag == true)                           // flag buton endif/while_________________________________________________________________________________
            {
                AUX++;
                verificareEndifSauEndWhile();

                if (isEndif == true)                           // daca se refera la if, are loc urmatoarea chestie...
                {
                    isEndifManipulator();
                    
                }
                else if (isEndwhile == true)
                {
                    isEndWhileManipulator();
                }

                finisareEndif();

              }

            if (button4Flag == true)         // flag buton citeste
            {
                DET_Coordonate_Control(out controlX, out controlY);
                pbArray[globalContor].Location = new Point(controlX, controlY);

                TEXT_BOX_EVENT.Click += new System.EventHandler(TEXT_BOX_EVENT_MouseClick);
                TEXT_BOX_EVENT.TextChanged += new System.EventHandler(TEXT_BOX_EVENT_Text_Changed);

                textBoxControl[globalContor].Text = "Citeste";
                textBoxControl[globalContor].ForeColor = Color.DimGray;
                textBoxControl[globalContor].Parent = pbArray[globalContor];
                pbArray[globalContor].Controls.Add(textBoxControl[globalContor]);
                textBoxControl[globalContor].Location = new Point(textBoxControl[globalContor].Location.X + 19, textBoxControl[globalContor].Location.Y + 13);

                button4Flag = false;
                
                I = controlX / latimeControl;
                J = controlY / inaltimeControl;
               
                
                arborePrincipal[contorArbore].X = I;
                arborePrincipal[contorArbore].Y = J;

                matriceCoordonate[I, J].activ = false;
                matriceCoordonate[I, J].ocupat = true;

                J++;

                matriceCoordonate[I, J].activ = true;

                if (CONTROL_AFLAT_IN_IF(out ifIndent) == true)
                {
                    if (I < ifIndent)              
                    {
                        arborePrincipal[contorArbore].DA = true;
                                                                        
                    }
                    else arborePrincipal[contorArbore].DA = false;
                }

                arborePrincipal[contorArbore++].ID = 2;
                
            }
            if (button5Flag == true)         // flag buton scrie
            {
                DET_Coordonate_Control(out controlX, out controlY);
                pbArray[globalContor].Location = new Point(controlX, controlY);
                
                TEXT_BOX_EVENT.Click += new System.EventHandler(TEXT_BOX_EVENT_MouseClick);
                TEXT_BOX_EVENT.TextChanged += new System.EventHandler(TEXT_BOX_EVENT_Text_Changed);

                textBoxControl[globalContor].Text = "Scrie";
                textBoxControl[globalContor].ForeColor = Color.DimGray;
                textBoxControl[globalContor].Parent = pbArray[globalContor];
                pbArray[globalContor].Controls.Add(textBoxControl[globalContor]);
                textBoxControl[globalContor].Location = new Point(textBoxControl[globalContor].Location.X + 19, textBoxControl[globalContor].Location.Y + 13);

                button5Flag = false;

                I = controlX / latimeControl;
                J = controlY / inaltimeControl;
              

                arborePrincipal[contorArbore].X = I;
                arborePrincipal[contorArbore].Y = J;

                matriceCoordonate[I, J].activ = false;
                matriceCoordonate[I, J].ocupat = true;

                J++;

                matriceCoordonate[I, J].activ = true;

                if (CONTROL_AFLAT_IN_IF(out ifIndent) == true)
                {
                    if (I < ifIndent)
                    {
                        arborePrincipal[contorArbore].DA = true;

                    }
                    else arborePrincipal[contorArbore].DA = false;
                }

                arborePrincipal[contorArbore++].ID = 3;

            }
            if (button6Flag == true)         // flag buton while
            {
                

                DET_Coordonate_Control(out controlX, out controlY);
                pbArray[globalContor].Location = new Point(controlX, controlY);
                
                TEXT_BOX_EVENT.Click += new System.EventHandler(TEXT_BOX_EVENT_MouseClick);
                TEXT_BOX_EVENT.TextChanged += new System.EventHandler(TEXT_BOX_EVENT_Text_Changed);

                textBoxControl[globalContor].Text = "Cat timp";
                textBoxControl[globalContor].Multiline = true;
                textBoxControl[globalContor].Height = 21;
                textBoxControl[globalContor].Width = 65;
                textBoxControl[globalContor].ForeColor = Color.DimGray;
                textBoxControl[globalContor].Parent = pbArray[globalContor];
                pbArray[globalContor].Controls.Add(textBoxControl[globalContor]);
                textBoxControl[globalContor].Location = new Point(textBoxControl[globalContor].Location.X + 36, textBoxControl[globalContor].Location.Y + 14);

                button6Flag = false;

                activareButoanePanou(end);

                I = controlX / latimeControl;
                J = controlY / inaltimeControl;
               

                arborePrincipal[contorArbore].X = I;
                arborePrincipal[contorArbore].Y = J;

                matriceCoordonate[I, J].activ = false;
                matriceCoordonate[I, J].ocupat = true;

                J++;

                matriceCoordonate[I, J].activ = true;

                if (CONTROL_AFLAT_IN_IF(out ifIndent) == true)
                {
                    if (I < ifIndent)
                    {
                         arborePrincipal[contorArbore].DA = true;
                    }
                    else arborePrincipal[contorArbore].DA = false;
                }

                arborePrincipal[contorArbore++].ID = 6;
            }
            if (button7Flag == true)          // flag buton if
            {
               
                DET_Coordonate_Control(out controlX, out controlY);
                pbArray[globalContor].Location = new Point(controlX - 90, controlY);
                
                TEXT_BOX_EVENT.Click += new System.EventHandler(TEXT_BOX_EVENT_MouseClick);
                TEXT_BOX_EVENT.TextChanged += new System.EventHandler(TEXT_BOX_EVENT_Text_Changed);

                textBoxControl[globalContor].Text = "Conditie";
                textBoxControl[globalContor].Multiline = true;
                textBoxControl[globalContor].Height = 19;
                textBoxControl[globalContor].Width = 65;
                textBoxControl[globalContor].ForeColor = Color.DimGray;
                textBoxControl[globalContor].Parent = pbArray[globalContor];
                pbArray[globalContor].Controls.Add(textBoxControl[globalContor]);
                textBoxControl[globalContor].Location = new Point(textBoxControl[globalContor].Location.X + 120, textBoxControl[globalContor].Location.Y + 16);

                button7Flag = false;

                I = controlX / latimeControl;
                J = controlY / inaltimeControl;

                activareButoanePanou(end);

                arborePrincipal[contorArbore].X = I;
                arborePrincipal[contorArbore].Y = J;

                matriceCoordonate[I, J].activ = false;
                matriceCoordonate[I, J].ocupat = true;
                matriceCoordonate[I - 1, J].ocupat = true;
                matriceCoordonate[I + 1, J].ocupat = true;

                J++;
                
                matriceCoordonate[I - 1, J].activ = true;
                matriceCoordonate[I + 1, J].activ = true;

                

                if (CONTROL_AFLAT_IN_IF(out ifIndent) == true)
                {
                    if (I < ifIndent)
                    {

                        //Acum verificam daca pe NU avem un if. Daca avem, atunci schimbam if-ul principal si mutam toate controalele aflate pe DA cu o pozitie la stanga
                        bool IF_PE_NU = false;

                        for (int i = 0; i < contorArbore; i++)
                        {
                            if (arborePrincipal[i].ID == 4 && arborePrincipal[i].X - I  == 1)
                                for (int j = i + 1; j < contorArbore; j++)
                                    if (arborePrincipal[j].ID == 4 && arborePrincipal[j].X - arborePrincipal[i].X == 2)
                                    {
                                        IF_PE_NU = true;
                                        break;
                                    }
                            if (IF_PE_NU == true)
                                break;
                        }

                        if (IF_PE_NU == true)
                        {
                            arborePrincipal[contorArbore].DA = true;

                            matriceCoordonate[I + 1, J - 1].activ = false;
                            matriceCoordonate[I - 2, J - 1].activ = true;

                            matriceCoordonate[I - 1, J].activ = false;
                            matriceCoordonate[I + 1, J].activ = false;

                            matriceCoordonate[I - 2, J].activ = true;
                            matriceCoordonate[I, J].activ = true;

                            pbArray[globalContor].Location = new Point(matriceCoordonate[I - 1, J - 1].x - 90, matriceCoordonate[I - 1, J - 1].y);

                            int j_, GLOBAL_CONTOR_ALIAS;

                            j_ = (Int32)(J - 1);
                            GLOBAL_CONTOR_ALIAS = (Int32)globalContor;

                            for (int i = contorArbore - 1; arborePrincipal[i].ID != 4 || arborePrincipal[i].ifInchis != false; i--)   // aici se muta toate controalele aflate pe DA cu o pozitie 
                            {                                                                                                            // la stanga

                                GLOBAL_CONTOR_ALIAS--;
                                j_--;

                                if (arborePrincipal[i].DA == true && ifIndent - arborePrincipal[i].X == 1)
                                {
                                    pbArray[GLOBAL_CONTOR_ALIAS].Location = new Point(matriceCoordonate[I - 1, j_].x, matriceCoordonate[I - 1, j_].y);
                                    matriceCoordonate[I, j_].ocupat = false;
                                    matriceCoordonate[I - 1, j_].ocupat = true;
                                }

                            }

                           

                            GLOBAL_CONTOR_ALIAS--;
                            pbArray[GLOBAL_CONTOR_ALIAS].Image = (Bitmap)Logical_SCH__ATESTAT___TRY_.Properties.Resources.conditie_da_nu;
                            pbArray[GLOBAL_CONTOR_ALIAS].Size = new Size(pbArray[GLOBAL_CONTOR_ALIAS].Image.Width, pbArray[GLOBAL_CONTOR_ALIAS].Image.Height);
                            pbArray[GLOBAL_CONTOR_ALIAS].Location = new Point(pbArray[GLOBAL_CONTOR_ALIAS].Location.X - 122, pbArray[GLOBAL_CONTOR_ALIAS].Location.Y);
                            textBoxControl[GLOBAL_CONTOR_ALIAS].Location = new Point(textBoxControl[GLOBAL_CONTOR_ALIAS].Location.X + 120, textBoxControl[GLOBAL_CONTOR_ALIAS].Location.Y);
                            arborePrincipal[contorArbore].X = I - 1;
                        }
                        else
                        {
                            //ifDA = true;

                            arborePrincipal[contorArbore].DA = true;

                            matriceCoordonate[I + 1, J - 1].ocupat = false;
                            matriceCoordonate[I - 2, J - 1].ocupat = true;

                            matriceCoordonate[I - 1, J].activ = false;
                            matriceCoordonate[I + 1, J].activ = false;

                            matriceCoordonate[I - 2, J].activ = true;
                            matriceCoordonate[I, J].activ = true;

                            pbArray[globalContor].Location = new Point(matriceCoordonate[I - 1, J - 1].x - 90, matriceCoordonate[I - 1, J - 1].y);

                            int j_, GLOBAL_CONTOR_ALIAS;

                            j_ = (Int32)(J - 1);
                            GLOBAL_CONTOR_ALIAS = (Int32)globalContor;

                            for (int i = contorArbore - 1; arborePrincipal[i].ID != 4; i--)   // aici se muta toate controalele aflate pe DA cu o pozitie 
                            {                                                                   // la stanga

                                GLOBAL_CONTOR_ALIAS--;
                                j_--;

                                if (arborePrincipal[i].DA == true)
                                {
                                    pbArray[GLOBAL_CONTOR_ALIAS].Location = new Point(matriceCoordonate[I - 1, j_].x, matriceCoordonate[I - 1, j_].y);
                                    matriceCoordonate[I, j_].ocupat = false;
                                    matriceCoordonate[I - 1, j_].ocupat = true;

                                }
                            }

                            

                            GLOBAL_CONTOR_ALIAS--;
                            pbArray[GLOBAL_CONTOR_ALIAS].Image = (Bitmap)Logical_SCH__ATESTAT___TRY_.Properties.Resources.conditie_da;
                            pbArray[GLOBAL_CONTOR_ALIAS].Size = new Size(pbArray[GLOBAL_CONTOR_ALIAS].Image.Width, pbArray[GLOBAL_CONTOR_ALIAS].Image.Height);
                            pbArray[GLOBAL_CONTOR_ALIAS].Location = new Point(pbArray[GLOBAL_CONTOR_ALIAS].Location.X - 133, pbArray[GLOBAL_CONTOR_ALIAS].Location.Y);
                            textBoxControl[GLOBAL_CONTOR_ALIAS].Location = new Point(textBoxControl[GLOBAL_CONTOR_ALIAS].Location.X + 134, textBoxControl[GLOBAL_CONTOR_ALIAS].Location.Y - 2);
                            arborePrincipal[contorArbore].X = I - 1;
                        }
                    }
                    else
                    {
                        /* Aici verificam daca e deja pus pe ramura cu DA un if, pentru a mari if-ul principal la double-if, adica if cu if pe ambele ramuri
                         * Mecanismul este urmatorul: Cautam acel if pentru care diferenta dintre I-ul if-ului ce urmeaza a fi pozitionat (acesta) si X-ul acelui if
                         * sa fie 1, adica fix if-ul principal. Apoi verficam daca diferenta dintre X-ul acestui if si X-ul imediat urmatorului if, (care se afla pe ramura
                         * cu DA) este 2. Daca este 2, atunci if-ul are ramura DA departata, deci se incearca plasarea unui if intr-un if marit pe ramura cu DA, deci actionam 
                         * in consecinta. */


                        bool IF_PE_DA = false;
                        
                        for (int i = 0; i < contorArbore; i++)
                        {
                            if (arborePrincipal[i].ID == 4 && I - arborePrincipal[i].X == 1)
                                for (int j = i + 1; j < contorArbore; j++)
                                    if (arborePrincipal[j].ID == 4 && arborePrincipal[i].X - arborePrincipal[j].X == 2)
                                    {
                                        IF_PE_DA = true;
                                        break;
                                    }
                            if (IF_PE_DA == true)
                                break;
                        }

                        if (IF_PE_DA == true)
                        {
                            arborePrincipal[contorArbore].DA = false;

                            matriceCoordonate[I - 1, J - 1].ocupat = false;
                            matriceCoordonate[I + 2, J - 1].ocupat = true;

                            matriceCoordonate[I - 1, J].activ = false;
                            matriceCoordonate[I + 1, J].activ = false;

                            matriceCoordonate[I + 2, J].activ = true;
                            matriceCoordonate[I, J].activ = true;

                            pbArray[globalContor].Location = new Point(matriceCoordonate[I + 1, J - 1].x - 90, matriceCoordonate[I + 1, J - 1].y);

                            int j_, GLOBAL_CONTOR_ALIAS;

                            j_ = (Int32)(J - 1);
                            GLOBAL_CONTOR_ALIAS = (Int32)globalContor;

                            for (int i = contorArbore - 1; arborePrincipal[i].ID != 4 || arborePrincipal[i].ifInchis != false; i--)   // aici se muta toate controalele aflate pe NU cu o pozitie 
                            {                                                                                                             // la dreapta

                                GLOBAL_CONTOR_ALIAS--;
                                j_--;

                                if (arborePrincipal[i].DA == false && arborePrincipal[i].X - ifIndent == 1)
                                {
                                    pbArray[GLOBAL_CONTOR_ALIAS].Location = new Point(matriceCoordonate[I + 1, j_].x, matriceCoordonate[I + 1, j_].y);
                                    matriceCoordonate[I, j_].ocupat = false;
                                    matriceCoordonate[I + 1, j_].ocupat = true;
                                }
                            }

                           

                            GLOBAL_CONTOR_ALIAS--;
                            pbArray[GLOBAL_CONTOR_ALIAS].Image = (Bitmap)Logical_SCH__ATESTAT___TRY_.Properties.Resources.conditie_da_nu;
                            pbArray[GLOBAL_CONTOR_ALIAS].Size = new Size(pbArray[GLOBAL_CONTOR_ALIAS].Image.Width, pbArray[GLOBAL_CONTOR_ALIAS].Image.Height);
                            arborePrincipal[contorArbore].X = I + 1;
                        }

                        else
                        {
                            

                            arborePrincipal[contorArbore].DA = false;

                            matriceCoordonate[I - 1, J - 1].ocupat = false;
                            matriceCoordonate[I + 2, J - 1].ocupat = true;

                            matriceCoordonate[I - 1, J].activ = false;
                            matriceCoordonate[I + 1, J].activ = false;

                            matriceCoordonate[I + 2, J].activ = true;
                            matriceCoordonate[I, J].activ = true;

                            pbArray[globalContor].Location = new Point(matriceCoordonate[I + 1, J - 1].x - 90, matriceCoordonate[I + 1, J - 1].y);

                            int j_, GLOBAL_CONTOR_ALIAS;

                            j_ = (Int32)(J - 1);
                            GLOBAL_CONTOR_ALIAS = (Int32)globalContor;

                            for (int i = contorArbore - 1; arborePrincipal[i].ID != 4; i--)   // aici se muta toate controalele aflate pe NU cu o pozitie 
                            {                                                                   // la dreapta

                                GLOBAL_CONTOR_ALIAS--;
                                j_--;

                                if (arborePrincipal[i].DA == false)
                                {
                                    pbArray[GLOBAL_CONTOR_ALIAS].Location = new Point(matriceCoordonate[I + 1, j_].x, matriceCoordonate[I + 1, j_].y);
                                    matriceCoordonate[I, j_].ocupat = false;
                                    matriceCoordonate[I + 1, j_].ocupat = true;
                                }
                            }

                            GLOBAL_CONTOR_ALIAS--;
                            pbArray[GLOBAL_CONTOR_ALIAS].Image = (Bitmap)Logical_SCH__ATESTAT___TRY_.Properties.Resources.conditie_nu;
                            pbArray[GLOBAL_CONTOR_ALIAS].Size = new Size(pbArray[GLOBAL_CONTOR_ALIAS].Image.Width, pbArray[GLOBAL_CONTOR_ALIAS].Image.Height);
                            arborePrincipal[contorArbore].X = I + 1;
                        }
                    }
                }

                arborePrincipal[contorArbore].ID = 4;

                                                    

                contorArbore++;
               
            }
            if (button8Flag == true)                           // flag buton de prelucrare
            {
                DET_Coordonate_Control(out controlX, out controlY);
                pbArray[globalContor].Location = new Point(controlX, controlY);

                TEXT_BOX_EVENT.Click += new System.EventHandler(TEXT_BOX_EVENT_MouseClick);
                TEXT_BOX_EVENT.TextChanged += new System.EventHandler(TEXT_BOX_EVENT_Text_Changed);

                textBoxControl[globalContor].Text = "Prelucrare";
                textBoxControl[globalContor].Multiline = true;
                textBoxControl[globalContor].ScrollBars = ScrollBars.Vertical;
                textBoxControl[globalContor].Height = 31;
                textBoxControl[globalContor].Width = 96;
                textBoxControl[globalContor].ForeColor = Color.DimGray;
                textBoxControl[globalContor].Parent = pbArray[globalContor];
                pbArray[globalContor].Controls.Add(textBoxControl[globalContor]);
                textBoxControl[globalContor].Location = new Point(textBoxControl[globalContor].Location.X + 21, textBoxControl[globalContor].Location.Y + 9);

                button8Flag = false;

                I = controlX / latimeControl;
                J = controlY / inaltimeControl;
             

                arborePrincipal[contorArbore].X = I;
                arborePrincipal[contorArbore].Y = J;

                matriceCoordonate[I, J].activ = false;
                matriceCoordonate[I, J].ocupat = true;

                J++;

                matriceCoordonate[I, J].activ = true;

                if (CONTROL_AFLAT_IN_IF(out ifIndent) == true)
                {
                    if (I < ifIndent)
                    {
                        arborePrincipal[contorArbore].DA = true;

                    }
                    else arborePrincipal[contorArbore].DA = false;
                }

                arborePrincipal[contorArbore++].ID = 5;
            }



            if (arborePrincipal[contorArbore - 1].ID != 1)
            {
               if(meniuStergere.Items.Count == 1)
                meniuStergere.Items.RemoveAt(0);
                pbArray[globalContor].ContextMenuStrip = meniuStergere;           // adaug meniul contextual la picture_box
                meniuStergere.Items.Add("Sterge", null, PB_DELETE);
            }
            panel4.Controls.Add(pbArray[globalContor]);               // adaug picture_box-ul la forma


            globalContor++;                  // dupa fiecare executie a acestei functii, se incrementeaza contorul global
           
            
        }

        private void finisareEndif()
        {
            pbArray[globalContor].Size = new Size(pbArray[globalContor].Image.Width - 2, pbArray[globalContor].Image.Height - 1);

            arborePrincipal[contorArbore].X = I;
            arborePrincipal[contorArbore].Y = J;

            matriceCoordonate[I, J].activ = false;
            matriceCoordonate[I, J].ocupat = true;

            J++;

            matriceCoordonate[I, J].activ = true;

            button3Flag = false;

            isEndif = false;
            isEndwhile = false;

            liniiInchidereContor++;
            arborePrincipal[contorArbore++].ID = 0;
        }

        private void isEndWhileManipulator()
        {
            /* Mai jos este definit mecanismul de pozitionare a endwhile-ului asociat unui control de tip <cat_timp>, precum si procedurile de creare a liniilor
         * de inchidere ale endwhile-ului. 
         *         Endwhile-ul poate avea orice pozitie (pe abscisa) in raport cu pozitia controlului de tip while. Astfel, vom aborda o modalitate de pozitionare
         *         cat mai generala, care sa acopere majoritatea cazurilor in care poate fi pozitionat un endwhile. 
         *      
         *     -> Vom compara pozitia pe care urmeaza sa fie pozitionat endwhile-ul (X-ul), cu pozitia controlului de tip while. Daca while-ul este in dreapta
         *        endwhile-ului, atunci inchiderea se va face prin stanga. Analog pentru celalalt caz. Dupa stabilirea acestui aspect, se pozitioneaza endwhile-ul
         *        care poate fi pe stanga sau pe dreapta.
         *     -> Inchiderea efectiva consta in plasarea de controale cu linii orizontale, verticale si colturi, astfel: de la endwhile porneste o linie orizontala
         *        pana la coloana care are toate elementele active pana la nivelul while-ului. 
         *     -> Pentru gasirea acestei coloane, se realizeaza o cautare directa: luam prima coloana din stanga endwhile-ului (asta in primul caz), apoi mergem in sus, 
         *        pana la y-ul while-ului, verificand daca toate elementele din acest interval sunt active. Prima coloana astfel gasita este cea pe care se va afla linia
         *        verticala de inchidere. 
         *        
        */
            

            int j = 0, coloana_libera = 0, D_OX_END = 0, D_OX_WH = 0, D_OY = 0, l = 0, k = 1;

            DET_Coordonate_Control(out controlX, out controlY);
            pbArray[globalContor].Location = new Point(controlX, controlY);

            I = controlX / latimeControl;
            J = controlY / inaltimeControl;

            if (arborePrincipal[WH_INDEX].DA == true)
                arborePrincipal[contorArbore].DA = true;
            else arborePrincipal[contorArbore].DA = false;

            arborePrincipal[WH_INDEX].coefAdancime = J - arborePrincipal[WH_INDEX].Y + 1; //endwh

            if (arborePrincipal[WH_INDEX].X < I)                                           // daca endwhile-ul se afla la dreapta while-ului
            {
                pbArray[globalContor].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.endif1);



                for (int i = I + 1; i <= 30; i++)
                {
                    for (j = J - 1; j >= arborePrincipal[WH_INDEX].Y; j--)
                    {
                        if (matriceCoordonate[i, j].ocupat == true)                // daca am gasit un element ocupat cu un control pe coloana respectiva
                            break;                                                 // atunci opresc ciclul si merg la coloana urmatoare
                    }
                    if (j == arborePrincipal[WH_INDEX].Y - 1)
                    {
                        coloana_libera = i;                                         //am gasit coloana libera. Incepem trasarea liniilor de inchidere
                        break;
                    }

                }

                D_OX_END = coloana_libera - I;                                     //numarul de linii orizontale care pornesc de la endwhile

                D_OY = J - arborePrincipal[WH_INDEX].Y;

                D_OX_WH = coloana_libera - arborePrincipal[WH_INDEX].X;


                k = 1;
                for (k = 1; k < D_OX_END; k++)                                        // inchidere orizontala
                {
                    matriceCoordonate[I + k, J].ocupat = true;

                    liniiInchidere[liniiInchidereContor, k] = new PictureBox();
                    liniiInchidere[liniiInchidereContor, k].Parent = panel4;
                    liniiInchidere[liniiInchidereContor, k].Location = new Point(matriceCoordonate[I + k, J].x, matriceCoordonate[I + k, J].y);
                    liniiInchidere[liniiInchidereContor, k].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.linie_orizontala);
                    liniiInchidere[liniiInchidereContor, k].Size = new Size(liniiInchidere[liniiInchidereContor, k].Image.Width, liniiInchidere[liniiInchidereContor, k].Image.Height);
                }

               
                matriceCoordonate[I + k, J].ocupat = true;                            // plasare colt stanga

                liniiInchidere[liniiInchidereContor, k] = new PictureBox();
                liniiInchidere[liniiInchidereContor, k].Parent = panel4;
                liniiInchidere[liniiInchidereContor, k].Location = new Point(matriceCoordonate[I + k, J].x, matriceCoordonate[I + k, J].y);
                liniiInchidere[liniiInchidereContor, k].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.colt_stanga);
                liniiInchidere[liniiInchidereContor, k].Size = new Size(liniiInchidere[liniiInchidereContor, k].Image.Width, liniiInchidere[liniiInchidereContor, k].Image.Height);

                for (l = 1; l < D_OY; l++)                                             // inchidere pe verticala
                {
                    matriceCoordonate[I + k, J - l].ocupat = true;

                    liniiInchidere[liniiInchidereContor, k + l] = new PictureBox();
                    liniiInchidere[liniiInchidereContor, k + l].Parent = panel4;
                    liniiInchidere[liniiInchidereContor, k + l].Location = new Point(matriceCoordonate[I + k, J - l].x, matriceCoordonate[I + k, J - l].y);
                    liniiInchidere[liniiInchidereContor, k + l].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.linie_verticala);
                    liniiInchidere[liniiInchidereContor, k + l].Size = new Size(liniiInchidere[liniiInchidereContor, k + l].Image.Width, liniiInchidere[liniiInchidereContor, k + l].Image.Height);
                }

                matriceCoordonate[I + k, J - l].ocupat = true;                         // plasare colt stanga sus

                liniiInchidere[liniiInchidereContor, k + l] = new PictureBox();
                liniiInchidere[liniiInchidereContor, k + l].Parent = panel4;
                liniiInchidere[liniiInchidereContor, k + l].Location = new Point(matriceCoordonate[I + k, J - l].x, matriceCoordonate[I + k, J - l].y);
                liniiInchidere[liniiInchidereContor, k + l].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.colt_stanga_sus);
                liniiInchidere[liniiInchidereContor, k + l].Size = new Size(liniiInchidere[liniiInchidereContor, k + l].Image.Width, liniiInchidere[liniiInchidereContor, k + l].Image.Height);



                for (int p = 1; p < D_OX_WH; p++)                                        // inchidere sus
                {

                    liniiInchidere[liniiInchidereContor, k + l + p] = new PictureBox();
                    liniiInchidere[liniiInchidereContor, k + l + p].Parent = panel4;
                    liniiInchidere[liniiInchidereContor, k + l + p].Location = new Point(matriceCoordonate[I + k - p, arborePrincipal[WH_INDEX].Y].x, matriceCoordonate[I + k - p, arborePrincipal[WH_INDEX].Y].y);
                    liniiInchidere[liniiInchidereContor, k + l + p].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.linie_orizontala_inchidere_while_sus);
                    liniiInchidere[liniiInchidereContor, k + l + p].Size = new Size(liniiInchidere[liniiInchidereContor, k + l + p].Image.Width, liniiInchidere[liniiInchidereContor, k + l + p].Image.Height);
                }
            }

            else                                                                       // daca endwhile-ul se afla in stanga while-ului
            {
                pbArray[globalContor].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.endif2);

                for (int i = I - 1; i >= 0; i--)
                {
                    
                    for (j = J - 1; j >= arborePrincipal[WH_INDEX].Y; j--)
                    {
                        if (matriceCoordonate[i, j].ocupat == true)                // daca am gasit un element ocupat cu un control pe coloana respectiva
                            break;                                                 // atunci opresc ciclul si merg la coloana urmatoare
                    }

                    if (j == arborePrincipal[WH_INDEX].Y - 1)
                    {
                        coloana_libera = i;                                         //am gasit coloana libera. Incepem trasarea liniilor de inchidere
                        break;
                    }

                }

                D_OX_END = I - coloana_libera;                                     //numarul de linii orizontale care pornesc de la endwhile

                D_OY = J - arborePrincipal[WH_INDEX].Y;

                D_OX_WH = arborePrincipal[WH_INDEX].X - coloana_libera;

                k = 1;

                for (k = 1; k < D_OX_END; k++)                                        // inchidere orizontala
                {
                    matriceCoordonate[I - k, J].ocupat = true;

                    liniiInchidere[liniiInchidereContor, k] = new PictureBox();
                    liniiInchidere[liniiInchidereContor, k].Parent = panel4;
                    liniiInchidere[liniiInchidereContor, k].Location = new Point(matriceCoordonate[I - k, J].x, matriceCoordonate[I - k, J].y);
                    liniiInchidere[liniiInchidereContor, k].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.linie_orizontala);
                    liniiInchidere[liniiInchidereContor, k].Size = new Size(liniiInchidere[liniiInchidereContor, k].Image.Width, liniiInchidere[liniiInchidereContor, k].Image.Height);
                }

                
                matriceCoordonate[I - k, J].ocupat = true;                            // plasare colt dreapta

                liniiInchidere[liniiInchidereContor, k] = new PictureBox();
                liniiInchidere[liniiInchidereContor, k].Parent = panel4;
                liniiInchidere[liniiInchidereContor, k].Location = new Point(matriceCoordonate[I - k, J].x, matriceCoordonate[I - k, J].y);
                liniiInchidere[liniiInchidereContor, k].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.colt_dreapta);
                liniiInchidere[liniiInchidereContor, k].Size = new Size(liniiInchidere[liniiInchidereContor, k].Image.Width, liniiInchidere[liniiInchidereContor, k].Image.Height);

                for (l = 1; l < D_OY; l++)                                             // inchidere pe verticala
                {
                    matriceCoordonate[I - k, J - l].ocupat = true;

                    liniiInchidere[liniiInchidereContor, k + l] = new PictureBox();
                    liniiInchidere[liniiInchidereContor, k + l].Parent = panel4;
                    liniiInchidere[liniiInchidereContor, k + l].Location = new Point(matriceCoordonate[I - k, J - l].x, matriceCoordonate[I - k, J - l].y);
                    liniiInchidere[liniiInchidereContor, k + l].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.linie_verticala);
                    liniiInchidere[liniiInchidereContor, k + l].Size = new Size(liniiInchidere[liniiInchidereContor, k + l].Image.Width, liniiInchidere[liniiInchidereContor, k + l].Image.Height);
                }

                matriceCoordonate[I - k, J - l].ocupat = true;                         // plasare colt dreapta sus

                liniiInchidere[liniiInchidereContor, k + l] = new PictureBox();
                liniiInchidere[liniiInchidereContor, k + l].Parent = panel4;
                liniiInchidere[liniiInchidereContor, k + l].Location = new Point(matriceCoordonate[I - k, J - l].x, matriceCoordonate[I - k, J - l].y);
                liniiInchidere[liniiInchidereContor, k + l].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.colt_dreapta_sus);
                liniiInchidere[liniiInchidereContor, k + l].Size = new Size(liniiInchidere[liniiInchidereContor, k + l].Image.Width, liniiInchidere[liniiInchidereContor, k + l].Image.Height);



                for (int p = 1; p < D_OX_WH; p++)                                        // inchidere sus
                {

                    liniiInchidere[liniiInchidereContor, k + l + p] = new PictureBox();
                    liniiInchidere[liniiInchidereContor, k + l + p].Parent = panel4;
                    liniiInchidere[liniiInchidereContor, k + l + p].Location = new Point(matriceCoordonate[I - k + p, arborePrincipal[WH_INDEX].Y].x, matriceCoordonate[I - k + p, arborePrincipal[WH_INDEX].Y].y);
                    liniiInchidere[liniiInchidereContor, k + l + p].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.linie_orizontala_inchidere_while_sus);
                    liniiInchidere[liniiInchidereContor, k + l + p].Size = new Size(liniiInchidere[liniiInchidereContor, k + l + p].Image.Width, liniiInchidere[liniiInchidereContor, k + l + p].Image.Height);
                }

            }
            
            liniiInchidereContor++;
            
            arborePrincipal[WH_INDEX].whileInchis = true;
        }

        private void isEndifManipulator()
        {
            numarareElementeIf();


            if (NR_EL_PE_DA > NR_EL_PE_NU)                                       // daca am mai multe elemente pe DA, atunci ramura cu DA e cea mai lunga, deci acolo
            {
                                                                           // trebuie sa fie endif-ul.
                for (i = contorArbore - 1; arborePrincipal[i].ID != 4 || arborePrincipal[i].ifInchis == true; i--)
                {

                    /* Aici se cauta elementul de deasupra endif-ului ce urmeaza a fi pozitionat. Daca acest element este un control oarecare, se verifica daca
                     * acesta este situat pe ramura cu DA si daca se afla intr-un if neinchis. Daca este un endif, se compara indentarea acestuia (X-ul) cu cel 
                     * al if-ului pe care urmeaza sa il inchida. Astfel, aceasta conditie duce la localizarea exacta a indicelui (i-ul) elementului aflat fix
                     * deasupra endif-ului ce urmeaza a fi pozitionat.
                     * */

                    if ((arborePrincipal[i].DA == true && arborePrincipal[i].ifInchis == false && arborePrincipal[i].X < arborePrincipal[startPos].X) || (arborePrincipal[i].ID == 0 && (arborePrincipal[i].X < arborePrincipal[startPos].X)))
                    {
                        break;
                    }
                }

                arborePrincipal[startPos].coefAdancime = NR_EL_PE_DA + 2; // 2 inseamna if-ul (deci controlul cu conditia) si endif-ul

                pbArray[globalContor].Location = new Point(matriceCoordonate[arborePrincipal[i].X, arborePrincipal[i].Y + 1].x, matriceCoordonate[arborePrincipal[i].X, arborePrincipal[i].Y + 1].y);
                pbArray[globalContor].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.endif1);
                I = arborePrincipal[i].X;
                J = arborePrincipal[i].Y + 1;

                /* Endif-ul este pozitionat pe ramura cu DA (care are mai multe elemente decat cealalta ramura). Acum urmeaza partea de trasare a liniei
                 * de inchidere a if-ului, adica acea linie care uneste endif-ul cu ultimul element de pe ramura cu NU.
                 * 
                 * Acest lucru se face prin urmatorul mecanism:
                 *          
                 *         -> Se determina coordonatele (X-ul si Y-ul din arbore, alias I-ul si J-ul din matrice) ultimului element aflat pe cealalta ramura (NU)
                 *         -> Se face diferenta dintre Y-ul elementului respectiv si J-ul endif-ului (calculat in ultima instructiune de mai sus).
                 *         -> Notam aceasta diferenta cu D. Pe directia OX, sensul pozitiv, se creeaza runtime D - 1 controale de tip PictureBox avand la proprietatea Image
                 *            imaginea cu denumirea linie_orizontala. Pe pozitia imediat urmatoare, in dreapta, se pune imaginea colt_stanga.
                 *         -> Se face diferenta dintre X-ul elementului despectiv si I-ul endif-ului, notata cu E. De la pozitia imaginii colt_stanga, se creeaza 
                 *            runtime E controale de acelasi tip, avand la proprietatea Image, bitmap-ul denumit linie_verticala.
                 *         -> Inainte de a plasa controlul, elementul matricei principale corespunzator pozitiei pe care va fi plasat controlul este marcat ca inactiv.
                 * 
                         
                 */
                i = 0;
                Int32 alias_I_NU = 0, alias_J_NU = 0, D_OX, D_OY;

                for (i = contorArbore - 1; arborePrincipal[i].ID != 4 || arborePrincipal[i].ifInchis == true; i--)
                {
                    if ((arborePrincipal[i].DA == false && arborePrincipal[i].ifInchis == false && arborePrincipal[i].X > arborePrincipal[startPos].X) || (arborePrincipal[i].ID == 0 && (arborePrincipal[i].X > arborePrincipal[startPos].X)))
                    {
                        break;
                    }
                }

                alias_I_NU = arborePrincipal[i].X;
                alias_J_NU = arborePrincipal[i].Y;

                if (NR_EL_PE_NU == 0)
                {
                    alias_I_NU = arborePrincipal[startPos].X + 1;
                    alias_J_NU = arborePrincipal[startPos].Y;
                }

                D_OX = alias_I_NU - I;
                D_OY = J - alias_J_NU;

                // daca nu avem niciun element pe NU/DA \|/ (mai jos) atunci D_OX il setam la 2, pentru a desena o singura linie orizontala de inchidere
                // inchidem pe orizontala
                int k;
                for (k = 1; k < D_OX; k++)
                {
                    matriceCoordonate[I + k, J].ocupat = true;

                    liniiInchidere[liniiInchidereContor, k] = new PictureBox();
                    liniiInchidere[liniiInchidereContor, k].Parent = panel4;
                    liniiInchidere[liniiInchidereContor, k].Location = new Point(matriceCoordonate[I + k, J].x, matriceCoordonate[I + k, J].y);
                    liniiInchidere[liniiInchidereContor, k].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.linie_orizontala);
                    liniiInchidere[liniiInchidereContor, k].Size = new Size(liniiInchidere[liniiInchidereContor, k].Image.Width, liniiInchidere[liniiInchidereContor, k].Image.Height);
                }

                // plasam coltul_stanga



                matriceCoordonate[I + k, J].ocupat = true;

                liniiInchidere[liniiInchidereContor, k] = new PictureBox();
                liniiInchidere[liniiInchidereContor, k].Parent = panel4;
                liniiInchidere[liniiInchidereContor, k].Location = new Point(matriceCoordonate[I + k, J].x, matriceCoordonate[I + k, J].y);
                liniiInchidere[liniiInchidereContor, k].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.colt_stanga);
                liniiInchidere[liniiInchidereContor, k].Size = new Size(liniiInchidere[liniiInchidereContor, k].Image.Width, liniiInchidere[liniiInchidereContor, k].Image.Height);

                // inchidem pe verticala 


                for (int l = 1; l <= D_OY; l++)
                {
                    matriceCoordonate[I + k, J - l].ocupat = true;

                    liniiInchidere[liniiInchidereContor, k + l] = new PictureBox();
                    liniiInchidere[liniiInchidereContor, k + l].Parent = panel4;
                    liniiInchidere[liniiInchidereContor, k + l].Location = new Point(matriceCoordonate[I + k, J - l].x, matriceCoordonate[I + k, J - l].y);
                    liniiInchidere[liniiInchidereContor, k + l].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.linie_verticala);
                    liniiInchidere[liniiInchidereContor, k + l].Size = new Size(liniiInchidere[liniiInchidereContor, k + l].Image.Width, liniiInchidere[liniiInchidereContor, k + l].Image.Height);

                }

                liniiInchidereContor++;

                arborePrincipal[contorArbore].DA = true;


            }     // Pe acest else intra si daca NR_EL_PE_DA este egal cu NR_EL_PE_NU
            else  //_____________________________________________________________________________________________________ daca am mai multe pe NU, pozitionez aici endif-ul
            {
                int i;
                for (i = contorArbore - 1; arborePrincipal[i].ID != 4 || arborePrincipal[i].ifInchis == true; i--)
                {
                    if ((arborePrincipal[i].DA == false && arborePrincipal[i].ifInchis == false && arborePrincipal[i].X > arborePrincipal[startPos].X) || (arborePrincipal[i].ID == 0 && arborePrincipal[i].X > arborePrincipal[startPos].X))
                    {
                        break;
                    }
                }


                arborePrincipal[startPos].coefAdancime = NR_EL_PE_NU + 2; // 2 inseamna if-ul (deci controlul cu conditia) si endif-ul

                pbArray[globalContor].Location = new Point(matriceCoordonate[arborePrincipal[i].X, arborePrincipal[i].Y + 1].x, matriceCoordonate[arborePrincipal[i].X, arborePrincipal[i].Y + 1].y);
                pbArray[globalContor].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.endif2);
                I = arborePrincipal[i].X;
                J = arborePrincipal[i].Y + 1;

                //Incepem inchiderea pe dreapta, cu endif-ul pozitionat pe NU:

                i = 0;
                Int32 alias_I_DA = 0, alias_J_DA = 0, D_OX = 0, D_OY = 0;

                for (i = contorArbore - 1; arborePrincipal[i].ID != 4 || arborePrincipal[i].ifInchis == true; i--)    //cautam ultimul element de pe ramura cu DA
                {
                    if ((arborePrincipal[i].DA == true && arborePrincipal[i].ifInchis == false && arborePrincipal[i].X < arborePrincipal[startPos].X) || (arborePrincipal[i].ID == 0 && arborePrincipal[i].X < arborePrincipal[startPos].X))
                    {
                        break;
                    }
                }

                alias_I_DA = arborePrincipal[i].X;
                alias_J_DA = arborePrincipal[i].Y;

                if (NR_EL_PE_DA == 0)
                {
                    alias_I_DA = arborePrincipal[startPos].X - 1;
                    alias_J_DA = arborePrincipal[startPos].Y;
                }


                D_OX = I - alias_I_DA;
                D_OY = J - alias_J_DA;

                if (NR_EL_PE_DA == NR_EL_PE_NU)
                {
                    D_OY = 0;
                }

                // inchidem pe orizontala

                int k;
                for (k = 1; k < D_OX; k++)
                {
                    matriceCoordonate[I - k, J].ocupat = true;

                    liniiInchidere[liniiInchidereContor, k] = new PictureBox();
                    liniiInchidere[liniiInchidereContor, k].Parent = panel4;
                    liniiInchidere[liniiInchidereContor, k].Location = new Point(matriceCoordonate[I - k, J].x, matriceCoordonate[I - k, J].y);
                    liniiInchidere[liniiInchidereContor, k].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.linie_orizontala);
                    liniiInchidere[liniiInchidereContor, k].Size = new Size(liniiInchidere[liniiInchidereContor, k].Image.Width, liniiInchidere[liniiInchidereContor, k].Image.Height);
                }

                //plasam coltul_dreapta

                matriceCoordonate[I - k, J].ocupat = true;

                liniiInchidere[liniiInchidereContor, k] = new PictureBox();
                liniiInchidere[liniiInchidereContor, k].Parent = panel4;
                liniiInchidere[liniiInchidereContor, k].Location = new Point(matriceCoordonate[I - k, J].x, matriceCoordonate[I - k, J].y);
                liniiInchidere[liniiInchidereContor, k].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.colt_dreapta);
                liniiInchidere[liniiInchidereContor, k].Size = new Size(liniiInchidere[liniiInchidereContor, k].Image.Width, liniiInchidere[liniiInchidereContor, k].Image.Height);

                //inchidem vertical

                for (int l = 1; l <= D_OY; l++)
                {
                    matriceCoordonate[I - k, J - l].ocupat = true;

                    liniiInchidere[liniiInchidereContor, k + l] = new PictureBox();
                    liniiInchidere[liniiInchidereContor, k + l].Parent = panel4;
                    liniiInchidere[liniiInchidereContor, k + l].Location = new Point(matriceCoordonate[I - k, J - l].x, matriceCoordonate[I - k, J - l].y);
                    liniiInchidere[liniiInchidereContor, k + l].Image = (Bitmap)(Logical_SCH__ATESTAT___TRY_.Properties.Resources.linie_verticala);
                    liniiInchidere[liniiInchidereContor, k + l].Size = new Size(liniiInchidere[liniiInchidereContor, k + l].Image.Width, liniiInchidere[liniiInchidereContor, k + l].Image.Height);
                }

                liniiInchidereContor++;

                arborePrincipal[contorArbore].DA = false;
            }

            //Acum marcam toate elementele aflate intre 0 si 4 (if) ca fiind inchise, prin setarea atributului IF_INCHIS al arborelui la true
            int alias_i;
            for (alias_i = contorArbore - 1; arborePrincipal[alias_i].ID != 4 || arborePrincipal[alias_i].ifInchis != false; alias_i--)
            {
                arborePrincipal[alias_i].ifInchis = true;
            }
            arborePrincipal[alias_i].ifInchis = true;        //inchide si if-ul (elementul de ID == 4, care nu e inchis in for)
            
        }

        private void numarareElementeIf()
        {
            /* Aici are loc procedura de numarare a elementelor care se afla pe ramurile if-ului ce urmeaza a fi inchis. Se numara elementele aflate
             * pe ramura cu DA si pe cele aflate pe ramura cu NU pentru a stabili pozitia pe care trebuie sa o ocupe endif-ul (adica pe ramura cea mai lunga).
             * 
             * Mecanismul este urmatorul: se aduce un contor la pozitia primului if neinchis, dupa care se incepe numararea astfel: se ia fiecare element si se
             * verifica: daca este pe DA se incrementeaza NR_EL_PE_DA, daca este pe NU se incrementeaza NR_EL_PE_NU. Problema apare cand avem un if in acest if
             * pe care urmeaza sa il inchidem. Arborele principal are un atribut denumit COEF_ADANCIME, care are valoare nenula doar in cazul elementelor cu ID = 4
             * adica a if-urilor, si a celor cu ID = 6, adica a while-urilor. Acest atribut arata inaltimea if-ului, adica distanta de la controlul propriuzis pana la endif. Astfel, numararea se face prin
             * adunarea acestui atribut, urmata de cresterea contorului pana la intalnirea endif-ului asociat (nu au nicio importanta elementele din acel if)
             * */

            NR_EL_PE_DA = 0;
            NR_EL_PE_NU = 0;



            startPos = contorArbore - 1;

            while (arborePrincipal[startPos].ID != 4 || arborePrincipal[startPos].ifInchis == true) startPos--;

            for (int i = startPos + 1; i <= contorArbore - 1; i++)
            {
                if (arborePrincipal[i].ID == 4 && arborePrincipal[i].DA == true)
                {
                    NR_EL_PE_DA += arborePrincipal[i].coefAdancime;

                    while (arborePrincipal[i].ID != 0) i++;

                }
                else if (arborePrincipal[i].ID == 4 && arborePrincipal[i].DA == false)
                {
                    NR_EL_PE_NU += arborePrincipal[i].coefAdancime;

                    while (arborePrincipal[i].ID != 0) i++;
                }
                else if (arborePrincipal[i].ID == 6 && arborePrincipal[i].DA == true)
                {
                    NR_EL_PE_DA += arborePrincipal[i].coefAdancime;

                    while (arborePrincipal[i].ID != 0) i++;
                }
                else if (arborePrincipal[i].ID == 6 && arborePrincipal[i].DA == false)
                {
                    NR_EL_PE_NU += arborePrincipal[i].coefAdancime;

                    while (arborePrincipal[i].ID != 0) i++;
                }
                else if (arborePrincipal[i].DA == true)
                    NR_EL_PE_DA++;
                else if (arborePrincipal[i].DA == false)
                    NR_EL_PE_NU++;
            }
        }

        private void verificareEndifSauEndWhile()
        {
            for (int i = contorArbore - 1; i >= 0; i--)
            {
                if (arborePrincipal[i].ID == 4 && arborePrincipal[i].ifInchis == false)
                {
                    arborePrincipal[contorArbore].endifParentIndex = i;
                    isEndif = true;
                    break;
                }
                else if (arborePrincipal[i].ID == 6 && arborePrincipal[i].whileInchis == false)              // aici verific daca terminatorul se refera la if sau la while
                {
                    isEndwhile = true;
                    WH_INDEX = i;
                    arborePrincipal[contorArbore].endifParentIndex = i;
                    break;
                }
            }
        }

        private void butonStop()
        {
            DET_Coordonate_Control(out controlX, out controlY);
            pbArray[globalContor].Location = new Point(controlX, controlY);
            arborePrincipal[contorArbore++].ID = -1;
        }

        private void butonStart()
        {
            

            I = 16;                        // pozitie de start
            J = 0;
            pbArray[globalContor].Location = new Point(matriceCoordonate[I, J].x, matriceCoordonate[I, J].y);

            J++;

            matriceCoordonate[I, J].activ = true;
            matriceCoordonate[I, J].ocupat = true;

            arborePrincipal[contorArbore++].ID = 1;


            button1Flag = false;
        }
        #endregion
        
        private void TEXT_BOX_EVENT_MouseClick(object sender, EventArgs e)
        {  
           if( textBoxControl[globalContor - 1].Text == "Cat timp"   ||
               textBoxControl[globalContor - 1].Text == "Scrie"      ||
               textBoxControl[globalContor - 1].Text == "Citeste"    ||
               textBoxControl[globalContor - 1].Text == "Prelucrare" ||
               textBoxControl[globalContor - 1].Text == "Conditie" )

            textBoxControl[globalContor - 1].Text = "";                    // scad contorul cu 1 pentru ca
            textBoxControl[globalContor - 1].ForeColor = Color.Black;     // este transmis incrementat din functia de drag

            //pbArray[globalContor - 1].ContextMenuStrip = null;
        }

        private void TEXT_BOX_EVENT_Text_Changed(object sender, EventArgs e)
        {
            //pbArray[globalContor - 1].ContextMenuStrip.Hide();
        }
        

        #region butoaneMouseDownEvent

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {

            button1Flag = true;
            start.FlatAppearance.BorderSize = 1;
            start.FlatAppearance.BorderColor = Color.Blue;
            start.DoDragDrop(Logical_SCH__ATESTAT___TRY_.Properties.Resources.start, DragDropEffects.Copy | DragDropEffects.Move);

        }

        private void button2_MouseDown(object sender, MouseEventArgs e)    
        {
            button2Flag = true;
            stop.FlatAppearance.BorderSize = 1;
            stop.FlatAppearance.BorderColor = Color.Blue;
            stop.DoDragDrop(Logical_SCH__ATESTAT___TRY_.Properties.Resources.stop, DragDropEffects.Copy | DragDropEffects.All);


            
        }
        
        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            button4Flag = true;
            citeste.FlatAppearance.BorderSize = 1;
            citeste.FlatAppearance.BorderColor = Color.Blue;
            citeste.DoDragDrop(Logical_SCH__ATESTAT___TRY_.Properties.Resources.citeste, DragDropEffects.Copy | DragDropEffects.Move);

            
                    
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            button5Flag = true;
            scrie.FlatAppearance.BorderSize = 1;
            scrie.FlatAppearance.BorderColor = Color.Blue;
            scrie.DoDragDrop(Logical_SCH__ATESTAT___TRY_.Properties.Resources.citeste, DragDropEffects.Copy | DragDropEffects.Move);

            
            
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            button6Flag = true;
            cat_timp.FlatAppearance.BorderSize = 1;
            cat_timp.FlatAppearance.BorderColor = Color.Blue;
            cat_timp.DoDragDrop(Logical_SCH__ATESTAT___TRY_.Properties.Resources.conditie1, DragDropEffects.Copy | DragDropEffects.Move);

            
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            button7Flag = true;
            daca.FlatAppearance.BorderSize = 1;
            daca.FlatAppearance.BorderColor = Color.Blue;
            daca.DoDragDrop(Logical_SCH__ATESTAT___TRY_.Properties.Resources.conditie2, DragDropEffects.Copy | DragDropEffects.Move);

            
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            button8Flag = true;
            prelucrare.FlatAppearance.BorderSize = 1;
            prelucrare.FlatAppearance.BorderColor = Color.Blue;
            prelucrare.DoDragDrop(Logical_SCH__ATESTAT___TRY_.Properties.Resources.prelucrare, DragDropEffects.Copy | DragDropEffects.Move);

            
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)      // butonul de endif/while 
        {
            button3Flag = true;
            end.FlatAppearance.BorderSize = 1;
            end.FlatAppearance.BorderColor = Color.Blue;
            end.DoDragDrop(Logical_SCH__ATESTAT___TRY_.Properties.Resources.endif1, DragDropEffects.Copy | DragDropEffects.Move);
            
            
        }
        #endregion

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void DECLARARE_VARIABILE(object sender, EventArgs e)
        {

            winDeclVar.Show();

        }

        private void PB_DELETE(object sender, EventArgs e)             // Butonul de meniu contextual corespunzator comenzii de stergere a unui control
        { 
            globalContor -= 1;                                      

            if (arborePrincipal[contorArbore - 1].ID == 4)
            {
                int X_EL_IF, Y_EL_IF;                              // aceste doua variabile memoreaza coordonatele elementului de deasupra if-ului

                X_EL_IF = arborePrincipal[contorArbore - 1].X;
                Y_EL_IF = arborePrincipal[contorArbore - 1].Y - 1;

                I = X_EL_IF;
                J = Y_EL_IF + 1;

                matriceCoordonate[I, J].activ = false;
                matriceCoordonate[I - 1, J + 1].activ = false;
                matriceCoordonate[I + 1, J + 1].activ = false;
                matriceCoordonate[I - 1, J].activ = false;
                matriceCoordonate[I + 1, J].activ = false;
                matriceCoordonate[I, J].ocupat = false;
                matriceCoordonate[I - 1, J + 1].ocupat = false;
                matriceCoordonate[I + 1, J + 1].ocupat = false;
                matriceCoordonate[I - 1, J].ocupat = false;
                matriceCoordonate[I + 1, J].ocupat = false;

                matriceCoordonate[I, J].activ = true;

                pbArray[globalContor].Image = null;

                pbArray[globalContor].Controls.Remove(textBoxControl[globalContor]);
                panel4.Controls.Remove(pbArray[globalContor]);
                
                contorArbore -= 1;
            }

            else if (arborePrincipal[contorArbore - 1].ID == 0)
            {
               
                liniiInchidereContor -= 2;

                for (int i = 1; liniiInchidere[liniiInchidereContor, i] != null; i++)
                {
                    liniiInchidere[liniiInchidereContor, i].Image = null;
                    matriceCoordonate[liniiInchidere[liniiInchidereContor, i].Location.X / latimeControl, liniiInchidere[liniiInchidereContor, i].Location.Y / inaltimeControl].ocupat = false;
                    panel4.Controls.Remove(liniiInchidere[liniiInchidereContor, i]);

                }

                if (arborePrincipal[arborePrincipal[contorArbore - 1].endifParentIndex].ID == 4)
                {
                    arborePrincipal[arborePrincipal[contorArbore - 1].endifParentIndex].ifInchis = false;

                    for (int i = arborePrincipal[contorArbore - 1].endifParentIndex + 1; i < contorArbore; i++)
                    {
                        if (arborePrincipal[i].ID == 4)
                        {
                            while (arborePrincipal[i].ID != 0)
                                i++;
                        }
                        arborePrincipal[i].ifInchis = false;
                    }
                }
                else if (arborePrincipal[arborePrincipal[contorArbore - 1].endifParentIndex].ID == 6)
                {
                    arborePrincipal[arborePrincipal[contorArbore - 1].endifParentIndex].whileInchis = false;

                    for (int i = arborePrincipal[contorArbore - 1].endifParentIndex + 1; i < contorArbore; i++)
                    {
                        arborePrincipal[i].whileInchis = false;
                    }
                }

                matriceCoordonate[I, J].activ = false;
                J--;
                matriceCoordonate[I, J].activ = true;
                
                pbArray[globalContor].Image = null;
                pbArray[globalContor].Controls.Remove(textBoxControl[globalContor]);
                panel4.Controls.Remove(pbArray[globalContor]);
                contorArbore -= 1;

            }
            else                                                                       // sterge controale de tip afisare/prelucrare/citeste
            {
                matriceCoordonate[I, J].activ = false;
                matriceCoordonate[I, J].ocupat = false;
                J--;
                matriceCoordonate[I, J].activ = true;

                pbArray[globalContor].Image = null;

                textBoxControl[globalContor].Text = "";

                pbArray[globalContor].Controls.Remove(textBoxControl[globalContor]);
                panel4.Controls.Remove(pbArray[globalContor]);
                contorArbore -= 1;
            }
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (contorArbore == 0)
            {
                MessageBox.Show("Nu există o schemă logică reprezentată!", "Eroare de stergere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < globalContor; i++)
                {
                    pbArray[i].Image = null;
                    panel4.Controls.Remove(pbArray[i]);
                    pbArray[i] = null;
                }

                for (int i = 0; i < contorArbore; i++)
                {
                    arborePrincipal[i].ID = -2;
                    arborePrincipal[i].ifInchis = false;
                    arborePrincipal[i].whileInchis = false;
                    arborePrincipal[i].DA = false;
                    arborePrincipal[i].endifParentIndex = 0;
                    arborePrincipal[i].coefAdancime = 0;
                    arborePrincipal[i].X = 0;
                    arborePrincipal[i].Y = 0;
                }

                J = 0;
                Initializare_Matrice_Principala();



                contorArbore = 0;
                globalContor = 0;

                activareButoanePanou(start);
                dezactivareButoanePanou(citeste, scrie, prelucrare, interpretare, cat_timp, end, daca, stop);

                for (int i = 0; i < liniiInchidereContor; i++)
                {
                    for (int j = 1; liniiInchidere[i, j] != null; j++)
                    {
                        liniiInchidere[i, j].Image = null;
                        panel4.Controls.Remove(liniiInchidere[i, j]);
                        liniiInchidere[i, j] = null;
                    }
                }
                liniiInchidereContor = 0;

                button4Flag = false; button1Flag = false; button2Flag = false; button3Flag = false;
                button5Flag = false; button6Flag = false; button7Flag = false; button8Flag = false;

                winDeclVar.Dispose();

                winDeclVar = new Form3(this);

                meniuDeclarareVariabile.Items.RemoveAt(0);

               

                panel4.Refresh();

            }
            
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            Pen p = new Pen(Brushes.Red);
            
            for (int i = 1; i <= 70; i++)
            {
                g.DrawLine(p, new Point(138 * i, 0), new Point(138 * i, 5000));  // verticale
            }
            for (int i = 1; i <= 70; i++)
            {
                g.DrawLine(p, new Point(0, 85 * i), new Point(5000, 85 * i));  // orizontale
            }
        }

        
        private void panel4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Scroll(object sender, ScrollEventArgs e)
        {
            panel4.Invalidate();
        }

        private void Form1_Scroll(object sender, ScrollEventArgs e)
        {

            
            
                if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    if (e.NewValue > e.OldValue)
                    {
                        panel1.Location = new Point(panel1.Location.X, panel1.Location.Y + e.NewValue - e.OldValue);
                    }
                    else
                    {
                        panel1.Location = new Point(panel1.Location.X, panel1.Location.Y - (e.OldValue - e.NewValue));
                    }
                }
                else if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                    if (e.NewValue > e.OldValue)
                    {
                        panel1.Location = new Point(panel1.Location.X + e.NewValue - e.OldValue, panel1.Location.Y);
                    }
                    else
                    {
                        panel1.Location = new Point(panel1.Location.X - (e.OldValue - e.NewValue), panel1.Location.Y);
                    }
                }
            
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            
        }

        public string TextInterpretat
        {
            set { textInterpretat = value; } 
        }

        public string TextHeaders
        {
            get { return textHeaders; }
            set { textHeaders = value; }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            

            #region Initializare_indentare_spatii

            spatiiIndentare[0] = "          ";

            for (int i = 1; i <= 10; i++)
                spatiiIndentare[i] = spatiiIndentare[i - 1] + "        ";

            #endregion

            if (arborePrincipal[contorArbore - 1].ID != -1)
                MessageBox.Show("Eroare de interpretare. Schema logica nu este finalizata corespunzator.", "Mesaj de eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                Form2 WIN_GEN_COD = new Form2(this);

                textHeaders += "#include<iostream>\r\n";

                textInterpretat += textHeaders;
                textInterpretat += "using namespace std;\r\n";
                
                //__Procedura de interpretare a declararii variabilelor__


                textInterpretat += textDeclarareVariabile;           // camp public setat in Form3 (de declarare variabile)

                textInterpretat += "int main() \r\n{ \r\n";
          
                for (int i = 0; i < contorArbore; i++)
                {
                    if (((i > 1) && (arborePrincipal[i - 1].ifInchis == true && arborePrincipal[i].ifInchis == true && (arborePrincipal[i - 1].DA == true || (arborePrincipal[i-1].ID == 0 && arborePrincipal[arborePrincipal[i-1].endifParentIndex].DA == true)) && arborePrincipal[i].DA == false))) //|| (i>1 && arborePrincipal[i].DA == false && arborePrincipal[i].ifInchis == true && arborePrincipal[i].ID != 4))
                    {
                       
                        textInterpretat += spatiiIndentare[CONTOR_INDENT - 1] + "}" + "\r\n";
                        textInterpretat += spatiiIndentare[CONTOR_INDENT - 1] + "else" + "\r\n";
                        textInterpretat += spatiiIndentare[CONTOR_INDENT - 1] + "{" + "\r\n";
                    }
                    switch (arborePrincipal[i].ID)
                    {
                        case 2: {                                                                                   // citeste
                                    string[] variabileCitire = textBoxControl[i].Text.Split(',');

                                    foreach (string var_ in variabileCitire)
                                    {
                                        textInterpretat += spatiiIndentare[CONTOR_INDENT] + "cin>>" + var_ + ";\r\n";
                                    }
                                    break; 
                                }
                        case 3: {                                                                                 // afisaza
                                    string[] variabileAfisare = textBoxControl[i].Text.Split(',');

                                   foreach (string var_ in variabileAfisare)
                                   {
                                        textInterpretat += spatiiIndentare[CONTOR_INDENT] + "cout<<" + var_ + ";\r\n";
                                   }
                            
                                   break;
                               }
                        case 5: {                                                                              // prelucreaza
                                     string[] instructiuniPrelucrare = textBoxControl[i].Text.Split(',');

                                     foreach (string var_ in instructiuniPrelucrare)
                                     {
                                           textInterpretat += spatiiIndentare[CONTOR_INDENT] + var_ + ";\r\n";
                                     }
                                    break; 
                                }
                        case 4:
                            {
                                textInterpretat += spatiiIndentare[CONTOR_INDENT] + "if(" + textBoxControl[i].Text + ")" + "\r\n";
                                textInterpretat += spatiiIndentare[CONTOR_INDENT] + "{" + "\r\n";

                                 CONTOR_INDENT++;
                                 break;
                            }
                        case 6: {  textInterpretat += spatiiIndentare[CONTOR_INDENT] + "while(" + textBoxControl[i].Text + ")" + "\r\n";
                                   textInterpretat += spatiiIndentare[CONTOR_INDENT] + "{" + "\r\n";
                                   
                                    CONTOR_INDENT++;
                                    break;
                                 }

                        case 0:
                            {
                                if(CONTOR_INDENT > 0)
                                        CONTOR_INDENT--;

                                textInterpretat += spatiiIndentare[CONTOR_INDENT] + "}" + "\r\n";
                                break;
                            }
                     }

                 }
                
                textInterpretat += spatiiIndentare[CONTOR_INDENT] + "return 0;\r\n";
                textInterpretat += "}";

                WIN_GEN_COD.richTextBox1.AppendText(textInterpretat);

                WIN_GEN_COD.Show();
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form5 Help = new Form5();

            Help.Show();
        }

       
        }
      }

