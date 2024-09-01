namespace SrtBinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Lbx_SRT1 = new ListBox();
            Lbx_SRT2 = new ListBox();
            Btn_Combine = new Button();
            Btn_Clear1 = new Button();
            Btn_Clear2 = new Button();
            Lbx_Output = new ListBox();
            SuspendLayout();
            // 
            // Lbx_SRT1
            // 
            Lbx_SRT1.AllowDrop = true;
            Lbx_SRT1.FormattingEnabled = true;
            Lbx_SRT1.ItemHeight = 23;
            Lbx_SRT1.Location = new Point(18, 54);
            Lbx_SRT1.Name = "Lbx_SRT1";
            Lbx_SRT1.Size = new Size(500, 50);
            Lbx_SRT1.TabIndex = 0;
            Lbx_SRT1.DragEnter += listBox1_DragEnter;
            // 
            // Lbx_SRT2
            // 
            Lbx_SRT2.AllowDrop = true;
            Lbx_SRT2.FormattingEnabled = true;
            Lbx_SRT2.ItemHeight = 23;
            Lbx_SRT2.Location = new Point(553, 54);
            Lbx_SRT2.Name = "Lbx_SRT2";
            Lbx_SRT2.Size = new Size(500, 50);
            Lbx_SRT2.TabIndex = 1;
            // 
            // Btn_Combine
            // 
            Btn_Combine.Location = new Point(480, 139);
            Btn_Combine.Name = "Btn_Combine";
            Btn_Combine.Size = new Size(112, 34);
            Btn_Combine.TabIndex = 2;
            Btn_Combine.Text = "Combine";
            Btn_Combine.UseVisualStyleBackColor = true;
            Btn_Combine.Click += button1_Click;
            // 
            // Btn_Clear1
            // 
            Btn_Clear1.Location = new Point(234, 12);
            Btn_Clear1.Name = "Btn_Clear1";
            Btn_Clear1.Size = new Size(112, 34);
            Btn_Clear1.TabIndex = 2;
            Btn_Clear1.Text = "Clear";
            Btn_Clear1.UseVisualStyleBackColor = true;
            Btn_Clear1.Click += Btn_Clear1_Click;
            // 
            // Btn_Clear2
            // 
            Btn_Clear2.Location = new Point(752, 12);
            Btn_Clear2.Name = "Btn_Clear2";
            Btn_Clear2.Size = new Size(112, 34);
            Btn_Clear2.TabIndex = 2;
            Btn_Clear2.Text = "Clear";
            Btn_Clear2.UseVisualStyleBackColor = true;
            Btn_Clear2.Click += Btn_Clear2_Click;
            // 
            // Lbx_Output
            // 
            Lbx_Output.FormattingEnabled = true;
            Lbx_Output.ItemHeight = 23;
            Lbx_Output.Location = new Point(18, 209);
            Lbx_Output.Name = "Lbx_Output";
            Lbx_Output.Size = new Size(1035, 211);
            Lbx_Output.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1078, 444);
            Controls.Add(Lbx_Output);
            Controls.Add(Btn_Clear2);
            Controls.Add(Btn_Clear1);
            Controls.Add(Btn_Combine);
            Controls.Add(Lbx_SRT2);
            Controls.Add(Lbx_SRT1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "SrtBinder by CHL";
            ResumeLayout(false);
        }

        #endregion

        private ListBox Lbx_SRT1;
        private ListBox Lbx_SRT2;
        private Button Btn_Combine;
        private Button Btn_Clear1;
        private Button Btn_Clear2;
        private ListBox Lbx_Output;
    }
}
