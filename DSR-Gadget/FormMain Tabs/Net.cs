using DSR_Gadget.SubForms;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {

        private DSRPlayer[] CurrentPlayers = new DSRPlayer[5];
        private DSRPlayer[] RecentPlayers = new DSRPlayer[5];
        private List<DSRSummonSign> SummonSignList;
        private List<DSRSummonSignSfx> AllSignList;
        private DSRPlayer EmptyPlayer;
        private DSRSummonSign EmptySummonSign;
        private DSRSummonSignSfx EmptySummonSignSfx;

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
            EmptySummonSignSfx = Hook.GetEmptySummonSignSfx();
            btnCurrentPlayerMore.Enabled = false;

            nudSosSoulLevel.Maximum = int.MaxValue;
            nudSosSoulLevel.Minimum = int.MinValue;

            lbxNetSosList.DataSource = SummonSignList;

            foreach (DSRSummon summon in DSRSummon.All)
                cmbSosSummonType.Items.Add(summon);
            cmbSosSummonType.SelectedIndex = 0;
            cmbSosSummonType.SelectedIndexChanged += cmbSummonType_SelectedIndexChanged;

            foreach (DSRSummon summon in DSRSummon.All)
                cmbSosAllSummonType.Items.Add(summon);
            cmbSosAllSummonType.SelectedIndex = 0;

            btnCurrentPlayerFamilyShare.Enabled = true;

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

            updateListBox(lbxNetSosList, SummonSignList);

            DSRSummonSign selectedSummonSign = lbxNetSosList.SelectedItem as DSRSummonSign;
            if (selectedSummonSign != null)
                updateSummonSignUI(selectedSummonSign);
            else
                updateSummonSignUI(EmptySummonSign);


            AllSignList = Hook.GetSummonSignsSfx();

            updateListBox(lbxNetSosAll, AllSignList);

            DSRSummonSignSfx selectedSummonSignSfx = lbxNetSosAll.SelectedItem as DSRSummonSignSfx;
            if (selectedSummonSignSfx != null)
                updateSignAllUI(selectedSummonSignSfx);
            else
                updateSignAllUI(EmptySummonSignSfx);

        }

        private void updateListBox<T>(ListBox lbx, List<T> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i >= lbx.Items.Count)
                {
                    lbx.Items.Add(items[i]);
                    if (lbx.Items.Count == 1)
                        lbx.SelectedIndex = 0;
                }
                else
                    lbx.Items[i] = items[i];
            }
            if (lbx.Items.Count > items.Count)
            {
                for (int i = items.Count; i < lbx.Items.Count; i++)
                {
                    if (lbx.SelectedIndex == i)
                        lbx.SelectedIndex -= 1;
                    lbx.Items.RemoveAt(i);
                }
            }
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

            txtRecentPlayerHair.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, player.Hair, player.Hair.ToString());
            txtRecentPlayerHead.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, player.ArmorHead, player.ArmorHead.ToString());
            txtRecentPlayerChest.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, player.ArmorChest, player.ArmorChest.ToString());
            txtRecentPlayerHands.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, player.ArmorHands, player.ArmorHands.ToString());
            txtRecentPlayerLegs.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, player.ArmorLegs, player.ArmorLegs.ToString());

            txtRecentPlayerArrow1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.Arrow1, player.Arrow1.ToString());
            txtRecentPlayerArrow2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.Arrow2, player.Arrow2.ToString());
            txtRecentPlayerBolt1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.Bolt1, player.Bolt1.ToString());
            txtRecentPlayerBolt2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.Bolt2, player.Bolt2.ToString());

            txtRecentPlayerRightWep1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.RightWep1, player.RightWep1.ToString());
            txtRecentPlayerRightWep2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.RightWep2, player.RightWep2.ToString());
            txtRecentPlayerLeftWep1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.LeftWep1, player.LeftWep1.ToString());
            txtRecentPlayerLeftWep2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.LeftWep2, player.LeftWep2.ToString());

            txtRecentPlayerRing1.Text = Util.Util.DictGetOrDefault(DSRAccessory.Dict, player.Ring1, player.Ring1.ToString());
            txtRecentPlayerRing2.Text = Util.Util.DictGetOrDefault(DSRAccessory.Dict, player.Ring2, player.Ring2.ToString());

            txtRecentPlayerQuickbar1.Text = Util.Util.DictGetOrDefault(DSRGood.Dict, player.Quickbar1, player.Quickbar1.ToString());
            txtRecentPlayerQuickbar2.Text = Util.Util.DictGetOrDefault(DSRGood.Dict, player.Quickbar2, player.Quickbar2.ToString());
            txtRecentPlayerQuickbar3.Text = Util.Util.DictGetOrDefault(DSRGood.Dict, player.Quickbar3, player.Quickbar3.ToString());
            txtRecentPlayerQuickbar4.Text = Util.Util.DictGetOrDefault(DSRGood.Dict, player.Quickbar4, player.Quickbar4.ToString());
            txtRecentPlayerQuickbar5.Text = Util.Util.DictGetOrDefault(DSRGood.Dict, player.Quickbar5, player.Quickbar5.ToString());
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

            txtCurrentPlayerHair.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, player.Hair, player.Hair.ToString());
            txtCurrentPlayerHead.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, player.ArmorHead, player.ArmorHead.ToString());
            txtCurrentPlayerChest.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, player.ArmorChest, player.ArmorChest.ToString());
            txtCurrentPlayerHands.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, player.ArmorHands, player.ArmorHands.ToString());
            txtCurrentPlayerLegs.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, player.ArmorLegs, player.ArmorLegs.ToString());
            
            txtCurrentPlayerArrow1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.Arrow1, player.Arrow1.ToString());
            txtCurrentPlayerArrow2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.Arrow2, player.Arrow2.ToString());
            txtCurrentPlayerBolt1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.Bolt1, player.Bolt1.ToString());
            txtCurrentPlayerBolt2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.Bolt2, player.Bolt2.ToString());
            
            txtCurrentPlayerRightWep1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.RightWep1, player.RightWep1.ToString());
            txtCurrentPlayerRightWep2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.RightWep2, player.RightWep2.ToString());
            txtCurrentPlayerLeftWep1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.LeftWep1, player.LeftWep1.ToString());
            txtCurrentPlayerLeftWep2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, player.LeftWep2, player.LeftWep2.ToString());
            
            txtCurrentPlayerRing1.Text = Util.Util.DictGetOrDefault(DSRAccessory.Dict, player.Ring1, player.Ring1.ToString());
            txtCurrentPlayerRing2.Text = Util.Util.DictGetOrDefault(DSRAccessory.Dict, player.Ring2, player.Ring2.ToString());
            
            txtCurrentPlayerQuickbar1.Text = Util.Util.DictGetOrDefault(DSRGood.Dict, player.Quickbar1, player.Quickbar1.ToString());
            txtCurrentPlayerQuickbar2.Text = Util.Util.DictGetOrDefault(DSRGood.Dict, player.Quickbar2, player.Quickbar2.ToString());
            txtCurrentPlayerQuickbar3.Text = Util.Util.DictGetOrDefault(DSRGood.Dict, player.Quickbar3, player.Quickbar3.ToString());
            txtCurrentPlayerQuickbar4.Text = Util.Util.DictGetOrDefault(DSRGood.Dict, player.Quickbar4, player.Quickbar4.ToString());
            txtCurrentPlayerQuickbar5.Text = Util.Util.DictGetOrDefault(DSRGood.Dict, player.Quickbar5, player.Quickbar5.ToString());
        }

        private void lbxNetCurrentPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCamera();
        }

        private void updateSummonSignUI (DSRSummonSign sign)
        {
            nudSosSoulLevel.Value = sign.SoulLevel;
            txtSosName.Text = sign.Name;
            txtSosSteamID.Text = sign.SteamID64.ToString();

            DSRPlayer.Position pos = sign.GetPosition();
            nudSosPosX.Value = (decimal)pos.X;
            nudSosPosY.Value = (decimal)pos.Y;
            nudSosPosZ.Value = (decimal)pos.Z;
            nudSosPosAngle.Value = angleToDegree(pos.Angle);
            updateDropdown<DSRSummon>(cmbSosSummonType, sign.SummonType);

            txtSosHair.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, sign.Hair, sign.Hair.ToString());
            txtSosHead.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, sign.ArmorHead, sign.ArmorHead.ToString());
            txtSosChest.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, sign.ArmorChest, sign.ArmorChest.ToString());
            txtSosHands.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, sign.ArmorHands, sign.ArmorHands.ToString());
            txtSosLegs.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, sign.ArmorLegs, sign.ArmorLegs.ToString());
            
            txtSosArrow1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.Arrow1, sign.Arrow1.ToString());
            txtSosArrow2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.Arrow2, sign.Arrow2.ToString());
            txtSosBolt1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.Bolt1, sign.Bolt1.ToString());
            txtSosBolt2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.Bolt2, sign.Bolt2.ToString());
            
            txtSosRightWep1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.RightWep1, sign.RightWep1.ToString());
            txtSosRightWep2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.RightWep2, sign.RightWep2.ToString());
            txtSosLeftWep1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.LeftWep1, sign.LeftWep1.ToString());
            txtSosLeftWep2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.LeftWep2, sign.LeftWep2.ToString());
        }

        private void updateSignAllUI(DSRSummonSignSfx sign)
        {
            txtSosAllName.Text = sign.Name;

            DSRPlayer.Position pos = sign.GetPosition();
            nudSosAllPosX.Value = (decimal)pos.X;
            nudSosAllPosY.Value = (decimal)pos.Y;
            nudSosAllPosZ.Value = (decimal)pos.Z;
            nudSosAllPosAngle.Value = angleToDegree(pos.Angle);
            updateDropdown<DSRSummon>(cmbSosAllSummonType, sign.SummonType);

            if (sign.SummonType == 7)
                sign = EmptySummonSignSfx;

            txtSosAllHair.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, sign.Hair, sign.Hair.ToString());
            txtSosAllHead.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, sign.ArmorHead, sign.ArmorHead.ToString());
            txtSosAllChest.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, sign.ArmorChest, sign.ArmorChest.ToString());
            txtSosAllHands.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, sign.ArmorHands, sign.ArmorHands.ToString());
            txtSosAllLegs.Text = Util.Util.DictGetOrDefault(DSRProtector.Dict, sign.ArmorLegs, sign.ArmorLegs.ToString());

            txtSosAllArrow1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.Arrow1, sign.Arrow1.ToString());
            txtSosAllArrow2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.Arrow2, sign.Arrow2.ToString());
            txtSosAllBolt1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.Bolt1, sign.Bolt1.ToString());
            txtSosAllBolt2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.Bolt2, sign.Bolt2.ToString());

            txtSosAllRightWep1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.RightWep1, sign.RightWep1.ToString());
            txtSosAllRightWep2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.RightWep2, sign.RightWep2.ToString());
            txtSosAllLeftWep1.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.LeftWep1, sign.LeftWep1.ToString());
            txtSosAllLeftWep2.Text = Util.Util.DictGetOrDefault(DSRWeapon.Dict, sign.LeftWep2, sign.LeftWep2.ToString());
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

        private void btnRecentPlayerFamilyShare_Click(object sender, EventArgs e)
        {
            FamilyShareInputForm familyShareInputForm = new FamilyShareInputForm();
            familyShareInputForm.StartPosition = FormStartPosition.CenterScreen;
            familyShareInputForm.Show();
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

        private void btnSosSteamProfile_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^.*(?<ID>[0-9]{17}).*$");
            string steamID = txtSosSteamID.Text;
            if (regex.IsMatch(steamID))
                Util.Util.OpenUrl("https://www.steamcommunity.com/profiles/" + steamID);
        }

        private void btnSosRestorePos_Click(object sender, EventArgs e)
        {
            DSRSummonSign sign = lbxNetSosList.SelectedItem as DSRSummonSign;
            if (loaded && !reading && sign != null)
            {
                sign.PosX = (float)nudStoredX.Value;
                sign.PosY = (float)nudStoredY.Value;
                sign.PosZ = (float)nudStoredZ.Value;
                sign.PosAngle = degreeToAngle(nudStoredAngle.Value);
            }
        }

        private void btnSosAllTrigger_Click(object sender, EventArgs e)
        {
            DSRSummonSignSfx sign = lbxNetSosAll.SelectedItem as DSRSummonSignSfx;
            if (sign != null)
                Hook.TriggerSign(sign);
        }

        private void btnSosAllRestorePos_Click(object sender, EventArgs e)
        {
            DSRSummonSignSfx sign = lbxNetSosAll.SelectedItem as DSRSummonSignSfx;
            if (loaded && !reading && sign != null)
            {
                sign.PosX = (float)nudStoredX.Value;
                sign.PosY = (float)nudStoredY.Value;
                sign.PosZ = (float)nudStoredZ.Value;
                sign.PosAngle = degreeToAngle(nudStoredAngle.Value);
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

        private void nudSosAllPosX_ValueChanged(object sender, EventArgs e)
        {
            DSRSummonSignSfx sign = lbxNetSosAll.SelectedItem as DSRSummonSignSfx;
            if (loaded && !reading && sign != null)
                sign.PosX = (float)nudSosAllPosX.Value;
        }

        private void nudSosAllPosY_ValueChanged(object sender, EventArgs e)
        {
            DSRSummonSignSfx sign = lbxNetSosAll.SelectedItem as DSRSummonSignSfx;
            if (loaded && !reading && sign != null)
                sign.PosY = (float)nudSosAllPosY.Value;
        }

        private void nudSosAllPosZ_ValueChanged(object sender, EventArgs e)
        {
            DSRSummonSignSfx sign = lbxNetSosAll.SelectedItem as DSRSummonSignSfx;
            if (loaded && !reading && sign != null)
                sign.PosZ = (float)nudSosAllPosZ.Value;
        }

        private void nudSosAllPosAngle_ValueChanged(object sender, EventArgs e)
        {
            DSRSummonSignSfx sign = lbxNetSosAll.SelectedItem as DSRSummonSignSfx;
            if (loaded && !reading && sign != null)
                sign.PosAngle = (float)nudSosAllPosAngle.Value;
        }
    }
}
