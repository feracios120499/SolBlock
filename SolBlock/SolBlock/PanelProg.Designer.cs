namespace SolBlock
{
    partial class PanelProg
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
            this.typeBlock = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.pictureDelete = new Bunifu.Framework.UI.BunifuImageButton();
            this.nameProg = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.pictureProg = new System.Windows.Forms.PictureBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProg)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Controls.Add(this.typeBlock);
            this.panel.Controls.Add(this.pictureDelete);
            this.panel.Controls.Add(this.nameProg);
            this.panel.Controls.Add(this.pictureProg);
            this.panel.Location = new System.Drawing.Point(5, 5);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(900, 75);
            this.panel.TabIndex = 1;
            // 
            // typeBlock
            // 
            this.typeBlock.Font = new System.Drawing.Font("Century Gothic", 18F);
            this.typeBlock.ForeColor = System.Drawing.Color.White;
            this.typeBlock.Location = new System.Drawing.Point(407, 19);
            this.typeBlock.Name = "typeBlock";
            this.typeBlock.Size = new System.Drawing.Size(379, 42);
            this.typeBlock.TabIndex = 5;
            this.typeBlock.Text = "bunifuCustomLabel2";
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
            // nameProg
            // 
            this.nameProg.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameProg.ForeColor = System.Drawing.Color.White;
            this.nameProg.Location = new System.Drawing.Point(107, 19);
            this.nameProg.Name = "nameProg";
            this.nameProg.Size = new System.Drawing.Size(240, 42);
            this.nameProg.TabIndex = 3;
            this.nameProg.Text = "bunifuCustomLabel1";
            // 
            // pictureProg
            // 
            this.pictureProg.Image = global::SolBlock.Properties.Resources.Delete_Folder_50px;
            this.pictureProg.Location = new System.Drawing.Point(26, 0);
            this.pictureProg.Name = "pictureProg";
            this.pictureProg.Size = new System.Drawing.Size(75, 75);
            this.pictureProg.TabIndex = 2;
            this.pictureProg.TabStop = false;
            // 
            // PanelProg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel);
            this.Name = "PanelProg";
            this.Size = new System.Drawing.Size(910, 85);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private Bunifu.Framework.UI.BunifuCustomLabel typeBlock;
        public Bunifu.Framework.UI.BunifuImageButton pictureDelete;
        private Bunifu.Framework.UI.BunifuCustomLabel nameProg;
        private System.Windows.Forms.PictureBox pictureProg;
    }
}
