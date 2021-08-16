using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private struct PlayerState
        {
            public bool Set;
            public decimal Health, Stamina;
            public bool DeathCam;
            public byte[] FollowCam;
            public int[][] EquipMagicData;
        }

        private List<int> unknownBonfires = new List<int>();
        private PlayerState playerState;

        private void initPlayer()
        {
            cbxRestoreState.Checked = settings.RestoreState;
            foreach (DSRBonfire bonfire in DSRBonfire.All)
                cmbBonfire.Items.Add(bonfire);
            cmbBonfire.SelectedIndex = 0;
            foreach (DSRTeam team in DSRTeam.All)
                cmbChrSelect.Items.Add(team);
            cmbChrSelect.SelectedIndex = 0;
            foreach (DSRInvasion invasion in DSRInvasion.All)
                cmbInvasionSelect.Items.Add(invasion);
            cmbInvasionSelect.SelectedIndex = 0;
            foreach (DSRArea area in DSRArea.All)
            {
                cmbMPAreaID.Items.Add(area);
                cmbAreaID.Items.Add(area);
            }
            cmbMPAreaID.SelectedIndex = 0;
            cmbAreaID.SelectedIndex = 0;
            nudSpeed.Value = settings.AnimSpeed;
        }

        private void savePlayer()
        {
            settings.RestoreState = cbxRestoreState.Checked;
            settings.AnimSpeed = nudSpeed.Value;
        }

        private void resetPlayer()
        {
            if (loaded)
            {
                if (!cbxGravity.Checked)
                    Hook.NoGravity = false;
                if (!cbxCollision.Checked)
                    Hook.NoCollision = false;
                if (cbxSpeed.Checked)
                    Hook.AnimSpeed = 1;
            }
        }

        private void reloadPlayer()
        {
            if (!cbxGravity.Checked)
                Hook.NoGravity = true;
            if (!cbxCollision.Checked)
                Hook.NoCollision = true;
        }

        private void updatePlayer()
        {
            nudHealth.Value = Hook.Health;
            nudHealthMax.Value = Hook.HealthMax;
            nudStamina.Value = Hook.Stamina;
            nudStaminaMax.Value = Hook.StaminaMax;

            if (cbxFreezeChrType.Checked)
                Hook.ChrType = (int)nudChrType.Value;
            else
                nudChrType.Value = Hook.ChrType;

            if (cbxFreezeTeamType.Checked)
                Hook.TeamType = (int)nudTeamType.Value;
            else
                nudTeamType.Value = Hook.TeamType;

            if (cbxFreezeInvadeType.Checked)
                Hook.InvadeType = (byte)nudInvadeType.Value;
            else
                nudInvadeType.Value = Hook.InvadeType;

            try
            {
                Hook.GetPosition(out float x, out float y, out float z, out float angle);
                nudPosX.Value = (decimal)x;
                nudPosY.Value = (decimal)y;
                nudPosZ.Value = (decimal)z;
                nudPosAngle.Value = angleToDegree(angle);

                Hook.GetStablePosition(out x, out y, out z, out angle);
                nudStableX.Value = (decimal)x;
                nudStableY.Value = (decimal)y;
                nudStableZ.Value = (decimal)z;
                nudStableAngle.Value = angleToDegree(angle);
            }
            catch (OverflowException)
            {
                nudPosX.Value = 0;
                nudPosY.Value = 0;
                nudPosZ.Value = 0;
                nudPosAngle.Value = 0;

                nudStableX.Value = 0;
                nudStableY.Value = 0;
                nudStableZ.Value = 0;
                nudStableAngle.Value = 0;
            }

            cbxDeathCam.Checked = Hook.DeathCam;
            if (cbxSpeed.Checked)
                Hook.AnimSpeed = (float)nudSpeed.Value;

            updateBonfire(cmbBonfire, Hook.LastBonfire);

            updateTeam(cmbChrSelect, Hook.ChrType, Hook.TeamType);

            updateInvadeType(cmbInvasionSelect, Hook.InvadeType);

            updateAreaID(cmbMPAreaID, cbxFreezeMPAreaID, Hook.MPAreaID, value => Hook.MPAreaID = value);
            updateAreaID(cmbAreaID, cbxFreezeAreaID, Hook.AreaID, value => Hook.AreaID = value);

        }

        private void nudHealth_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.Health = (int)nudHealth.Value;
        }

        private void nudStamina_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.Stamina = (int)nudStamina.Value;
        }

        private void btnResetMagicQuantity_Click(object sender, EventArgs e)
        {
            resetMagicQuantity();
        }

        private void btnLeaveSession_Click(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.LeaveSession();
        }

        private void resetMagicQuantity()
        {
            if (loaded && !reading)
            {
                Hook.ResetMagicQuantity();
            }
        }

        private void nudChrType_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.ChrType = (int)nudChrType.Value;
        }

        private void nudTeamType_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.TeamType = (int)nudTeamType.Value;
        }

        private void nudInvadeType_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.InvadeType = (byte)nudInvadeType.Value;
        }

        private void cmbChrSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRTeam item = cmbChrSelect.SelectedItem as DSRTeam;
                Hook.ChrType = item.ChrType;
                Hook.TeamType = item.TeamType;
            }
        }

        private void cmbInvasionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRInvasion item = cmbInvasionSelect.SelectedItem as DSRInvasion;
                Hook.InvadeType = item.InvadeType;
            }
        }

        private void cmbMPAreaID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRArea item = cmbMPAreaID.SelectedItem as DSRArea;
                Hook.MPAreaID = item.AreaID;
            }
        }

        private void cmbAreaID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRArea item = cmbAreaID.SelectedItem as DSRArea;
                Hook.AreaID = item.AreaID;
            }
        }

        private void btnPosStore_Click(object sender, EventArgs e)
        {
            if (loaded)
                storePosition();
        }

        private void storePosition()
        {
            nudStoredX.Value = nudPosX.Value;
            nudStoredY.Value = nudPosY.Value;
            nudStoredZ.Value = nudPosZ.Value;
            nudStoredAngle.Value = nudPosAngle.Value;

            playerState.Health = nudHealth.Value;
            playerState.Stamina = nudStamina.Value;
            playerState.FollowCam = Hook.DumpFollowCam();
            playerState.DeathCam = cbxDeathCam.Checked;
            playerState.EquipMagicData = Hook.EquipMagicData;
            playerState.Set = true;
        }

        private void btnPosRestore_Click(object sender, EventArgs e)
        {
            if (loaded)
                restorePosition();
        }

        private void restorePosition()
        {
            float x = (float)nudStoredX.Value;
            float y = (float)nudStoredY.Value;
            float z = (float)nudStoredZ.Value;
            float angle = degreeToAngle(nudStoredAngle.Value);
            Hook.PosWarp(x, y, z, angle);

            if (playerState.Set)
            {
                // Two frames for safety, wait until after warp
                System.Threading.Thread.Sleep(1000 / 15);
                Hook.UndumpFollowCam(playerState.FollowCam);

                if (cbxRestoreState.Checked)
                {
                    nudHealth.Value = playerState.Health;
                    nudStamina.Value = playerState.Stamina;
                    cbxDeathCam.Checked = playerState.DeathCam;
                    Hook.EquipMagicData = playerState.EquipMagicData;
                }
            }
        }

        private void btnTeleportBloodstain_Click(object sender, EventArgs e)
        {
            teleportBloodstain();
        }

        private void teleportBloodstain()
        {
            if (loaded && !reading)
            {
                Hook.GetLastBloodstainPosition(out float x, out float y, out float z, out float angle);
                Hook.PosWarp(x, y, z, angle);
            }
        }

        private void btnTeleportInitialPosition_Click(object sender, EventArgs e)
        {
            teleportInitialPosition();
        }

        private void teleportInitialPosition()
        {
            if (loaded && !reading)
            {
                Hook.GetInitialPosition(out float x, out float y, out float z, out float angle);
                Hook.PosWarp(x, y, z, angle);
            }
        }

        private void cbxGravity_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                Hook.NoGravity = !cbxGravity.Checked;
        }

        private void cbxCollision_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                Hook.NoCollision = !cbxCollision.Checked;
        }

        private void cbxDeathCam_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Hook.DeathCam = cbxDeathCam.Checked;
        }

        private void cmbBonfire_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRBonfire bonfire = cmbBonfire.SelectedItem as DSRBonfire;
                Hook.LastBonfire = bonfire.ID;
            }
        }

        private void btnWarp_Click(object sender, EventArgs e)
        {
            if (loaded)
                Hook.BonfireWarp();
        }

        private void cbxSpeed_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded && !cbxSpeed.Checked)
                Hook.AnimSpeed = 1;
        }

        private decimal angleToDegree(float angle)
        {
            return (decimal)((angle + Math.PI) / (Math.PI * 2) * 360);
        }

        private float degreeToAngle(decimal degree)
        {
            return (float)((double)degree / 360 * (Math.PI * 2) - Math.PI);
        }

        private void updateBonfire(ComboBox cmbBonfire, int bonfireID)
        {
            DSRBonfire lastBonfire = cmbBonfire.SelectedItem as DSRBonfire;
            if (!cmbBonfire.DroppedDown && bonfireID != lastBonfire.ID && !unknownBonfires.Contains(bonfireID))
            {
                DSRBonfire thisBonfire = null;
                foreach (object item in cmbBonfire.Items)
                {
                    DSRBonfire bonfire = item as DSRBonfire;
                    if (bonfireID == bonfire.ID)
                    {
                        thisBonfire = bonfire;
                        break;
                    }
                }

                if (thisBonfire == null)
                {
                    unknownBonfires.Add(bonfireID);
                    MessageBox.Show("Unknown bonfire ID, please report me: " + bonfireID, "Unknown Bonfire");
                }
                else
                    cmbBonfire.SelectedItem = thisBonfire;
            }
        }

        private void updateTeam(ComboBox cmbChrSelect, int chrType, int teamType)
        {
            DSRTeam lastTeam = cmbChrSelect.SelectedItem as DSRTeam;
            if (!cmbChrSelect.DroppedDown && (lastTeam.ChrType != chrType || lastTeam.TeamType != teamType))
            {
                bool found = false;
                foreach (DSRTeam item in cmbChrSelect.Items)
                {
                    if (item.ChrType == chrType && item.TeamType == teamType)
                    {
                        cmbChrSelect.SelectedItem = item;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    DSRTeam item = new DSRTeam("Unknown", chrType, teamType);
                    cmbChrSelect.Items.Add(item);
                    cmbChrSelect.SelectedItem = item;
                }
            }
        }

        private void updateInvadeType(ComboBox cmbInvasionSelect, byte invadeType)
        {
            DSRInvasion lastInvasion = cmbInvasionSelect.SelectedItem as DSRInvasion;
            if (!cmbInvasionSelect.DroppedDown && lastInvasion.InvadeType != invadeType)
            {
                bool found = false;
                foreach (DSRInvasion item in cmbInvasionSelect.Items)
                {
                    if (item.InvadeType == invadeType)
                    {
                        cmbInvasionSelect.SelectedItem = item;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    DSRInvasion item = new DSRInvasion("Unknown", invadeType);
                    cmbInvasionSelect.Items.Add(item);
                    cmbInvasionSelect.SelectedItem = item;
                }
            }
        }
        private void updateAreaID(ComboBox cmbAreaID, CheckBox cbxFreezeAreaID, int areaID, Action<int> setHook)
        {
            DSRArea lastAreaID = cmbAreaID.SelectedItem as DSRArea;
            if (cbxFreezeAreaID.Checked)
            {
                //cmbAreaID.DropDownStyle = ComboBoxStyle.DropDown;
                if (lastAreaID != null)
                    setHook(lastAreaID.AreaID);

                /*
                else
                {
                    try
                    {
                        Hook.AreaID = Convert.ToInt32(lastAreaIDItem.ToString());
                    }
                    catch
                    {
                        cmbAreaID.SelectedItem = Hook.AreaID;
                    }
                }
                */
            }
            else
            {
                //cmbAreaID.DropDownStyle = ComboBoxStyle.DropDownList;
                if (!cmbAreaID.DroppedDown && lastAreaID.AreaID != areaID)
                {
                    bool found = false;
                    foreach (DSRArea item in cmbAreaID.Items)
                    {
                        if (item.AreaID == areaID)
                        {
                            cmbAreaID.SelectedItem = item;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        DSRArea item = new DSRArea(areaID.ToString(), areaID);
                        cmbAreaID.Items.Add(item);
                        cmbAreaID.SelectedItem = item;
                    }
                }
            }
        }
    }
}
