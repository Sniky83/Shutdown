namespace Shutdown
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BTN_Start = new System.Windows.Forms.Button();
            this.BTN_Stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_etat = new System.Windows.Forms.Label();
            this.lbl_debit = new System.Windows.Forms.Label();
            this.Timer_Debit = new System.Windows.Forms.Timer(this.components);
            this.Timer_Temps = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_temps = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTN_Start
            // 
            this.BTN_Start.Location = new System.Drawing.Point(12, 139);
            this.BTN_Start.Name = "BTN_Start";
            this.BTN_Start.Size = new System.Drawing.Size(75, 23);
            this.BTN_Start.TabIndex = 0;
            this.BTN_Start.Text = "Démarrer";
            this.BTN_Start.UseVisualStyleBackColor = true;
            this.BTN_Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // BTN_Stop
            // 
            this.BTN_Stop.Enabled = false;
            this.BTN_Stop.Location = new System.Drawing.Point(220, 139);
            this.BTN_Stop.Name = "BTN_Stop";
            this.BTN_Stop.Size = new System.Drawing.Size(75, 23);
            this.BTN_Stop.TabIndex = 1;
            this.BTN_Stop.Text = "Arrêter";
            this.BTN_Stop.UseVisualStyleBackColor = true;
            this.BTN_Stop.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Download :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "by Sniky";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "État :";
            // 
            // lbl_etat
            // 
            this.lbl_etat.AutoSize = true;
            this.lbl_etat.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_etat.ForeColor = System.Drawing.Color.Red;
            this.lbl_etat.Location = new System.Drawing.Point(98, 33);
            this.lbl_etat.Name = "lbl_etat";
            this.lbl_etat.Size = new System.Drawing.Size(33, 18);
            this.lbl_etat.TabIndex = 6;
            this.lbl_etat.Text = "OFF";
            // 
            // lbl_debit
            // 
            this.lbl_debit.AutoSize = true;
            this.lbl_debit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_debit.Location = new System.Drawing.Point(97, 86);
            this.lbl_debit.Name = "lbl_debit";
            this.lbl_debit.Size = new System.Drawing.Size(55, 19);
            this.lbl_debit.TabIndex = 7;
            this.lbl_debit.Text = " 0 KB/s";
            // 
            // Timer_Debit
            // 
            this.Timer_Debit.Interval = 1000;
            this.Timer_Debit.Tick += new System.EventHandler(this.Timer_Debit_Tick);
            // 
            // Timer_Temps
            // 
            this.Timer_Temps.Interval = 1000;
            this.Timer_Temps.Tick += new System.EventHandler(this.Timer_Temps_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(116, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Avancé";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lbl_temps
            // 
            this.lbl_temps.AutoSize = true;
            this.lbl_temps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_temps.Location = new System.Drawing.Point(98, 62);
            this.lbl_temps.Name = "lbl_temps";
            this.lbl_temps.Size = new System.Drawing.Size(16, 16);
            this.lbl_temps.TabIndex = 9;
            this.lbl_temps.Text = "0";
            this.lbl_temps.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 171);
            this.Controls.Add(this.lbl_temps);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_debit);
            this.Controls.Add(this.lbl_etat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_Stop);
            this.Controls.Add(this.BTN_Start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(460, 210);
            this.Name = "Form1";
            this.Text = "Shutdown";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_Start;
        private System.Windows.Forms.Button BTN_Stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_etat;
        private System.Windows.Forms.Label lbl_debit;
        private System.Windows.Forms.Timer Timer_Debit;
        private System.Windows.Forms.Timer Timer_Temps;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_temps;
    }
}

