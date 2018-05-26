﻿using System;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private void initStats()
        {
            foreach (DSRClass charClass in DSRClass.All)
                cmbClass.Items.Add(charClass);
            nudHumanity.Maximum = Int32.MaxValue;
            nudHumanity.Minimum = Int32.MinValue;
        }

        private void saveStats() { }

        private void resetStats() { }

        private void reloadStats()
        {
            cmbClass.SelectedIndex = dsrProcess.GetClass();
        }

        private void updateStats()
        {
            txtSoulLevel.Text = dsrProcess.GetSoulLevel().ToString();
            nudHumanity.Value = dsrProcess.GetHumanity();
            nudSouls.Value = dsrProcess.GetSouls();

            try
            {
                nudVitality.Value = dsrProcess.GetVitality();
                nudAttunement.Value = dsrProcess.GetAttunement();
                nudEndurance.Value = dsrProcess.GetEndurance();
                nudStrength.Value = dsrProcess.GetStrength();
                nudDexterity.Value = dsrProcess.GetDexterity();
                nudResistance.Value = dsrProcess.GetResistance();
                nudIntelligence.Value = dsrProcess.GetIntelligence();
                nudFaith.Value = dsrProcess.GetFaith();
            }
            catch (ArgumentOutOfRangeException) { }
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
            //if (!reading) ;
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

            dsrProcess.LevelUp(vit, att, end, str, dex, res, intel, fth, sl);
        }
    }
}
