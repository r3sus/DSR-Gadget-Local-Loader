using System;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {

        private ComboBox[] cmbGestures;
        private CheckBox[] cbxGestures;

        private void initStats()
        {
            foreach (DSRClass charClass in DSRClass.All)
                cmbClass.Items.Add(charClass);

            nudHumanity.Maximum = int.MaxValue;
            nudHumanity.Minimum = int.MinValue;

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
            
            foreach (ComboBox item in cmbGestures)
            {
                foreach (DSRGesture gesture in DSRGesture.All)
                    item.Items.Add(gesture);
                item.SelectedIndex = 0;
            }
            cmbGestureSlot1.SelectedIndexChanged += cmbGestureSlot1_SelectedIndexChanged;
            cmbGestureSlot2.SelectedIndexChanged += cmbGestureSlot2_SelectedIndexChanged;
            cmbGestureSlot3.SelectedIndexChanged += cmbGestureSlot3_SelectedIndexChanged;
            cmbGestureSlot4.SelectedIndexChanged += cmbGestureSlot4_SelectedIndexChanged;
            cmbGestureSlot5.SelectedIndexChanged += cmbGestureSlot5_SelectedIndexChanged;
            cmbGestureSlot6.SelectedIndexChanged += cmbGestureSlot6_SelectedIndexChanged;
            cmbGestureSlot7.SelectedIndexChanged += cmbGestureSlot7_SelectedIndexChanged;

            foreach (DSRGender gender in DSRGender.All)
                cmbGender.Items.Add(gender);
            cmbGender.SelectedIndex = 0;
            cmbGender.SelectedIndexChanged += cmbGender_SelectedIndexChanged;

            foreach (DSRPhysique physique in DSRPhysique.All)
                cmbPhysique.Items.Add(physique);
            cmbPhysique.SelectedIndex = 0;
            cmbPhysique.SelectedIndexChanged += cmbPhysique_SelectedIndexChanged;

            foreach (DSRCovenant covenant in DSRCovenant.All)
                cmbCovenant.Items.Add(covenant);
            cmbCovenant.SelectedIndex = 0;
            cmbCovenant.SelectedIndexChanged += cmbCovenant_SelectedIndexChanged;

#if DEBUG
            criticalControls.Add(nudIndictments);
            criticalControls.AddRange(cbxGestures);
            criticalControls.Add(nudVitality);
            criticalControls.Add(nudAttunement);
            criticalControls.Add(nudEndurance);
            criticalControls.Add(nudStrength);
            criticalControls.Add(nudDexterity);
            criticalControls.Add(nudResistance);
            criticalControls.Add(nudIntelligence);
            criticalControls.Add(nudFaith);
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
            nudClearCount.Value = Hook.ClearCount;

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

            cbxGesturePointForward.Checked = Player.GesturePointForward;
            cbxGesturePointUp.Checked = Player.GesturePointUp;
            cbxGesturePointDown.Checked = Player.GesturePointDown;
            cbxGestureBeckon.Checked = Player.GestureBeckon;
            cbxGestureWave.Checked = Player.GestureWave;
            cbxGestureBow.Checked = Player.GestureBow;
            cbxGestureProperBow.Checked = Player.GestureProperBow;
            cbxGestureHurrah.Checked = Player.GestureHurrah;
            cbxGestureJoy.Checked = Player.GestureJoy;
            cbxGestureShrug.Checked = Player.GestureShrug;
            cbxGestureLookSkyward.Checked = Player.GestureLookSkyward;
            cbxGestureWellWhatIsIt.Checked = Player.GestureWellWhatIsIt;
            cbxGestureProstration.Checked = Player.GestureProstration;
            cbxGesturePrayer.Checked = Player.GesturePrayer;
            cbxGesturePraiseTheSun.Checked = Player.GesturePraiseTheSun;

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

            for (int i = 0; i < cmbGestures.Length; i++)
            {
                updateDropdown<DSRGesture>(cmbGestures[i], Player.GetGestureSlot(i));
            }

            updateDropdown<DSRGender>(cmbGender, Player.Gender);
            updateDropdown<DSRPhysique>(cmbPhysique, Player.Physique);
            updateDropdown<DSRCovenant>(cmbCovenant, Player.Covenant);

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
            if (!reading)
                recalculateStats();
        }

        #region Covenant

        private void cmbCovenant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRCovenant item = cmbCovenant.SelectedItem as DSRCovenant;
                Player.Covenant = (byte)item.ID;
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

        #endregion

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

        private void nudClearCount_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.ClearCount = (byte)nudClearCount.Value;
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

            if (sl < charClass.SoulLevel || vit < charClass.Vitality || att < charClass.Attunement || end < charClass.Endurance || str < charClass.Strength ||
                dex < charClass.Dexterity || res < charClass.Resistance || intel < charClass.Intelligence || fth < charClass.Faith)
                return;
            
            Hook.LevelUp(sl, vit, att, end, str, dex, res, intel, fth, Player.Humanity, Player.Souls);
        }

        #region Fashion

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

        private void cmbPhysique_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.Physique = (byte)(cmbPhysique.SelectedItem as DSRPhysique).ID;
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.Gender = (byte)(cmbGender.SelectedItem as DSRGender).ID;
        }

        #endregion

        #region Gestures

        private void btnGesturesUnlockAll_Click(object sender, EventArgs e)
        {
            Player.GestureUnlockAll();
        }

        private void cbxGesturePraiseTheSun_CheckedChanged(object sender, EventArgs e)
        {
            Player.GesturePraiseTheSun = cbxGesturePraiseTheSun.Checked;
        }

        private void cbxGesturePrayer_CheckedChanged(object sender, EventArgs e)
        {
            Player.GesturePrayer = cbxGesturePrayer.Checked;
        }

        private void cbxGestureProstration_CheckedChanged(object sender, EventArgs e)
        {
            Player.GestureProstration = cbxGestureProstration.Checked;
        }

        private void cbxGestureWellWhatIsIt_CheckedChanged(object sender, EventArgs e)
        {
            Player.GestureWellWhatIsIt = cbxGestureWellWhatIsIt.Checked;
        }

        private void cbxGestureLookSkyward_CheckedChanged(object sender, EventArgs e)
        {
            Player.GestureLookSkyward = cbxGestureLookSkyward.Checked;
        }

        private void cbxGestureShrug_CheckedChanged(object sender, EventArgs e)
        {
            Player.GestureShrug = cbxGestureShrug.Checked;
        }

        private void cbxGestureJoy_CheckedChanged(object sender, EventArgs e)
        {
            Player.GestureJoy = cbxGestureJoy.Checked;
        }

        private void cbxGestureHurrah_CheckedChanged(object sender, EventArgs e)
        {
            Player.GestureHurrah = cbxGestureHurrah.Checked;
        }

        private void cbxGestureProperBow_CheckedChanged(object sender, EventArgs e)
        {
            Player.GestureProperBow = cbxGestureProperBow.Checked;
        }

        private void cbxGestureBow_CheckedChanged(object sender, EventArgs e)
        {
            Player.GestureBow = cbxGestureProperBow.Checked;
        }

        private void cbxGestureWave_CheckedChanged(object sender, EventArgs e)
        {
            Player.GestureWave = cbxGestureWave.Checked;
        }

        private void cbxGestureBeckon_CheckedChanged(object sender, EventArgs e)
        {
            Player.GestureBeckon = cbxGestureBeckon.Checked;
        }

        private void cbxGesturePointDown_CheckedChanged(object sender, EventArgs e)
        {
            Player.GesturePointDown = cbxGesturePointDown.Checked;
        }

        private void cbxGesturePointUp_CheckedChanged(object sender, EventArgs e)
        {
            Player.GesturePointUp = cbxGesturePointUp.Checked;
        }

        private void cbxGesturePointForward_CheckedChanged(object sender, EventArgs e)
        {
            Player.GesturePointForward = cbxGesturePointForward.Checked;
        }

        private void cmbGestureSlot1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player.SetGestureSlot(0, (byte)(cmbGestureSlot1.SelectedItem as DSRGesture).ID);
        }

        private void cmbGestureSlot2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player.SetGestureSlot(1, (byte)(cmbGestureSlot2.SelectedItem as DSRGesture).ID);
        }

        private void cmbGestureSlot3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player.SetGestureSlot(2, (byte)(cmbGestureSlot3.SelectedItem as DSRGesture).ID);
        }

        private void cmbGestureSlot4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player.SetGestureSlot(3, (byte)(cmbGestureSlot4.SelectedItem as DSRGesture).ID);
        }

        private void cmbGestureSlot5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player.SetGestureSlot(4, (byte)(cmbGestureSlot5.SelectedItem as DSRGesture).ID);
        }

        private void cmbGestureSlot6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player.SetGestureSlot(5, (byte)(cmbGestureSlot6.SelectedItem as DSRGesture).ID);
        }

        private void cmbGestureSlot7_SelectedIndexChanged(object sender, EventArgs e)
        {
            Player.SetGestureSlot(6, (byte)(cmbGestureSlot7.SelectedItem as DSRGesture).ID);
        }

        #endregion
    }
}
