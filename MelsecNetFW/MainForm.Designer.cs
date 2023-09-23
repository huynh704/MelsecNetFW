namespace MelsecNetFW
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Station = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtb_Setting = new System.Windows.Forms.RichTextBox();
            this.grb_Infor = new System.Windows.Forms.GroupBox();
            this.txt_syslog = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.grb_Infor.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Import);
            this.groupBox1.Controls.Add(this.btn_Export);
            this.groupBox1.Controls.Add(this.btn_Connect);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_Station);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rtb_Setting);
            this.groupBox1.Location = new System.Drawing.Point(3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 171);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cài đặt";
            // 
            // btn_Import
            // 
            this.btn_Import.Location = new System.Drawing.Point(331, 114);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(108, 53);
            this.btn_Import.TabIndex = 5;
            this.btn_Import.Text = "Nhập cài đặt";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Location = new System.Drawing.Point(331, 61);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(108, 53);
            this.btn_Export.TabIndex = 5;
            this.btn_Export.Text = "Xuất cài đặt";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(331, 8);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(108, 53);
            this.btn_Connect.TabIndex = 5;
            this.btn_Connect.Text = "Kết nối";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Logic station number";
            // 
            // txt_Station
            // 
            this.txt_Station.Location = new System.Drawing.Point(118, 14);
            this.txt_Station.Name = "txt_Station";
            this.txt_Station.Size = new System.Drawing.Size(105, 20);
            this.txt_Station.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cài đặt dữ liệu\r\nĐịnh dạng: Địa chỉ dữ liệu, Triger, Độ dài dữ liệu (Word)";
            // 
            // rtb_Setting
            // 
            this.rtb_Setting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_Setting.Location = new System.Drawing.Point(7, 67);
            this.rtb_Setting.Name = "rtb_Setting";
            this.rtb_Setting.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtb_Setting.Size = new System.Drawing.Size(309, 98);
            this.rtb_Setting.TabIndex = 1;
            this.rtb_Setting.Text = "";
            // 
            // grb_Infor
            // 
            this.grb_Infor.Controls.Add(this.txt_syslog);
            this.grb_Infor.Location = new System.Drawing.Point(3, 178);
            this.grb_Infor.Name = "grb_Infor";
            this.grb_Infor.Size = new System.Drawing.Size(445, 257);
            this.grb_Infor.TabIndex = 5;
            this.grb_Infor.TabStop = false;
            this.grb_Infor.Text = "Thông tin: (0)";
            // 
            // txt_syslog
            // 
            this.txt_syslog.Location = new System.Drawing.Point(7, 18);
            this.txt_syslog.Multiline = true;
            this.txt_syslog.Name = "txt_syslog";
            this.txt_syslog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_syslog.Size = new System.Drawing.Size(432, 233);
            this.txt_syslog.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(453, 437);
            this.Controls.Add(this.grb_Infor);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "PLC syslog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grb_Infor.ResumeLayout(false);
            this.grb_Infor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtb_Setting;
        private System.Windows.Forms.TextBox txt_Station;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grb_Infor;
        private System.Windows.Forms.TextBox txt_syslog;
    }
}

