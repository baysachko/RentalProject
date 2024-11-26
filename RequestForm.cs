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
                    using (var command = new SqlCommand("sp_GetAllRequests", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                if (dataGridView1.Columns.Count > 0)
                {
                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading requests: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        using (var command = new SqlCommand("sp_GetResidentNameById", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ResidentID", residentid.Text);

                            connection.Open();
                            try
                            {
                                var result = command.ExecuteScalar();
                                if (result != null)
                                {
                                    residentname.Text = result.ToString();
                                }
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number == 50001)
                                {
                                    MessageBox.Show("Resident not found!", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    residentname.Text = string.Empty;
                                }
                                else throw;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error finding resident: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        using (var command = new SqlCommand("sp_GetServiceNameById", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ServiceID", serviceid.Text);

                            connection.Open();
                            try
                            {
                                var result = command.ExecuteScalar();
                                if (result != null)
                                {
                                    servicename.Text = result.ToString();
                                }
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number == 50002)
                                {
                                    MessageBox.Show("Service not found!", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    servicename.Text = string.Empty;
                                }
                                else throw;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error finding service: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    using (var command = new SqlCommand("sp_InsertRequest", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RequestDate", requestdate.Value);
                        command.Parameters.AddWithValue("@Description", description.Text);
                        command.Parameters.AddWithValue("@Status", status.Text);
                        command.Parameters.AddWithValue("@ResidentID", Convert.ToInt32(residentid.Text));
                        command.Parameters.AddWithValue("@ServiceID", Convert.ToInt32(serviceid.Text));

                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Request inserted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRequests();
                            ClearForm();
                        }
                        catch (SqlException ex)
                        {
                            switch (ex.Number)
                            {
                                case 50001:
                                    MessageBox.Show("Resident not found!", "Error");
                                    break;
                                case 50002:
                                    MessageBox.Show("Service not found!", "Error");
                                    break;
                                case 50003:
                                    MessageBox.Show("Invalid status value!", "Error");
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
                MessageBox.Show($"Error inserting request: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void update_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            if (string.IsNullOrEmpty(requestid.Text))
            {
                MessageBox.Show("Please select a request to update!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    using (var command = new SqlCommand("sp_UpdateRequest", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RequestID", Convert.ToInt32(requestid.Text));
                        command.Parameters.AddWithValue("@RequestDate", requestdate.Value);
                        command.Parameters.AddWithValue("@Description", description.Text);
                        command.Parameters.AddWithValue("@Status", status.Text);
                        command.Parameters.AddWithValue("@ResidentID", Convert.ToInt32(residentid.Text));
                        command.Parameters.AddWithValue("@ServiceID", Convert.ToInt32(serviceid.Text));

                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Request updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRequests();
                            ClearForm();
                        }
                        catch (SqlException ex)
                        {
                            switch (ex.Number)
                            {
                                case 50001:
                                    MessageBox.Show("Resident not found!", "Error");
                                    break;
                                case 50002:
                                    MessageBox.Show("Service not found!", "Error");
                                    break;
                                case 50003:
                                    MessageBox.Show("Invalid status value!", "Error");
                                    break;
                                case 50004:
                                    MessageBox.Show("Request not found!", "Error");
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
                MessageBox.Show($"Error updating request: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Please enter a description.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(residentid.Text) || string.IsNullOrWhiteSpace(residentname.Text))
            {
                MessageBox.Show("Please enter a valid Resident ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(serviceid.Text) || string.IsNullOrWhiteSpace(servicename.Text))
            {
                MessageBox.Show("Please enter a valid Service ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    using (var command = new SqlCommand("sp_SearchRequests", connection))
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
        if (string.IsNullOrEmpty(requestid.Text))
        {
            MessageBox.Show("Please select a request to delete!", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show("Are you sure you want to delete this request?",
            "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    using (var command = new SqlCommand("sp_DeleteRequest", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RequestID", Convert.ToInt32(requestid.Text));

                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Request deleted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRequests();
                            ClearForm();
                        }
                        catch (SqlException ex)
                        {
                            switch (ex.Number)
                            {
                                case 50004:
                                    MessageBox.Show("Request not found!", "Error");
                                    break;
                                case 50005:
                                    MessageBox.Show("Cannot delete completed requests!", "Error");
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