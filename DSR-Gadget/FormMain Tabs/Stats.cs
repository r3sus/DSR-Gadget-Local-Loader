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

            nudHairRed.Maximum = decimal.MaxValue;
            nudHairGreen.Maximum = decimal.MaxValue;
            nudHairBlue.Maximum = decimal.MaxValue;
            nudHairAlpha.Maximum = decimal.MaxValue;

            nudEyeRed.Maximum = decimal.MaxValue;
            nudEyeGreen.Maximum = decimal.MaxValue;
            nudEyeBlue.Maximum = decimal.MaxValue;

            nudHairRed.Minimum = decimal.MinValue;
            nudHairGreen.Minimum = decimal.MinValue;
            nudHairBlue.Minimum = decimal.MinValue;
            nudHairAlpha.Minimum = decimal.MinValue;

            nudEyeRed.Minimum = decimal.MinValue;
            nudEyeGreen.Minimum = decimal.MinValue;
            nudEyeBlue.Minimum = decimal.MinValue;


#if DEBUG
            criticalControls.Add(nudIndictments);
#endif
        }

        private void saveStats() { }

        private void resetStats() { }

        private void reloadStats()
        {
            cmbClass.SelectedIndex = Player.Class;
            cmbCovenant.SelectedIndex = Player.Covenant;
        }

        private void updateStats()
        {
            txtSoulLevel.Text = Player.SoulLevel.ToString();
            nudHumanity.Value = Player.Humanity;
            nudSouls.Value = Player.Souls;

            nudWarriorOfSunlight.Value = Player.WarriorOfSunlight;
            nudDarkwraith.Value = Player.Darkwraith;
            nudPathOfTheDragon.Value = Player.PathOfTheDragon;
            nudGravelordServant.Value = Player.GravelordServant;
            nudForestHunter.Value = Player.ForestHunter;
            nudDarkmoonBlade.Value = Player.DarkmoonBlade;
            nudChaosServant.Value = Player.ChaosServant;

            txtName.Text = Player.NameString1;
            txtOnlineName.Text = Player.NameString2;

            nudWeaponMemory.Value = Player.WeaponMemory;
            nudIndictments.Value = Player.Indictments;

            nudHair.Value = Player.Hair;
            try
            {
                nudHairRed.Value = Convert.ToDecimal(Player.HairRed);
                nudHairGreen.Value = Convert.ToDecimal(Player.HairGreen);
                nudHairBlue.Value = Convert.ToDecimal(Player.HairBlue);
                nudHairAlpha.Value = Convert.ToDecimal(Player.HairAlpha);

                nudEyeRed.Value = Convert.ToDecimal(Player.EyeRed);
                nudEyeGreen.Value = Convert.ToDecimal(Player.EyeGreen);
                nudEyeBlue.Value = Convert.ToDecimal(Player.EyeBlue);
            } catch (OverflowException)
            {
                // TODO: handle this better
                nudHairRed.Enabled = false;
                nudHairGreen.Enabled = false;
                nudHairBlue.Enabled = false;
                nudHairAlpha.Enabled = false;

                nudEyeRed.Enabled = false;
                nudEyeGreen.Enabled = false;
                nudEyeBlue.Enabled = false;
            }


            try
            {
                nudVitality.Value = Player.Vitality;
                nudAttunement.Value = Player.Attunement;
                nudEndurance.Value = Player.Endurance;
                nudStrength.Value = Player.Strength;
                nudDexterity.Value = Player.Dexterity;
                nudResistance.Value = Player.Resistance;
                nudIntelligence.Value = Player.Intelligence;
                nudFaith.Value = Player.Faith;
            }
            catch (ArgumentOutOfRangeException) { }

            updateCovenant(cmbCovenant, Player.Covenant);
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
                Player.Covenant = item.ID;
            }
        }
        private void nudWarriorOfSunlight_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.WarriorOfSunlight = (byte)nudWarriorOfSunlight.Value;
        }

        private void nudDarkwraith_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.Darkwraith = (byte)nudDarkwraith.Value;
        }

        private void nudPathOfTheDragon_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.PathOfTheDragon = (byte)nudPathOfTheDragon.Value;
        }

        private void nudGravelordServant_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.GravelordServant = (byte)nudGravelordServant.Value;
        }

        private void nudForestHunter_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.ForestHunter = (byte)nudForestHunter.Value;
        }

        private void nudDarkmoonBlade_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.DarkmoonBlade = (byte)nudDarkmoonBlade.Value;
        }

        private void nudChaosServant_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.ChaosServant = (byte)nudChaosServant.Value;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                Player.NameString1 = txtName.Text;
                Player.NameString2 = txtName.Text;
            }
        }

        private void nudWeaponMemory_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.WeaponMemory = (byte)nudWeaponMemory.Value;
        }

        private void nudIndictments_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.Indictments = (int)nudIndictments.Value;
        }

        private void cbxFashionHex_CheckedChanged(object sender, EventArgs e)
        {
            nudHair.Hexadecimal = cbxFashionHex.Checked;
            nudHairRed.Hexadecimal = cbxFashionHex.Checked;
            nudHairGreen.Hexadecimal = cbxFashionHex.Checked;
            nudHairBlue.Hexadecimal = cbxFashionHex.Checked;
            nudHairAlpha.Hexadecimal = cbxFashionHex.Checked;

            nudEyeRed.Hexadecimal = cbxFashionHex.Checked;
            nudEyeGreen.Hexadecimal = cbxFashionHex.Checked;
            nudEyeBlue.Hexadecimal = cbxFashionHex.Checked;
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

        private void nudHair_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.Hair = (int)nudHair.Value;
        }

        private void nudHairRed_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.HairRed = (float)nudHairRed.Value;
        }

        private void nudHairGreen_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.HairGreen = (float)nudHairGreen.Value;
        }

        private void nudHairBlue_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.HairBlue = (float)nudHairBlue.Value;
        }

        private void nudHairAlpha_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.HairAlpha = (float)nudHairAlpha.Value;
        }

        private void nudEyeRed_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.EyeRed = (float)nudEyeRed.Value;
        }

        private void nudEyeGreen_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.EyeGreen = (float)nudEyeGreen.Value;
        }

        private void nudEyeBlue_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.EyeBlue = (float)nudEyeBlue.Value;
        }
    }
}
