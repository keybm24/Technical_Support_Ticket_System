using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SistemaSoporteTecnico
{
    public partial class VerTicketsControl : UserControl
    {
        private DataGridView grid;
        private ComboBox cmbEstado, cmbPrioridad;

        public VerTicketsControl()
        {
            InitializeComponent();
            InicializarFormulario();
            CargarTickets();
        }

        private void InicializarFormulario()
        {
            this.BackColor = Color.FromArgb(240, 240, 240);

            Label titulo = new Label();
            titulo.Text = "🎫 Ver Tickets";
            titulo.Font = new Font("Arial", 16, FontStyle.Bold);
            titulo.Location = new Point(20, 20);
            titulo.AutoSize = true;
            this.Controls.Add(titulo);

            Panel filtros = new Panel();
            filtros.Location = new Point(20, 60);
            filtros.Size = new Size(900, 50);
            filtros.BackColor = Color.White;
            this.Controls.Add(filtros);

            // Estado
            filtros.Controls.Add(new Label() { Text = "Estado:", Location = new Point(10, 15) });
            cmbEstado = new ComboBox()
            {
                Location = new Point(65, 12),
                Size = new Size(150, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbEstado.Items.AddRange(new string[] { "Todos", "Abierto", "En Progreso", "Resuelto", "Cerrado" });
            cmbEstado.SelectedIndex = 0;
            cmbEstado.SelectedIndexChanged += (s, e) => CargarTickets();
            filtros.Controls.Add(cmbEstado);

            // Prioridad
            filtros.Controls.Add(new Label() { Text = "Prioridad:", Location = new Point(230, 15) });
            cmbPrioridad = new ComboBox()
            {
                Location = new Point(300, 12),
                Size = new Size(150, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPrioridad.Items.AddRange(new string[] { "Todos", "Baja", "Media", "Alta", "Crítica" });
            cmbPrioridad.SelectedIndex = 0;
            cmbPrioridad.SelectedIndexChanged += (s, e) => CargarTickets();
            filtros.Controls.Add(cmbPrioridad);

            // Botones
            Button btnActualizar = new Button()
            {
                Text = "🔄",
                Location = new Point(470, 10),
                Size = new Size(50, 30)
            };
            btnActualizar.Click += (s, e) => CargarTickets();
            filtros.Controls.Add(btnActualizar);

            Button btnCambiarEstado = new Button()
            {
                Text = "✏️ Estado",
                Location = new Point(530, 10),
                Size = new Size(100, 30)
            };
            btnCambiarEstado.Click += BtnCambiarEstado_Click;
            filtros.Controls.Add(btnCambiarEstado);

            Button btnEliminar = new Button()
            {
                Text = "🗑️ Eliminar",
                Location = new Point(640, 10),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White
            };
            btnEliminar.Click += BtnEliminar_Click;
            filtros.Controls.Add(btnEliminar);

            Button btnVolver = new Button()
            {
                Text = "⬅ Volver",
                Location = new Point(750, 10),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White
            };
            btnVolver.Click += BtnVolver_Click;
            filtros.Controls.Add(btnVolver);

            // Grid
            grid = new DataGridView();
            grid.Location = new Point(20, 120);
            grid.Size = new Size(900, 400);
            grid.BackgroundColor = Color.White;
            grid.RowHeadersVisible = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = true; // ✅ IMPORTANTE
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Controls.Add(grid);
        }

        private void CargarTickets()
        {
            try
            {
                string whereEstado = cmbEstado.SelectedItem.ToString() == "Todos" ? "" : $"AND t.estado = '{cmbEstado.SelectedItem}'";
                string wherePrioridad = cmbPrioridad.SelectedItem.ToString() == "Todos" ? "" : $"AND t.prioridad = '{cmbPrioridad.SelectedItem}'";

                var conn = Conexion.ObtenerConexion();
                string query = $@"SELECT t.id AS 'ID', t.titulo AS 'Título',
                                t.categoria AS 'Categoría', t.prioridad AS 'Prioridad',
                                t.estado AS 'Estado',
                                IFNULL(tc.nombre, 'Sin asignar') AS 'Técnico',
                                DATE_FORMAT(t.fecha_creacion, '%d/%m/%Y') AS 'Fecha'
                                FROM tickets t
                                LEFT JOIN tecnicos tc ON t.tecnico_id = tc.id
                                WHERE 1=1 {whereEstado} {wherePrioridad}
                                ORDER BY t.fecha_creacion DESC";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                var dt = new System.Data.DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona al menos un ticket.");
                return;
            }

            var confirm = MessageBox.Show("¿Eliminar tickets seleccionados?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var conn = Conexion.ObtenerConexion();

                foreach (DataGridViewRow row in grid.SelectedRows)
                {
                    int id = Convert.ToInt32(row.Cells["ID"].Value);
                    var cmd = new MySqlCommand("DELETE FROM tickets WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                CargarTickets();

                MessageBox.Show("Tickets eliminados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            Form1 form = (Form1)this.FindForm();
            form.MostrarDashboard();
        }

        private void BtnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(grid.SelectedRows[0].Cells["ID"].Value);

            var conn = Conexion.ObtenerConexion();
            var cmd = new MySqlCommand("UPDATE tickets SET estado='Cerrado' WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();

            CargarTickets();
            MessageBox.Show("Estado actualizado.");
        }
    }
}