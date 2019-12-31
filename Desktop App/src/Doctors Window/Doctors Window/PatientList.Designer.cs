namespace Doctors_Window
{
    partial class PatientList
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
            this.pList = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // pList
            // 
            this.pList.ActiveViewIndex = -1;
            this.pList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pList.Cursor = System.Windows.Forms.Cursors.Default;
            this.pList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pList.EnableRefresh = false;
            this.pList.Location = new System.Drawing.Point(0, 0);
            this.pList.Name = "pList";
            this.pList.ShowCloseButton = false;
            this.pList.ShowCopyButton = false;
            this.pList.ShowGotoPageButton = false;
            this.pList.ShowGroupTreeButton = false;
            this.pList.ShowLogo = false;
            this.pList.ShowPageNavigateButtons = false;
            this.pList.ShowParameterPanelButton = false;
            this.pList.ShowRefreshButton = false;
            this.pList.Size = new System.Drawing.Size(1020, 692);
            this.pList.TabIndex = 0;
            this.pList.ToolPanelWidth = 0;
            // 
            // PatientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 692);
            this.Controls.Add(this.pList);
            this.Name = "PatientList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PatientList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer pList;

    }
}