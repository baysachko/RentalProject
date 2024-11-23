namespace WinFormsApp1
{
    partial class ReservationForm
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
            dataGridView1 = new DataGridView();
            column7 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            clear = new Button();
            update = new Button();
            reservationenddate = new DateTimePicker();
            reservationstartdate = new DateTimePicker();
            reservationdate = new DateTimePicker();
            reservationid = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            reservationstatus = new ComboBox();
            residentid = new TextBox();
            roomid = new TextBox();
            insertid = new Button();
            residentname = new TextBox();
            roomnum = new TextBox();
            delete = new Button();
            back = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { column7, Column1, Column2, Column3, Column4, Column5, Column6 });
            dataGridView1.Location = new Point(29, 267);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(742, 259);
            dataGridView1.TabIndex = 37;
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // column7
            // 
            column7.HeaderText = "ReservationID";
            column7.Name = "column7";
            // 
            // Column1
            // 
            Column1.HeaderText = "ReservationDate";
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            Column2.HeaderText = "StartDate";
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.HeaderText = "EndDate";
            Column3.Name = "Column3";
            // 
            // Column4
            // 
            Column4.HeaderText = "Status";
            Column4.Name = "Column4";
            // 
            // Column5
            // 
            Column5.HeaderText = "ResidentID";
            Column5.Name = "Column5";
            // 
            // Column6
            // 
            Column6.HeaderText = "RoomID";
            Column6.Name = "Column6";
            // 
            // clear
            // 
            clear.Font = new Font("Segoe UI", 9F);
            clear.Location = new Point(679, 60);
            clear.Name = "clear";
            clear.Size = new Size(69, 47);
            clear.TabIndex = 36;
            clear.Text = "Clear";
            clear.UseVisualStyleBackColor = true;
            // 
            // update
            // 
            update.Font = new Font("Segoe UI", 9F);
            update.Location = new Point(679, 109);
            update.Name = "update";
            update.Size = new Size(69, 47);
            update.TabIndex = 35;
            update.Text = "Update";
            update.UseVisualStyleBackColor = true;
            // 
            // reservationenddate
            // 
            reservationenddate.Font = new Font("Segoe UI", 12F);
            reservationenddate.Location = new Point(224, 118);
            reservationenddate.Name = "reservationenddate";
            reservationenddate.Size = new Size(212, 29);
            reservationenddate.TabIndex = 30;
            // 
            // reservationstartdate
            // 
            reservationstartdate.Font = new Font("Segoe UI", 12F);
            reservationstartdate.Location = new Point(224, 84);
            reservationstartdate.Name = "reservationstartdate";
            reservationstartdate.Size = new Size(212, 29);
            reservationstartdate.TabIndex = 29;
            // 
            // reservationdate
            // 
            reservationdate.Font = new Font("Segoe UI", 12F);
            reservationdate.Location = new Point(224, 47);
            reservationdate.Name = "reservationdate";
            reservationdate.Size = new Size(212, 29);
            reservationdate.TabIndex = 28;
            // 
            // reservationid
            // 
            reservationid.Font = new Font("Segoe UI", 12F);
            reservationid.Location = new Point(224, 11);
            reservationid.Name = "reservationid";
            reservationid.ReadOnly = true;
            reservationid.Size = new Size(212, 29);
            reservationid.TabIndex = 27;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(62, 234);
            label7.Name = "label7";
            label7.Size = new Size(74, 21);
            label7.TabIndex = 26;
            label7.Text = "RoomID :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(61, 193);
            label6.Name = "label6";
            label6.Size = new Size(92, 21);
            label6.TabIndex = 25;
            label6.Text = "ResidentID :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(62, 154);
            label5.Name = "label5";
            label5.Size = new Size(59, 21);
            label5.TabIndex = 24;
            label5.Text = "Status :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(61, 120);
            label4.Name = "label4";
            label4.Size = new Size(75, 21);
            label4.TabIndex = 23;
            label4.Text = "EndDate :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(62, 86);
            label3.Name = "label3";
            label3.Size = new Size(81, 21);
            label3.TabIndex = 22;
            label3.Text = "StartDate :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(62, 49);
            label2.Name = "label2";
            label2.Size = new Size(131, 21);
            label2.TabIndex = 21;
            label2.Text = "ReservationDate :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(62, 13);
            label1.Name = "label1";
            label1.Size = new Size(114, 21);
            label1.TabIndex = 20;
            label1.Text = "ReservationID :";
            label1.Click += label1_Click;
            // 
            // reservationstatus
            // 
            reservationstatus.Font = new Font("Segoe UI", 12F);
            reservationstatus.FormattingEnabled = true;
            reservationstatus.Location = new Point(224, 152);
            reservationstatus.Name = "reservationstatus";
            reservationstatus.Size = new Size(212, 29);
            reservationstatus.TabIndex = 39;
            // 
            // residentid
            // 
            residentid.Font = new Font("Segoe UI", 12F);
            residentid.Location = new Point(224, 190);
            residentid.Name = "residentid";
            residentid.PlaceholderText = "Input Resident ID To Display Name";
            residentid.Size = new Size(273, 29);
            residentid.TabIndex = 42;
            residentid.TextAlign = HorizontalAlignment.Center;
            // 
            // roomid
            // 
            roomid.Font = new Font("Segoe UI", 12F);
            roomid.Location = new Point(224, 232);
            roomid.Name = "roomid";
            roomid.ReadOnly = true;
            roomid.Size = new Size(212, 29);
            roomid.TabIndex = 43;
            roomid.TextAlign = HorizontalAlignment.Center;
            // 
            // insertid
            // 
            insertid.Font = new Font("Segoe UI", 9F);
            insertid.Location = new Point(679, 8);
            insertid.Name = "insertid";
            insertid.Size = new Size(69, 47);
            insertid.TabIndex = 44;
            insertid.Text = "Insert";
            insertid.UseVisualStyleBackColor = true;
            insertid.Click += button1_Click;
            // 
            // residentname
            // 
            residentname.Font = new Font("Segoe UI", 12F);
            residentname.Location = new Point(503, 190);
            residentname.Name = "residentname";
            residentname.ReadOnly = true;
            residentname.Size = new Size(89, 29);
            residentname.TabIndex = 45;
            residentname.TextAlign = HorizontalAlignment.Center;
            // 
            // roomnum
            // 
            roomnum.Font = new Font("Segoe UI", 12F);
            roomnum.Location = new Point(442, 231);
            roomnum.Name = "roomnum";
            roomnum.ReadOnly = true;
            roomnum.Size = new Size(89, 29);
            roomnum.TabIndex = 46;
            roomnum.TextAlign = HorizontalAlignment.Center;
            // 
            // delete
            // 
            delete.Font = new Font("Segoe UI", 9F);
            delete.Location = new Point(679, 161);
            delete.Name = "delete";
            delete.Size = new Size(69, 47);
            delete.TabIndex = 47;
            delete.Text = "Delete";
            delete.UseVisualStyleBackColor = true;
            // 
            // back
            // 
            back.Font = new Font("Segoe UI", 9F);
            back.Location = new Point(679, 214);
            back.Name = "back";
            back.Size = new Size(69, 47);
            back.TabIndex = 48;
            back.Text = "Back";
            back.UseVisualStyleBackColor = true;
            // 
            // ReservationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(800, 538);
            Controls.Add(back);
            Controls.Add(delete);
            Controls.Add(roomnum);
            Controls.Add(residentname);
            Controls.Add(insertid);
            Controls.Add(roomid);
            Controls.Add(residentid);
            Controls.Add(reservationstatus);
            Controls.Add(dataGridView1);
            Controls.Add(clear);
            Controls.Add(update);
            Controls.Add(reservationenddate);
            Controls.Add(reservationstartdate);
            Controls.Add(reservationdate);
            Controls.Add(reservationid);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ReservationForm";
            Text = "ReservationForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView1;
        private Button clear;
        private Button update;
        private DateTimePicker reservationenddate;
        private DateTimePicker reservationstartdate;
        private DateTimePicker reservationdate;
        private TextBox reservationid;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox reservationstatus;
        private DataGridViewTextBoxColumn column7;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private TextBox residentid;
        private TextBox roomid;
        private Button insertid;
        private TextBox residentname;
        private TextBox roomnum;
        private Button delete;
        private Button back;
    }
}