using WinFormsApp1.Model;
using WinFormsApp1.Helper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class RentForm : Form
    {
        public RentForm()
        {
            InitializeComponent();

            // Wire up all event handlers
            searchbox.TextChanged += searchbox_TextChanged;
            dataGridView1.CellClick += dataGridView1_CellClick;
            update.Click += update_Click;
            clear.Click += clear_Click;
            back.Click += back_Click;
            insert.Click += insert_Click;  
            residentid.TextChanged += residentid_TextChanged;  
            roomid.TextChanged += roomid_TextChanged;  
            delete.Click += delete_Click;

            if (dataGridView1 == null)
            {
                MessageBox.Show("DataGridView is not initialized!");
                return;
            }

            LoadRentData();

            // Set DataGridView properties
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void LoadRentData()
        {
            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    // Configure DataGridView properties
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;  // Allow only one row selection
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AutoGenerateColumns = true;
                    connection.Open();
                    string query = @"SELECT r.RentID, r.StartDate, r.EndDate, r.RentAmount, 
                           r.ResidentID, r.RoomID, res.ResidentName, rm.RoomNumber
                           FROM Rent r
                           INNER JOIN Resident res ON r.ResidentID = res.ResidentID
                           INNER JOIN Room rm ON r.RoomID = rm.RoomID";

                    var adapter = new SqlDataAdapter(query, connection);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    // Clear existing columns and data
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    dataGridView1.AutoGenerateColumns = true;

                    // Set the data source
                    dataGridView1.DataSource = dt;

                    // Configure DataGridView properties
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.RowHeadersVisible = false;

                    // Set individual column properties
                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns["RentID"].Width = 70;
                        dataGridView1.Columns["StartDate"].Width = 100;
                        dataGridView1.Columns["EndDate"].Width = 100;
                        dataGridView1.Columns["RentAmount"].Width = 100;
                        dataGridView1.Columns["RoomID"].Width = 70;
                        dataGridView1.Columns["ResidentID"].Width = 80;
                        dataGridView1.Columns["ResidentName"].Width = 120;
                        dataGridView1.Columns["RoomNumber"].Width = 100;

                        // Format columns
                        dataGridView1.Columns["StartDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                        dataGridView1.Columns["EndDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                        dataGridView1.Columns["RentAmount"].DefaultCellStyle.Format = "C2";

                        // Set headers
                        dataGridView1.Columns["RentID"].HeaderText = "Rent ID";
                        dataGridView1.Columns["StartDate"].HeaderText = "Start Date";
                        dataGridView1.Columns["EndDate"].HeaderText = "End Date";
                        dataGridView1.Columns["RentAmount"].HeaderText = "Rent Amount";
                        dataGridView1.Columns["RoomID"].HeaderText = "Room ID";
                        dataGridView1.Columns["ResidentID"].HeaderText = "Resident ID";
                        dataGridView1.Columns["ResidentName"].HeaderText = "Resident Name";
                        dataGridView1.Columns["RoomNumber"].HeaderText = "Room Number";
                    }

                    // Style the grid
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rent data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInsertInput()) return;

                using (var connection = DbHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"INSERT INTO Rent (StartDate, EndDate, RentAmount, ResidentID, RoomID)
                                   VALUES (@StartDate, @EndDate, @RentAmount, @ResidentID, @RoomID)";

                    var command = new SqlCommand(query, connection);

                    // Add parameters
                    command.Parameters.Add("@StartDate", SqlDbType.Date).Value = startdate.Value.Date;
                    command.Parameters.Add("@EndDate", SqlDbType.Date).Value = enddate.Value.Date;
                    command.Parameters.Add("@RentAmount", SqlDbType.Decimal).Value =
                        decimal.Parse(rentamount.Text);
                    command.Parameters.Add("@ResidentID", SqlDbType.Int).Value =
                        int.Parse(residentid.Text);
                    command.Parameters.Add("@RoomID", SqlDbType.Int).Value =
                        int.Parse(roomid.Text);

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Rent record inserted successfully!");
                        LoadRentData();
                        ClearFields();
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please ensure all numeric fields contain valid numbers.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting rent record: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                using (var connection = DbHelper.GetConnection())
                {
                    connection.Open();

                    // Get RentID from selected row
                    int rentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["RentID"].Value);

                    string query = @"UPDATE Rent 
                 SET StartDate = @StartDate, 
                     EndDate = @EndDate,
                     RentAmount = @RentAmount 
                 WHERE RentID = @RentID";

                    var command = new SqlCommand(query, connection);

                    // Add parameters with correct parameter names
                    command.Parameters.Add("@RentID", SqlDbType.Int).Value = rentId;
                    command.Parameters.Add("@StartDate", SqlDbType.Date).Value = startdate.Value.Date;
                    command.Parameters.Add("@EndDate", SqlDbType.Date).Value = enddate.Value.Date;

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Update successful!");
                        LoadRentData();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("No records were updated. Please check if the RentID exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating rent record: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear all input fields
                if (rentid != null) rentid.Text = string.Empty;
                if (rentamount != null) rentamount.Text = string.Empty;
                if (roomid != null) roomid.Text = string.Empty;
                if (residentname != null) residentname.Text = string.Empty;
                if (roomnum != null) roomnum.Text = string.Empty;
                if (residentid != null) residentid.Text = string.Empty;
                if (searchbox != null) searchbox.Text = string.Empty;

                // Reset date pickers to current date
                if (startdate != null) startdate.Value = DateTime.Now;
                if (enddate != null) enddate.Value = DateTime.Now;

                // Clear any selected rows in the DataGridView
                if (dataGridView1 != null && dataGridView1.CurrentRow != null)
                {
                    dataGridView1.ClearSelection();
                }

                // Optional: Refresh the DataGridView
                LoadRentData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing fields: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            rentamount.Clear();
            roomid.Clear();
            residentname.Clear();
            roomnum.Clear();
            residentid.Clear();
            startdate.Value = DateTime.Now;
            enddate.Value = DateTime.Now;
        }

        private void back_Click(object sender, EventArgs e)
        {
            try
            {
                // Optionally prompt user to confirm if there are unsaved changes
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to go back? Any unsaved changes will be lost.",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Close the current form
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // For update, we only need a selected row
            if (dataGridView1.CurrentRow == null || dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to update.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool ValidateInsertInput()
        {
            if (string.IsNullOrWhiteSpace(rentamount.Text))
            {
                MessageBox.Show("Please enter a rent amount.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(residentid.Text))
            {
                MessageBox.Show("Please enter a resident ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(roomid.Text))
            {
                MessageBox.Show("Please enter a room ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (enddate.Value < startdate.Value)
            {
                MessageBox.Show("End date cannot be earlier than start date.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate if ResidentID exists
            using (var connection = DbHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT COUNT(1) FROM Resident WHERE ResidentID = @ResidentID",
                    connection);
                command.Parameters.AddWithValue("@ResidentID", int.Parse(residentid.Text));
                int residentExists = (int)command.ExecuteScalar();

                if (residentExists == 0)
                {
                    MessageBox.Show("The specified Resident ID does not exist.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Validate if RoomID exists
            using (var connection = DbHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT COUNT(1) FROM Room WHERE RoomID = @RoomID",
                    connection);
                command.Parameters.AddWithValue("@RoomID", int.Parse(roomid.Text));
                int roomExists = (int)command.ExecuteScalar();

                if (roomExists == 0)
                {
                    MessageBox.Show("The specified Room ID does not exist.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void residentid_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(residentid.Text) && int.TryParse(residentid.Text, out int id))
            {
                try
                {
                    using (var connection = DbHelper.GetConnection())
                    {
                        connection.Open();
                        string query = "SELECT ResidentName FROM Resident WHERE ResidentID = @ResidentID";
                        var command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ResidentID", id);

                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            residentname.Text = result.ToString();
                        }
                        else
                        {
                            residentname.Clear();
                        }
                    }
                }
                catch (Exception)
                {
                    residentname.Clear();
                }
            }
            else
            {
                residentname.Clear();
            }
        }

        private void roomid_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(roomid.Text) && int.TryParse(roomid.Text, out int id))
            {
                try
                {
                    using (var connection = DbHelper.GetConnection())
                    {
                        connection.Open();
                        string query = "SELECT RoomNumber FROM Room WHERE RoomID = @RoomID";
                        var command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@RoomID", id);

                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            roomnum.Text = result.ToString();
                        }
                        else
                        {
                            roomnum.Clear();
                        }
                    }
                }
                catch (Exception)
                {
                    roomnum.Clear();
                }
            }
            else
            {
                roomnum.Clear();
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    // Pre-populate all fields
                    rentid.Text = row.Cells["RentID"].Value.ToString();  // Add this line
                    startdate.Value = Convert.ToDateTime(row.Cells["StartDate"].Value);
                    enddate.Value = Convert.ToDateTime(row.Cells["EndDate"].Value);
                    rentamount.Text = row.Cells["RentAmount"].Value.ToString();
                    roomid.Text = row.Cells["RoomID"].Value.ToString();
                    residentid.Text = row.Cells["ResidentID"].Value.ToString();
                    residentname.Text = row.Cells["ResidentName"].Value.ToString();
                    roomnum.Text = row.Cells["RoomNumber"].Value.ToString();

                    // Select the entire row
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting record: {ex.Message}");
            }
        }

        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT r.RentID, r.StartDate, r.EndDate, r.RentAmount, 
                           r.ResidentID, r.RoomID, res.ResidentName, rm.RoomNumber
                           FROM Rent r
                           INNER JOIN Resident res ON r.ResidentID = res.ResidentID
                           INNER JOIN Room rm ON r.RoomID = rm.RoomID
                           WHERE res.ResidentName LIKE @SearchTerm + '%'";

                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SearchTerm", searchbox.Text);

                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Maintain column formatting
                    if (dataGridView1.Columns.Count > 0)
                    {
                        FormatDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormatDataGridView()
        {
            dataGridView1.Columns["RentID"].Width = 70;
            dataGridView1.Columns["StartDate"].Width = 100;
            dataGridView1.Columns["EndDate"].Width = 100;
            dataGridView1.Columns["RentAmount"].Width = 100;
            dataGridView1.Columns["RoomID"].Width = 70;
            dataGridView1.Columns["ResidentID"].Width = 80;
            dataGridView1.Columns["ResidentName"].Width = 120;
            dataGridView1.Columns["RoomNumber"].Width = 100;

            // Format columns
            dataGridView1.Columns["StartDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.Columns["EndDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.Columns["RentAmount"].DefaultCellStyle.Format = "C2";

            // Set headers
            dataGridView1.Columns["RentID"].HeaderText = "Rent ID";
            dataGridView1.Columns["StartDate"].HeaderText = "Start Date";
            dataGridView1.Columns["EndDate"].HeaderText = "End Date";
            dataGridView1.Columns["RentAmount"].HeaderText = "Rent Amount";
            dataGridView1.Columns["RoomID"].HeaderText = "Room ID";
            dataGridView1.Columns["ResidentID"].HeaderText = "Resident ID";
            dataGridView1.Columns["ResidentName"].HeaderText = "Resident Name";
            dataGridView1.Columns["RoomNumber"].HeaderText = "Room Number";
        }
        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected
                if (dataGridView1.CurrentRow == null || dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a record to delete.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get RentID from selected row
                int rentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["RentID"].Value);

                // Show confirmation dialog
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this rent record?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (var connection = DbHelper.GetConnection())
                    {
                        connection.Open();
                        string query = "DELETE FROM Rent WHERE RentID = @RentID";
                        var command = new SqlCommand(query, connection);
                        command.Parameters.Add("@RentID", SqlDbType.Int).Value = rentId;

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRentData(); // Refresh the grid
                            ClearFields(); // Clear the form fields
                        }
                        else
                        {
                            MessageBox.Show("Record could not be deleted. It may have been removed already.",
                                "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}