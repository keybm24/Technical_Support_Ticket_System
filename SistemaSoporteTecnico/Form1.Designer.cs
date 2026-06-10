namespace SistemaSoporteTecnico
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ticketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuvoTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verTicketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarTécnicosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ticketsToolStripMenuItem,
            this.tToolStripMenuItem,
            this.acercaDeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ticketsToolStripMenuItem
            // 
            this.ticketsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuvoTicketToolStripMenuItem,
            this.verTicketsToolStripMenuItem});
            this.ticketsToolStripMenuItem.Name = "ticketsToolStripMenuItem";
            this.ticketsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.ticketsToolStripMenuItem.Text = "Tickets";
            // 
            // nuvoTicketToolStripMenuItem
            // 
            this.nuvoTicketToolStripMenuItem.Name = "nuvoTicketToolStripMenuItem";
            this.nuvoTicketToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nuvoTicketToolStripMenuItem.Text = "Nuevo Ticket";
            this.nuvoTicketToolStripMenuItem.Click += new System.EventHandler(this.nuevoTicketToolStripMenuItem_Click);
            // 
            // verTicketsToolStripMenuItem
            // 
            this.verTicketsToolStripMenuItem.Name = "verTicketsToolStripMenuItem";
            this.verTicketsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.verTicketsToolStripMenuItem.Text = "Ver Tickets";
            this.verTicketsToolStripMenuItem.Click += new System.EventHandler(this.verTicketsToolStripMenuItem_Click);
            // 
            // tToolStripMenuItem
            // 
            this.tToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionarTécnicosToolStripMenuItem});
            this.tToolStripMenuItem.Name = "tToolStripMenuItem";
            this.tToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.tToolStripMenuItem.Text = "Técnicos ";
            // 
            // gestionarTécnicosToolStripMenuItem
            // 
            this.gestionarTécnicosToolStripMenuItem.Name = "gestionarTécnicosToolStripMenuItem";
            this.gestionarTécnicosToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gestionarTécnicosToolStripMenuItem.Text = "Gestionar Técnicos";
            this.gestionarTécnicosToolStripMenuItem.Click += new System.EventHandler(this.gestionarTecnicosToolStripMenuItem_Click);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.acercaDeToolStripMenuItem.Text = "Acerca de";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 24);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(984, 587);
            this.panelPrincipal.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Soporte Técnico";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ticketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuvoTicketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verTicketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarTécnicosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.Panel panelPrincipal;
    }
}

