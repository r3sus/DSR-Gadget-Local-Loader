
namespace DSR_Gadget.SubForms
{
    partial class EnemyInsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnemyInsForm));
            this.lblStamina = new System.Windows.Forms.Label();
            this.lblHealth = new System.Windows.Forms.Label();
            this.lblStatusMax = new System.Windows.Forms.Label();
            this.lblStatusCurrent = new System.Windows.Forms.Label();
            this.nudStaminaMax = new System.Windows.Forms.NumericUpDown();
            this.nudStamina = new System.Windows.Forms.NumericUpDown();
            this.nudHealthMax = new System.Windows.Forms.NumericUpDown();
            this.nudHealth = new System.Windows.Forms.NumericUpDown();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.cbxKill = new System.Windows.Forms.CheckBox();
            this.lblTeamType = new System.Windows.Forms.Label();
            this.lblChrType = new System.Windows.Forms.Label();
            this.nudChrType = new System.Windows.Forms.NumericUpDown();
            this.nudTeamType = new System.Windows.Forms.NumericUpDown();
            this.cbxFreezeTeamType = new System.Windows.Forms.CheckBox();
            this.cbxFreezeChrType = new System.Windows.Forms.CheckBox();
            this.lblFreezeChr = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudStaminaMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStamina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHealthMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChrType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTeamType)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStamina
            // 
            this.lblStamina.AutoSize = true;
            this.lblStamina.Location = new System.Drawing.Point(14, 53);
            this.lblStamina.Name = "lblStamina";
            this.lblStamina.Size = new System.Drawing.Size(45, 13);
            this.lblStamina.TabIndex = 28;
            this.lblStamina.Text = "Stamina";
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.Location = new System.Drawing.Point(21, 27);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(38, 13);
            this.lblHealth.TabIndex = 27;
            this.lblHealth.Text = "Health";
            // 
            // lblStatusMax
            // 
            this.lblStatusMax.AutoSize = true;
            this.lblStatusMax.Location = new System.Drawing.Point(143, 9);
            this.lblStatusMax.Name = "lblStatusMax";
            this.lblStatusMax.Size = new System.Drawing.Size(27, 13);
            this.lblStatusMax.TabIndex = 24;
            this.lblStatusMax.Text = "Max";
            // 
            // lblStatusCurrent
            // 
            this.lblStatusCurrent.AutoSize = true;
            this.lblStatusCurrent.Location = new System.Drawing.Point(62, 9);
            this.lblStatusCurrent.Name = "lblStatusCurrent";
            this.lblStatusCurrent.Size = new System.Drawing.Size(41, 13);
            this.lblStatusCurrent.TabIndex = 23;
            this.lblStatusCurrent.Text = "Current";
            // 
            // nudStaminaMax
            // 
            this.nudStaminaMax.Enabled = false;
            this.nudStaminaMax.Location = new System.Drawing.Point(146, 51);
            this.nudStaminaMax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudStaminaMax.Name = "nudStaminaMax";
            this.nudStaminaMax.Size = new System.Drawing.Size(75, 20);
            this.nudStaminaMax.TabIndex = 22;
            // 
            // nudStamina
            // 
            this.nudStamina.Location = new System.Drawing.Point(65, 51);
            this.nudStamina.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudStamina.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudStamina.Name = "nudStamina";
            this.nudStamina.Size = new System.Drawing.Size(75, 20);
            this.nudStamina.TabIndex = 21;
            this.nudStamina.ValueChanged += new System.EventHandler(this.nudStamina_ValueChanged);
            // 
            // nudHealthMax
            // 
            this.nudHealthMax.Enabled = false;
            this.nudHealthMax.Location = new System.Drawing.Point(146, 25);
            this.nudHealthMax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudHealthMax.Name = "nudHealthMax";
            this.nudHealthMax.Size = new System.Drawing.Size(75, 20);
            this.nudHealthMax.TabIndex = 25;
            // 
            // nudHealth
            // 
            this.nudHealth.Location = new System.Drawing.Point(65, 25);
            this.nudHealth.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudHealth.Name = "nudHealth";
            this.nudHealth.Size = new System.Drawing.Size(75, 20);
            this.nudHealth.TabIndex = 26;
            this.nudHealth.ValueChanged += new System.EventHandler(this.nudHealth_ValueChanged);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 16;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // cbxKill
            // 
            this.cbxKill.AutoSize = true;
            this.cbxKill.Location = new System.Drawing.Point(373, 12);
            this.cbxKill.Name = "cbxKill";
            this.cbxKill.Size = new System.Drawing.Size(39, 17);
            this.cbxKill.TabIndex = 29;
            this.cbxKill.Text = "Kill";
            this.cbxKill.UseVisualStyleBackColor = true;
            // 
            // lblTeamType
            // 
            this.lblTeamType.AutoSize = true;
            this.lblTeamType.Location = new System.Drawing.Point(20, 116);
            this.lblTeamType.Name = "lblTeamType";
            this.lblTeamType.Size = new System.Drawing.Size(61, 13);
            this.lblTeamType.TabIndex = 33;
            this.lblTeamType.Text = "Team Type";
            // 
            // lblChrType
            // 
            this.lblChrType.AutoSize = true;
            this.lblChrType.Location = new System.Drawing.Point(31, 90);
            this.lblChrType.Name = "lblChrType";
            this.lblChrType.Size = new System.Drawing.Size(50, 13);
            this.lblChrType.TabIndex = 32;
            this.lblChrType.Text = "Chr Type";
            // 
            // nudChrType
            // 
            this.nudChrType.Location = new System.Drawing.Point(87, 88);
            this.nudChrType.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudChrType.Name = "nudChrType";
            this.nudChrType.Size = new System.Drawing.Size(53, 20);
            this.nudChrType.TabIndex = 31;
            this.nudChrType.ValueChanged += new System.EventHandler(this.nudChrType_ValueChanged);
            // 
            // nudTeamType
            // 
            this.nudTeamType.Location = new System.Drawing.Point(87, 114);
            this.nudTeamType.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudTeamType.Name = "nudTeamType";
            this.nudTeamType.Size = new System.Drawing.Size(53, 20);
            this.nudTeamType.TabIndex = 30;
            this.nudTeamType.ValueChanged += new System.EventHandler(this.nudTeamType_ValueChanged);
            // 
            // cbxFreezeTeamType
            // 
            this.cbxFreezeTeamType.AutoSize = true;
            this.cbxFreezeTeamType.Location = new System.Drawing.Point(146, 116);
            this.cbxFreezeTeamType.Name = "cbxFreezeTeamType";
            this.cbxFreezeTeamType.Size = new System.Drawing.Size(15, 14);
            this.cbxFreezeTeamType.TabIndex = 36;
            this.cbxFreezeTeamType.UseVisualStyleBackColor = true;
            // 
            // cbxFreezeChrType
            // 
            this.cbxFreezeChrType.AutoSize = true;
            this.cbxFreezeChrType.Location = new System.Drawing.Point(146, 90);
            this.cbxFreezeChrType.Name = "cbxFreezeChrType";
            this.cbxFreezeChrType.Size = new System.Drawing.Size(15, 14);
            this.cbxFreezeChrType.TabIndex = 35;
            this.cbxFreezeChrType.UseVisualStyleBackColor = true;
            // 
            // lblFreezeChr
            // 
            this.lblFreezeChr.AutoSize = true;
            this.lblFreezeChr.Location = new System.Drawing.Point(143, 74);
            this.lblFreezeChr.Name = "lblFreezeChr";
            this.lblFreezeChr.Size = new System.Drawing.Size(39, 13);
            this.lblFreezeChr.TabIndex = 34;
            this.lblFreezeChr.Text = "Freeze";
            // 
            // EnemyInsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 561);
            this.Controls.Add(this.cbxFreezeTeamType);
            this.Controls.Add(this.cbxFreezeChrType);
            this.Controls.Add(this.lblFreezeChr);
            this.Controls.Add(this.lblTeamType);
            this.Controls.Add(this.lblChrType);
            this.Controls.Add(this.nudChrType);
            this.Controls.Add(this.nudTeamType);
            this.Controls.Add(this.cbxKill);
            this.Controls.Add(this.lblStamina);
            this.Controls.Add(this.lblHealth);
            this.Controls.Add(this.lblStatusMax);
            this.Controls.Add(this.lblStatusCurrent);
            this.Controls.Add(this.nudStaminaMax);
            this.Controls.Add(this.nudStamina);
            this.Controls.Add(this.nudHealthMax);
            this.Controls.Add(this.nudHealth);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EnemyInsForm";
            this.Text = "Enemy";
            ((System.ComponentModel.ISupportInitialize)(this.nudStaminaMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStamina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHealthMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChrType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTeamType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblStamina;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.Label lblStatusMax;
        private System.Windows.Forms.Label lblStatusCurrent;
        private System.Windows.Forms.NumericUpDown nudStaminaMax;
        private System.Windows.Forms.NumericUpDown nudStamina;
        private System.Windows.Forms.NumericUpDown nudHealthMax;
        private System.Windows.Forms.NumericUpDown nudHealth;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.CheckBox cbxKill;
        private System.Windows.Forms.Label lblTeamType;
        private System.Windows.Forms.Label lblChrType;
        private System.Windows.Forms.NumericUpDown nudChrType;
        private System.Windows.Forms.NumericUpDown nudTeamType;
        private System.Windows.Forms.CheckBox cbxFreezeTeamType;
        private System.Windows.Forms.CheckBox cbxFreezeChrType;
        private System.Windows.Forms.Label lblFreezeChr;
    }
}