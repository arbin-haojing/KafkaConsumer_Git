
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
            this.txtGroupId = new System.Windows.Forms.TextBox();
            this.cmbMessageFormat = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtSchemaRegisterServer = new System.Windows.Forms.TextBox();
            this.txtBootstrapServer = new System.Windows.Forms.TextBox();
            this.LabSchemaRegisterServer = new System.Windows.Forms.Label();
            this.labBootstrapServer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtTestID = new System.Windows.Forms.TextBox();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.txtTestName = new System.Windows.Forms.TextBox();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.chkConsumerMonitor = new System.Windows.Forms.CheckBox();
            this.chkTestID = new System.Windows.Forms.CheckBox();
            this.chkChannel = new System.Windows.Forms.CheckBox();
            this.chkTestName = new System.Windows.Forms.CheckBox();
            this.chkSerialNumber = new System.Windows.Forms.CheckBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileSize = new System.Windows.Forms.TextBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
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
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1215, 401);
            this.dataGridView1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1215, 707);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainer1.Size = new System.Drawing.Size(1215, 707);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.labelControl2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtFileSize, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtBootstrapServer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabSchemaRegisterServer, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labBootstrapServer, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtTestID, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtChannel, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtTestName, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtSerialNumber, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtSchemaRegisterServer, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtGroupId, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbMessageFormat, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.chkConsumerMonitor, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.chkTestID, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.chkChannel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.chkTestName, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.chkSerialNumber, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnStart, 1, 9);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.909912F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1215, 302);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtGroupId
            // 
            this.txtGroupId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGroupId.Location = new System.Drawing.Point(201, 63);
            this.txtGroupId.Name = "txtGroupId";
            this.txtGroupId.ReadOnly = true;
            this.txtGroupId.Size = new System.Drawing.Size(1011, 21);
            this.txtGroupId.TabIndex = 7;
            // 
            // cmbMessageFormat
            // 
            this.cmbMessageFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMessageFormat.EditValue = "AVRO";
            this.cmbMessageFormat.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbMessageFormat.Location = new System.Drawing.Point(200, 93);
            this.cmbMessageFormat.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbMessageFormat.Name = "cmbMessageFormat";
            this.cmbMessageFormat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMessageFormat.Properties.DropDownRows = 2;
            this.cmbMessageFormat.Properties.Items.AddRange(new object[] {
            "AVRO",
            "JSON"});
            this.cmbMessageFormat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbMessageFormat.Size = new System.Drawing.Size(1013, 20);
            this.cmbMessageFormat.TabIndex = 34;
            // 
            // txtSchemaRegisterServer
            // 
            this.txtSchemaRegisterServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSchemaRegisterServer.Location = new System.Drawing.Point(201, 33);
            this.txtSchemaRegisterServer.Name = "txtSchemaRegisterServer";
            this.txtSchemaRegisterServer.Size = new System.Drawing.Size(1011, 21);
            this.txtSchemaRegisterServer.TabIndex = 5;
            // 
            // txtBootstrapServer
            // 
            this.txtBootstrapServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBootstrapServer.Location = new System.Drawing.Point(201, 3);
            this.txtBootstrapServer.Name = "txtBootstrapServer";
            this.txtBootstrapServer.Size = new System.Drawing.Size(1011, 21);
            this.txtBootstrapServer.TabIndex = 4;
            // 
            // LabSchemaRegisterServer
            // 
            this.LabSchemaRegisterServer.AutoSize = true;
            this.LabSchemaRegisterServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabSchemaRegisterServer.Location = new System.Drawing.Point(3, 30);
            this.LabSchemaRegisterServer.Name = "LabSchemaRegisterServer";
            this.LabSchemaRegisterServer.Size = new System.Drawing.Size(192, 30);
            this.LabSchemaRegisterServer.TabIndex = 1;
            this.LabSchemaRegisterServer.Text = "Schema Register Server";
            // 
            // labBootstrapServer
            // 
            this.labBootstrapServer.AutoSize = true;
            this.labBootstrapServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labBootstrapServer.Location = new System.Drawing.Point(3, 0);
            this.labBootstrapServer.Name = "labBootstrapServer";
            this.labBootstrapServer.Size = new System.Drawing.Size(192, 30);
            this.labBootstrapServer.TabIndex = 0;
            this.labBootstrapServer.Text = "Bootstrap Server";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 30);
            this.label4.TabIndex = 3;
            this.label4.Text = "GroupId";
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Location = new System.Drawing.Point(201, 273);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(1011, 26);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start Consumer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtTestID
            // 
            this.txtTestID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTestID.Location = new System.Drawing.Point(201, 243);
            this.txtTestID.Name = "txtTestID";
            this.txtTestID.Size = new System.Drawing.Size(1011, 21);
            this.txtTestID.TabIndex = 8;
            // 
            // txtChannel
            // 
            this.txtChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChannel.Location = new System.Drawing.Point(201, 213);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(1011, 21);
            this.txtChannel.TabIndex = 9;
            // 
            // txtTestName
            // 
            this.txtTestName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTestName.Location = new System.Drawing.Point(201, 183);
            this.txtTestName.Name = "txtTestName";
            this.txtTestName.Size = new System.Drawing.Size(1011, 21);
            this.txtTestName.TabIndex = 9;
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSerialNumber.Location = new System.Drawing.Point(201, 153);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(1011, 21);
            this.txtSerialNumber.TabIndex = 6;
            // 
            // chkConsumerMonitor
            // 
            this.chkConsumerMonitor.AutoSize = true;
            this.chkConsumerMonitor.Checked = true;
            this.chkConsumerMonitor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConsumerMonitor.Location = new System.Drawing.Point(3, 273);
            this.chkConsumerMonitor.Name = "chkConsumerMonitor";
            this.chkConsumerMonitor.Size = new System.Drawing.Size(174, 16);
            this.chkConsumerMonitor.TabIndex = 9;
            this.chkConsumerMonitor.Text = "Export ChannelMonitorData";
            this.chkConsumerMonitor.UseVisualStyleBackColor = true;
            // 
            // chkTestID
            // 
            this.chkTestID.AutoSize = true;
            this.chkTestID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkTestID.Location = new System.Drawing.Point(3, 243);
            this.chkTestID.Name = "chkTestID";
            this.chkTestID.Size = new System.Drawing.Size(192, 24);
            this.chkTestID.TabIndex = 10;
            this.chkTestID.Text = "TestID";
            this.chkTestID.UseVisualStyleBackColor = true;
            this.chkTestID.CheckedChanged += new System.EventHandler(this.chkTestID_CheckedChanged);
            // 
            // chkChannel
            // 
            this.chkChannel.AutoSize = true;
            this.chkChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkChannel.Location = new System.Drawing.Point(3, 213);
            this.chkChannel.Name = "chkChannel";
            this.chkChannel.Size = new System.Drawing.Size(192, 24);
            this.chkChannel.TabIndex = 10;
            this.chkChannel.Text = "Channel";
            this.chkChannel.UseVisualStyleBackColor = true;
            this.chkChannel.CheckedChanged += new System.EventHandler(this.chkChannel_CheckedChanged);
            // 
            // chkTestName
            // 
            this.chkTestName.AutoSize = true;
            this.chkTestName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkTestName.Location = new System.Drawing.Point(3, 183);
            this.chkTestName.Name = "chkTestName";
            this.chkTestName.Size = new System.Drawing.Size(192, 24);
            this.chkTestName.TabIndex = 10;
            this.chkTestName.Text = "TestName（ChannelDataTopic）";
            this.chkTestName.UseVisualStyleBackColor = true;
            this.chkTestName.CheckedChanged += new System.EventHandler(this.chkTestName_CheckedChanged);
            // 
            // chkSerialNumber
            // 
            this.chkSerialNumber.AutoSize = true;
            this.chkSerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSerialNumber.Location = new System.Drawing.Point(3, 153);
            this.chkSerialNumber.Name = "chkSerialNumber";
            this.chkSerialNumber.Size = new System.Drawing.Size(192, 24);
            this.chkSerialNumber.TabIndex = 4;
            this.chkSerialNumber.Text = "SerialNumber";
            this.chkSerialNumber.UseVisualStyleBackColor = true;
            this.chkSerialNumber.CheckedChanged += new System.EventHandler(this.chkSerialNumber_CheckedChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(3, 93);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(192, 24);
            this.labelControl1.TabIndex = 35;
            this.labelControl1.Text = "Message Format";
            // 
            // txtFileSize
            // 
            this.txtFileSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileSize.Location = new System.Drawing.Point(201, 123);
            this.txtFileSize.Name = "txtFileSize";
            this.txtFileSize.Size = new System.Drawing.Size(1011, 21);
            this.txtFileSize.TabIndex = 37;
            this.txtFileSize.Text = "100";
            // 
            // labelControl2
            // 
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl2.Location = new System.Drawing.Point(3, 123);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(192, 24);
            this.labelControl2.TabIndex = 36;
            this.labelControl2.Text = "Export File Size (M)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 707);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Kafka Consumer (V2.0.5)";
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkSerialNumber;
        private System.Windows.Forms.Label LabSchemaRegisterServer;
        private System.Windows.Forms.Label labBootstrapServer;
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
    }
}