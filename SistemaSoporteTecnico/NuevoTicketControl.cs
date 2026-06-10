using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SistemaSoporteTecnico
{
    public partial class NuevoTicketControl : UserControl
    {
        public NuevoTicketControl()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private ComboBox cmbPrioridad, cmbCategoria, cmbTecnico;
        private TextBox txtTitulo, txtDescripcion;

        private void InicializarFormulario()
        {
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Padding = new Padding(20);

            // Título
            Label titulo = new Label();
            titulo.Text = "🎫 Nuevo Ticket";
            titulo.Font = new Font("Arial", 16, FontStyle.Bold);
            titulo.ForeColor = Color.FromArgb(50, 50, 50);
            titulo.Location = new Point(20, 20);
            titulo.AutoSize = true;
            this.Controls.Add(titulo);

            // Panel formulario
            Panel panel = new Panel();
            panel.Location = new Point(20, 60);
            panel.Size = new Size(600, 420);
            panel.BackColor = Color.White;
            panel.Padding = new Padding(20);
            this.Controls.Add(panel);

            int y = 20;

            // Título del ticket
            AgregarLabel(panel, "Título:", 20, y);
            txtTitulo = new TextBox();
            txtTitulo.Location = new Point(20, y + 25);
            txtTitulo.Size = new Size(550, 30);
            txtTitulo.Font = new Font("Arial", 10);
            panel.Controls.Add(txtTitulo);
            y += 70;

            // Descripción
            AgregarLabel(panel, "Descripción:", 20, y);
            txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(20, y + 25);
            txtDescripcion.Size = new Size(550, 80);
            txtDescripcion.Multiline = true;
            txtDescripcion.Font = new Font("Arial", 10);
            panel.Controls.Add(txtDescripcion);
            y += 125;

            // Categoría
            AgregarLabel(panel, "Categoría:", 20, y);
            cmbCategoria = new ComboBox();
            cmbCategoria.Location = new Point(20, y + 25);
            cmbCategoria.Size = new Size(170, 30);
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Items.AddRange(new string[] { "Hardware", "Software", "Redes", "Otro" });
            cmbCategoria.SelectedIndex = 0;
            panel.Controls.Add(cmbCategoria);

            // Prioridad
            AgregarLabel(panel, "Prioridad:", 210, y);
            cmbPrioridad = new ComboBox();
            cmbPrioridad.Location = new Point(210, y + 25);
            cmbPrioridad.Size = new Size(170, 30);
            cmbPrioridad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPrioridad.Items.AddRange(new string[] { "Baja", "Media", "Alta", "Crítica" });
            cmbPrioridad.SelectedIndex = 1;
            panel.Controls.Add(cmbPrioridad);
            y += 70;

            // Técnico
            AgregarLabel(panel, "Asignar Técnico:", 20, y);
            cmbTecnico = new ComboBox();
            cmbTecnico.Location = new Point(20, y + 25);
            cmbTecnico.Size = new Size(250, 30);
            cmbTecnico.DropDownStyle = ComboBoxStyle.DropDownList;
            CargarTecnicos();
            panel.Controls.Add(cmbTecnico);
            y += 70;

            // Botón guardar
            Button btnGuardar = new Button();
            btnGuardar.Text = "💾 Guardar Ticket";
            btnGuardar.Location = new Point(20, y);
            btnGuardar.Size = new Size(160, 40);
            btnGuardar.BackColor = Color.FromArgb(39, 174, 96);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Click += BtnGuardar_Click;
            panel.Controls.Add(btnGuardar);
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
                cmbTecnico.Items.Add("Sin asignar");
                var conn = Conexion.ObtenerConexion();
                string query = "SELECT id, nombre FROM tecnicos ORDER BY nombre";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbTecnico.Items.Add(new TecnicoItem(
                        Convert.ToInt32(reader["id"]),
                        reader["nombre"].ToString()
                    ));
                }
                reader.Close();
                conn.Close();
                cmbTecnico.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar técnicos: " + ex.Message);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("El título es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var conn = Conexion.ObtenerConexion();
                string query = @"INSERT INTO tickets (titulo, descripcion, categoria, prioridad, estado, tecnico_id)
                                VALUES (@titulo, @desc, @cat, @prior, 'Abierto', @tecnico)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@titulo", txtTitulo.Text);
                cmd.Parameters.AddWithValue("@desc", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@cat", cmbCategoria.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@prior", cmbPrioridad.SelectedItem.ToString());

                int? tecnicoId = null;
                if (cmbTecnico.SelectedItem is TecnicoItem tecnico)
                    tecnicoId = tecnico.Id;
                cmd.Parameters.AddWithValue("@tecnico", (object)tecnicoId ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("¡Ticket creado exitosamente!", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtTitulo.Clear();
                txtDescripcion.Clear();
                cmbCategoria.SelectedIndex = 0;
                cmbPrioridad.SelectedIndex = 1;
                cmbTecnico.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar ticket: " + ex.Message);
            }
        }
    }

    public class TecnicoItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TecnicoItem(int id, string nombre) { Id = id; Nombre = nombre; }
        public override string ToString() => Nombre;
    }
}