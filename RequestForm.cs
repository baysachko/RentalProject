using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using WinFormsApp1.Helper;
using WinFormsApp1.Model;

namespace WinFormsApp1
{
    public partial class RequestForm : Form
    {
        public RequestForm()
        {


            InitializeComponent();
            InitializeForm();
            insertid.Click += insert_Click;
            update.Click += update_Click;
            delete.Click += delete_Click;  
            back.Click += back_Click;


        }

        private void InitializeForm()
        {
            searchbox.TextChanged += searchbox_TextChanged;
            // Initialize status ComboBox if not already done
            if (status.Items.Count == 0)
            {
                status.Items.AddRange(new string[] { "Pending", "Confirmed", "Completed", "Cancelled" });
                status.SelectedIndex = 0;
            }

            // Load the initial data
            LoadRequests();

            // Initialize status ComboBox
            status.Items.AddRange(new string[] { "Pending", "Confirmed", "Completed", "Cancelled" });
            status.SelectedIndex = 0;

            // Set up DataGridView
            LoadRequests();

            // Add event handlers
            residentid.KeyDown += ResidentID_KeyDown;
            serviceid.KeyDown += ServiceID_KeyDown;
            dataGridView1.CellClick += dataGridView1_CellClick;

            // Make requestid read-only
            requestid.ReadOnly = true;
        }

        private void LoadRequests()
        {
            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Request ORDER BY RequestID DESC";
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading requests: {ex.Message}");
            }
            if (dataGridView1.Columns.Count > 0)
            {
                FormatDataGridView();
            }

        }

        private void ResidentID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    using (var connection = DbHelper.GetConnection())
                    {
                        connection.Open();
                        string query = "SELECT ResidentName FROM Resident WHERE ResidentID = @ResidentID";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ResidentID", residentid.Text);
                            var result = command.ExecuteScalar();
                            if (result != null)
                            {
                                residentname.Text = result.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Resident not found!");
                                residentname.Text = string.Empty;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error finding resident: {ex.Message}");
                }
            }
        }

        private void ServiceID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    using (var connection = DbHelper.GetConnection())
                    {
                        connection.Open();
                        string query = "SELECT ServiceName FROM Service WHERE ServiceID = @ServiceID";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ServiceID", serviceid.Text);
                            var result = command.ExecuteScalar();
                            if (result != null)
                            {
                                servicename.Text = result.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Service not found!");
                                servicename.Text = string.Empty;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error finding service: {ex.Message}");
                }
            }
        }

        private void insert_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"INSERT INTO Request 
                           (RequestDate, Description, Status, ResidentID, ServiceID) 
                           VALUES 
                           (@RequestDate, @Description, @Status, @ResidentID, @ServiceID)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RequestDate", requestdate.Value);
                        command.Parameters.AddWithValue("@Description", description.Text);
                        command.Parameters.AddWithValue("@Status", status.Text);
                        command.Parameters.AddWithValue("@ResidentID", Convert.ToInt32(residentid.Text));
                        command.Parameters.AddWithValue("@ServiceID", Convert.ToInt32(serviceid.Text));

                        command.ExecuteNonQuery();
                        MessageBox.Show("Request inserted successfully!");
                        LoadRequests();
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting request: {ex.Message}");
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            if (string.IsNullOrEmpty(requestid.Text))
            {
                MessageBox.Show("Please select a request to update!");
                return;
            }

            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"UPDATE Request 
                           SET RequestDate = @RequestDate,
                               Description = @Description,
                               Status = @Status,
                               ResidentID = @ResidentID,
                               ServiceID = @ServiceID
                           WHERE RequestID = @RequestID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RequestID", Convert.ToInt32(requestid.Text));
                        command.Parameters.AddWithValue("@RequestDate", requestdate.Value);
                        command.Parameters.AddWithValue("@Description", description.Text);
                        command.Parameters.AddWithValue("@Status", status.Text);
                        command.Parameters.AddWithValue("@ResidentID", Convert.ToInt32(residentid.Text));
                        command.Parameters.AddWithValue("@ServiceID", Convert.ToInt32(serviceid.Text));

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Request updated successfully!");
                            LoadRequests();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("No request was updated. Please check if the request exists.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating request: {ex.Message}");
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            requestid.Text = string.Empty;
            description.Text = string.Empty;
            status.SelectedIndex = 0;
            residentid.Text = string.Empty;
            serviceid.Text = string.Empty;
            residentname.Text = string.Empty;
            servicename.Text = string.Empty;
            requestdate.Value = DateTime.Now;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Safely get values with null checking
                requestid.Text = row.Cells["RequestID"].Value?.ToString() ?? string.Empty;

                if (row.Cells["RequestDate"].Value != DBNull.Value && row.Cells["RequestDate"].Value != null)
                {
                    requestdate.Value = Convert.ToDateTime(row.Cells["RequestDate"].Value);
                }
                else
                {
                    requestdate.Value = DateTime.Now;
                }

                description.Text = row.Cells["Description"].Value?.ToString() ?? string.Empty;
                status.Text = row.Cells["Status"].Value?.ToString() ?? string.Empty;
                residentid.Text = row.Cells["ResidentID"].Value?.ToString() ?? string.Empty;
                serviceid.Text = row.Cells["ServiceID"].Value?.ToString() ?? string.Empty;
                residentname.Text = row.Cells["ResidentName"].Value?.ToString() ?? string.Empty;
                servicename.Text = row.Cells["ServiceName"].Value?.ToString() ?? string.Empty;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(description.Text))
            {
                MessageBox.Show("Please enter a description.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(residentid.Text) || string.IsNullOrWhiteSpace(residentname.Text))
            {
                MessageBox.Show("Please enter a valid Resident ID.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(serviceid.Text) || string.IsNullOrWhiteSpace(servicename.Text))
            {
                MessageBox.Show("Please enter a valid Service ID.");
                return false;
            }

            return true;
        }

        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT r.RequestID, r.RequestDate, r.Description, r.Status, 
                   r.ResidentID, r.ServiceID, res.ResidentName, s.ServiceName
                   FROM Request r
                   INNER JOIN Resident res ON r.ResidentID = res.ResidentID
                   INNER JOIN Service s ON r.ServiceID = s.ServiceID
                   WHERE res.ResidentName LIKE @SearchTerm + '%'
                   OR r.Description LIKE @SearchTerm + '%'
                   OR r.Status LIKE @SearchTerm + '%'
                   OR s.ServiceName LIKE @SearchTerm + '%'";

                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SearchTerm", searchbox.Text);
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            // Set column headers
            dataGridView1.Columns["RequestID"].HeaderText = "Request ID";
            dataGridView1.Columns["RequestDate"].HeaderText = "Request Date";
            dataGridView1.Columns["Description"].HeaderText = "Description";
            dataGridView1.Columns["Status"].HeaderText = "Status";
            dataGridView1.Columns["ResidentID"].HeaderText = "Resident ID";
            dataGridView1.Columns["ServiceID"].HeaderText = "Service ID";
            dataGridView1.Columns["ResidentName"].HeaderText = "Resident Name";
            dataGridView1.Columns["ServiceName"].HeaderText = "Service Name";

            // Format date column
            dataGridView1.Columns["RequestDate"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Set column widths
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            // Optional: Set alternating row colors for better readability
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Make the grid read-only
            dataGridView1.ReadOnly = true;

            // Enable sorting
            dataGridView1.Sort(dataGridView1.Columns["RequestID"], ListSortDirection.Descending);
        }

        private void searchbox_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void delete_Click(object sender, EventArgs e)
        {
            // Check if a record is selected
            if (string.IsNullOrEmpty(requestid.Text))
            {
                MessageBox.Show("Please select a request to delete!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirm deletion
            var result = MessageBox.Show("Are you sure you want to delete this request?",
                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var connection = DbHelper.GetConnection())
                    {
                        connection.Open();
                        string query = "DELETE FROM Request WHERE RequestID = @RequestID";

                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@RequestID", Convert.ToInt32(requestid.Text));

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Request deleted successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadRequests(); // Refresh the grid
                                ClearForm(); // Clear the form fields
                            }
                            else
                            {
                                MessageBox.Show("No request was deleted. Please check if the request exists.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting request: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void back_Click(object sender, EventArgs e)
        {
            // Close the current form
            this.Close();
        }
    }
}