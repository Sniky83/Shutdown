namespace Shutdown
{
    partial class AdvancedForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedForm));
            this.numUpDown_Download = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_OptSys = new System.Windows.Forms.ComboBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numUpDown_TempsRestant = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Download)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_TempsRestant)).BeginInit();
            this.SuspendLayout();
            // 
            // numUpDown_Download
            // 
            this.numUpDown_Download.Location = new System.Drawing.Point(134, 25);
            this.numUpDown_Download.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUpDown_Download.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUpDown_Download.Name = "numUpDown_Download";
            this.numUpDown_Download.Size = new System.Drawing.Size(58, 20);
            this.numUpDown_Download.TabIndex = 0;
            this.numUpDown_Download.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choissisez un débit de base pour votre téléchargement :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "KB/s";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(315, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Choissisez une option système une fois votre téléchargement fini :";
            // 
            // cmb_OptSys
            // 
            this.cmb_OptSys.FormattingEnabled = true;
            this.cmb_OptSys.Items.AddRange(new object[] {
            "Eteindre",
            "Mettre en veille"});
            this.cmb_OptSys.Location = new System.Drawing.Point(108, 129);
            this.cmb_OptSys.Name = "cmb_OptSys";
            this.cmb_OptSys.Size = new System.Drawing.Size(121, 21);
            this.cmb_OptSys.TabIndex = 4;
            this.cmb_OptSys.Tag = "";
            this.cmb_OptSys.Text = "Eteindre";
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(108, 156);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(121, 23);
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "Valider";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(344, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Choix du temps pour effectuer l\'option système après le téléchargement:";
            // 
            // numUpDown_TempsRestant
            // 
            this.numUpDown_TempsRestant.Location = new System.Drawing.Point(134, 76);
            this.numUpDown_TempsRestant.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numUpDown_TempsRestant.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDown_TempsRestant.Name = "numUpDown_TempsRestant";
            this.numUpDown_TempsRestant.Size = new System.Drawing.Size(58, 20);
            this.numUpDown_TempsRestant.TabIndex = 8;
            this.numUpDown_TempsRestant.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numUpDown_TempsRestant.ValueChanged += new System.EventHandler(this.NumUpDown_TempsRestant_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Minutes";
            // 
            // AdvancedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 188);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numUpDown_TempsRestant);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.cmb_OptSys);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numUpDown_Download);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AdvancedForm";
            this.Text = "Avancé";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Download)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_TempsRestant)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.NumericUpDown numUpDown_Download;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmb_OptSys;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numUpDown_TempsRestant;
        private System.Windows.Forms.Label label5;
    }
}