using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SistemaSoporteTecnico
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
            CargarResumen();
        }

        private void CargarResumen()
        {
            try
            {
                var conn = Conexion.ObtenerConexion();

                // Contar tickets por estado
                int abiertos = ContarTickets(conn, "Abierto");
                int enProgreso = ContarTickets(conn, "En Progreso");
                int resueltos = ContarTickets(conn, "Resuelto");
                int cerrados = ContarTickets(conn, "Cerrado");
                conn.Close();

                // Panel principal
                this.BackColor = Color.FromArgb(240, 240, 240);
                this.Padding = new Padding(20);

                // Título
                Label titulo = new Label();
                titulo.Text = "📊 Dashboard - Resumen de Tickets";
                titulo.Font = new Font("Arial", 16, FontStyle.Bold);
                titulo.ForeColor = Color.FromArgb(50, 50, 50);
                titulo.Location = new Point(20, 20);
                titulo.AutoSize = true;
                this.Controls.Add(titulo);

                // Tarjetas
                AgregarTarjeta("Abiertos", abiertos, Color.FromArgb(231, 76, 60), 20, 70);
                AgregarTarjeta("En Progreso", enProgreso, Color.FromArgb(241, 196, 15), 220, 70);
                AgregarTarjeta("Resueltos", resueltos, Color.FromArgb(39, 174, 96), 420, 70);
                AgregarTarjeta("Cerrados", cerrados, Color.FromArgb(52, 152, 219), 620, 70);

                // Tickets recientes
                Label lblRecientes = new Label();
                lblRecientes.Text = "🕒 Tickets Recientes";
                lblRecientes.Font = new Font("Arial", 13, FontStyle.Bold);
                lblRecientes.Location = new Point(20, 220);
                lblRecientes.AutoSize = true;
                this.Controls.Add(lblRecientes);

                DataGridView grid = new DataGridView();
                grid.Location = new Point(20, 255);
                grid.Size = new Size(900, 250);
                grid.BackgroundColor = Color.White;
                grid.BorderStyle = BorderStyle.None;
                grid.RowHeadersVisible = false;
                grid.AllowUserToAddRows = false;
                grid.ReadOnly = true;
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                var conn2 = Conexion.ObtenerConexion();
                string query = @"SELECT t.id AS 'ID', t.titulo AS 'Título', 
                                t.prioridad AS 'Prioridad', t.estado AS 'Estado',
                                IFNULL(tc.nombre, 'Sin asignar') AS 'Técnico',
                                t.fecha_creacion AS 'Fecha'
                                FROM tickets t
                                LEFT JOIN tecnicos tc ON t.tecnico_id = tc.id
                                ORDER BY t.fecha_creacion DESC LIMIT 10";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn2);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                conn2.Close();

                this.Controls.Add(grid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar dashboard: " + ex.Message);
            }
        }

        private int ContarTickets(MySqlConnection conn, string estado)
        {
            string query = "SELECT COUNT(*) FROM tickets WHERE estado = @estado";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@estado", estado);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private void AgregarTarjeta(string titulo, int cantidad, Color color, int x, int y)
        {
            Panel tarjeta = new Panel();
            tarjeta.Location = new Point(x, y);
            tarjeta.Size = new Size(180, 100);
            tarjeta.BackColor = color;

            Label lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.Font = new Font("Arial", 11, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(10, 15);
            lblTitulo.AutoSize = true;
            tarjeta.Controls.Add(lblTitulo);

            Label lblCantidad = new Label();
            lblCantidad.Text = cantidad.ToString();
            lblCantidad.Font = new Font("Arial", 28, FontStyle.Bold);
            lblCantidad.ForeColor = Color.White;
            lblCantidad.Location = new Point(10, 45);
            lblCantidad.AutoSize = true;
            tarjeta.Controls.Add(lblCantidad);

            this.Controls.Add(tarjeta);
        }
    }
}