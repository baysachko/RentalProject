namespace WinFormsApp1
{
    partial class ServiceForm
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
            servicedescription = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label = new Label();
            label4 = new Label();
            servicename = new TextBox();
            serviceid = new TextBox();
            servicecost = new TextBox();
            insert = new Button();
            clear = new Button();
            update = new Button();
            back = new Button();
            servicegrid = new DataGridView();
            label1 = new Label();
            searchbox = new TextBox();
            delete = new Button();
            ((System.ComponentModel.ISupportInitialize)servicegrid).BeginInit();
            SuspendLayout();
            // 
            // servicedescription
            // 
            servicedescription.Font = new Font("Segoe UI", 12F);
            servicedescription.Location = new Point(200, 110);
            servicedescription.Name = "servicedescription";
            servicedescription.Size = new Size(346, 29);
            servicedescription.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(48, 19);
            label3.Name = "label3";
            label3.Size = new Size(82, 21);
            label3.TabIndex = 1;
            label3.Text = "ServiceID :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(48, 67);
            label2.Name = "label2";
            label2.Size = new Size(109, 21);
            label2.TabIndex = 2;
            label2.Text = "Service Name:";
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 12F);
            label.Location = new Point(48, 113);
            label.Name = "label";
            label.Size = new Size(146, 21);
            label.TabIndex = 3;
            label.Text = "Service Description:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(480, 24);
            label4.Name = "label4";
            label4.Size = new Size(48, 21);
            label4.TabIndex = 4;
            label4.Text = "Cost: ";
            // 
            // servicename
            // 
            servicename.Font = new Font("Segoe UI", 12F);
            servicename.Location = new Point(200, 64);
            servicename.Name = "servicename";
            servicename.Size = new Size(206, 29);
            servicename.TabIndex = 5;
            // 
            // serviceid
            // 
            serviceid.Font = new Font("Segoe UI", 12F);
            serviceid.Location = new Point(200, 16);
            serviceid.Name = "serviceid";
            serviceid.ReadOnly = true;
            serviceid.Size = new Size(206, 29);
            serviceid.TabIndex = 6;
            // 
            // servicecost
            // 
            servicecost.Font = new Font("Segoe UI", 12F);
            servicecost.Location = new Point(534, 19);
            servicecost.Name = "servicecost";
            servicecost.PlaceholderText = "Enter Cost Of The Service";
            servicecost.Size = new Size(206, 29);
            servicecost.TabIndex = 7;
            servicecost.TextAlign = HorizontalAlignment.Center;
            // 
            // insert
            // 
            insert.Font = new Font("Segoe UI", 9F);
            insert.Location = new Point(357, 168);
            insert.Name = "insert";
            insert.Size = new Size(59, 47);
            insert.TabIndex = 49;
            insert.Text = "Insert";
            insert.UseVisualStyleBackColor = true;
            // 
            // clear
            // 
            clear.Font = new Font("Segoe UI", 9F);
            clear.Location = new Point(521, 168);
            clear.Name = "clear";
            clear.Size = new Size(59, 47);
            clear.TabIndex = 48;
            clear.Text = "Clear";
            clear.UseVisualStyleBackColor = true;
            // 
            // update
            // 
            update.Font = new Font("Segoe UI", 9F);
            update.Location = new Point(442, 168);
            update.Name = "update";
            update.Size = new Size(59, 47);
            update.TabIndex = 47;
            update.Text = "Update";
            update.UseVisualStyleBackColor = true;
            // 
            // back
            // 
            back.Font = new Font("Segoe UI", 9F);
            back.Location = new Point(681, 168);
            back.Name = "back";
            back.Size = new Size(59, 47);
            back.TabIndex = 50;
            back.Text = "Back";
            back.UseVisualStyleBackColor = true;
            // 
            // servicegrid
            // 
            servicegrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            servicegrid.Location = new Point(12, 231);
            servicegrid.Name = "servicegrid";
            servicegrid.Size = new Size(776, 198);
            servicegrid.TabIndex = 51;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(25, 183);
            label1.Name = "label1";
            label1.Size = new Size(64, 21);
            label1.TabIndex = 52;
            label1.Text = "Search :";
            // 
            // searchbox
            // 
            searchbox.Font = new Font("Segoe UI", 12F);
            searchbox.Location = new Point(95, 179);
            searchbox.Name = "searchbox";
            searchbox.PlaceholderText = "Seach by Name, Description";
            searchbox.Size = new Size(256, 29);
            searchbox.TabIndex = 53;
            // 
            // delete
            // 
            delete.Font = new Font("Segoe UI", 9F);
            delete.Location = new Point(604, 168);
            delete.Name = "delete";
            delete.Size = new Size(59, 47);
            delete.TabIndex = 54;
            delete.Text = "Delete";
            delete.UseVisualStyleBackColor = true;
            // 
            // ServiceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 440);
            Controls.Add(delete);
            Controls.Add(searchbox);
            Controls.Add(label1);
            Controls.Add(servicegrid);
            Controls.Add(back);
            Controls.Add(insert);
            Controls.Add(clear);
            Controls.Add(update);
            Controls.Add(servicecost);
            Controls.Add(serviceid);
            Controls.Add(servicename);
            Controls.Add(label4);
            Controls.Add(label);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(servicedescription);
            Name = "ServiceForm";
            Text = "ServiceForm";
            ((System.ComponentModel.ISupportInitialize)servicegrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox servicedescription;
        private Label label3;
        private Label label2;
        private Label label;
        private Label label4;
        private TextBox servicename;
        private TextBox serviceid;
        private TextBox servicecost;
        private Button insert;
        private Button clear;
        private Button update;
        private Button back;
        private DataGridView servicegrid;
        private Label label1;
        private TextBox searchbox;
        private Button delete;
    }
}