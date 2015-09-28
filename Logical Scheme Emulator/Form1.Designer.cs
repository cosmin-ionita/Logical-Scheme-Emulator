namespace Logical_SCH__ATESTAT___TRY_
{
    partial class generareSchema
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(generareSchema));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.interpretare = new System.Windows.Forms.Button();
            this.prelucrare = new System.Windows.Forms.Button();
            this.daca = new System.Windows.Forms.Button();
            this.cat_timp = new System.Windows.Forms.Button();
            this.scrie = new System.Windows.Forms.Button();
            this.citeste = new System.Windows.Forms.Button();
            this.end = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.start = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.interpretare);
            this.panel1.Controls.Add(this.prelucrare);
            this.panel1.Controls.Add(this.daca);
            this.panel1.Controls.Add(this.cat_timp);
            this.panel1.Controls.Add(this.scrie);
            this.panel1.Controls.Add(this.citeste);
            this.panel1.Controls.Add(this.end);
            this.panel1.Controls.Add(this.stop);
            this.panel1.Controls.Add(this.start);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Location = new System.Drawing.Point(1584, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 578);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Neuropol", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(3, 533);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Ajutor";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Neuropol", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.Black;
            this.button9.Location = new System.Drawing.Point(3, 493);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(181, 34);
            this.button9.TabIndex = 3;
            this.button9.Text = "Stergere schema";
            this.toolTip1.SetToolTip(this.button9, "Buton care șterge întreaga schemă logică reprezentată.");
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // interpretare
            // 
            this.interpretare.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("interpretare.BackgroundImage")));
            this.interpretare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.interpretare.Location = new System.Drawing.Point(3, 438);
            this.interpretare.Name = "interpretare";
            this.interpretare.Size = new System.Drawing.Size(181, 49);
            this.interpretare.TabIndex = 8;
            this.toolTip1.SetToolTip(this.interpretare, "Buton care generează codul in C++ aferent schemei logice reprezentate.");
            this.interpretare.UseVisualStyleBackColor = true;
            this.interpretare.Click += new System.EventHandler(this.button10_Click);
            // 
            // prelucrare
            // 
            this.prelucrare.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("prelucrare.BackgroundImage")));
            this.prelucrare.FlatAppearance.BorderSize = 0;
            this.prelucrare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prelucrare.Location = new System.Drawing.Point(3, 328);
            this.prelucrare.Name = "prelucrare";
            this.prelucrare.Size = new System.Drawing.Size(181, 49);
            this.prelucrare.TabIndex = 7;
            this.toolTip1.SetToolTip(this.prelucrare, "Control care acceptă instrucțiuni de atribuire.");
            this.prelucrare.UseVisualStyleBackColor = true;
            this.prelucrare.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button8_MouseDown);
            // 
            // daca
            // 
            this.daca.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("daca.BackgroundImage")));
            this.daca.FlatAppearance.BorderSize = 0;
            this.daca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.daca.Location = new System.Drawing.Point(3, 273);
            this.daca.Name = "daca";
            this.daca.Size = new System.Drawing.Size(181, 49);
            this.daca.TabIndex = 6;
            this.toolTip1.SetToolTip(this.daca, "Control care marchează începutul unei structuri condiționale (necesită end).");
            this.daca.UseVisualStyleBackColor = true;
            this.daca.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button7_MouseDown);
            // 
            // cat_timp
            // 
            this.cat_timp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cat_timp.BackgroundImage")));
            this.cat_timp.FlatAppearance.BorderSize = 0;
            this.cat_timp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cat_timp.Location = new System.Drawing.Point(3, 219);
            this.cat_timp.Name = "cat_timp";
            this.cat_timp.Size = new System.Drawing.Size(181, 48);
            this.cat_timp.TabIndex = 5;
            this.toolTip1.SetToolTip(this.cat_timp, "Control care marchează începutul unei structuri repetitive (necesită end).");
            this.cat_timp.UseVisualStyleBackColor = true;
            this.cat_timp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button6_MouseDown);
            // 
            // scrie
            // 
            this.scrie.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("scrie.BackgroundImage")));
            this.scrie.FlatAppearance.BorderSize = 0;
            this.scrie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scrie.Location = new System.Drawing.Point(3, 165);
            this.scrie.Name = "scrie";
            this.scrie.Size = new System.Drawing.Size(181, 48);
            this.scrie.TabIndex = 4;
            this.toolTip1.SetToolTip(this.scrie, "Control cu rol de afișare a variabilelor.");
            this.scrie.UseVisualStyleBackColor = true;
            this.scrie.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button5_MouseDown);
            // 
            // citeste
            // 
            this.citeste.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("citeste.BackgroundImage")));
            this.citeste.FlatAppearance.BorderSize = 0;
            this.citeste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.citeste.Location = new System.Drawing.Point(3, 111);
            this.citeste.Name = "citeste";
            this.citeste.Size = new System.Drawing.Size(181, 48);
            this.citeste.TabIndex = 3;
            this.toolTip1.SetToolTip(this.citeste, "Control cu rol de citire a variabilelor. ");
            this.citeste.UseVisualStyleBackColor = true;
            this.citeste.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button4_MouseDown);
            // 
            // end
            // 
            this.end.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("end.BackgroundImage")));
            this.end.FlatAppearance.BorderSize = 0;
            this.end.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.end.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.end.Location = new System.Drawing.Point(3, 383);
            this.end.Name = "end";
            this.end.Size = new System.Drawing.Size(181, 49);
            this.end.TabIndex = 2;
            this.end.Text = "\r\n";
            this.end.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.toolTip1.SetToolTip(this.end, "Control care marchează sfârșitul unei instrucțiuni repetitive/condiționale.");
            this.end.UseVisualStyleBackColor = true;
            this.end.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button3_MouseDown);
            // 
            // stop
            // 
            this.stop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stop.BackgroundImage")));
            this.stop.FlatAppearance.BorderSize = 0;
            this.stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stop.Location = new System.Drawing.Point(3, 57);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(181, 48);
            this.stop.TabIndex = 1;
            this.toolTip1.SetToolTip(this.stop, "Control care închide schema logică.");
            this.stop.UseVisualStyleBackColor = true;
            this.stop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
            // 
            // start
            // 
            this.start.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("start.BackgroundImage")));
            this.start.FlatAppearance.BorderSize = 0;
            this.start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.start.Location = new System.Drawing.Point(3, 3);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(181, 48);
            this.start.TabIndex = 0;
            this.toolTip1.SetToolTip(this.start, "Control care determină punctul de început al schemei logice. Click-dreapta pe ace" +
                    "st control va deschide fereastra de declarare variabile.");
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.button1_Click);
            this.start.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Location = new System.Drawing.Point(32767, 32767);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(102, 534);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(4816, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(0, 0);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Font = new System.Drawing.Font("Garamond", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(3, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(4813, 5000);
            this.panel4.TabIndex = 5;
            this.panel4.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel4_Scroll);
            this.panel4.Click += new System.EventHandler(this.panel4_Click);
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(3, 4990);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(5008, 20);
            this.panel5.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(4813, 642);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(181, 18);
            this.textBox1.TabIndex = 2;
            // 
            // generareSchema
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1370, 750);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Garamond", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "generareSchema";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "L.S. Emulator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Form1_Scroll);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button end;
        private System.Windows.Forms.Button citeste;
        private System.Windows.Forms.Button cat_timp;
        private System.Windows.Forms.Button scrie;
        private System.Windows.Forms.Button prelucrare;
        private System.Windows.Forms.Button daca;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button interpretare;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
    }
}

