namespace DidelcoCapacitacionDiApi
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ServerTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.UserNameTxt = new System.Windows.Forms.TextBox();
            this.PasswordTxt = new System.Windows.Forms.TextBox();
            this.CompanyDbTxt = new System.Windows.Forms.TextBox();
            this.UserDbTxt = new System.Windows.Forms.TextBox();
            this.PassDbTxt = new System.Windows.Forms.TextBox();
            this.DbType = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cardcodeTxt = new System.Windows.Forms.TextBox();
            this.nombreTxt = new System.Windows.Forms.TextBox();
            this.tipoTxt = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(566, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "CreaDocumento";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(566, 68);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "ActualizaDocumento";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ServerTxt
            // 
            this.ServerTxt.Location = new System.Drawing.Point(162, 39);
            this.ServerTxt.Name = "ServerTxt";
            this.ServerTxt.Size = new System.Drawing.Size(314, 22);
            this.ServerTxt.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "SERVER";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "USER SAP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "PASS SAP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "SOCIEDAD";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 373);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "DB SERVER TYPE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "USER DB";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 316);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "PASS DB";
            // 
            // UserNameTxt
            // 
            this.UserNameTxt.Location = new System.Drawing.Point(162, 95);
            this.UserNameTxt.Name = "UserNameTxt";
            this.UserNameTxt.Size = new System.Drawing.Size(314, 22);
            this.UserNameTxt.TabIndex = 9;
            // 
            // PasswordTxt
            // 
            this.PasswordTxt.Location = new System.Drawing.Point(162, 152);
            this.PasswordTxt.Name = "PasswordTxt";
            this.PasswordTxt.PasswordChar = '*';
            this.PasswordTxt.Size = new System.Drawing.Size(314, 22);
            this.PasswordTxt.TabIndex = 10;
            this.PasswordTxt.UseSystemPasswordChar = true;
            // 
            // CompanyDbTxt
            // 
            this.CompanyDbTxt.Location = new System.Drawing.Point(162, 204);
            this.CompanyDbTxt.Name = "CompanyDbTxt";
            this.CompanyDbTxt.Size = new System.Drawing.Size(314, 22);
            this.CompanyDbTxt.TabIndex = 11;
            // 
            // UserDbTxt
            // 
            this.UserDbTxt.Location = new System.Drawing.Point(162, 259);
            this.UserDbTxt.Name = "UserDbTxt";
            this.UserDbTxt.Size = new System.Drawing.Size(314, 22);
            this.UserDbTxt.TabIndex = 12;
            // 
            // PassDbTxt
            // 
            this.PassDbTxt.Location = new System.Drawing.Point(162, 313);
            this.PassDbTxt.Name = "PassDbTxt";
            this.PassDbTxt.Size = new System.Drawing.Size(314, 22);
            this.PassDbTxt.TabIndex = 13;
            this.PassDbTxt.UseSystemPasswordChar = true;
            // 
            // DbType
            // 
            this.DbType.FormattingEnabled = true;
            this.DbType.Items.AddRange(new object[] {
            "dst_MSSQL",
            "dst_DB_2",
            "dst_SYBASE",
            "dst_MSSQL2005",
            "dst_MAXDB",
            "dst_MSSQL2008",
            "dst_MSSQL2012",
            "dst_MSSQL2014",
            "dst_HANADB",
            "dst_MSSQL2016",
            "dst_MSSQL2017",
            "dst_MSSQL2019"});
            this.DbType.Location = new System.Drawing.Point(208, 369);
            this.DbType.Name = "DbType";
            this.DbType.Size = new System.Drawing.Size(268, 24);
            this.DbType.TabIndex = 14;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(550, 258);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(205, 63);
            this.button3.TabIndex = 15;
            this.button3.Text = "CONECTAR";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(550, 330);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(205, 63);
            this.button4.TabIndex = 16;
            this.button4.Text = "DESCONECTAR";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(12, 425);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(20, 17);
            this.ErrorLabel.TabIndex = 17;
            this.ErrorLabel.Text = "...";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(780, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "CARDCODE";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(780, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "NOMBRE";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(780, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 17);
            this.label10.TabIndex = 20;
            this.label10.Text = "TIPO";
            // 
            // cardcodeTxt
            // 
            this.cardcodeTxt.Location = new System.Drawing.Point(883, 39);
            this.cardcodeTxt.Name = "cardcodeTxt";
            this.cardcodeTxt.Size = new System.Drawing.Size(302, 22);
            this.cardcodeTxt.TabIndex = 21;
            // 
            // nombreTxt
            // 
            this.nombreTxt.Location = new System.Drawing.Point(883, 80);
            this.nombreTxt.Name = "nombreTxt";
            this.nombreTxt.Size = new System.Drawing.Size(302, 22);
            this.nombreTxt.TabIndex = 22;
            // 
            // tipoTxt
            // 
            this.tipoTxt.FormattingEnabled = true;
            this.tipoTxt.Items.AddRange(new object[] {
            "CLIENTE",
            "PROVEEDOR",
            "LEAD"});
            this.tipoTxt.Location = new System.Drawing.Point(883, 128);
            this.tipoTxt.Name = "tipoTxt";
            this.tipoTxt.Size = new System.Drawing.Size(302, 24);
            this.tipoTxt.TabIndex = 23;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(883, 180);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(302, 73);
            this.button5.TabIndex = 24;
            this.button5.Text = "CREAR SOCIO DE NEGOCIOS";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(883, 311);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(302, 22);
            this.textBox1.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(780, 311);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 17);
            this.label11.TabIndex = 25;
            this.label11.Text = "CARDCODE";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 736);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.tipoTxt);
            this.Controls.Add(this.nombreTxt);
            this.Controls.Add(this.cardcodeTxt);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.DbType);
            this.Controls.Add(this.PassDbTxt);
            this.Controls.Add(this.UserDbTxt);
            this.Controls.Add(this.CompanyDbTxt);
            this.Controls.Add(this.PasswordTxt);
            this.Controls.Add(this.UserNameTxt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerTxt);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_exit);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox ServerTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox UserNameTxt;
        private System.Windows.Forms.TextBox PasswordTxt;
        private System.Windows.Forms.TextBox CompanyDbTxt;
        private System.Windows.Forms.TextBox UserDbTxt;
        private System.Windows.Forms.TextBox PassDbTxt;
        private System.Windows.Forms.ComboBox DbType;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox cardcodeTxt;
        private System.Windows.Forms.TextBox nombreTxt;
        private System.Windows.Forms.ComboBox tipoTxt;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label11;
    }
}

