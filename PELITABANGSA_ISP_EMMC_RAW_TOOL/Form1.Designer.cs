namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.log = new System.Windows.Forms.RichTextBox();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.cbpartisi = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statuslabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statuslabelProses = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statuslabelPartisi = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statuslabelSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statuslabelProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.buttonStop = new System.Windows.Forms.Button();
            this.groupBoxWrite = new System.Windows.Forms.GroupBox();
            this.buttonLoadPartitions = new System.Windows.Forms.Button();
            this.checkBoxloadpartition = new System.Windows.Forms.CheckBox();
            this.numericLengthWrite = new System.Windows.Forms.TextBox();
            this.buttonWritePartition = new System.Windows.Forms.Button();
            this.numericStartWrite = new System.Windows.Forms.TextBox();
            this.buttonWriteManual = new System.Windows.Forms.Button();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonbrowse = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxRead = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonReadPartitionOnly = new System.Windows.Forms.Button();
            this.checkBoxreadpartisi = new System.Windows.Forms.CheckBox();
            this.numericLengthRead = new System.Windows.Forms.TextBox();
            this.numericStartRead = new System.Windows.Forms.TextBox();
            this.buttonReadManual = new System.Windows.Forms.Button();
            this.buttonReadSelected = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReadFull = new System.Windows.Forms.Button();
            this.groupBoxDrive = new System.Windows.Forms.GroupBox();
            this.comboBoxDrive = new System.Windows.Forms.ComboBox();
            this.buttonrefreshDrive = new System.Windows.Forms.Button();
            this.Log1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.choose = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startaddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endaddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bgworkerReadAddressPartitions = new System.ComponentModel.BackgroundWorker();
            this.bgworkerWriteAddressPartitions = new System.ComponentModel.BackgroundWorker();
            this.bgworkerReadPartition = new System.ComponentModel.BackgroundWorker();
            this.bgworkerReadFull = new System.ComponentModel.BackgroundWorker();
            this.bgworkerReadAddress = new System.ComponentModel.BackgroundWorker();
            this.bgworkerWriteAddress = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.groupBoxWrite.SuspendLayout();
            this.groupBoxRead.SuspendLayout();
            this.groupBoxDrive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.log);
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.metroPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(957, 540);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 0;
            // 
            // log
            // 
            this.log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.log.Location = new System.Drawing.Point(0, 0);
            this.log.Name = "log";
            this.log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(25, 100);
            this.log.TabIndex = 0;
            this.log.Text = "";
            this.log.TextChanged += new System.EventHandler(this.log_TextChanged);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.cbpartisi);
            this.metroPanel1.Controls.Add(this.statusStrip1);
            this.metroPanel1.Controls.Add(this.metroPanel2);
            this.metroPanel1.Controls.Add(this.dataGridView1);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(957, 540);
            this.metroPanel1.TabIndex = 9;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // cbpartisi
            // 
            this.cbpartisi.Checked = true;
            this.cbpartisi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbpartisi.Location = new System.Drawing.Point(16, 5);
            this.cbpartisi.Name = "cbpartisi";
            this.cbpartisi.Size = new System.Drawing.Size(15, 14);
            this.cbpartisi.TabIndex = 9;
            this.cbpartisi.UseVisualStyleBackColor = true;
            this.cbpartisi.CheckedChanged += new System.EventHandler(this.cbpartisi_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statuslabelStatus,
            this.toolStripStatusLabel2,
            this.statuslabelProses,
            this.toolStripStatusLabel3,
            this.statuslabelPartisi,
            this.toolStripStatusLabel4,
            this.statuslabelSpeed,
            this.toolStripStatusLabel5,
            this.statuslabelProgress,
            this.statusProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(957, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 19);
            this.toolStripStatusLabel1.Text = "Status : ";
            // 
            // statuslabelStatus
            // 
            this.statuslabelStatus.AutoSize = false;
            this.statuslabelStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statuslabelStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statuslabelStatus.ForeColor = System.Drawing.Color.Blue;
            this.statuslabelStatus.Name = "statuslabelStatus";
            this.statuslabelStatus.Size = new System.Drawing.Size(192, 19);
            this.statuslabelStatus.Text = "Ready";
            this.statuslabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(54, 19);
            this.toolStripStatusLabel2.Text = "Address : ";
            // 
            // statuslabelProses
            // 
            this.statuslabelProses.AutoSize = false;
            this.statuslabelProses.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statuslabelProses.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statuslabelProses.ForeColor = System.Drawing.Color.Red;
            this.statuslabelProses.Name = "statuslabelProses";
            this.statuslabelProses.Size = new System.Drawing.Size(118, 19);
            this.statuslabelProses.Text = "0x0000000000000000";
            this.statuslabelProses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(54, 19);
            this.toolStripStatusLabel3.Text = "Partition :";
            // 
            // statuslabelPartisi
            // 
            this.statuslabelPartisi.AutoSize = false;
            this.statuslabelPartisi.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statuslabelPartisi.ForeColor = System.Drawing.Color.Fuchsia;
            this.statuslabelPartisi.Name = "statuslabelPartisi";
            this.statuslabelPartisi.Size = new System.Drawing.Size(150, 19);
            this.statuslabelPartisi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.AutoSize = false;
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(48, 19);
            this.toolStripStatusLabel4.Text = "Speed : ";
            // 
            // statuslabelSpeed
            // 
            this.statuslabelSpeed.AutoSize = false;
            this.statuslabelSpeed.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statuslabelSpeed.ForeColor = System.Drawing.Color.Green;
            this.statuslabelSpeed.Name = "statuslabelSpeed";
            this.statuslabelSpeed.Size = new System.Drawing.Size(70, 19);
            this.statuslabelSpeed.Text = "0 MB/s";
            this.statuslabelSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.AutoSize = false;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(61, 19);
            this.toolStripStatusLabel5.Text = "Progress : ";
            // 
            // statuslabelProgress
            // 
            this.statuslabelProgress.AutoSize = false;
            this.statuslabelProgress.ForeColor = System.Drawing.Color.DarkOrange;
            this.statuslabelProgress.Name = "statuslabelProgress";
            this.statuslabelProgress.Size = new System.Drawing.Size(40, 19);
            this.statuslabelProgress.Text = "0 %";
            this.statuslabelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusProgressBar1
            // 
            this.statusProgressBar1.AutoSize = false;
            this.statusProgressBar1.Name = "statusProgressBar1";
            this.statusProgressBar1.Size = new System.Drawing.Size(140, 18);
            // 
            // metroPanel2
            // 
            this.metroPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroPanel2.Controls.Add(this.buttonStop);
            this.metroPanel2.Controls.Add(this.groupBoxWrite);
            this.metroPanel2.Controls.Add(this.groupBoxRead);
            this.metroPanel2.Controls.Add(this.groupBoxDrive);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(589, 0);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(368, 516);
            this.metroPanel2.TabIndex = 10;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.Enabled = false;
            this.buttonStop.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.Location = new System.Drawing.Point(7, 489);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(353, 23);
            this.buttonStop.TabIndex = 16;
            this.buttonStop.Text = "CANCEL";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // groupBoxWrite
            // 
            this.groupBoxWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWrite.Controls.Add(this.buttonLoadPartitions);
            this.groupBoxWrite.Controls.Add(this.checkBoxloadpartition);
            this.groupBoxWrite.Controls.Add(this.numericLengthWrite);
            this.groupBoxWrite.Controls.Add(this.buttonWritePartition);
            this.groupBoxWrite.Controls.Add(this.numericStartWrite);
            this.groupBoxWrite.Controls.Add(this.buttonWriteManual);
            this.groupBoxWrite.Controls.Add(this.textBoxFileName);
            this.groupBoxWrite.Controls.Add(this.label6);
            this.groupBoxWrite.Controls.Add(this.buttonbrowse);
            this.groupBoxWrite.Controls.Add(this.comboBox2);
            this.groupBoxWrite.Controls.Add(this.label8);
            this.groupBoxWrite.Controls.Add(this.label7);
            this.groupBoxWrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxWrite.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxWrite.Location = new System.Drawing.Point(186, 193);
            this.groupBoxWrite.Name = "groupBoxWrite";
            this.groupBoxWrite.Size = new System.Drawing.Size(180, 289);
            this.groupBoxWrite.TabIndex = 16;
            this.groupBoxWrite.TabStop = false;
            this.groupBoxWrite.Text = "Write";
            // 
            // buttonLoadPartitions
            // 
            this.buttonLoadPartitions.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadPartitions.Location = new System.Drawing.Point(4, 54);
            this.buttonLoadPartitions.Name = "buttonLoadPartitions";
            this.buttonLoadPartitions.Size = new System.Drawing.Size(170, 23);
            this.buttonLoadPartitions.TabIndex = 23;
            this.buttonLoadPartitions.Text = "Load Partition File";
            this.buttonLoadPartitions.UseVisualStyleBackColor = true;
            this.buttonLoadPartitions.Click += new System.EventHandler(this.buttonLoadPartitions_Click);
            // 
            // checkBoxloadpartition
            // 
            this.checkBoxloadpartition.AutoSize = true;
            this.checkBoxloadpartition.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxloadpartition.Location = new System.Drawing.Point(8, 83);
            this.checkBoxloadpartition.Name = "checkBoxloadpartition";
            this.checkBoxloadpartition.Size = new System.Drawing.Size(136, 16);
            this.checkBoxloadpartition.TabIndex = 15;
            this.checkBoxloadpartition.Text = "Show Partition Gaps";
            this.checkBoxloadpartition.UseVisualStyleBackColor = true;
            this.checkBoxloadpartition.CheckedChanged += new System.EventHandler(this.checkBoxloadpartition_CheckedChanged);
            // 
            // numericLengthWrite
            // 
            this.numericLengthWrite.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericLengthWrite.Location = new System.Drawing.Point(4, 229);
            this.numericLengthWrite.MaxLength = 16;
            this.numericLengthWrite.Name = "numericLengthWrite";
            this.numericLengthWrite.Size = new System.Drawing.Size(170, 20);
            this.numericLengthWrite.TabIndex = 22;
            this.numericLengthWrite.Text = "0000000000000000";
            this.numericLengthWrite.TextChanged += new System.EventHandler(this.numericLengthWrite_TextChanged);
            // 
            // buttonWritePartition
            // 
            this.buttonWritePartition.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonWritePartition.Location = new System.Drawing.Point(4, 106);
            this.buttonWritePartition.Name = "buttonWritePartition";
            this.buttonWritePartition.Size = new System.Drawing.Size(170, 23);
            this.buttonWritePartition.TabIndex = 7;
            this.buttonWritePartition.Text = "Write Selected Partitions";
            this.buttonWritePartition.UseVisualStyleBackColor = true;
            this.buttonWritePartition.Click += new System.EventHandler(this.buttonWritePartition_Click);
            // 
            // numericStartWrite
            // 
            this.numericStartWrite.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericStartWrite.Location = new System.Drawing.Point(4, 187);
            this.numericStartWrite.MaxLength = 16;
            this.numericStartWrite.Name = "numericStartWrite";
            this.numericStartWrite.Size = new System.Drawing.Size(170, 20);
            this.numericStartWrite.TabIndex = 21;
            this.numericStartWrite.Text = "0000000000000000";
            this.numericStartWrite.TextChanged += new System.EventHandler(this.numericStartWrite_TextChanged);
            // 
            // buttonWriteManual
            // 
            this.buttonWriteManual.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonWriteManual.Location = new System.Drawing.Point(3, 257);
            this.buttonWriteManual.Name = "buttonWriteManual";
            this.buttonWriteManual.Size = new System.Drawing.Size(171, 23);
            this.buttonWriteManual.TabIndex = 20;
            this.buttonWriteManual.Text = "Write";
            this.buttonWriteManual.UseVisualStyleBackColor = true;
            this.buttonWriteManual.Click += new System.EventHandler(this.buttonWriteManual_Click);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFileName.Location = new System.Drawing.Point(4, 24);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(109, 20);
            this.textBoxFileName.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "Presets";
            // 
            // buttonbrowse
            // 
            this.buttonbrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonbrowse.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonbrowse.Location = new System.Drawing.Point(119, 22);
            this.buttonbrowse.Name = "buttonbrowse";
            this.buttonbrowse.Size = new System.Drawing.Size(55, 23);
            this.buttonbrowse.TabIndex = 6;
            this.buttonbrowse.Text = "Browse";
            this.buttonbrowse.UseVisualStyleBackColor = true;
            this.buttonbrowse.Click += new System.EventHandler(this.buttonbrowse_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "128 KB",
            "512 KB",
            "1 MB",
            "8 MB",
            "16 MB",
            "32 MB",
            "64 MB",
            "128 MB",
            "256 MB",
            "512 MB",
            "1 GB",
            "4 GB",
            "8 GB",
            "16 GB",
            "32 GB",
            "64 GB",
            "128 GB"});
            this.comboBox2.Location = new System.Drawing.Point(54, 139);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 18;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "Start Address";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(2, 214);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "Length";
            // 
            // groupBoxRead
            // 
            this.groupBoxRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxRead.Controls.Add(this.comboBox1);
            this.groupBoxRead.Controls.Add(this.label5);
            this.groupBoxRead.Controls.Add(this.buttonReadPartitionOnly);
            this.groupBoxRead.Controls.Add(this.checkBoxreadpartisi);
            this.groupBoxRead.Controls.Add(this.numericLengthRead);
            this.groupBoxRead.Controls.Add(this.numericStartRead);
            this.groupBoxRead.Controls.Add(this.buttonReadManual);
            this.groupBoxRead.Controls.Add(this.buttonReadSelected);
            this.groupBoxRead.Controls.Add(this.label2);
            this.groupBoxRead.Controls.Add(this.label1);
            this.groupBoxRead.Controls.Add(this.buttonReadFull);
            this.groupBoxRead.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRead.Location = new System.Drawing.Point(3, 193);
            this.groupBoxRead.Name = "groupBoxRead";
            this.groupBoxRead.Size = new System.Drawing.Size(180, 289);
            this.groupBoxRead.TabIndex = 15;
            this.groupBoxRead.TabStop = false;
            this.groupBoxRead.Text = "Read";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "128 KB",
            "512 KB",
            "1 MB",
            "8 MB",
            "16 MB",
            "32 MB",
            "64 MB",
            "128 MB",
            "256 MB",
            "512 MB",
            "1 GB",
            "4 GB",
            "8 GB",
            "16 GB",
            "32 GB",
            "64 GB",
            "128 GB"});
            this.comboBox1.Location = new System.Drawing.Point(55, 139);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(119, 20);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "Presets";
            // 
            // buttonReadPartitionOnly
            // 
            this.buttonReadPartitionOnly.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReadPartitionOnly.Location = new System.Drawing.Point(4, 24);
            this.buttonReadPartitionOnly.Name = "buttonReadPartitionOnly";
            this.buttonReadPartitionOnly.Size = new System.Drawing.Size(170, 23);
            this.buttonReadPartitionOnly.TabIndex = 0;
            this.buttonReadPartitionOnly.Text = "Load Partition Emmc";
            this.buttonReadPartitionOnly.UseVisualStyleBackColor = true;
            this.buttonReadPartitionOnly.Click += new System.EventHandler(this.buttonReadPartitionOnly_Click);
            // 
            // checkBoxreadpartisi
            // 
            this.checkBoxreadpartisi.AutoSize = true;
            this.checkBoxreadpartisi.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxreadpartisi.Location = new System.Drawing.Point(4, 83);
            this.checkBoxreadpartisi.Name = "checkBoxreadpartisi";
            this.checkBoxreadpartisi.Size = new System.Drawing.Size(136, 16);
            this.checkBoxreadpartisi.TabIndex = 14;
            this.checkBoxreadpartisi.Text = "Show Partition Gaps";
            this.checkBoxreadpartisi.UseVisualStyleBackColor = true;
            this.checkBoxreadpartisi.CheckedChanged += new System.EventHandler(this.checkBoxreadpartisi_CheckedChanged);
            // 
            // numericLengthRead
            // 
            this.numericLengthRead.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericLengthRead.Location = new System.Drawing.Point(4, 229);
            this.numericLengthRead.MaxLength = 16;
            this.numericLengthRead.Name = "numericLengthRead";
            this.numericLengthRead.Size = new System.Drawing.Size(170, 20);
            this.numericLengthRead.TabIndex = 13;
            this.numericLengthRead.Text = "0000000000000000";
            this.numericLengthRead.TextChanged += new System.EventHandler(this.numericLengthRead_TextChanged);
            // 
            // numericStartRead
            // 
            this.numericStartRead.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericStartRead.Location = new System.Drawing.Point(4, 187);
            this.numericStartRead.MaxLength = 16;
            this.numericStartRead.Name = "numericStartRead";
            this.numericStartRead.Size = new System.Drawing.Size(170, 20);
            this.numericStartRead.TabIndex = 12;
            this.numericStartRead.Text = "0000000000000000";
            this.numericStartRead.TextChanged += new System.EventHandler(this.numericStartRead_TextChanged);
            // 
            // buttonReadManual
            // 
            this.buttonReadManual.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReadManual.Location = new System.Drawing.Point(4, 257);
            this.buttonReadManual.Name = "buttonReadManual";
            this.buttonReadManual.Size = new System.Drawing.Size(170, 23);
            this.buttonReadManual.TabIndex = 11;
            this.buttonReadManual.Text = "Read";
            this.buttonReadManual.UseVisualStyleBackColor = true;
            this.buttonReadManual.Click += new System.EventHandler(this.buttonReadManual_Click);
            // 
            // buttonReadSelected
            // 
            this.buttonReadSelected.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReadSelected.Location = new System.Drawing.Point(4, 106);
            this.buttonReadSelected.Name = "buttonReadSelected";
            this.buttonReadSelected.Size = new System.Drawing.Size(170, 23);
            this.buttonReadSelected.TabIndex = 8;
            this.buttonReadSelected.Text = "Read Selected Partitions";
            this.buttonReadSelected.UseVisualStyleBackColor = true;
            this.buttonReadSelected.Click += new System.EventHandler(this.buttonReadSelected_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Length";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start Address";
            // 
            // buttonReadFull
            // 
            this.buttonReadFull.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReadFull.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonReadFull.Location = new System.Drawing.Point(4, 54);
            this.buttonReadFull.Name = "buttonReadFull";
            this.buttonReadFull.Size = new System.Drawing.Size(170, 23);
            this.buttonReadFull.TabIndex = 1;
            this.buttonReadFull.Text = "Read Full Image";
            this.buttonReadFull.UseVisualStyleBackColor = true;
            this.buttonReadFull.Click += new System.EventHandler(this.buttonReadFull_Click);
            // 
            // groupBoxDrive
            // 
            this.groupBoxDrive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDrive.Controls.Add(this.comboBoxDrive);
            this.groupBoxDrive.Controls.Add(this.buttonrefreshDrive);
            this.groupBoxDrive.Controls.Add(this.Log1);
            this.groupBoxDrive.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDrive.Location = new System.Drawing.Point(3, 0);
            this.groupBoxDrive.Name = "groupBoxDrive";
            this.groupBoxDrive.Size = new System.Drawing.Size(363, 187);
            this.groupBoxDrive.TabIndex = 14;
            this.groupBoxDrive.TabStop = false;
            this.groupBoxDrive.Text = "Drive";
            // 
            // comboBoxDrive
            // 
            this.comboBoxDrive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDrive.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDrive.FormattingEnabled = true;
            this.comboBoxDrive.Location = new System.Drawing.Point(6, 17);
            this.comboBoxDrive.Name = "comboBoxDrive";
            this.comboBoxDrive.Size = new System.Drawing.Size(246, 20);
            this.comboBoxDrive.TabIndex = 10;
            this.comboBoxDrive.SelectedIndexChanged += new System.EventHandler(this.comboBoxDrive_SelectedIndexChanged);
            // 
            // buttonrefreshDrive
            // 
            this.buttonrefreshDrive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonrefreshDrive.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonrefreshDrive.Location = new System.Drawing.Point(258, 14);
            this.buttonrefreshDrive.Name = "buttonrefreshDrive";
            this.buttonrefreshDrive.Size = new System.Drawing.Size(99, 25);
            this.buttonrefreshDrive.TabIndex = 12;
            this.buttonrefreshDrive.Text = "Refresh";
            this.buttonrefreshDrive.UseVisualStyleBackColor = true;
            this.buttonrefreshDrive.Click += new System.EventHandler(this.buttonrefreshDrive_Click);
            // 
            // Log1
            // 
            this.Log1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Log1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Log1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Log1.Location = new System.Drawing.Point(6, 45);
            this.Log1.Name = "Log1";
            this.Log1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.Log1.Size = new System.Drawing.Size(351, 139);
            this.Log1.TabIndex = 0;
            this.Log1.Text = "";
            this.Log1.TextChanged += new System.EventHandler(this.Log1_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.choose,
            this.id,
            this.name,
            this.startaddress,
            this.endaddress,
            this.length});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 18;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(589, 516);
            this.dataGridView1.TabIndex = 9;
            // 
            // choose
            // 
            this.choose.HeaderText = "";
            this.choose.Name = "choose";
            this.choose.Width = 40;
            // 
            // id
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.id.DefaultCellStyle = dataGridViewCellStyle1;
            this.id.HeaderText = "#";
            this.id.Name = "id";
            this.id.Width = 25;
            // 
            // name
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.name.DefaultCellStyle = dataGridViewCellStyle2;
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            // 
            // startaddress
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = null;
            this.startaddress.DefaultCellStyle = dataGridViewCellStyle3;
            this.startaddress.HeaderText = "Start Address";
            this.startaddress.Name = "startaddress";
            this.startaddress.Width = 140;
            // 
            // endaddress
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = null;
            this.endaddress.DefaultCellStyle = dataGridViewCellStyle4;
            this.endaddress.HeaderText = "End Address";
            this.endaddress.Name = "endaddress";
            this.endaddress.Width = 140;
            // 
            // length
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.length.DefaultCellStyle = dataGridViewCellStyle5;
            this.length.HeaderText = "Length (bytes)";
            this.length.Name = "length";
            this.length.Width = 140;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "File Image";
            this.openFileDialog1.Filter = "Semua File|*.*|File Image|*.img|File Bin|*.bin";
            // 
            // bgworkerReadAddressPartitions
            // 
            this.bgworkerReadAddressPartitions.WorkerReportsProgress = true;
            this.bgworkerReadAddressPartitions.WorkerSupportsCancellation = true;
            this.bgworkerReadAddressPartitions.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworkerReadAddressPartitions_DoWork);
            this.bgworkerReadAddressPartitions.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgworkerReadAddressPartitions_ProgressChanged);
            this.bgworkerReadAddressPartitions.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgworkerReadAddressPartitions_RunWorkerCompleted);
            // 
            // bgworkerWriteAddressPartitions
            // 
            this.bgworkerWriteAddressPartitions.WorkerReportsProgress = true;
            this.bgworkerWriteAddressPartitions.WorkerSupportsCancellation = true;
            this.bgworkerWriteAddressPartitions.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworkerWriteAddressPartitions_DoWork);
            this.bgworkerWriteAddressPartitions.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgworkerWriteAddressPartitions_ProgressChanged);
            this.bgworkerWriteAddressPartitions.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgworkerWriteAddressPartitions_RunWorkerCompleted);
            // 
            // bgworkerReadPartition
            // 
            this.bgworkerReadPartition.WorkerReportsProgress = true;
            this.bgworkerReadPartition.WorkerSupportsCancellation = true;
            this.bgworkerReadPartition.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworkerReadPartition_DoWork);
            this.bgworkerReadPartition.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgworkerReadPartition_ProgressChanged);
            this.bgworkerReadPartition.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgworkerReadPartition_RunWorkerCompleted);
            // 
            // bgworkerReadFull
            // 
            this.bgworkerReadFull.WorkerReportsProgress = true;
            this.bgworkerReadFull.WorkerSupportsCancellation = true;
            this.bgworkerReadFull.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworkerReadFull_DoWork);
            this.bgworkerReadFull.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgworkerReadFull_ProgressChanged);
            this.bgworkerReadFull.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgworkerReadFull_RunWorkerCompleted);
            // 
            // bgworkerReadAddress
            // 
            this.bgworkerReadAddress.WorkerReportsProgress = true;
            this.bgworkerReadAddress.WorkerSupportsCancellation = true;
            this.bgworkerReadAddress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworkerReadAddress_DoWork);
            this.bgworkerReadAddress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgworkerReadAddress_ProgressChanged);
            this.bgworkerReadAddress.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgworkerReadAddress_RunWorkerCompleted);
            // 
            // bgworkerWriteAddress
            // 
            this.bgworkerWriteAddress.WorkerReportsProgress = true;
            this.bgworkerWriteAddress.WorkerSupportsCancellation = true;
            this.bgworkerWriteAddress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgworkerWriteAddress_DoWork);
            this.bgworkerWriteAddress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgworkerWriteAddress_ProgressChanged);
            this.bgworkerWriteAddress.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgworkerWriteAddress_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 620);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Resizable = false;
            this.Text = "PELITABANGSA EMMC RAW TOOL";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.groupBoxWrite.ResumeLayout(false);
            this.groupBoxWrite.PerformLayout();
            this.groupBoxRead.ResumeLayout(false);
            this.groupBoxRead.PerformLayout();
            this.groupBoxDrive.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox log;
        private System.Windows.Forms.GroupBox groupBoxDrive;
        private System.Windows.Forms.ComboBox comboBoxDrive;
        private System.Windows.Forms.Button buttonrefreshDrive;
        private System.Windows.Forms.RichTextBox Log1;
        private System.Windows.Forms.GroupBox groupBoxRead;
        private System.Windows.Forms.Button buttonReadPartitionOnly;
        private System.Windows.Forms.CheckBox checkBoxreadpartisi;
        private System.Windows.Forms.TextBox numericLengthRead;
        private System.Windows.Forms.TextBox numericStartRead;
        private System.Windows.Forms.Button buttonReadManual;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonReadSelected;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReadFull;
        private System.Windows.Forms.GroupBox groupBoxWrite;
        private System.Windows.Forms.Button buttonLoadPartitions;
        private System.Windows.Forms.CheckBox checkBoxloadpartition;
        private System.Windows.Forms.TextBox numericLengthWrite;
        private System.Windows.Forms.Button buttonWritePartition;
        private System.Windows.Forms.TextBox numericStartWrite;
        private System.Windows.Forms.Button buttonWriteManual;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonbrowse;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statuslabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statuslabelProses;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel statuslabelPartisi;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel statuslabelSpeed;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel statuslabelProgress;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.ComponentModel.BackgroundWorker bgworkerReadAddressPartitions;
        private System.ComponentModel.BackgroundWorker bgworkerWriteAddressPartitions;
        private System.ComponentModel.BackgroundWorker bgworkerReadPartition;
        private System.ComponentModel.BackgroundWorker bgworkerReadFull;
        private System.ComponentModel.BackgroundWorker bgworkerReadAddress;
        private System.ComponentModel.BackgroundWorker bgworkerWriteAddress;
        private System.Windows.Forms.CheckBox cbpartisi;
        private System.Windows.Forms.DataGridViewCheckBoxColumn choose;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn startaddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn endaddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn length;
    }
}

