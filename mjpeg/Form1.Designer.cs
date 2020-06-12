namespace mjpeg
{
    partial class ROSITA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ROSITA));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_pInDevices = new System.Windows.Forms.ComboBox();
            this.m_pOutDevices = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_pLoacalIP = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_pLocalPort = new System.Windows.Forms.NumericUpDown();
            this.m_pToggleRun = new System.Windows.Forms.Button();
            this.m_pRemotePort = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_pToggleMic = new System.Windows.Forms.Button();
            this.m_pRemoteIP = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panelBawah = new System.Windows.Forms.Panel();
            this.btnShowRight = new System.Windows.Forms.Button();
            this.panelWeb = new System.Windows.Forms.Panel();
            this.autoConnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_pLocalPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pRemotePort)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelBawah.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(171, 520);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(212, 51);
            this.button1.TabIndex = 0;
            this.button1.Text = "Tampilkan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(10, 426);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(373, 81);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mic";
            // 
            // m_pInDevices
            // 
            this.m_pInDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_pInDevices.FormattingEnabled = true;
            this.m_pInDevices.Location = new System.Drawing.Point(75, 12);
            this.m_pInDevices.Name = "m_pInDevices";
            this.m_pInDevices.Size = new System.Drawing.Size(308, 39);
            this.m_pInDevices.TabIndex = 4;
            // 
            // m_pOutDevices
            // 
            this.m_pOutDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_pOutDevices.FormattingEnabled = true;
            this.m_pOutDevices.Location = new System.Drawing.Point(75, 63);
            this.m_pOutDevices.Name = "m_pOutDevices";
            this.m_pOutDevices.Size = new System.Drawing.Size(308, 39);
            this.m_pOutDevices.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "Spk";
            // 
            // m_pLoacalIP
            // 
            this.m_pLoacalIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_pLoacalIP.FormattingEnabled = true;
            this.m_pLoacalIP.Location = new System.Drawing.Point(10, 148);
            this.m_pLoacalIP.Name = "m_pLoacalIP";
            this.m_pLoacalIP.Size = new System.Drawing.Size(243, 39);
            this.m_pLoacalIP.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 31);
            this.label4.TabIndex = 9;
            this.label4.Text = "IP Rosita";
            // 
            // m_pLocalPort
            // 
            this.m_pLocalPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_pLocalPort.Location = new System.Drawing.Point(259, 148);
            this.m_pLocalPort.Name = "m_pLocalPort";
            this.m_pLocalPort.Size = new System.Drawing.Size(124, 38);
            this.m_pLocalPort.TabIndex = 11;
            // 
            // m_pToggleRun
            // 
            this.m_pToggleRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_pToggleRun.Location = new System.Drawing.Point(171, 198);
            this.m_pToggleRun.Name = "m_pToggleRun";
            this.m_pToggleRun.Size = new System.Drawing.Size(212, 45);
            this.m_pToggleRun.TabIndex = 12;
            this.m_pToggleRun.Text = "Aktifkan";
            this.m_pToggleRun.UseVisualStyleBackColor = true;
            this.m_pToggleRun.Click += new System.EventHandler(this.m_pToggleRun_Click);
            // 
            // m_pRemotePort
            // 
            this.m_pRemotePort.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_pRemotePort.Location = new System.Drawing.Point(259, 285);
            this.m_pRemotePort.Name = "m_pRemotePort";
            this.m_pRemotePort.Size = new System.Drawing.Size(124, 38);
            this.m_pRemotePort.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 31);
            this.label5.TabIndex = 13;
            this.label5.Text = "IP Nurse Station";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 387);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 31);
            this.label6.TabIndex = 16;
            this.label6.Text = "Kamera Nurse";
            // 
            // m_pToggleMic
            // 
            this.m_pToggleMic.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_pToggleMic.Location = new System.Drawing.Point(171, 335);
            this.m_pToggleMic.Name = "m_pToggleMic";
            this.m_pToggleMic.Size = new System.Drawing.Size(212, 48);
            this.m_pToggleMic.TabIndex = 17;
            this.m_pToggleMic.Text = "Hubungkan";
            this.m_pToggleMic.UseVisualStyleBackColor = true;
            this.m_pToggleMic.Click += new System.EventHandler(this.m_pToggleMic_Click);
            // 
            // m_pRemoteIP
            // 
            this.m_pRemoteIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_pRemoteIP.Location = new System.Drawing.Point(10, 284);
            this.m_pRemoteIP.Name = "m_pRemoteIP";
            this.m_pRemoteIP.Size = new System.Drawing.Size(243, 38);
            this.m_pRemoteIP.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.autoConnect);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_pRemoteIP);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.m_pToggleMic);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.m_pInDevices);
            this.panel1.Controls.Add(this.m_pRemotePort);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.m_pOutDevices);
            this.panel1.Controls.Add(this.m_pToggleRun);
            this.panel1.Controls.Add(this.m_pLocalPort);
            this.panel1.Controls.Add(this.m_pLoacalIP);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(586, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 729);
            this.panel1.TabIndex = 19;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 694);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(44, 35);
            this.button2.TabIndex = 19;
            this.button2.Text = ">>";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panelBawah
            // 
            this.panelBawah.Controls.Add(this.btnShowRight);
            this.panelBawah.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBawah.Location = new System.Drawing.Point(0, 694);
            this.panelBawah.Name = "panelBawah";
            this.panelBawah.Size = new System.Drawing.Size(586, 35);
            this.panelBawah.TabIndex = 20;
            // 
            // btnShowRight
            // 
            this.btnShowRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnShowRight.Location = new System.Drawing.Point(511, 0);
            this.btnShowRight.Name = "btnShowRight";
            this.btnShowRight.Size = new System.Drawing.Size(75, 35);
            this.btnShowRight.TabIndex = 0;
            this.btnShowRight.Text = ">>";
            this.btnShowRight.UseVisualStyleBackColor = true;
            this.btnShowRight.Click += new System.EventHandler(this.btnShowRight_Click);
            // 
            // panelWeb
            // 
            this.panelWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWeb.Location = new System.Drawing.Point(0, 0);
            this.panelWeb.Name = "panelWeb";
            this.panelWeb.Size = new System.Drawing.Size(586, 694);
            this.panelWeb.TabIndex = 21;
            this.panelWeb.Paint += new System.Windows.Forms.PaintEventHandler(this.panelWeb_Paint);
            // 
            // autoConnect
            // 
            this.autoConnect.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.autoConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoConnect.Location = new System.Drawing.Point(10, 577);
            this.autoConnect.Name = "autoConnect";
            this.autoConnect.Size = new System.Drawing.Size(373, 111);
            this.autoConnect.TabIndex = 20;
            this.autoConnect.Text = "Auto Connect";
            this.autoConnect.UseVisualStyleBackColor = false;
            this.autoConnect.Click += new System.EventHandler(this.autoConnect_Click);
            // 
            // ROSITA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 729);
            this.Controls.Add(this.panelWeb);
            this.Controls.Add(this.panelBawah);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ROSITA";
            this.Text = "ROSITA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.wfrm_Main_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_pLocalPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pRemotePort)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelBawah.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_pInDevices;
        private System.Windows.Forms.ComboBox m_pOutDevices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_pLoacalIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown m_pLocalPort;
        private System.Windows.Forms.Button m_pToggleRun;
        private System.Windows.Forms.NumericUpDown m_pRemotePort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button m_pToggleMic;
        private System.Windows.Forms.TextBox m_pRemoteIP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panelBawah;
        private System.Windows.Forms.Button btnShowRight;
        private System.Windows.Forms.Panel panelWeb;
        private System.Windows.Forms.Button autoConnect;
    }
}

