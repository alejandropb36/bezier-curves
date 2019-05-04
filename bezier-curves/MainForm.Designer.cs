namespace bezier_curves
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.WorkSpace = new System.Windows.Forms.Panel();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WorkSpace
            // 
            this.WorkSpace.BackColor = System.Drawing.SystemColors.Window;
            this.WorkSpace.Location = new System.Drawing.Point(12, 104);
            this.WorkSpace.Name = "WorkSpace";
            this.WorkSpace.Size = new System.Drawing.Size(818, 725);
            this.WorkSpace.TabIndex = 0;
            this.WorkSpace.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WorkSpace_MouseClick);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.SystemColors.Menu;
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearButton.Location = new System.Drawing.Point(309, 25);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(167, 58);
            this.ClearButton.TabIndex = 1;
            this.ClearButton.Text = "Limpiar";
            this.ClearButton.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(842, 841);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.WorkSpace);
            this.Name = "MainForm";
            this.Text = "Curvas de Bézier";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel WorkSpace;
        private System.Windows.Forms.Button ClearButton;
    }
}

