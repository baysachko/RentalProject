namespace WinFormsApp1
{
    partial class RentForm
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
            rentid = new TextBox();
            text1 = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            rentamount = new TextBox();
            roomid = new TextBox();
            residentname = new TextBox();
            roomnum = new TextBox();
            residentid = new TextBox();
            startdate = new DateTimePicker();
            enddate = new DateTimePicker();
            clear = new Button();
            update = new Button();
            back = new Button();
            dataGridView1 = new DataGridView();
            label8 = new Label();
            searchbox = new TextBox();
            insert = new Button();
            delete = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // rentid
            // 
            rentid.Font = new Font("Segoe UI", 12F);
            rentid.Location = new Point(148, 22);
            rentid.Name = "rentid";
            rentid.ReadOnly = true;
            rentid.Size = new Size(230, 29);
            rentid.TabIndex = 0;
            rentid.TextAlign = HorizontalAlignment.Center;
            // 
            // text1
            // 
            text1.AutoSize = true;
            text1.Font = new Font("Segoe UI", 12F);
            text1.Location = new Point(41, 25);
            text1.Name = "text1";
            text1.Size = new Size(64, 21);
            text1.TabIndex = 1;
            text1.Text = "RentID :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(41, 71);
            label1.Name = "label1";
            label1.Size = new Size(81, 21);
            label1.TabIndex = 2;
            label1.Text = "StartDate :";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(41, 114);
            label2.Name = "label2";
            label2.Size = new Size(75, 21);
            label2.TabIndex = 3;
            label2.Text = "EndDate :";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(41, 155);
            label3.Name = "label3";
            label3.Size = new Size(105, 21);
            label3.TabIndex = 4;
            label3.Text = "RentAmount :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(423, 155);
            label4.Name = "label4";
            label4.Size = new Size(92, 21);
            label4.TabIndex = 5;
            label4.Text = "ResidentID :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(423, 25);
            label5.Name = "label5";
            label5.Size = new Size(74, 21);
            label5.TabIndex = 6;
            label5.Text = "RoomID :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(422, 71);
            label6.Name = "label6";
            label6.Size = new Size(119, 21);
            label6.TabIndex = 7;
            label6.Text = "ResidentName :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(423, 114);
            label7.Name = "label7";
            label7.Size = new Size(117, 21);
            label7.TabIndex = 8;
            label7.Text = "RoomNumber :";
            // 
            // rentamount
            // 
            rentamount.Font = new Font("Segoe UI", 12F);
            rentamount.Location = new Point(148, 152);
            rentamount.Name = "rentamount";
            rentamount.Size = new Size(230, 29);
            rentamount.TabIndex = 9;
            rentamount.TextAlign = HorizontalAlignment.Center;
            // 
            // roomid
            // 
            roomid.Font = new Font("Segoe UI", 12F);
            roomid.Location = new Point(554, 22);
            roomid.Name = "roomid";
            roomid.Size = new Size(211, 29);
            roomid.TabIndex = 10;
            roomid.TextAlign = HorizontalAlignment.Center;
            // 
            // residentname
            // 
            residentname.Font = new Font("Segoe UI", 12F);
            residentname.Location = new Point(554, 68);
            residentname.Name = "residentname";
            residentname.ReadOnly = true;
            residentname.Size = new Size(211, 29);
            residentname.TabIndex = 11;
            residentname.TextAlign = HorizontalAlignment.Center;
            // 
            // roomnum
            // 
            roomnum.Font = new Font("Segoe UI", 12F);
            roomnum.Location = new Point(554, 116);
            roomnum.Name = "roomnum";
            roomnum.ReadOnly = true;
            roomnum.Size = new Size(211, 29);
            roomnum.TabIndex = 12;
            roomnum.TextAlign = HorizontalAlignment.Center;
            // 
            // residentid
            // 
            residentid.Font = new Font("Segoe UI", 12F);
            residentid.Location = new Point(554, 157);
            residentid.Name = "residentid";
            residentid.Size = new Size(211, 29);
            residentid.TabIndex = 13;
            residentid.TextAlign = HorizontalAlignment.Center;
            // 
            // startdate
            // 
            startdate.CalendarFont = new Font("Segoe UI", 9F);
            startdate.Font = new Font("Segoe UI", 9F);
            startdate.Location = new Point(148, 65);
            startdate.Name = "startdate";
            startdate.Size = new Size(230, 23);
            startdate.TabIndex = 14;
            // 
            // enddate
            // 
            enddate.CalendarFont = new Font("Segoe UI", 9F);
            enddate.Font = new Font("Segoe UI", 9F);
            enddate.Location = new Point(148, 108);
            enddate.Name = "enddate";
            enddate.Size = new Size(230, 23);
            enddate.TabIndex = 15;
            // 
            // clear
            // 
            clear.Font = new Font("Segoe UI", 9F);
            clear.Location = new Point(533, 215);
            clear.Name = "clear";
            clear.Size = new Size(81, 47);
            clear.TabIndex = 39;
            clear.Text = "Clear";
            clear.UseVisualStyleBackColor = true;
            // 
            // update
            // 
            update.Font = new Font("Segoe UI", 9F);
            update.Location = new Point(446, 215);
            update.Name = "update";
            update.Size = new Size(81, 47);
            update.TabIndex = 38;
            update.Text = "Update";
            update.UseVisualStyleBackColor = true;
            // 
            // back
            // 
            back.Font = new Font("Segoe UI", 9F);
            back.Location = new Point(707, 215);
            back.Name = "back";
            back.Size = new Size(81, 47);
            back.TabIndex = 40;
            back.Text = "Back";
            back.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 279);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(776, 244);
            dataGridView1.TabIndex = 41;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.Location = new Point(12, 229);
            label8.Name = "label8";
            label8.Size = new Size(64, 21);
            label8.TabIndex = 42;
            label8.Text = "Search :";
            // 
            // searchbox
            // 
            searchbox.Location = new Point(82, 231);
            searchbox.Name = "searchbox";
            searchbox.Size = new Size(153, 23);
            searchbox.TabIndex = 43;
            // 
            // insert
            // 
            insert.Font = new Font("Segoe UI", 9F);
            insert.Location = new Point(353, 215);
            insert.Name = "insert";
            insert.Size = new Size(81, 47);
            insert.TabIndex = 44;
            insert.Text = "Insert";
            insert.UseVisualStyleBackColor = true;
            // 
            // delete
            // 
            delete.Font = new Font("Segoe UI", 9F);
            delete.Location = new Point(620, 215);
            delete.Name = "delete";
            delete.Size = new Size(81, 47);
            delete.TabIndex = 45;
            delete.Text = "Delete";
            delete.UseVisualStyleBackColor = true;
            // 
            // RentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 535);
            Controls.Add(delete);
            Controls.Add(insert);
            Controls.Add(searchbox);
            Controls.Add(label8);
            Controls.Add(dataGridView1);
            Controls.Add(back);
            Controls.Add(clear);
            Controls.Add(update);
            Controls.Add(enddate);
            Controls.Add(startdate);
            Controls.Add(residentid);
            Controls.Add(roomnum);
            Controls.Add(residentname);
            Controls.Add(roomid);
            Controls.Add(rentamount);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(text1);
            Controls.Add(rentid);
            Name = "RentForm";
            Text = "RentForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox rentid;
        private Label text1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox rentamount;
        private TextBox roomid;
        private TextBox residentname;
        private TextBox roomnum;
        private TextBox residentid;
        private DateTimePicker startdate;
        private DateTimePicker enddate;
        private Button clear;
        private Button update;
        private Button back;
        private DataGridView dataGridView1;
        private Label label8;
        private TextBox searchbox;
        private Button insert;
        private Button delete;
    }
}