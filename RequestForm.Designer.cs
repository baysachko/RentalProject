namespace WinFormsApp1
{
    partial class RequestForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            requestid = new TextBox();
            description = new TextBox();
            residentid = new TextBox();
            serviceid = new TextBox();
            dataGridView1 = new DataGridView();
            clear = new Button();
            update = new Button();
            requestdate = new DateTimePicker();
            status = new ComboBox();
            residentname = new TextBox();
            label7 = new Label();
            servicename = new TextBox();
            label8 = new Label();
            insertid = new Button();
            searchbox = new TextBox();
            label9 = new Label();
            delete = new Button();
            back = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(43, 22);
            label1.Name = "label1";
            label1.Size = new Size(88, 21);
            label1.TabIndex = 0;
            label1.Text = "RequestID :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(43, 69);
            label2.Name = "label2";
            label2.Size = new Size(105, 21);
            label2.TabIndex = 1;
            label2.Text = "RequestDate :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(43, 117);
            label3.Name = "label3";
            label3.Size = new Size(96, 21);
            label3.TabIndex = 2;
            label3.Text = "Description :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(446, 69);
            label4.Name = "label4";
            label4.Size = new Size(92, 21);
            label4.TabIndex = 3;
            label4.Text = "ResidentID :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(446, 22);
            label5.Name = "label5";
            label5.Size = new Size(59, 21);
            label5.TabIndex = 4;
            label5.Text = "Status :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(446, 117);
            label6.Name = "label6";
            label6.Size = new Size(82, 21);
            label6.TabIndex = 5;
            label6.Text = "ServiceID :";
            // 
            // requestid
            // 
            requestid.Location = new Point(158, 24);
            requestid.Name = "requestid";
            requestid.ReadOnly = true;
            requestid.Size = new Size(262, 23);
            requestid.TabIndex = 6;
            // 
            // description
            // 
            description.Location = new Point(158, 119);
            description.Name = "description";
            description.PlaceholderText = "Input Description";
            description.Size = new Size(262, 23);
            description.TabIndex = 8;
            description.TextAlign = HorizontalAlignment.Center;
            // 
            // residentid
            // 
            residentid.AccessibleDescription = "";
            residentid.Location = new Point(540, 67);
            residentid.Name = "residentid";
            residentid.PlaceholderText = "Input ResidentID To Display Resident Name";
            residentid.Size = new Size(248, 23);
            residentid.TabIndex = 10;
            residentid.TextAlign = HorizontalAlignment.Center;
            // 
            // serviceid
            // 
            serviceid.Location = new Point(540, 115);
            serviceid.Name = "serviceid";
            serviceid.PlaceholderText = "Input ServiceID To Display Service Name";
            serviceid.Size = new Size(248, 23);
            serviceid.TabIndex = 11;
            serviceid.TextAlign = HorizontalAlignment.Center;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 303);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(776, 241);
            dataGridView1.TabIndex = 12;
            // 
            // clear
            // 
            clear.Font = new Font("Segoe UI", 9F);
            clear.Location = new Point(454, 232);
            clear.Name = "clear";
            clear.Size = new Size(79, 47);
            clear.TabIndex = 38;
            clear.Text = "Clear";
            clear.UseVisualStyleBackColor = true;
            // 
            // update
            // 
            update.Font = new Font("Segoe UI", 9F);
            update.Location = new Point(539, 232);
            update.Name = "update";
            update.Size = new Size(79, 47);
            update.TabIndex = 37;
            update.Text = "Update";
            update.UseVisualStyleBackColor = true;
            // 
            // requestdate
            // 
            requestdate.Location = new Point(158, 69);
            requestdate.Name = "requestdate";
            requestdate.Size = new Size(264, 23);
            requestdate.TabIndex = 39;
            // 
            // status
            // 
            status.FormattingEnabled = true;
            status.Location = new Point(540, 20);
            status.Name = "status";
            status.Size = new Size(248, 23);
            status.TabIndex = 41;
            // 
            // residentname
            // 
            residentname.Location = new Point(172, 171);
            residentname.Name = "residentname";
            residentname.ReadOnly = true;
            residentname.Size = new Size(248, 23);
            residentname.TabIndex = 42;
            residentname.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(43, 173);
            label7.Name = "label7";
            label7.Size = new Size(123, 21);
            label7.TabIndex = 43;
            label7.Text = "Resident Name :";
            // 
            // servicename
            // 
            servicename.Location = new Point(565, 171);
            servicename.Name = "servicename";
            servicename.ReadOnly = true;
            servicename.Size = new Size(223, 23);
            servicename.TabIndex = 44;
            servicename.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.Location = new Point(446, 173);
            label8.Name = "label8";
            label8.Size = new Size(113, 21);
            label8.TabIndex = 45;
            label8.Text = "Service Name :";
            // 
            // insertid
            // 
            insertid.Font = new Font("Segoe UI", 9F);
            insertid.Location = new Point(369, 232);
            insertid.Name = "insertid";
            insertid.Size = new Size(79, 47);
            insertid.TabIndex = 46;
            insertid.Text = "Insert";
            insertid.UseVisualStyleBackColor = true;
            // 
            // searchbox
            // 
            searchbox.Location = new Point(82, 245);
            searchbox.Name = "searchbox";
            searchbox.PlaceholderText = "Search by Status, Name, Service";
            searchbox.Size = new Size(229, 23);
            searchbox.TabIndex = 48;
            searchbox.TextAlign = HorizontalAlignment.Center;
            searchbox.TextChanged += searchbox_TextChanged_1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F);
            label9.Location = new Point(12, 247);
            label9.Name = "label9";
            label9.Size = new Size(64, 21);
            label9.TabIndex = 47;
            label9.Text = "Search :";
            // 
            // delete
            // 
            delete.Font = new Font("Segoe UI", 9F);
            delete.Location = new Point(624, 232);
            delete.Name = "delete";
            delete.Size = new Size(79, 47);
            delete.TabIndex = 49;
            delete.Text = "Delete";
            delete.UseVisualStyleBackColor = true;
            // 
            // back
            // 
            back.Font = new Font("Segoe UI", 9F);
            back.Location = new Point(709, 232);
            back.Name = "back";
            back.Size = new Size(79, 47);
            back.TabIndex = 50;
            back.Text = "Back";
            back.UseVisualStyleBackColor = true;
            // 
            // RequestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(801, 556);
            Controls.Add(back);
            Controls.Add(delete);
            Controls.Add(searchbox);
            Controls.Add(label9);
            Controls.Add(insertid);
            Controls.Add(label8);
            Controls.Add(servicename);
            Controls.Add(label7);
            Controls.Add(residentname);
            Controls.Add(status);
            Controls.Add(requestdate);
            Controls.Add(clear);
            Controls.Add(update);
            Controls.Add(dataGridView1);
            Controls.Add(serviceid);
            Controls.Add(residentid);
            Controls.Add(description);
            Controls.Add(requestid);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RequestForm";
            Text = "RequestForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox requestid;
        private TextBox description;
        private TextBox residentid;
        private TextBox serviceid;
        private DataGridView dataGridView1;
        private Button clear;
        private Button update;
        private DateTimePicker requestdate;
        private ComboBox status;
        private TextBox residentname;
        private Label label7;
        private TextBox servicename;
        private Label label8;
        private Button insertid;
        private TextBox searchbox;
        private Label label9;
        private Button delete;
        private Button back;
    }
}