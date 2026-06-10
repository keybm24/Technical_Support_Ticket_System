using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SistemaSoporteTecnico
{
    public partial class TecnicosControl : UserControl
    {
        private DataGridView grid;
        private TextBox txtNombre, txtEmail, txtTelefono, txtEspecialidad;

        public TecnicosControl()
        {
            InitializeComponent();
            InicializarFormulario();
            CargarTecnicos();
        }

        private void InicializarFormulario()
        {
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Título
            Label titulo = new Label();
            titulo.Text = "👨‍💻 Gestionar Técnicos";
            titulo.Font = new Font("Arial", 16, FontStyle.Bold);
            titulo.ForeColor = Color.FromArgb(50, 50, 50);
            titulo.Location = new Point(20, 20);
            titulo.AutoSize = true;
            this.Controls.Add(titulo);

            // Panel formulario
            Panel panel = new Panel();
            panel.Location = new Point(20, 60);
            panel.Size = new Size(400, 450);
            panel.BackColor = Color.White;
            this.Controls.Add(panel);

            int y = 20;

            AgregarLabel(panel, "Nombre:", 20, y);
            txtNombre = new TextBox();
            txtNombre.Location = new Point(20, y + 25);
            txtNombre.Size = new Size(350, 30);
            txtNombre.Font = new Font("Arial", 10);
            panel.Controls.Add(txtNombre);
            y += 80;

            AgregarLabel(panel, "Email:", 20, y);
            txtEmail = new TextBox();
            txtEmail.Location = new Point(20, y + 25);
            txtEmail.Size = new Size(350, 30);
            txtEmail.Font = new Font("Arial", 10);
            panel.Controls.Add(txtEmail);
            y += 80;

            AgregarLabel(panel, "Teléfono:", 20, y);
            txtTelefono = new TextBox();
            txtTelefono.Location = new Point(20, y + 25);
            txtTelefono.Size = new Size(350, 30);
            txtTelefono.Font = new Font("Arial", 10);
            panel.Controls.Add(txtTelefono);
            y += 80;

            AgregarLabel(panel, "Especialidad:", 20, y);
            txtEspecialidad = new TextBox();
            txtEspecialidad.Location = new Point(20, y + 25);
            txtEspecialidad.Size = new Size(350, 30);
            txtEspecialidad.Font = new Font("Arial", 10);
            panel.Controls.Add(txtEspecialidad);
            y += 80;

            Button btnGuardar = new Button();
            btnGuardar.Text = "💾 Agregar Técnico";
            btnGuardar.Location = new Point(20, y);
            btnGuardar.Size = new Size(180, 40);
            btnGuardar.BackColor = Color.FromArgb(39, 174, 96);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Click += BtnGuardar_Click;
            panel.Controls.Add(btnGuardar);

            // Grid
            grid = new DataGridView();
            grid.Location = new Point(440, 60);
            grid.Size = new Size(500, 380);
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.Font = new Font("Arial", 9);
            this.Controls.Add(grid);

            Button btnEliminar = new Button();
            btnEliminar.Text = "🗑️ Eliminar Técnico";
            btnEliminar.Location = new Point(440, 450);
            btnEliminar.Size = new Size(160, 40);
            btnEliminar.BackColor = Color.FromArgb(231, 76, 60);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Click += BtnEliminar_Click;
            this.Controls.Add(btnEliminar);
        }

        private void AgregarLabel(Panel panel, string texto, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = texto;
            lbl.Font = new Font("Arial", 9, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(80, 80, 80);
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            panel.Controls.Add(lbl);
        }

        private void CargarTecnicos()
        {
            try
            {
                var conn = Conexion.ObtenerConexion();
                string query = "SELECT id AS 'ID', nombre AS 'Nombre', email AS 'Email', telefono AS 'Teléfono', especialidad AS 'Especialidad' FROM tecnicos ORDER BY nombre";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar técnicos: " + ex.Message);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var conn = Conexion.ObtenerConexion();
                string query = "INSERT INTO tecnicos (nombre, email, telefono, especialidad) VALUES (@nombre, @email, @tel, @esp)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@tel", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@esp", txtEspecialidad.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("¡Técnico agregado exitosamente!", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombre.Clear();
                txtEmail.Clear();
                txtTelefono.Clear();
                txtEspecialidad.Clear();
                CargarTecnicos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar técnico: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un técnico primero.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(grid.SelectedRows[0].Cells["ID"].Value);
            string nombre = grid.SelectedRows[0].Cells["Nombre"].Value.ToString();

            var confirm = MessageBox.Show($"¿Eliminar al técnico '{nombre}'?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    var conn = Conexion.ObtenerConexion();
                    string query = "DELETE FROM tecnicos WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    CargarTecnicos();
                    MessageBox.Show("Técnico eliminado.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}