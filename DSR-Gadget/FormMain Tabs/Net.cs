using DSR_Gadget.SubForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {

        private DSRPlayer[] CurrentPlayers = new DSRPlayer[5];
        private DSRPlayer[] RecentPlayers = new DSRPlayer[5];
        private List<DSRSummonSign> SummonSignList;
        private DSRPlayer EmptyPlayer;
        private DSRSummonSign EmptySummonSign;

        private void initInfo()
        {
            for (int i = 0; i < 5; i++)
            {
                lbxNetRecentPlayers.Items.Add("");
                lbxNetCurrentPlayers.Items.Add("");
            }
            lbxNetCurrentPlayers.SelectedIndex = 0;
            lbxNetRecentPlayers.SelectedIndex = 0;
            lbxNetCurrentPlayers.SelectedIndexChanged += lbxNetCurrentPlayers_SelectedIndexChanged;

            EmptyPlayer = Hook.GetEmptyPlayer();
            EmptySummonSign = Hook.GetEmptySummonSign();
            btnCurrentPlayerMore.Enabled = false;

            nudSosSoulLevel.Maximum = int.MaxValue;
            nudSosSoulLevel.Minimum = int.MinValue;

            lbxNetSosList.DataSource = SummonSignList;

            foreach (DSRSummon summon in DSRSummon.All)
                cmbSosSummonType.Items.Add(summon);
            cmbSosSummonType.SelectedIndex = 0;
            cmbSosSummonType.SelectedIndexChanged += cmbSummonType_SelectedIndexChanged;

            btnCurrentPlayerFamilyShare.Enabled = true;

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
            if (selectedCurrentPlayer != null && selectedCurrentPlayer.PlayerPtr.Resolve() != IntPtr.Zero)
                updateCurrentPlayerUI(selectedCurrentPlayer);
            else
            {
                updateCurrentPlayerUI(EmptyPlayer);
                resetCamera();
            }
            
            

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

            
            SummonSignList = Hook.GetSummonSigns();
            if (lbxNetSosList.Items.Count > SummonSignList.Count)
            {
                for (int i = SummonSignList.Count > 0 ? SummonSignList.Count - 1 : 0; i < lbxNetSosList.Items.Count; i++)
                    lbxNetSosList.Items.RemoveAt(i);
            }
            if (lbxNetSosList.Items.Count < SummonSignList.Count)
            {
                for (int i = lbxNetSosList.Items.Count > 0 ? lbxNetSosList.Items.Count - 1 : 0; i < SummonSignList.Count; i++)
                    lbxNetSosList.Items.Add(SummonSignList[i]);
            }

            DSRSummonSign selectedSummonSign = lbxNetSosList.SelectedItem as DSRSummonSign;
            if (selectedSummonSign != null)
                updateSummonSignUI(selectedSummonSign);
            else
                updateSummonSignUI(EmptySummonSign);
            
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
            nupRecentPlayerWeaponMemory.Value = player.WeaponMemory;
            txtRecentPlayerName.Text = player.NameString1;
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
            nupCurrentPlayerWeaponMemory.Value = player.WeaponMemory;
            txtCurrentPlayerName.Text = player.NameString1;
            txtCurrentPlayerSteamName.Text = player.SteamName;
        }

        private void lbxNetCurrentPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCamera();
        }

        private void updateSummonSignUI (DSRSummonSign sign)
        {
            nudSosSoulLevel.Value = sign.SoulLevel;
            txtSosName.Text = sign.Name;

            DSRPlayer.Position pos = sign.GetPosition();
            nudSosPosX.Value = (decimal)pos.X;
            nudSosPosY.Value = (decimal)pos.Y;
            nudSosPosZ.Value = (decimal)pos.Z;
            nudSosPosAngle.Value = (decimal)pos.Angle;
            updateDropdown<DSRSummon>(cmbSosSummonType, sign.SummonType);
        }

        private void btnCurrentPlayerKick_Click(object sender, EventArgs e)
        {
            DSRPlayer player = lbxNetCurrentPlayers.SelectedItem as DSRPlayer;
            byte index = (byte)lbxNetCurrentPlayers.SelectedIndex;
            if (player != null && CurrentPlayers[index] != null)
                Hook.KickPlayer(player, index);
        }

        private async void btnCurrentPlayerFamilyShare_Click(object sender, EventArgs e)
        {
            
            DSRPlayer player = lbxNetCurrentPlayers.SelectedItem as DSRPlayer;
            if (player != null && player.SteamID64 > 0)
            {
                FamilyShareForm familyShareForm = new FamilyShareForm();
                familyShareForm.SteamID = player.SteamID64;
                familyShareForm.StartPosition = FormStartPosition.CenterScreen;
                familyShareForm.Show();
                await familyShareForm.LoadFamilyShareInfo();
            }
        }

        private void cbxCurrentPlayerCamera_CheckedChanged(object sender, EventArgs e)
        {
            updateCamera();
        }

        private void updateCamera()
        {
            if (cbxCurrentPlayerCamera.Checked)
            {
                DSRPlayer player = lbxNetCurrentPlayers.SelectedItem as DSRPlayer;

                if (player != null)
                {
                    IntPtr playerInsPtr = player.PlayerInsPtr.Resolve();
                    if (playerInsPtr != IntPtr.Zero)
                        Hook.SetCamera(playerInsPtr);
                }
                else
                    cbxCurrentPlayerCamera.Checked = false;
            }
            else
                Hook.SetCamera(IntPtr.Zero);
        }

        private void resetCamera()
        {
            Hook.SetCamera(IntPtr.Zero);
            cbxCurrentPlayerCamera.Checked = false;
        }

        private void btnCurrentPlayerTeleport_Click(object sender, EventArgs e)
        {
            DSRPlayer player = lbxNetCurrentPlayers.SelectedItem as DSRPlayer;
            if (player != null && player.PlayerPtr.Resolve() != IntPtr.Zero)
            {
                DSRPlayer.Position pos = player.GetPosition();
                Player.PosWarp(pos);
            }
        }

        private void cmbSummonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSRSummonSign sign = lbxNetSosList.SelectedItem as DSRSummonSign;

            if (loaded && !reading && sign != null)
                sign.SummonType = (cmbSosSummonType.SelectedItem as DSRSummon).ID;
        }

        private void btnSosRestorePos_Click(object sender, EventArgs e)
        {
            DSRSummonSign sign = lbxNetSosList.SelectedItem as DSRSummonSign;
            if (loaded && !reading && sign != null)
            {
                sign.PosX = (float)nudStoredX.Value;
                sign.PosY = (float)nudStoredY.Value;
                sign.PosZ = (float)nudStoredZ.Value;
                sign.PosAngle = (float)nudStoredAngle.Value;
            }
        }

        private void nudSosPosX_ValueChanged(object sender, EventArgs e)
        {
            DSRSummonSign sign = lbxNetSosList.SelectedItem as DSRSummonSign;
            if (loaded && !reading && sign != null)
                sign.PosX = (float)nudSosPosX.Value;
        }

        private void nudSosPosY_ValueChanged(object sender, EventArgs e)
        {
            DSRSummonSign sign = lbxNetSosList.SelectedItem as DSRSummonSign;
            if (loaded && !reading && sign != null)
                sign.PosY = (float)nudSosPosY.Value;
        }

        private void nudSosPosZ_ValueChanged(object sender, EventArgs e)
        {
            DSRSummonSign sign = lbxNetSosList.SelectedItem as DSRSummonSign;
            if (loaded && !reading && sign != null)
                sign.PosZ = (float)nudSosPosZ.Value;
        }

        private void nudSosPosAngle_ValueChanged(object sender, EventArgs e)
        {
            DSRSummonSign sign = lbxNetSosList.SelectedItem as DSRSummonSign;
            if (loaded && !reading && sign != null)
                sign.PosAngle = (float)nudSosPosAngle.Value;
        }
    }
}
