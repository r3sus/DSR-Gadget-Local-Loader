using DSR_Gadget.SubForms;
using System;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        EnemyInsForm LastTargetEntityForm;
        EnemyInsForm LastHitEntityForm;

        private void initMisc() { }

        private void saveMisc() { }

        private void resetMisc() 
        {
            Hook.LastTargetEntity = false;
            Hook.LastHitEntity = false;
        }

        private void reloadMisc() { }

        private void onHookedMisc()
        {
            if (cbxLastTargetEntity.Checked)
                Hook.LastTargetEntity = true;
            if (cbxLastHitEntity.Checked)
                Hook.LastHitEntity = true;
        }

        private void updateMisc() { }

        private void btnEventRead_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(txtEventFlag.Text, out int id))
                cbxEventFlag.Checked = Hook.ReadEventFlag(id);
        }

        private void btnEventWrite_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(txtEventFlag.Text, out int id))
                Hook.WriteEventFlag(id, cbxEventFlag.Checked);
        }

        private void btnLastTargetEntity_Click(object sender, EventArgs e)
        {
            LastTargetEntityForm = new EnemyInsForm();
            LastTargetEntityForm.InitForm(Hook, Hook.GetLastTargetEntity(), "Last Targeted Entity");
            LastTargetEntityForm.StartPosition = FormStartPosition.CenterScreen;
            LastTargetEntityForm.Show();
        }

        private void cbxLastTargetEntity_CheckedChanged(object sender, EventArgs e)
        {
            Hook.LastTargetEntity = cbxLastTargetEntity.Checked;
            btnLastTargetEntity.Enabled = cbxLastTargetEntity.Checked;
            if (!cbxLastTargetEntity.Checked)
                LastTargetEntityForm.Close();
        }

        private void btnLastHitEntity_Click(object sender, EventArgs e)
        {
            LastHitEntityForm = new EnemyInsForm();
            LastHitEntityForm.InitForm(Hook, Hook.GetLastHitEntity(), "Last Hit Entity");
            LastHitEntityForm.StartPosition = FormStartPosition.CenterScreen;
            LastHitEntityForm.Show();
        }

        private void cbxLastHitEntity_CheckedChanged(object sender, EventArgs e)
        {
            Hook.LastHitEntity = cbxLastHitEntity.Checked;
            btnLastHitEntity.Enabled = cbxLastHitEntity.Checked;
            if (!cbxLastHitEntity.Checked)
                LastHitEntityForm.Close();
        }
    }
}
