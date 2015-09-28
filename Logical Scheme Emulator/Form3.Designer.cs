namespace Logical_SCH__ATESTAT___TRY_
{
    partial class Form3
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
            this.label1 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.mathBox = new System.Windows.Forms.CheckBox();
            this.charBox = new System.Windows.Forms.CheckBox();
            this.Închidere = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Nume variabile:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(434, 6);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(71, 15);
            this.label24.TabIndex = 67;
            this.label24.Text = "Tip de date:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(741, 357);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 31);
            this.button1.TabIndex = 68;
            this.button1.Text = "Executare";
            this.toolTip1.SetToolTip(this.button1, "Acest buton va închide fereastra curentă, iar datele\r\nintroduse vor fi memorate î" +
                    "n cadrul aplicației. La deschiderea\r\nulterioară a acestei ferestre, datele pot f" +
                    "i modificate.");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(10, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 40);
            this.button2.TabIndex = 70;
            this.button2.Text = "_Variabila nouă_";
            this.toolTip1.SetToolTip(this.button2, "Acest buton va genera un panou în care se poate declara \r\nuna sau mai multe varia" +
                    "bile de același tip. Pentru a declara\r\no variabilă de alt tip, se va apăsa din n" +
                    "ou pe acest buton.");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mathBox
            // 
            this.mathBox.AutoSize = true;
            this.mathBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mathBox.Location = new System.Drawing.Point(13, 369);
            this.mathBox.Name = "mathBox";
            this.mathBox.Size = new System.Drawing.Size(201, 19);
            this.mathBox.TabIndex = 71;
            this.mathBox.Text = "Se vor utiliza functii matematice";
            this.mathBox.UseVisualStyleBackColor = true;
            // 
            // charBox
            // 
            this.charBox.AutoSize = true;
            this.charBox.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.charBox.Location = new System.Drawing.Point(220, 369);
            this.charBox.Name = "charBox";
            this.charBox.Size = new System.Drawing.Size(348, 19);
            this.charBox.TabIndex = 72;
            this.charBox.Text = "Se vor utiliza functii de manipulare a sirurilor de caractere";
            this.charBox.UseVisualStyleBackColor = true;
            // 
            // Închidere
            // 
            this.Închidere.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Închidere.Location = new System.Drawing.Point(575, 357);
            this.Închidere.Name = "Închidere";
            this.Închidere.Size = new System.Drawing.Size(160, 31);
            this.Închidere.TabIndex = 73;
            this.Închidere.Text = "Închidere";
            this.toolTip1.SetToolTip(this.Închidere, "Acest buton va închide fereastra curentă. \r\nDatele introduse nu vor fi memorate.");
            this.Închidere.UseVisualStyleBackColor = true;
            this.Închidere.Click += new System.EventHandler(this.Închidere_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 398);
            this.Controls.Add(this.Închidere);
            this.Controls.Add(this.charBox);
            this.Controls.Add(this.mathBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form3";
            this.Text = "Declarare_variabile";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox mathBox;
        private System.Windows.Forms.CheckBox charBox;
        private System.Windows.Forms.Button Închidere;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}