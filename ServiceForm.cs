// ServiceForm.cs
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using WinFormsApp1.Helper;

namespace WinFormsApp1
{
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            InitializeComponent();
            LoadServices(); // Load services when form opens
            servicegrid.SelectionChanged += Servicegrid_SelectionChanged;
            // Wire up the event handlers
            insert.Click += Insert_Click;
            update.Click += Update_Click;
            clear.Click += Clear_Click;
            back.Click += Back_Click;
            delete.Click += Delete_Click;
            searchbox.TextChanged += Searchbox_TextChanged;
            servicegrid.SelectionChanged += Servicegrid_SelectionChanged;
        }
        private void InitializeGrid()
        {
            servicegrid.AutoGenerateColumns = true;
            servicegrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            servicegrid.MultiSelect = false;
            servicegrid.ReadOnly = true;
        }

        private void LoadServices()
        {
            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    using (var command = new SqlCommand("sp_GetAllServices", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        servicegrid.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading services: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(servicename.Text))
                {
                    MessageBox.Show("Please enter a service name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(servicecost.Text, out decimal cost))
                {
                    MessageBox.Show("Please enter a valid cost.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var connection = DbHelper.GetConnection())
                {
                    using (var command = new SqlCommand("sp_InsertService", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ServiceName", servicename.Text.Trim());
                        command.Parameters.AddWithValue("@ServiceDescription", servicedescription.Text.Trim());
                        command.Parameters.AddWithValue("@Cost", cost);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Service added successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                            LoadServices();
                        }
                        catch (SqlException ex)
                        {
                            switch (ex.Number)
                            {
                                case 50001:
                                    MessageBox.Show("Service name cannot be empty.", "Error");
                                    break;
                                case 50002:
                                    MessageBox.Show("Cost cannot be negative.", "Error");
                                    break;
                                case 50003:
                                    MessageBox.Show("Service name already exists.", "Error");
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
                MessageBox.Show($"Error inserting service: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serviceid.Text))
                {
                    MessageBox.Show("Please select a service to update.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(servicecost.Text, out decimal cost))
                {
                    MessageBox.Show("Please enter a valid cost.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var connection = DbHelper.GetConnection())
                {
                    using (var command = new SqlCommand("sp_UpdateService", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ServiceID", int.Parse(serviceid.Text));
                        command.Parameters.AddWithValue("@ServiceName", servicename.Text.Trim());
                        command.Parameters.AddWithValue("@ServiceDescription", servicedescription.Text.Trim());
                        command.Parameters.AddWithValue("@Cost", cost);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Service updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                            LoadServices();
                        }
                        catch (SqlException ex)
                        {
                            switch (ex.Number)
                            {
                                case 50001:
                                    MessageBox.Show("Service name cannot be empty.", "Error");
                                    break;
                                case 50002:
                                    MessageBox.Show("Cost cannot be negative.", "Error");
                                    break;
                                case 50003:
                                    MessageBox.Show("Service name already exists.", "Error");
                                    break;
                                case 50004:
                                    MessageBox.Show("Service not found.", "Error");
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
                MessageBox.Show($"Error updating service: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            serviceid.Clear();
            servicename.Clear();
            servicedescription.Clear();
            servicecost.Clear();
            servicename.Focus();
        }

        private void Servicegrid_SelectionChanged(object sender, EventArgs e)
        {
            if (servicegrid.CurrentRow != null)
            {
                serviceid.Text = servicegrid.CurrentRow.Cells["ServiceID"].Value.ToString();
                servicename.Text = servicegrid.CurrentRow.Cells["ServiceName"].Value.ToString();
                servicedescription.Text = servicegrid.CurrentRow.Cells["ServiceDescription"].Value.ToString();
                servicecost.Text = servicegrid.CurrentRow.Cells["Cost"].Value.ToString();
            }
        }

        private void Searchbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (var connection = DbHelper.GetConnection())
                {
                    using (var command = new SqlCommand("sp_SearchServices", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SearchTerm", searchbox.Text.Trim());
                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        servicegrid.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(serviceid.Text))
            {
                MessageBox.Show("Please select a service to delete.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this service? This action cannot be undone.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (var connection = DbHelper.GetConnection())
                    {
                        using (var command = new SqlCommand("sp_DeleteService", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ServiceID", int.Parse(serviceid.Text));

                            try
                            {
                                connection.Open();
                                command.ExecuteNonQuery();
                                MessageBox.Show("Service deleted successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearFields();
                                LoadServices();
                            }
                            catch (SqlException ex)
                            {
                                switch (ex.Number)
                                {
                                    case 50004:
                                        MessageBox.Show("Service not found.", "Error");
                                        break;
                                    case 50005:
                                        MessageBox.Show("Cannot delete service because it is referenced in requests.", "Error");
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
                MessageBox.Show($"Error deleting service: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}