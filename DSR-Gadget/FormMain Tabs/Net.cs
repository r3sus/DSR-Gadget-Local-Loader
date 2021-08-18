using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {

        DSRPlayer[] CurrentPlayers = new DSRPlayer[5];
        DSRPlayer[] RecentPlayers = new DSRPlayer[5];
        DSRPlayer EmptyPlayer;

        private void initInfo()
        {
            for (int i = 0; i < 5; i++)
            {
                lbxNetRecentPlayers.Items.Add("");
                lbxNetCurrentPlayers.Items.Add("");
            }


            EmptyPlayer = Hook.GetEmptyPlayer();



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
            
            
            bool[] currentPlayerIndices = Hook.GetCurrentPlayers();


            // TODO: duplicate code
            
            
            for (int i = 0; i < currentPlayerIndices.Length; i++)
            {
                if (currentPlayerIndices[i])
                {
                    if (CurrentPlayers[i] == null)
                    {
                        DSRPlayer player = Hook.GetCurrentPlayer(i);
                        if (player != null)
                            CurrentPlayers[i] = player;
                    }

                }
                else
                {
                    lbxNetCurrentPlayers.Items[i] = "";
                }

                DSRPlayer item = lbxNetCurrentPlayers.Items[i] as DSRPlayer;
                if (CurrentPlayers[i] != null && item == null) //|| !item.ToString().Equals(CurrentPlayers[i].ToString())))
                    lbxNetCurrentPlayers.Items[i] = CurrentPlayers[i];
            }

            DSRPlayer selectedCurrentPlayer = lbxNetCurrentPlayers.SelectedItem as DSRPlayer;
            if (selectedCurrentPlayer != null)
                updateCurrentPlayerUI(selectedCurrentPlayer);
            else
                updateCurrentPlayerUI(EmptyPlayer);
            
            

            bool[] recentPlayerIndices = Hook.GetRecentPlayers();

            for (int i = 0; i < recentPlayerIndices.Length; i++)
            {
                if (recentPlayerIndices[i])
                {
                    if (RecentPlayers[i] == null)
                    {
                        DSRPlayer player = Hook.GetRecentPlayer(i);
                        if (player != null)
                            RecentPlayers[i] = player;
                    }
                }
                else
                {
                    lbxNetRecentPlayers.Items[i] = "";
                }

                DSRPlayer item = lbxNetRecentPlayers.Items[i] as DSRPlayer;
                if (RecentPlayers[i] != null && item == null)// || !item.ToString().Equals(RecentPlayers[i].ToString())))
                    lbxNetRecentPlayers.Items[i] = RecentPlayers[i];
            }

            // TODO handle this in a better way
            for (int i = 0; i < lbxNetCurrentPlayers.Items.Count; i++)
            {
                lbxNetCurrentPlayers.Items[i] = lbxNetCurrentPlayers.Items[i];
            }

            for (int i = 0; i < lbxNetRecentPlayers.Items.Count; i++)
            {
                lbxNetRecentPlayers.Items[i] = lbxNetRecentPlayers.Items[i];
            }

            DSRPlayer selectedRecentPlayer = lbxNetRecentPlayers.SelectedItem as DSRPlayer;
            if (selectedRecentPlayer != null)
                updateRecentPlayerUI(selectedRecentPlayer);
            else
                updateRecentPlayerUI(EmptyPlayer);
            
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
            byte index = (byte)lbxNetCurrentPlayers.SelectedIndex;
            if (player != null && CurrentPlayers[index] != null)
                Hook.KickPlayer(player, index);
        }
    }
}
