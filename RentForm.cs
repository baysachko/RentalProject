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
                    using (var command = new SqlCommand("sp_GetAllRents", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        // Configure DataGridView properties
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dataGridView1.MultiSelect = false;
                        dataGridView1.ReadOnly = true;
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.RowHeadersVisible = false;

                        // Set the data source
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns.Count > 0)
                        {
                            FormatDataGridView();
                        }
                    }
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

                decimal rentAmount;
                if (!decimal.TryParse(rentamount.Text, out rentAmount))
                {
                    MessageBox.Show("Please enter a valid rent amount.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var connection = DbHelper.GetConnection())
                {
                    using (var command = new SqlCommand("sp_InsertRent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startdate.Value.Date);
                        command.Parameters.AddWithValue("@EndDate", enddate.Value.Date);
                        command.Parameters.AddWithValue("@RentAmount", rentAmount);
                        command.Parameters.AddWithValue("@ResidentID", int.Parse(residentid.Text));
                        command.Parameters.AddWithValue("@RoomID", int.Parse(roomid.Text));

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Rent record inserted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRentData();
                            ClearFields();
                        }
                        catch (SqlException ex)
                        {
                            switch (ex.Number)
                            {
                                case 50001:
                                    MessageBox.Show("Resident not found.", "Error");
                                    break;
                                case 50002:
                                    MessageBox.Show("Room not found.", "Error");
                                    break;
                                case 50003:
                                    MessageBox.Show("End date cannot be earlier than start date.", "Error");
                                    break;
                                case 50004:
                                    MessageBox.Show("Rent amount must be greater than zero.", "Error");
                                    break;
                                case 50005:
                                    MessageBox.Show("Room is already rented during this period.", "Error");
                                    break;
                                default:
                                    throw;
                            }
                        }
                    }
                }
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

                decimal rentAmount;
                if (!decimal.TryParse(rentamount.Text, out rentAmount))
                {
                    MessageBox.Show("Please enter a valid rent amount.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var connection = DbHelper.GetConnection())
                {
                    using (var command = new SqlCommand("sp_UpdateRent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RentID", int.Parse(rentid.Text));
                        command.Parameters.AddWithValue("@StartDate", startdate.Value.Date);
                        command.Parameters.AddWithValue("@EndDate", enddate.Value.Date);
                        command.Parameters.AddWithValue("@RentAmount", rentAmount);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Rent record updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRentData();
                            ClearFields();
                        }
                        catch (SqlException ex)
                        {
                            switch (ex.Number)
                            {
                                case 50003:
                                    MessageBox.Show("End date cannot be earlier than start date.", "Error");
                                    break;
                                case 50004:
                                    MessageBox.Show("Rent amount must be greater than zero.", "Error");
                                    break;
                                case 50005:
                                    MessageBox.Show("Room is already rented during this period.", "Error");
                                    break;
                                case 50006:
                                    MessageBox.Show("Rent record not found.", "Error");
                                    break;
                                default:
                                    throw;
                            }
                        }
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
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to clear all fields?",
                    "Confirm Clear",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ClearFields();
                    if (dataGridView1.CurrentRow != null)
                    {
                        dataGridView1.ClearSelection();
                    }
                }
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
                        using (var command = new SqlCommand("sp_GetResidentNameById", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ResidentID", id);

                            try
                            {
                                connection.Open();
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
                            catch (SqlException ex)
                            {
                                if (ex.Number == 50001)
                                {
                                    residentname.Clear();
                                }
                                else throw;
                            }
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
                        using (var command = new SqlCommand("sp_GetRoomNumberById", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@RoomID", id);

                            try
                            {
                                connection.Open();
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
                            catch (SqlException ex)
                            {
                                if (ex.Number == 50002)
                                {
                                    roomnum.Clear();
                                }
                                else throw;
                            }
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

                    // Pre-populate all fields with null checking
                    rentid.Text = row.Cells["RentID"].Value?.ToString() ?? string.Empty;

                    if (DateTime.TryParse(row.Cells["StartDate"].Value?.ToString(), out DateTime startDt))
                    {
                        startdate.Value = startDt;
                    }

                    if (DateTime.TryParse(row.Cells["EndDate"].Value?.ToString(), out DateTime endDt))
                    {
                        enddate.Value = endDt;
                    }

                    rentamount.Text = row.Cells["RentAmount"].Value?.ToString() ?? string.Empty;
                    roomid.Text = row.Cells["RoomID"].Value?.ToString() ?? string.Empty;
                    residentid.Text = row.Cells["ResidentID"].Value?.ToString() ?? string.Empty;
                    residentname.Text = row.Cells["ResidentName"].Value?.ToString() ?? string.Empty;
                    roomnum.Text = row.Cells["RoomNumber"].Value?.ToString() ?? string.Empty;

                    // Select the entire row
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting record: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    using (var command = new SqlCommand("sp_SearchRents", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SearchTerm", searchbox.Text.Trim());

                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns.Count > 0)
                        {
                            FormatDataGridView();
                        }
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
            dataGridView1.Columns["RentID"].HeaderText = "Rent ID";
            dataGridView1.Columns["StartDate"].HeaderText = "Start Date";
            dataGridView1.Columns["EndDate"].HeaderText = "End Date";
            dataGridView1.Columns["RentAmount"].HeaderText = "Rent Amount";
            dataGridView1.Columns["RoomID"].HeaderText = "Room ID";
            dataGridView1.Columns["ResidentID"].HeaderText = "Resident ID";
            dataGridView1.Columns["ResidentName"].HeaderText = "Resident Name";
            dataGridView1.Columns["RoomNumber"].HeaderText = "Room Number";

            dataGridView1.Columns["StartDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.Columns["EndDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView1.Columns["RentAmount"].DefaultCellStyle.Format = "C2";

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(rentid.Text))
                {
                    MessageBox.Show("Please select a record to delete.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this rent record?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (var connection = DbHelper.GetConnection())
                    {
                        using (var command = new SqlCommand("sp_DeleteRent", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@RentID", int.Parse(rentid.Text));

                            try
                            {
                                connection.Open();
                                command.ExecuteNonQuery();
                                MessageBox.Show("Rent record deleted successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadRentData();
                                ClearFields();
                            }
                            catch (SqlException ex)
                            {
                                switch (ex.Number)
                                {
                                    case 50006:
                                        MessageBox.Show("Rent record not found.", "Error");
                                        break;
                                    case 50007:
                                        MessageBox.Show("Cannot delete an active rent record.", "Error");
                                        break;
                                    default:
                                        throw;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting rent record: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}