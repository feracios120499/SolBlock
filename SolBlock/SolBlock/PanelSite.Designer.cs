namespace SolBlock
{
    partial class PanelSite
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.Panel();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.pictureDelete = new Bunifu.Framework.UI.BunifuImageButton();
            this.pictureSite = new System.Windows.Forms.PictureBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSite)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Controls.Add(this.pictureDelete);
            this.panel.Controls.Add(this.bunifuCustomLabel1);
            this.panel.Controls.Add(this.pictureSite);
            this.panel.Location = new System.Drawing.Point(5, 5);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(900, 75);
            this.panel.TabIndex = 1;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(107, 19);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(575, 42);
            this.bunifuCustomLabel1.TabIndex = 3;
            this.bunifuCustomLabel1.Text = "bunifuCustomLabel1";
            // 
            // pictureDelete
            // 
            this.pictureDelete.BackColor = System.Drawing.Color.Transparent;
            this.pictureDelete.Image = global::SolBlock.Properties.Resources.Close_Window_48px;
            this.pictureDelete.ImageActive = null;
            this.pictureDelete.Location = new System.Drawing.Point(825, 0);
            this.pictureDelete.Name = "pictureDelete";
            this.pictureDelete.Size = new System.Drawing.Size(75, 75);
            this.pictureDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureDelete.TabIndex = 4;
            this.pictureDelete.TabStop = false;
            this.pictureDelete.Zoom = 10;
            this.pictureDelete.Click += new System.EventHandler(this.pictureDelete_Click);
            // 
            // pictureSite
            // 
            this.pictureSite.Image = global::SolBlock.Properties.Resources.Cloud_Cross_50px;
            this.pictureSite.Location = new System.Drawing.Point(26, 0);
            this.pictureSite.Name = "pictureSite";
            this.pictureSite.Size = new System.Drawing.Size(75, 75);
            this.pictureSite.TabIndex = 2;
            this.pictureSite.TabStop = false;
            // 
            // PanelSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel);
            this.Name = "PanelSite";
            this.Size = new System.Drawing.Size(910, 85);
            this.Load += new System.EventHandler(this.PanelSite_Load);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSite)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        public Bunifu.Framework.UI.BunifuImageButton pictureDelete;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private System.Windows.Forms.PictureBox pictureSite;
    }
}
