namespace JraVanCsharp
{
    partial class frmMain
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
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            mnuConfig = new ToolStripMenuItem();
            mnuConfJV = new ToolStripMenuItem();
            btnGetJVData = new Button();
            rtbData = new RichTextBox();
            prgDownload = new ProgressBar();
            prgJVRead = new ProgressBar();
            tmrDownload = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuConfig });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 40);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuConfig
            // 
            mnuConfig.DropDownItems.AddRange(new ToolStripItem[] { mnuConfJV });
            mnuConfig.Name = "mnuConfig";
            mnuConfig.Size = new Size(111, 36);
            mnuConfig.Text = "設定(&C)";
            // 
            // mnuConfJV
            // 
            mnuConfJV.Name = "mnuConfJV";
            mnuConfJV.Size = new Size(336, 44);
            mnuConfJV.Text = "JV-Link の設定(&J)...";
            mnuConfJV.Click += mnuConfJV_Click;
            // 
            // btnGetJVData
            // 
            btnGetJVData.Location = new Point(23, 42);
            btnGetJVData.Name = "btnGetJVData";
            btnGetJVData.Size = new Size(150, 70);
            btnGetJVData.TabIndex = 1;
            btnGetJVData.Text = "データ取得";
            btnGetJVData.UseVisualStyleBackColor = true;
            btnGetJVData.Click += btnGetJVData_Click;
            // 
            // rtbData
            // 
            rtbData.Location = new Point(25, 146);
            rtbData.Name = "rtbData";
            rtbData.Size = new Size(748, 292);
            rtbData.TabIndex = 2;
            rtbData.Text = "";
            rtbData.WordWrap = false;
            // 
            // prgDownload
            // 
            prgDownload.Location = new Point(183, 42);
            prgDownload.Name = "prgDownload";
            prgDownload.Size = new Size(590, 34);
            prgDownload.TabIndex = 3;
            // 
            // prgJVRead
            // 
            prgJVRead.Location = new Point(183, 94);
            prgJVRead.Name = "prgJVRead";
            prgJVRead.Size = new Size(590, 36);
            prgJVRead.TabIndex = 4;
            // 
            // tmrDownload
            // 
            tmrDownload.Interval = 500;
            tmrDownload.Tick += tmrDownload_Tick;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(prgJVRead);
            Controls.Add(prgDownload);
            Controls.Add(rtbData);
            Controls.Add(btnGetJVData);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "frmMain";
            Text = "Form1";
            Load += frmMain_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private JVDTLabLib.JVLinkClass axJVLink1 = new JVDTLabLib.JVLinkClass();
        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuConfig;
        private ToolStripMenuItem mnuConfJV;
        private Button btnGetJVData;
        private RichTextBox rtbData;
        private ProgressBar prgDownload;
        private ProgressBar prgJVRead;
        private System.Windows.Forms.Timer tmrDownload;
    }
}