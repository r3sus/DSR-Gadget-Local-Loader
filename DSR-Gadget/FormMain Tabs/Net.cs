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
            {
                lbxNetRecentPlayers.Items.Add("");
                lbxNetCurrentPlayers.Items.Add("");
            }


            lbxNetRecentPlayers.ValueMember = "PlayerIndex";
            lbxNetCurrentPlayers.ValueMember = "PlayerIndex";

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
            
            DSRPlayer[] currentPlayers = new DSRPlayer[5];
            bool[] currentPlayerIndices = Hook.GetCurrentPlayers();


            // TODO: duplicate code
            
            for (int i = 0; i < currentPlayerIndices.Length; i++)
            {
                if (currentPlayerIndices[i])
                {
                    if (currentPlayers[i] == null || currentPlayers[i].PlayerIndex != i)
                        currentPlayers[i] = new DSRPlayer(i);

                    currentPlayers[i] = Hook.UpdateRecentPlayer(currentPlayers[i]);
                }
                else
                {
                    if (currentPlayers[i] == null || currentPlayers[i].PlayerIndex != -1)
                        currentPlayers[i] = new DSRPlayer(-1);
                }

                DSRPlayer item = lbxNetCurrentPlayers.Items[i] as DSRPlayer;
                if (item == null || !item.Name.Equals(currentPlayers[i].Name) || item.PlayerIndex != currentPlayers[i].PlayerIndex)
                    lbxNetCurrentPlayers.Items[i] = currentPlayers[i];
            }

            DSRPlayer selectedCurrentPlayer = lbxNetCurrentPlayers.SelectedItem as DSRPlayer;
            if (selectedCurrentPlayer != null)
                updateCurrentPlayerUI(selectedCurrentPlayer);
            

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

                DSRPlayer item = lbxNetRecentPlayers.Items[i] as DSRPlayer;
                if (item == null || !item.Name.Equals(recentPlayers[i].Name) || item.PlayerIndex != recentPlayers[i].PlayerIndex)
                    lbxNetRecentPlayers.Items[i] = recentPlayers[i];
            }

            DSRPlayer selectedRecentPlayer = lbxNetRecentPlayers.SelectedItem as DSRPlayer;
            if (selectedRecentPlayer != null)
                updateRecentPlayerUI(selectedRecentPlayer);


        }

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
        private void updateCurrentPlayerUI(DSRPlayer player)
        {
            nupCurrentPlayerSoulLevel.Value = player.SoulLevel;
            nupCurrentPlayerVitality.Value = player.Vitality;
            nupCurrentPlayerAttunement.Value = player.Attunement;
            nupCurrentPlayerEndurance.Value = player.Endurance;
            nupCurrentPlayerStrength.Value = player.Strength;
            nupCurrentPlayerDexterity.Value = player.Dexterity;
            nupCurrentPlayerResistance.Value = player.Resistance;
            nupCurrentPlayerIntelligence.Value = player.Intelligence;
            nupCurrentPlayerFaith.Value = player.Faith;
            nupCurrentPlayerHumanity.Value = player.Humanity;
        }

        private void btnCurrentPlayerKick_Click(object sender, EventArgs e)
        {
            DSRPlayer player = lbxNetCurrentPlayers.SelectedItem as DSRPlayer;
            if (player != null && player.PlayerIndex != -1)
                Hook.KickPlayer(player);
        }
    }
}
