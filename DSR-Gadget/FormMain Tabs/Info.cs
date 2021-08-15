using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private void initInfo()
        {
            for (int i = 0; i < 5; i++)
                lbxInfoRecentPlayers.Items.Add("");

            lbxInfoRecentPlayers.ValueMember = "PlayerIndex";

            /*
            Dictionary<int, DSRItem> weapons = new Dictionary<int, DSRItem>();

            foreach (DSRItemCategory category in DSRItemCategory.All)
            {
                if (category.ID == 0x00000000)
                {
                    foreach(DSRItem item in category.Items)
                    {
                        weapons[item.ID] = item;
                        switch (item.UpgradeType)
                        {
                            case DSRItem.Upgrade.None:
                                break;
                            case DSRItem.Upgrade.Unique:
                                for (int i = 1; i <= 5; i++)
                                {
                                    DSRItem infusedItem = new DSRItem()
                                }
                                break;
                            case DSRItem.Upgrade.Armor:
                                break;
                            case DSRItem.Upgrade.Infusable:
                                break;
                            case DSRItem.Upgrade.InfusableRestricted:
                                break;
                            case DSRItem.Upgrade.PyroFlame:
                                break;
                            case DSRItem.Upgrade.PyroFlameAscended:
                                break;
                        }
                    }
                }
            }
            */

            /*
            foreach (DSRClass charClass in DSRClass.All)
                cmbClass.Items.Add(charClass);
            nudHumanity.Maximum = int.MaxValue;
            nudHumanity.Minimum = int.MinValue;
            */
        }

        private void saveInfo() { }

        private void resetInfo() { }

        private void reloadInfo()
        {
            //cmbClass.SelectedIndex = Hook.Class;
        }

        private void updateInfo()
        {
            //lbxInfoPlayers.Items.Add();
            DSRPlayer[] recentPlayers = new DSRPlayer[5];
            bool[] recentPlayerIndices = Hook.GetRecentPlayers();
            for (int i = 0; i < recentPlayerIndices.Length; i++)
            {
                if (recentPlayerIndices[i])
                {
                    if (recentPlayers[i] == null || recentPlayers[i].PlayerIndex != i)
                        recentPlayers[i] = new DSRPlayer(i);

                    recentPlayers[i] = Hook.UpdateRecentPlayer(recentPlayers[i]);
                }
                else
                {
                    if (recentPlayers[i] == null || recentPlayers[i].PlayerIndex != -1)
                        recentPlayers[i] = new DSRPlayer(-1);
                }
            }

            for (int i = 0; i< recentPlayerIndices.Length; i++)
            {
                DSRPlayer item = lbxInfoRecentPlayers.Items[i] as DSRPlayer;
                if (item == null || !item.Name.Equals(recentPlayers[i].Name) || item.PlayerIndex != recentPlayers[i].PlayerIndex)
                    lbxInfoRecentPlayers.Items[i] = recentPlayers[i];
            }

            DSRPlayer selectedRecentPlayer = lbxInfoRecentPlayers.SelectedItem as DSRPlayer;
            if (selectedRecentPlayer != null)
                updateRecentPlayerUI(selectedRecentPlayer);


            //updateRecentPlayerUI(lbxInfoRecentPlayers.SelectedItem as DSRPlayer);


            /*
            if (!lbxInfoRecentPlayers.Items[i].Equals(recentPlayerText))
                lbxInfoRecentPlayers.Items[i] = recentPlayerText;
            lbxInfoRecentPlayers.Items[i] = "";
            */

            /*
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
            */
        }


        /*
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

        */

        private void updateRecentPlayerUI(DSRPlayer player)
        {
            nupRecentPlayerSoulLevel.Value = player.SoulLevel;
            nupRecentPlayerVitality.Value = player.Vitality;
            nupRecentPlayerAttunement.Value = player.Attunement;
            nupRecentPlayerEndurance.Value = player.Endurance;
            nupRecentPlayerStrength.Value = player.Strength;
            nupRecentPlayerDexterity.Value = player.Dexterity;
            nupRecentPlayerResistance.Value = player.Resistance;
            nupRecentPlayerIntelligence.Value = player.Intelligence;
            nupRecentPlayerFaith.Value = player.Faith;
            nupRecentPlayerHumanity.Value = player.Humanity;
        }
    }
}
