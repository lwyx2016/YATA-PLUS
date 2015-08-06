namespace YATA {
    partial class Sim {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sim));
            this.Overlay_back_img = new System.Windows.Forms.PictureBox();
            this.bottomImage = new System.Windows.Forms.PictureBox();
            this.topImage = new System.Windows.Forms.PictureBox();
            this.overlay_text_img = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Overlay_back_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.overlay_text_img)).BeginInit();
            this.SuspendLayout();
            // 
            // Overlay_back_img
            // 
            this.Overlay_back_img.BackColor = System.Drawing.Color.Transparent;
            this.Overlay_back_img.Image = global::YATA.Properties.Resources.top_overlay_background;
            this.Overlay_back_img.Location = new System.Drawing.Point(0, 0);
            this.Overlay_back_img.Name = "Overlay_back_img";
            this.Overlay_back_img.Size = new System.Drawing.Size(400, 240);
            this.Overlay_back_img.TabIndex = 2;
            this.Overlay_back_img.TabStop = false;
            // 
            // bottomImage
            // 
            this.bottomImage.Location = new System.Drawing.Point(40, 240);
            this.bottomImage.Name = "bottomImage";
            this.bottomImage.Size = new System.Drawing.Size(320, 240);
            this.bottomImage.TabIndex = 1;
            this.bottomImage.TabStop = false;
            // 
            // topImage
            // 
            this.topImage.Location = new System.Drawing.Point(0, 0);
            this.topImage.Name = "topImage";
            this.topImage.Size = new System.Drawing.Size(400, 240);
            this.topImage.TabIndex = 0;
            this.topImage.TabStop = false;
            // 
            // overlay_text_img
            // 
            this.overlay_text_img.BackColor = System.Drawing.Color.Transparent;
            this.overlay_text_img.Image = global::YATA.Properties.Resources.top_overlay_text;
            this.overlay_text_img.Location = new System.Drawing.Point(0, 0);
            this.overlay_text_img.Name = "overlay_text_img";
            this.overlay_text_img.Size = new System.Drawing.Size(400, 240);
            this.overlay_text_img.TabIndex = 3;
            this.overlay_text_img.TabStop = false;
            // 
            // Sim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(400, 482);
            this.Controls.Add(this.overlay_text_img);
            this.Controls.Add(this.Overlay_back_img);
            this.Controls.Add(this.bottomImage);
            this.Controls.Add(this.topImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Sim";
            this.Text = "Theme Simulator";
            this.Load += new System.EventHandler(this.Sim_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sim_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Overlay_back_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.overlay_text_img)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox topImage;
        private System.Windows.Forms.PictureBox bottomImage;
        private System.Windows.Forms.PictureBox Overlay_back_img;
        private System.Windows.Forms.PictureBox overlay_text_img;
    }
}