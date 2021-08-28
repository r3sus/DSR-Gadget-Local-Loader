
namespace DSR_Gadget.SubForms
{
    partial class FamilyShareForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FamilyShareForm));
            this.txtSteamID = new System.Windows.Forms.TextBox();
            this.txtShareSteamID = new System.Windows.Forms.TextBox();
            this.pbxSteamAvatar = new System.Windows.Forms.PictureBox();
            this.txtSteamName = new System.Windows.Forms.TextBox();
            this.txtSteamProfileURL = new System.Windows.Forms.TextBox();
            this.txtShareSteamProfileURL = new System.Windows.Forms.TextBox();
            this.txtShareSteamName = new System.Windows.Forms.TextBox();
            this.pbxShareSteamAvatar = new System.Windows.Forms.PictureBox();
            this.btnSteamVisitProfile = new System.Windows.Forms.Button();
            this.btnShareSteamVisitProfile = new System.Windows.Forms.Button();
            this.lblShareSteamName = new System.Windows.Forms.Label();
            this.lblShareSteamID = new System.Windows.Forms.Label();
            this.lblSteamID = new System.Windows.Forms.Label();
            this.lblSteamName = new System.Windows.Forms.Label();
            this.lblShared = new System.Windows.Forms.Label();
            this.lblSharedFrom = new System.Windows.Forms.Label();
            this.btnCloseForm = new System.Windows.Forms.Button();
            this.lblFailed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSteamAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxShareSteamAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSteamID
            // 
            this.txtSteamID.Location = new System.Drawing.Point(118, 110);
            this.txtSteamID.Name = "txtSteamID";
            this.txtSteamID.ReadOnly = true;
            this.txtSteamID.Size = new System.Drawing.Size(254, 20);
            this.txtSteamID.TabIndex = 0;
            this.txtSteamID.Visible = false;
            // 
            // txtShareSteamID
            // 
            this.txtShareSteamID.Location = new System.Drawing.Point(118, 272);
            this.txtShareSteamID.Name = "txtShareSteamID";
            this.txtShareSteamID.ReadOnly = true;
            this.txtShareSteamID.Size = new System.Drawing.Size(254, 20);
            this.txtShareSteamID.TabIndex = 1;
            this.txtShareSteamID.Visible = false;
            // 
            // pbxSteamAvatar
            // 
            this.pbxSteamAvatar.Location = new System.Drawing.Point(12, 30);
            this.pbxSteamAvatar.Name = "pbxSteamAvatar";
            this.pbxSteamAvatar.Size = new System.Drawing.Size(100, 100);
            this.pbxSteamAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxSteamAvatar.TabIndex = 2;
            this.pbxSteamAvatar.TabStop = false;
            this.pbxSteamAvatar.Visible = false;
            // 
            // txtSteamName
            // 
            this.txtSteamName.Location = new System.Drawing.Point(118, 71);
            this.txtSteamName.Name = "txtSteamName";
            this.txtSteamName.ReadOnly = true;
            this.txtSteamName.Size = new System.Drawing.Size(254, 20);
            this.txtSteamName.TabIndex = 3;
            this.txtSteamName.Visible = false;
            // 
            // txtSteamProfileURL
            // 
            this.txtSteamProfileURL.Location = new System.Drawing.Point(118, 138);
            this.txtSteamProfileURL.Name = "txtSteamProfileURL";
            this.txtSteamProfileURL.ReadOnly = true;
            this.txtSteamProfileURL.Size = new System.Drawing.Size(254, 20);
            this.txtSteamProfileURL.TabIndex = 4;
            this.txtSteamProfileURL.Visible = false;
            // 
            // txtShareSteamProfileURL
            // 
            this.txtShareSteamProfileURL.Location = new System.Drawing.Point(118, 300);
            this.txtShareSteamProfileURL.Name = "txtShareSteamProfileURL";
            this.txtShareSteamProfileURL.ReadOnly = true;
            this.txtShareSteamProfileURL.Size = new System.Drawing.Size(254, 20);
            this.txtShareSteamProfileURL.TabIndex = 8;
            this.txtShareSteamProfileURL.Visible = false;
            // 
            // txtShareSteamName
            // 
            this.txtShareSteamName.Location = new System.Drawing.Point(118, 233);
            this.txtShareSteamName.Name = "txtShareSteamName";
            this.txtShareSteamName.ReadOnly = true;
            this.txtShareSteamName.Size = new System.Drawing.Size(254, 20);
            this.txtShareSteamName.TabIndex = 7;
            this.txtShareSteamName.Visible = false;
            // 
            // pbxShareSteamAvatar
            // 
            this.pbxShareSteamAvatar.Location = new System.Drawing.Point(12, 192);
            this.pbxShareSteamAvatar.Name = "pbxShareSteamAvatar";
            this.pbxShareSteamAvatar.Size = new System.Drawing.Size(100, 100);
            this.pbxShareSteamAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxShareSteamAvatar.TabIndex = 6;
            this.pbxShareSteamAvatar.TabStop = false;
            this.pbxShareSteamAvatar.Visible = false;
            // 
            // btnSteamVisitProfile
            // 
            this.btnSteamVisitProfile.Location = new System.Drawing.Point(12, 136);
            this.btnSteamVisitProfile.Name = "btnSteamVisitProfile";
            this.btnSteamVisitProfile.Size = new System.Drawing.Size(100, 23);
            this.btnSteamVisitProfile.TabIndex = 9;
            this.btnSteamVisitProfile.Text = "Visit profile";
            this.btnSteamVisitProfile.UseVisualStyleBackColor = true;
            this.btnSteamVisitProfile.Visible = false;
            this.btnSteamVisitProfile.Click += new System.EventHandler(this.btnSteamVisitProfile_Click);
            // 
            // btnShareSteamVisitProfile
            // 
            this.btnShareSteamVisitProfile.Location = new System.Drawing.Point(12, 298);
            this.btnShareSteamVisitProfile.Name = "btnShareSteamVisitProfile";
            this.btnShareSteamVisitProfile.Size = new System.Drawing.Size(100, 23);
            this.btnShareSteamVisitProfile.TabIndex = 10;
            this.btnShareSteamVisitProfile.Text = "Visit profile";
            this.btnShareSteamVisitProfile.UseVisualStyleBackColor = true;
            this.btnShareSteamVisitProfile.Visible = false;
            this.btnShareSteamVisitProfile.Click += new System.EventHandler(this.btnShareSteamVisitProfile_Click);
            // 
            // lblShareSteamName
            // 
            this.lblShareSteamName.AutoSize = true;
            this.lblShareSteamName.Location = new System.Drawing.Point(118, 217);
            this.lblShareSteamName.Name = "lblShareSteamName";
            this.lblShareSteamName.Size = new System.Drawing.Size(68, 13);
            this.lblShareSteamName.TabIndex = 11;
            this.lblShareSteamName.Text = "Steam Name";
            this.lblShareSteamName.Visible = false;
            // 
            // lblShareSteamID
            // 
            this.lblShareSteamID.AutoSize = true;
            this.lblShareSteamID.Location = new System.Drawing.Point(118, 256);
            this.lblShareSteamID.Name = "lblShareSteamID";
            this.lblShareSteamID.Size = new System.Drawing.Size(60, 13);
            this.lblShareSteamID.TabIndex = 12;
            this.lblShareSteamID.Text = "SteamID64";
            this.lblShareSteamID.Visible = false;
            // 
            // lblSteamID
            // 
            this.lblSteamID.AutoSize = true;
            this.lblSteamID.Location = new System.Drawing.Point(118, 94);
            this.lblSteamID.Name = "lblSteamID";
            this.lblSteamID.Size = new System.Drawing.Size(60, 13);
            this.lblSteamID.TabIndex = 13;
            this.lblSteamID.Text = "SteamID64";
            this.lblSteamID.Visible = false;
            // 
            // lblSteamName
            // 
            this.lblSteamName.AutoSize = true;
            this.lblSteamName.Location = new System.Drawing.Point(118, 55);
            this.lblSteamName.Name = "lblSteamName";
            this.lblSteamName.Size = new System.Drawing.Size(68, 13);
            this.lblSteamName.TabIndex = 14;
            this.lblSteamName.Text = "Steam Name";
            this.lblSteamName.Visible = false;
            // 
            // lblShared
            // 
            this.lblShared.AutoSize = true;
            this.lblShared.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShared.Location = new System.Drawing.Point(9, 9);
            this.lblShared.Name = "lblShared";
            this.lblShared.Size = new System.Drawing.Size(55, 18);
            this.lblShared.TabIndex = 15;
            this.lblShared.Text = "Shared";
            this.lblShared.Visible = false;
            // 
            // lblSharedFrom
            // 
            this.lblSharedFrom.AutoSize = true;
            this.lblSharedFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSharedFrom.Location = new System.Drawing.Point(12, 171);
            this.lblSharedFrom.Name = "lblSharedFrom";
            this.lblSharedFrom.Size = new System.Drawing.Size(90, 18);
            this.lblSharedFrom.TabIndex = 16;
            this.lblSharedFrom.Text = "Shared from";
            this.lblSharedFrom.Visible = false;
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.Location = new System.Drawing.Point(297, 326);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(75, 23);
            this.btnCloseForm.TabIndex = 17;
            this.btnCloseForm.Text = "Close";
            this.btnCloseForm.UseVisualStyleBackColor = true;
            this.btnCloseForm.Click += new System.EventHandler(this.btnCloseForm_Click);
            // 
            // lblFailed
            // 
            this.lblFailed.AutoSize = true;
            this.lblFailed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFailed.Location = new System.Drawing.Point(108, 161);
            this.lblFailed.Name = "lblFailed";
            this.lblFailed.Size = new System.Drawing.Size(165, 20);
            this.lblFailed.TabIndex = 18;
            this.lblFailed.Text = "Failed to Retrieve Info";
            this.lblFailed.Visible = false;
            // 
            // FamilyShareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.lblFailed);
            this.Controls.Add(this.btnCloseForm);
            this.Controls.Add(this.lblSharedFrom);
            this.Controls.Add(this.lblShared);
            this.Controls.Add(this.lblSteamName);
            this.Controls.Add(this.lblSteamID);
            this.Controls.Add(this.lblShareSteamID);
            this.Controls.Add(this.lblShareSteamName);
            this.Controls.Add(this.btnShareSteamVisitProfile);
            this.Controls.Add(this.btnSteamVisitProfile);
            this.Controls.Add(this.txtShareSteamProfileURL);
            this.Controls.Add(this.txtShareSteamName);
            this.Controls.Add(this.pbxShareSteamAvatar);
            this.Controls.Add(this.txtSteamProfileURL);
            this.Controls.Add(this.txtSteamName);
            this.Controls.Add(this.pbxSteamAvatar);
            this.Controls.Add(this.txtShareSteamID);
            this.Controls.Add(this.txtSteamID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FamilyShareForm";
            this.Text = "Family share info";
            this.Load += new System.EventHandler(this.FamilyShareForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxSteamAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxShareSteamAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSteamID;
        private System.Windows.Forms.TextBox txtShareSteamID;
        private System.Windows.Forms.PictureBox pbxSteamAvatar;
        private System.Windows.Forms.TextBox txtSteamName;
        private System.Windows.Forms.TextBox txtSteamProfileURL;
        private System.Windows.Forms.TextBox txtShareSteamProfileURL;
        private System.Windows.Forms.TextBox txtShareSteamName;
        private System.Windows.Forms.PictureBox pbxShareSteamAvatar;
        private System.Windows.Forms.Button btnSteamVisitProfile;
        private System.Windows.Forms.Button btnShareSteamVisitProfile;
        private System.Windows.Forms.Label lblShareSteamName;
        private System.Windows.Forms.Label lblShareSteamID;
        private System.Windows.Forms.Label lblSteamID;
        private System.Windows.Forms.Label lblSteamName;
        private System.Windows.Forms.Label lblShared;
        private System.Windows.Forms.Label lblSharedFrom;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label lblFailed;
    }
}