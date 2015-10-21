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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Top_screen_overlay = new System.Windows.Forms.PictureBox();
            this.Arrows_bottom = new System.Windows.Forms.PictureBox();
            this.Overlay_LR_TOP_img = new System.Windows.Forms.PictureBox();
            this.topImage = new System.Windows.Forms.PictureBox();
            this.bottomImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Top_screen_overlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Arrows_bottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Overlay_LR_TOP_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::YATA.Properties.Resources.adv;
            this.pictureBox2.Location = new System.Drawing.Point(364, 240);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(36, 240);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::YATA.Properties.Resources.space;
            this.pictureBox1.Location = new System.Drawing.Point(0, 240);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(46, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Top_screen_overlay
            // 
            this.Top_screen_overlay.BackColor = System.Drawing.Color.Transparent;
            this.Top_screen_overlay.Location = new System.Drawing.Point(46, 240);
            this.Top_screen_overlay.Name = "Top_screen_overlay";
            this.Top_screen_overlay.Size = new System.Drawing.Size(320, 240);
            this.Top_screen_overlay.TabIndex = 5;
            this.Top_screen_overlay.TabStop = false;
            // 
            // Arrows_bottom
            // 
            this.Arrows_bottom.BackColor = System.Drawing.Color.Transparent;
            this.Arrows_bottom.BackgroundImage = global::YATA.Properties.Resources.Bottom_arrow_back;
            this.Arrows_bottom.Image = global::YATA.Properties.Resources.Bottom_arrow_fore;
            this.Arrows_bottom.Location = new System.Drawing.Point(46, 240);
            this.Arrows_bottom.Name = "Arrows_bottom";
            this.Arrows_bottom.Size = new System.Drawing.Size(320, 240);
            this.Arrows_bottom.TabIndex = 4;
            this.Arrows_bottom.TabStop = false;
            // 
            // Overlay_LR_TOP_img
            // 
            this.Overlay_LR_TOP_img.BackColor = System.Drawing.Color.Transparent;
            this.Overlay_LR_TOP_img.BackgroundImage = global::YATA.Properties.Resources.top_overlay_background;
            this.Overlay_LR_TOP_img.Image = global::YATA.Properties.Resources.top_overlay_text;
            this.Overlay_LR_TOP_img.Location = new System.Drawing.Point(0, 0);
            this.Overlay_LR_TOP_img.Name = "Overlay_LR_TOP_img";
            this.Overlay_LR_TOP_img.Size = new System.Drawing.Size(400, 240);
            this.Overlay_LR_TOP_img.TabIndex = 2;
            this.Overlay_LR_TOP_img.TabStop = false;
            // 
            // topImage
            // 
            this.topImage.Location = new System.Drawing.Point(0, 0);
            this.topImage.Name = "topImage";
            this.topImage.Size = new System.Drawing.Size(400, 240);
            this.topImage.TabIndex = 0;
            this.topImage.TabStop = false;
            // 
            // bottomImage
            // 
            this.bottomImage.Location = new System.Drawing.Point(46, 240);
            this.bottomImage.Name = "bottomImage";
            this.bottomImage.Size = new System.Drawing.Size(320, 240);
            this.bottomImage.TabIndex = 1;
            this.bottomImage.TabStop = false;
            // 
            // Sim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(400, 482);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Top_screen_overlay);
            this.Controls.Add(this.Arrows_bottom);
            this.Controls.Add(this.Overlay_LR_TOP_img);
            this.Controls.Add(this.topImage);
            this.Controls.Add(this.bottomImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Sim";
            this.Text = "Theme Simulator";
            this.Load += new System.EventHandler(this.Sim_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Sim_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Top_screen_overlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Arrows_bottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Overlay_LR_TOP_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox topImage;
        private System.Windows.Forms.PictureBox bottomImage;
        private System.Windows.Forms.PictureBox Overlay_LR_TOP_img;
        private System.Windows.Forms.PictureBox Arrows_bottom;
        private System.Windows.Forms.PictureBox Top_screen_overlay;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}