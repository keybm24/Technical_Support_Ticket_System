using System;
using System.Windows.Forms;

namespace SistemaSoporteTecnico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MostrarDashboard();
        }

        public void MostrarDashboard()
        {
            panelPrincipal.Controls.Clear();
            DashboardControl dashboard = new DashboardControl();
            dashboard.Dock = DockStyle.Fill;
            panelPrincipal.Controls.Add(dashboard);
        }

        private void nuevoTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelPrincipal.Controls.Clear();
            NuevoTicketControl nuevoTicket = new NuevoTicketControl();
            nuevoTicket.Dock = DockStyle.Fill;
            panelPrincipal.Controls.Add(nuevoTicket);
        }

        private void verTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelPrincipal.Controls.Clear();
            VerTicketsControl verTickets = new VerTicketsControl();
            verTickets.Dock = DockStyle.Fill;
            panelPrincipal.Controls.Add(verTickets);
        }

        private void gestionarTecnicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelPrincipal.Controls.Clear();
            TecnicosControl tecnicos = new TecnicosControl();
            tecnicos.Dock = DockStyle.Fill;
            panelPrincipal.Controls.Add(tecnicos);
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sistema de Soporte Técnico v1.0\nDesarrollado por Keilyn Barrantes",
                "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}