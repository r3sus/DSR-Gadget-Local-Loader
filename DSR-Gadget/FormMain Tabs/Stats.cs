using System;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private void initStats()
        {
            foreach (DSRClass charClass in DSRClass.All)
                cmbClass.Items.Add(charClass);
            nudHumanity.Maximum = int.MaxValue;
            nudHumanity.Minimum = int.MinValue;

            foreach (DSRCovenant covenant in DSRCovenant.All)
                cmbCovenant.Items.Add(covenant);
        }

        private void saveStats() { }

        private void resetStats() { }

        private void reloadStats()
        {
            cmbClass.SelectedIndex = Hook.Class;
            cmbCovenant.SelectedIndex = Hook.Covenant;
        }

        private void updateStats()
        {
            txtSoulLevel.Text = Hook.SoulLevel.ToString();
            nudHumanity.Value = Hook.Humanity;
            nudSouls.Value = Hook.Souls;

            try
            {
                nudVitality.Value = Hook.Vitality;
                nudAttunement.Value = Hook.Attunement;
                nudEndurance.Value = Hook.Endurance;
                nudStrength.Value = Hook.Strength;
                nudDexterity.Value = Hook.Dexterity;
                nudResistance.Value = Hook.Resistance;
                nudIntelligence.Value = Hook.Intelligence;
                nudFaith.Value = Hook.Faith;
            }
            catch (ArgumentOutOfRangeException) { }

            updateCovenant(cmbCovenant, Hook.Covenant);
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSRClass charClass = cmbClass.SelectedItem as DSRClass;
            nudVitality.Minimum = charClass.Vitality;
            nudAttunement.Minimum = charClass.Attunement;
            nudEndurance.Minimum = charClass.Endurance;
            nudStrength.Minimum = charClass.Strength;
            nudDexterity.Minimum = charClass.Dexterity;
            nudResistance.Minimum = charClass.Resistance;
            nudIntelligence.Minimum = charClass.Intelligence;
            nudFaith.Minimum = charClass.Faith;

            if (!reading)
            {
                //dsrProcess.SetClass(charClass.ID);
                //recalculateStats();
            }
        }

        private void nudHumanity_ValueChanged(object sender, EventArgs e)
        {
            //if (!reading)
            //dsrProcess?.SetHumanity((int)nudHumanity.Value);
        }

        private void nudSouls_ValueChanged(object sender, EventArgs e)
        {
            //if (!reading)
            //dsrProcess?.SetSouls((int)nudSouls.Value);
        }

        private void nudStats_ValueChanged(object sender, EventArgs e)
        {
            //if (!reading)
            //recalculateStats();
        }

        private void cmbCovenant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRCovenant item = cmbCovenant.SelectedItem as DSRCovenant;
                Hook.Covenant = item.ID;
            }
        }

        private void recalculateStats()
        {
            int vit = (int)nudVitality.Value;
            int att = (int)nudAttunement.Value;
            int end = (int)nudEndurance.Value;
            int str = (int)nudStrength.Value;
            int dex = (int)nudDexterity.Value;
            int res = (int)nudResistance.Value;
            int intel = (int)nudIntelligence.Value;
            int fth = (int)nudFaith.Value;

            DSRClass charClass = cmbClass.SelectedItem as DSRClass;
            int sl = charClass.SoulLevel;
            sl += vit - charClass.Vitality;
            sl += att - charClass.Attunement;
            sl += end - charClass.Endurance;
            sl += str - charClass.Strength;
            sl += dex - charClass.Dexterity;
            sl += res - charClass.Resistance;
            sl += intel - charClass.Intelligence;
            sl += fth - charClass.Faith;

            //dsrProcess.LevelUp(vit, att, end, str, dex, res, intel, fth, sl);
        }
        private void updateCovenant(ComboBox cmbCovenant, byte id)
        {
            DSRCovenant lastCovenant = cmbCovenant.SelectedItem as DSRCovenant;
            if (!cmbCovenant.DroppedDown && lastCovenant.ID != id)
            {
                bool found = false;
                foreach (DSRCovenant item in cmbCovenant.Items)
                {
                    if (item.ID == id)
                    {
                        cmbCovenant.SelectedItem = item;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    DSRCovenant item = new DSRCovenant("Unknown", id);
                    cmbCovenant.Items.Add(item);
                    cmbCovenant.SelectedItem = item;
                }
            }
        }
    }
}
