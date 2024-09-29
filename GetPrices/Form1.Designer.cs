namespace GetPrices
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_gene_excek = new Button();
            panel1 = new Panel();
            txt_html = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_gene_excek
            // 
            btn_gene_excek.Location = new Point(66, 366);
            btn_gene_excek.Name = "btn_gene_excek";
            btn_gene_excek.Size = new Size(132, 23);
            btn_gene_excek.TabIndex = 1;
            btn_gene_excek.Text = "Generate Excel shop";
            btn_gene_excek.UseVisualStyleBackColor = true;
            btn_gene_excek.Click += btn_gene_excek_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(txt_html);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 337);
            panel1.TabIndex = 2;
            // 
            // txt_html
            // 
            txt_html.Dock = DockStyle.Fill;
            txt_html.Location = new Point(0, 0);
            txt_html.MaxLength = 1000000;
            txt_html.Multiline = true;
            txt_html.Name = "txt_html";
            txt_html.Size = new Size(800, 337);
            txt_html.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(375, 366);
            button1.Name = "button1";
            button1.Size = new Size(218, 23);
            button1.TabIndex = 3;
            button1.Text = "Generate Excel central sooq";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(66, 395);
            button2.Name = "button2";
            button2.Size = new Size(190, 23);
            button2.TabIndex = 4;
            button2.Text = "Generate Excel shop مستورد";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(375, 395);
            button3.Name = "button3";
            button3.Size = new Size(218, 23);
            button3.TabIndex = 5;
            button3.Text = "Generate Excel central sooq  مستورد";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(btn_gene_excek);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button btn_gene_excek;
        private Panel panel1;
        private TextBox txt_html;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}
