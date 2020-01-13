namespace Main_Form
{
    partial class DataBaseConnection
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Test_btn = new System.Windows.Forms.Button();
            this.lb_Test = new System.Windows.Forms.Label();
            this.tb_Id = new System.Windows.Forms.TextBox();
            this.tb_Pw = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_S = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Test_btn
            // 
            this.Test_btn.Location = new System.Drawing.Point(427, 44);
            this.Test_btn.Name = "Test_btn";
            this.Test_btn.Size = new System.Drawing.Size(75, 23);
            this.Test_btn.TabIndex = 0;
            this.Test_btn.Text = "TEST Btn";
            this.Test_btn.UseVisualStyleBackColor = true;
            this.Test_btn.Click += new System.EventHandler(this.Test_btn_Click);
            // 
            // lb_Test
            // 
            this.lb_Test.AutoSize = true;
            this.lb_Test.Location = new System.Drawing.Point(441, 90);
            this.lb_Test.Name = "lb_Test";
            this.lb_Test.Size = new System.Drawing.Size(42, 15);
            this.lb_Test.TabIndex = 1;
            this.lb_Test.Text = "TEST";
            // 
            // tb_Id
            // 
            this.tb_Id.Location = new System.Drawing.Point(303, 45);
            this.tb_Id.Name = "tb_Id";
            this.tb_Id.Size = new System.Drawing.Size(100, 25);
            this.tb_Id.TabIndex = 2;
            // 
            // tb_Pw
            // 
            this.tb_Pw.Location = new System.Drawing.Point(303, 80);
            this.tb_Pw.Name = "tb_Pw";
            this.tb_Pw.Size = new System.Drawing.Size(100, 25);
            this.tb_Pw.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "PW";
            // 
            // btn_S
            // 
            this.btn_S.Location = new System.Drawing.Point(427, 138);
            this.btn_S.Name = "btn_S";
            this.btn_S.Size = new System.Drawing.Size(75, 23);
            this.btn_S.TabIndex = 6;
            this.btn_S.Text = "S";
            this.btn_S.UseVisualStyleBackColor = true;
            this.btn_S.Click += new System.EventHandler(this.btn_S_Click);
            // 
            // DataBaseConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_S);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Pw);
            this.Controls.Add(this.tb_Id);
            this.Controls.Add(this.lb_Test);
            this.Controls.Add(this.Test_btn);
            this.Name = "DataBaseConnection";
            this.Size = new System.Drawing.Size(606, 451);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Test_btn;
        private System.Windows.Forms.Label lb_Test;
        private System.Windows.Forms.TextBox tb_Id;
        private System.Windows.Forms.TextBox tb_Pw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_S;
    }
}
