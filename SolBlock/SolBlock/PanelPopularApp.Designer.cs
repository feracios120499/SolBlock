namespace SolBlock
{
    partial class PanelPopularApp
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
            this.ImageApp = new System.Windows.Forms.PictureBox();
            this.NameApp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ImageApp)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageApp
            // 
            this.ImageApp.Location = new System.Drawing.Point(4, 4);
            this.ImageApp.Name = "ImageApp";
            this.ImageApp.Size = new System.Drawing.Size(62, 50);
            this.ImageApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageApp.TabIndex = 0;
            this.ImageApp.TabStop = false;
            // 
            // NameApp
            // 
            this.NameApp.AutoSize = true;
            this.NameApp.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameApp.Location = new System.Drawing.Point(72, 20);
            this.NameApp.Name = "NameApp";
            this.NameApp.Size = new System.Drawing.Size(31, 34);
            this.NameApp.TabIndex = 1;
            this.NameApp.Text = "0";
            // 
            // PopularApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NameApp);
            this.Controls.Add(this.ImageApp);
            this.Name = "PopularApp";
            this.Size = new System.Drawing.Size(733, 56);
            ((System.ComponentModel.ISupportInitialize)(this.ImageApp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageApp;
        private System.Windows.Forms.Label NameApp;
    }
}
