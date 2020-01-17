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
            this.tb_DataSource = new System.Windows.Forms.TextBox();
            this.tb_InitialCatalog = new System.Windows.Forms.TextBox();
            this.tb_UserId = new System.Windows.Forms.TextBox();
            this.tb_Title = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.listB_ConnectionList = new System.Windows.Forms.ListBox();
            this.btn_GetList = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_DataSource
            // 
            this.tb_DataSource.Location = new System.Drawing.Point(150, 20);
            this.tb_DataSource.Name = "tb_DataSource";
            this.tb_DataSource.Size = new System.Drawing.Size(100, 25);
            this.tb_DataSource.TabIndex = 7;
            // 
            // tb_InitialCatalog
            // 
            this.tb_InitialCatalog.Location = new System.Drawing.Point(150, 51);
            this.tb_InitialCatalog.Name = "tb_InitialCatalog";
            this.tb_InitialCatalog.Size = new System.Drawing.Size(100, 25);
            this.tb_InitialCatalog.TabIndex = 8;
            // 
            // tb_UserId
            // 
            this.tb_UserId.Location = new System.Drawing.Point(150, 82);
            this.tb_UserId.Name = "tb_UserId";
            this.tb_UserId.Size = new System.Drawing.Size(100, 25);
            this.tb_UserId.TabIndex = 9;
            // 
            // tb_Title
            // 
            this.tb_Title.Location = new System.Drawing.Point(150, 113);
            this.tb_Title.Name = "tb_Title";
            this.tb_Title.Size = new System.Drawing.Size(100, 25);
            this.tb_Title.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "DataSource";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Initial Catalog";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "UserId";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Title";
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(262, 52);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 15;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(262, 144);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 16;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(150, 144);
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(100, 25);
            this.tb_Password.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "Password";
            // 
            // listB_ConnectionList
            // 
            this.listB_ConnectionList.FormattingEnabled = true;
            this.listB_ConnectionList.ItemHeight = 15;
            this.listB_ConnectionList.Location = new System.Drawing.Point(33, 191);
            this.listB_ConnectionList.Name = "listB_ConnectionList";
            this.listB_ConnectionList.Size = new System.Drawing.Size(217, 244);
            this.listB_ConnectionList.TabIndex = 20;
            this.listB_ConnectionList.SelectedIndexChanged += new System.EventHandler(this.listB_ConnectionList_SelectedIndexChanged);
            // 
            // btn_GetList
            // 
            this.btn_GetList.Location = new System.Drawing.Point(262, 22);
            this.btn_GetList.Name = "btn_GetList";
            this.btn_GetList.Size = new System.Drawing.Size(75, 23);
            this.btn_GetList.TabIndex = 21;
            this.btn_GetList.Text = "GetList";
            this.btn_GetList.UseVisualStyleBackColor = true;
            this.btn_GetList.Click += new System.EventHandler(this.btn_GetList_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(262, 82);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Delete.TabIndex = 22;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // DataBaseConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_GetList);
            this.Controls.Add(this.listB_ConnectionList);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_Password);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_Title);
            this.Controls.Add(this.tb_UserId);
            this.Controls.Add(this.tb_InitialCatalog);
            this.Controls.Add(this.tb_DataSource);
            this.Name = "DataBaseConnection";
            this.Size = new System.Drawing.Size(606, 451);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tb_DataSource;
        private System.Windows.Forms.TextBox tb_InitialCatalog;
        private System.Windows.Forms.TextBox tb_UserId;
        private System.Windows.Forms.TextBox tb_Title;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listB_ConnectionList;
        private System.Windows.Forms.Button btn_GetList;
        private System.Windows.Forms.Button btn_Delete;
    }
}
