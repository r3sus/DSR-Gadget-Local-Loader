
namespace DSR_Gadget.SubForms
{
    partial class ParamEditForm
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
            this.cmbParamType = new System.Windows.Forms.ComboBox();
            this.cmbParamEntry = new System.Windows.Forms.ComboBox();
            this.flpParams = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // cmbParamType
            // 
            this.cmbParamType.FormattingEnabled = true;
            this.cmbParamType.Location = new System.Drawing.Point(12, 12);
            this.cmbParamType.Name = "cmbParamType";
            this.cmbParamType.Size = new System.Drawing.Size(510, 21);
            this.cmbParamType.TabIndex = 0;
            // 
            // cmbParamEntry
            // 
            this.cmbParamEntry.FormattingEnabled = true;
            this.cmbParamEntry.Location = new System.Drawing.Point(12, 39);
            this.cmbParamEntry.Name = "cmbParamEntry";
            this.cmbParamEntry.Size = new System.Drawing.Size(510, 21);
            this.cmbParamEntry.TabIndex = 1;
            // 
            // flpParams
            // 
            this.flpParams.Location = new System.Drawing.Point(12, 66);
            this.flpParams.Name = "flpParams";
            this.flpParams.Size = new System.Drawing.Size(510, 583);
            this.flpParams.TabIndex = 2;
            // 
            // ParamEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 661);
            this.Controls.Add(this.flpParams);
            this.Controls.Add(this.cmbParamEntry);
            this.Controls.Add(this.cmbParamType);
            this.Name = "ParamEditForm";
            this.Text = "ParamEditForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbParamType;
        private System.Windows.Forms.ComboBox cmbParamEntry;
        private System.Windows.Forms.FlowLayoutPanel flpParams;
    }
}