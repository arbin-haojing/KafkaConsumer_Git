
namespace KafkaConsumer
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtTestID = new System.Windows.Forms.TextBox();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.txtTestName = new System.Windows.Forms.TextBox();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.txtFileSize = new System.Windows.Forms.TextBox();
            this.cmbMessageFormat = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtGroupId = new System.Windows.Forms.TextBox();
            this.txtBasicAuthPassword = new System.Windows.Forms.TextBox();
            this.txtBasicAuthUsername = new System.Windows.Forms.TextBox();
            this.txtSchemaRegisterServer = new System.Windows.Forms.TextBox();
            this.txtSaslPassword = new System.Windows.Forms.TextBox();
            this.txtSaslUsername = new System.Windows.Forms.TextBox();
            this.txtBootstrapServer = new System.Windows.Forms.TextBox();
            this.chkConsumerMonitor = new System.Windows.Forms.CheckBox();
            this.chkTestID = new System.Windows.Forms.CheckBox();
            this.chkChannel = new System.Windows.Forms.CheckBox();
            this.chkTestName = new System.Windows.Forms.CheckBox();
            this.chkSerialNumber = new System.Windows.Forms.CheckBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.chkConfluentCloud = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMessageFormat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1952, 578);
            this.dataGridView1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1952, 1178);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1952, 1178);
            this.splitContainer1.SplitterDistance = 594;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnStart, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.txtTestID, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.txtChannel, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.txtTestName, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.txtSerialNumber, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtFileSize, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.cmbMessageFormat, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtGroupId, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtBasicAuthPassword, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtBasicAuthUsername, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtSchemaRegisterServer, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtSaslPassword, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtSaslUsername, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtBootstrapServer, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkConsumerMonitor, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.chkTestID, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.chkChannel, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.chkTestName, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.chkSerialNumber, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.labelControl9, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelControl6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelControl5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelControl8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelControl4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkConfluentCloud, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.672894F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.672894F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.672894F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.672894F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.672894F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.672894F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.672894F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.672894F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.672894F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.606167F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666222F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666222F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666222F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666222F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.672888F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1952, 594);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Location = new System.Drawing.Point(298, 550);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(1650, 40);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start Consumer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtTestID
            // 
            this.txtTestID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTestID.Location = new System.Drawing.Point(298, 511);
            this.txtTestID.Margin = new System.Windows.Forms.Padding(4);
            this.txtTestID.Name = "txtTestID";
            this.txtTestID.Size = new System.Drawing.Size(1650, 28);
            this.txtTestID.TabIndex = 8;
            // 
            // txtChannel
            // 
            this.txtChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChannel.Location = new System.Drawing.Point(298, 472);
            this.txtChannel.Margin = new System.Windows.Forms.Padding(4);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(1650, 28);
            this.txtChannel.TabIndex = 9;
            // 
            // txtTestName
            // 
            this.txtTestName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTestName.Location = new System.Drawing.Point(298, 433);
            this.txtTestName.Margin = new System.Windows.Forms.Padding(4);
            this.txtTestName.Name = "txtTestName";
            this.txtTestName.Size = new System.Drawing.Size(1650, 28);
            this.txtTestName.TabIndex = 9;
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSerialNumber.Location = new System.Drawing.Point(298, 394);
            this.txtSerialNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(1650, 28);
            this.txtSerialNumber.TabIndex = 6;
            // 
            // txtFileSize
            // 
            this.txtFileSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileSize.Location = new System.Drawing.Point(298, 355);
            this.txtFileSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtFileSize.Name = "txtFileSize";
            this.txtFileSize.Size = new System.Drawing.Size(1650, 28);
            this.txtFileSize.TabIndex = 37;
            this.txtFileSize.Text = "100";
            // 
            // cmbMessageFormat
            // 
            this.cmbMessageFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMessageFormat.EditValue = "AVRO";
            this.cmbMessageFormat.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbMessageFormat.Location = new System.Drawing.Point(297, 316);
            this.cmbMessageFormat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbMessageFormat.Name = "cmbMessageFormat";
            this.cmbMessageFormat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMessageFormat.Properties.DropDownRows = 2;
            this.cmbMessageFormat.Properties.Items.AddRange(new object[] {
            "AVRO",
            "JSON"});
            this.cmbMessageFormat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbMessageFormat.Size = new System.Drawing.Size(1652, 28);
            this.cmbMessageFormat.TabIndex = 34;
            // 
            // txtGroupId
            // 
            this.txtGroupId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGroupId.Location = new System.Drawing.Point(298, 277);
            this.txtGroupId.Margin = new System.Windows.Forms.Padding(4);
            this.txtGroupId.Name = "txtGroupId";
            this.txtGroupId.ReadOnly = true;
            this.txtGroupId.Size = new System.Drawing.Size(1650, 28);
            this.txtGroupId.TabIndex = 7;
            // 
            // txtBasicAuthPassword
            // 
            this.txtBasicAuthPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBasicAuthPassword.Enabled = false;
            this.txtBasicAuthPassword.Location = new System.Drawing.Point(298, 238);
            this.txtBasicAuthPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtBasicAuthPassword.Name = "txtBasicAuthPassword";
            this.txtBasicAuthPassword.Size = new System.Drawing.Size(1650, 28);
            this.txtBasicAuthPassword.TabIndex = 41;
            // 
            // txtBasicAuthUsername
            // 
            this.txtBasicAuthUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBasicAuthUsername.Enabled = false;
            this.txtBasicAuthUsername.Location = new System.Drawing.Point(298, 199);
            this.txtBasicAuthUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txtBasicAuthUsername.Name = "txtBasicAuthUsername";
            this.txtBasicAuthUsername.Size = new System.Drawing.Size(1650, 28);
            this.txtBasicAuthUsername.TabIndex = 39;
            // 
            // txtSchemaRegisterServer
            // 
            this.txtSchemaRegisterServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSchemaRegisterServer.Location = new System.Drawing.Point(298, 160);
            this.txtSchemaRegisterServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtSchemaRegisterServer.Name = "txtSchemaRegisterServer";
            this.txtSchemaRegisterServer.Size = new System.Drawing.Size(1650, 28);
            this.txtSchemaRegisterServer.TabIndex = 5;
            // 
            // txtSaslPassword
            // 
            this.txtSaslPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSaslPassword.Enabled = false;
            this.txtSaslPassword.Location = new System.Drawing.Point(298, 121);
            this.txtSaslPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtSaslPassword.Name = "txtSaslPassword";
            this.txtSaslPassword.Size = new System.Drawing.Size(1650, 28);
            this.txtSaslPassword.TabIndex = 38;
            // 
            // txtSaslUsername
            // 
            this.txtSaslUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSaslUsername.Enabled = false;
            this.txtSaslUsername.Location = new System.Drawing.Point(298, 82);
            this.txtSaslUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txtSaslUsername.Name = "txtSaslUsername";
            this.txtSaslUsername.Size = new System.Drawing.Size(1650, 28);
            this.txtSaslUsername.TabIndex = 40;
            // 
            // txtBootstrapServer
            // 
            this.txtBootstrapServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBootstrapServer.Location = new System.Drawing.Point(298, 43);
            this.txtBootstrapServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtBootstrapServer.Name = "txtBootstrapServer";
            this.txtBootstrapServer.Size = new System.Drawing.Size(1650, 28);
            this.txtBootstrapServer.TabIndex = 4;
            // 
            // chkConsumerMonitor
            // 
            this.chkConsumerMonitor.AutoSize = true;
            this.chkConsumerMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkConsumerMonitor.Location = new System.Drawing.Point(4, 550);
            this.chkConsumerMonitor.Margin = new System.Windows.Forms.Padding(4);
            this.chkConsumerMonitor.Name = "chkConsumerMonitor";
            this.chkConsumerMonitor.Size = new System.Drawing.Size(286, 40);
            this.chkConsumerMonitor.TabIndex = 9;
            this.chkConsumerMonitor.Text = "Export ChannelMonitorData";
            this.chkConsumerMonitor.UseVisualStyleBackColor = true;
            // 
            // chkTestID
            // 
            this.chkTestID.AutoSize = true;
            this.chkTestID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkTestID.Location = new System.Drawing.Point(4, 511);
            this.chkTestID.Margin = new System.Windows.Forms.Padding(4);
            this.chkTestID.Name = "chkTestID";
            this.chkTestID.Size = new System.Drawing.Size(286, 31);
            this.chkTestID.TabIndex = 10;
            this.chkTestID.Text = "TestID";
            this.chkTestID.UseVisualStyleBackColor = true;
            this.chkTestID.CheckedChanged += new System.EventHandler(this.chkTestID_CheckedChanged);
            // 
            // chkChannel
            // 
            this.chkChannel.AutoSize = true;
            this.chkChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkChannel.Location = new System.Drawing.Point(4, 472);
            this.chkChannel.Margin = new System.Windows.Forms.Padding(4);
            this.chkChannel.Name = "chkChannel";
            this.chkChannel.Size = new System.Drawing.Size(286, 31);
            this.chkChannel.TabIndex = 10;
            this.chkChannel.Text = "Channel";
            this.chkChannel.UseVisualStyleBackColor = true;
            this.chkChannel.CheckedChanged += new System.EventHandler(this.chkChannel_CheckedChanged);
            // 
            // chkTestName
            // 
            this.chkTestName.AutoSize = true;
            this.chkTestName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkTestName.Location = new System.Drawing.Point(4, 433);
            this.chkTestName.Margin = new System.Windows.Forms.Padding(4);
            this.chkTestName.Name = "chkTestName";
            this.chkTestName.Size = new System.Drawing.Size(286, 31);
            this.chkTestName.TabIndex = 10;
            this.chkTestName.Text = "TestName（ChannelDataTopic）";
            this.chkTestName.UseVisualStyleBackColor = true;
            this.chkTestName.CheckedChanged += new System.EventHandler(this.chkTestName_CheckedChanged);
            // 
            // chkSerialNumber
            // 
            this.chkSerialNumber.AutoSize = true;
            this.chkSerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSerialNumber.Location = new System.Drawing.Point(4, 394);
            this.chkSerialNumber.Margin = new System.Windows.Forms.Padding(4);
            this.chkSerialNumber.Name = "chkSerialNumber";
            this.chkSerialNumber.Size = new System.Drawing.Size(286, 31);
            this.chkSerialNumber.TabIndex = 4;
            this.chkSerialNumber.Text = "SerialNumber";
            this.chkSerialNumber.UseVisualStyleBackColor = true;
            this.chkSerialNumber.CheckedChanged += new System.EventHandler(this.chkSerialNumber_CheckedChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl2.Location = new System.Drawing.Point(4, 355);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(286, 31);
            this.labelControl2.TabIndex = 36;
            this.labelControl2.Text = "Export File Size (M)";
            // 
            // labelControl1
            // 
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(4, 316);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(286, 31);
            this.labelControl1.TabIndex = 35;
            this.labelControl1.Text = "Message Format";
            // 
            // labelControl9
            // 
            this.labelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl9.Location = new System.Drawing.Point(4, 277);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(286, 31);
            this.labelControl9.TabIndex = 48;
            this.labelControl9.Text = "GroupId";
            // 
            // labelControl6
            // 
            this.labelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl6.Location = new System.Drawing.Point(4, 238);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(286, 31);
            this.labelControl6.TabIndex = 45;
            this.labelControl6.Text = "Basic Auth Password";
            // 
            // labelControl5
            // 
            this.labelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl5.Location = new System.Drawing.Point(4, 199);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(286, 31);
            this.labelControl5.TabIndex = 44;
            this.labelControl5.Text = "Basic Auth Username";
            // 
            // labelControl8
            // 
            this.labelControl8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl8.Location = new System.Drawing.Point(4, 160);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(286, 31);
            this.labelControl8.TabIndex = 47;
            this.labelControl8.Text = "Schema Register Server";
            // 
            // labelControl4
            // 
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl4.Location = new System.Drawing.Point(4, 121);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(286, 31);
            this.labelControl4.TabIndex = 43;
            this.labelControl4.Text = "Sasl Password";
            // 
            // labelControl3
            // 
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl3.Location = new System.Drawing.Point(4, 82);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(286, 31);
            this.labelControl3.TabIndex = 42;
            this.labelControl3.Text = "Sasl Username";
            // 
            // labelControl7
            // 
            this.labelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl7.Location = new System.Drawing.Point(4, 43);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(286, 31);
            this.labelControl7.TabIndex = 46;
            this.labelControl7.Text = "Bootstrap Server";
            // 
            // chkConfluentCloud
            // 
            this.chkConfluentCloud.AutoSize = true;
            this.chkConfluentCloud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkConfluentCloud.Location = new System.Drawing.Point(4, 4);
            this.chkConfluentCloud.Margin = new System.Windows.Forms.Padding(4);
            this.chkConfluentCloud.Name = "chkConfluentCloud";
            this.chkConfluentCloud.Size = new System.Drawing.Size(286, 31);
            this.chkConfluentCloud.TabIndex = 49;
            this.chkConfluentCloud.Text = "Confluent Cloud";
            this.chkConfluentCloud.UseVisualStyleBackColor = true;
            this.chkConfluentCloud.CheckedChanged += new System.EventHandler(this.chkConfluentCloud_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1952, 1178);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Kafka Consumer (V4.0.6 Cloud)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMessageFormat.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtSchemaRegisterServer;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.TextBox txtGroupId;
        private System.Windows.Forms.TextBox txtBootstrapServer;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox chkSerialNumber;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkConsumerMonitor;
        private System.Windows.Forms.CheckBox chkTestName;
        private System.Windows.Forms.CheckBox chkChannel;
        private System.Windows.Forms.CheckBox chkTestID;
        private System.Windows.Forms.TextBox txtTestName;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.TextBox txtTestID;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMessageFormat;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.TextBox txtFileSize;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.TextBox txtBasicAuthPassword;
        private System.Windows.Forms.TextBox txtBasicAuthUsername;
        private System.Windows.Forms.TextBox txtSaslPassword;
        private System.Windows.Forms.TextBox txtSaslUsername;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.Windows.Forms.CheckBox chkConfluentCloud;
    }
}