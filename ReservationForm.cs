using Microsoft.Data.SqlClient;
using System.Data;
using WinFormsApp1.Helper;
using WinFormsApp1.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class ReservationForm : Form
    {
        public ReservationForm()
        {
            InitializeComponent();
            SetupDataGridView();
            SetupForm();
            LoadData();
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.DataGridView1_CellClick);

            // Add new button event handlers
            delete.Click += Delete_Click;
            back.Click += Back_Click;
            clear.Click += Clear_Click;

        }

        private void SetupForm()
        {
            insertid.Click += Insert_Click;
            // Setup Status ComboBox
            reservationstatus.Items.Clear();
            reservationstatus.DropDownStyle = ComboBoxStyle.DropDownList;
            reservationstatus.Items.AddRange(new string[]
            {
            "Pending",
            "Confirmed",
            "Cancelled"
            });
            reservationstatus.SelectedIndex = 0;

            // Make ReservationID read-only
            reservationid.ReadOnly = true;

            // Setup date pickers
            reservationdate.Format = DateTimePickerFormat.Custom;
            reservationdate.CustomFormat = "dddd , MMMM dd";
            reservationstartdate.Format = DateTimePickerFormat.Custom;
            reservationstartdate.CustomFormat = "dddd , MMMM dd";
            reservationenddate.Format = DateTimePickerFormat.Custom;
            reservationenddate.CustomFormat = "dddd , MMMM dd";

            // Setup buttons
            update.Text = "Update";
            clear.Text = "Clear";

            // Add event handlers for resident lookup
            residentid.KeyDown += ResidentID_KeyDown;
            roomid.KeyDown += RoomID_KeyDown;
        }

        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;

            // Add columns exactly as shown in the image
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ReservationID",
                HeaderText = "ReservationID",
                DataPropertyName = "ReservationID"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ReservationDate",
                HeaderText = "ReservationDate",
                DataPropertyName = "ReservationDate",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dddd , MMMM dd" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StartDate",
                HeaderText = "StartDate",
                DataPropertyName = "StartDate",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dddd , MMMM dd" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EndDate",
                HeaderText = "EndDate",
                DataPropertyName = "EndDate",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dddd , MMMM dd" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ResidentID",
                HeaderText = "ResidentID",
                DataPropertyName = "ResidentID"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RoomID",
                HeaderText = "RoomID",
                DataPropertyName = "RoomID"
            });
        }
        private void RoomID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                FetchAndDisplayRoomNumber();
            }
        }
        private void ResidentID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                FetchAndDisplayResidentName();
            }
        }
        private void FetchAndDisplayResidentName()
        {
            if (string.IsNullOrWhiteSpace(residentid.Text))
            {
                MessageBox.Show("Please enter a Resident ID", "Validation Error");
                return;
            }

            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    string query = @"
                SELECT r.ResidentName, 
                       ISNULL((SELECT TOP 1 RoomID 
                        FROM Rent 
                        WHERE ResidentID = @ResidentID 
                        ORDER BY StartDate DESC), '') as RoomID
                FROM Resident r
                WHERE r.ResidentID = @ResidentID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ResidentID", int.Parse(residentid.Text));
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string residentName = reader["ResidentName"].ToString();
                                string roomId = reader["RoomID"].ToString();

                                // Display resident name in textbox instead of MessageBox
                                residentname.Text = residentName;

                                // Auto-fill RoomID if available
                                if (!string.IsNullOrEmpty(roomId))
                                {
                                    roomid.Text = roomId;
                                    // Trigger room number lookup
                                    FetchAndDisplayRoomNumber();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Resident ID not found", "Error");
                                residentid.Text = "";
                                roomid.Text = "";
                                residentname.Text = "";
                                roomnum.Text = "";
                                residentid.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
                residentid.Text = "";
                roomid.Text = "";
                residentname.Text = "";
                roomnum.Text = "";
                residentid.Focus();
            }
        }

        private void FetchAndDisplayRoomNumber()
        {
            if (string.IsNullOrWhiteSpace(roomid.Text))
            {
                MessageBox.Show("Please enter a Room ID", "Validation Error");
                return;
            }

            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    string query = "SELECT RoomNumber FROM Room WHERE RoomID = @RoomID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomID", int.Parse(roomid.Text));
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Display room number in textbox instead of MessageBox
                            roomnum.Text = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Room ID not found", "Error");
                            roomid.Text = "";
                            roomnum.Text = "";
                            roomid.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
                roomid.Text = "";
                roomnum.Text = "";
                roomid.Focus();
            }
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    string query = @"SELECT ReservationID, ReservationDate, StartDate, EndDate, 
                                     Status, ResidentID, RoomID 
                                     FROM Reservation 
                                     ORDER BY ReservationID DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        conn.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                try
                {
                    // Fill form fields with selected row data
                    reservationid.Text = dataGridView1.CurrentRow.Cells["ReservationID"].Value?.ToString();
                    reservationdate.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["ReservationDate"].Value);
                    reservationstartdate.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["StartDate"].Value);
                    reservationenddate.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["EndDate"].Value);
                    residentid.Text = dataGridView1.CurrentRow.Cells["ResidentID"].Value?.ToString();
                    roomid.Text = dataGridView1.CurrentRow.Cells["RoomID"].Value?.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error selecting row: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                string query = @"INSERT INTO Reservation 
                            (ReservationDate, StartDate, EndDate, Status, ResidentID, RoomID)
                            VALUES
                            (@ReservationDate, @StartDate, @EndDate, @Status, @ResidentID, @RoomID)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ReservationDate", reservationdate.Value.Date),
                    new SqlParameter("@StartDate", reservationstartdate.Value.Date),
                    new SqlParameter("@EndDate", reservationenddate.Value.Date),
                    new SqlParameter("@Status", reservationstatus.SelectedItem.ToString()),
                    new SqlParameter("@ResidentID", int.Parse(residentid.Text)),
                    new SqlParameter("@RoomID", int.Parse(roomid.Text))
                };

                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddRange(parameters);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Reservation added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(reservationid.Text))
                {
                    MessageBox.Show("Please select a reservation to update.", "Validation Error");
                    return;
                }

                if (!ValidateInput()) return;

                string query = @"UPDATE Reservation 
                            SET ReservationDate = @ReservationDate,
                                StartDate = @StartDate,
                                EndDate = @EndDate,
                                Status = @Status,
                                ResidentID = @ResidentID,
                                RoomID = @RoomID
                            WHERE ReservationID = @ReservationID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ReservationID", int.Parse(reservationid.Text)),
                    new SqlParameter("@ReservationDate", reservationdate.Value.Date),
                    new SqlParameter("@StartDate", reservationstartdate.Value.Date),
                    new SqlParameter("@EndDate", reservationenddate.Value.Date),
                    new SqlParameter("@Status", reservationstatus.SelectedItem.ToString()),
                    new SqlParameter("@ResidentID", int.Parse(residentid.Text)),
                    new SqlParameter("@RoomID", int.Parse(roomid.Text))
                };

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to update this reservation?",
                    "Confirm Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = DbHelper.GetConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddRange(parameters);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Reservation updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear all fields?",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ClearFields();
            }
        }

        private void ClearFields()
        {
            // Clear all textboxes
            reservationid.Text = string.Empty;
            residentid.Text = string.Empty;
            roomid.Text = string.Empty;
            residentname.Text = string.Empty;
            roomnum.Text = string.Empty;

            // Reset date pickers to current date
            reservationdate.Value = DateTime.Now;
            reservationstartdate.Value = DateTime.Now;
            reservationenddate.Value = DateTime.Now;

            // Reset status combobox to first item
            if (reservationstatus.Items.Count > 0)
            {
                reservationstatus.SelectedIndex = 0;
            }

            // Set focus to first input field
            residentid.Focus();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dataGridView1?.Rows != null)  // Add null check
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    if (row.Cells["ReservationID"].Value != null) reservationid.Text = row.Cells["ReservationID"].Value.ToString();
                    if (row.Cells["ReservationDate"].Value != null) reservationdate.Value = Convert.ToDateTime(row.Cells["ReservationDate"].Value);
                    if (row.Cells["StartDate"].Value != null) reservationstartdate.Value = Convert.ToDateTime(row.Cells["StartDate"].Value);
                    if (row.Cells["EndDate"].Value != null) reservationenddate.Value = Convert.ToDateTime(row.Cells["EndDate"].Value);
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "Status",
                        HeaderText = "Status",
                        DataPropertyName = "Status",
                        Width = 80
                    });
                    if (row.Cells["ResidentID"].Value != null) residentid.Text = row.Cells["ResidentID"].Value.ToString();
                    if (row.Cells["RoomID"].Value != null) roomid.Text = row.Cells["RoomID"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting row: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(residentid.Text) || string.IsNullOrWhiteSpace(roomid.Text))
            {
                MessageBox.Show("Please fill in all required fields", "Validation Error");
                return false;
            }

            if (!int.TryParse(residentid.Text, out _))
            {
                MessageBox.Show("ResidentID must be a number", "Validation Error");
                return false;
            }

            if (!int.TryParse(roomid.Text, out _))
            {
                MessageBox.Show("RoomID must be a number", "Validation Error");
                return false;
            }

            if (reservationstartdate.Value > reservationenddate.Value)
            {
                MessageBox.Show("Start date cannot be after end date", "Validation Error");
                return false;
            }

            return true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void Delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(reservationid.Text))
            {
                MessageBox.Show("Please select a reservation to delete.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this reservation?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = DbHelper.GetConnection())
                    {
                        string query = "DELETE FROM Reservation WHERE ReservationID = @ReservationID";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ReservationID", int.Parse(reservationid.Text));

                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Reservation deleted successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                                ClearFields();
                            }
                            else
                            {
                                MessageBox.Show("No reservation was found to delete.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting reservation: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Back_Click(object sender, EventArgs e)
        {
            // Show confirmation dialog
            DialogResult result = MessageBox.Show(
                "Are you sure you want to go back? Any unsaved changes will be lost.",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}