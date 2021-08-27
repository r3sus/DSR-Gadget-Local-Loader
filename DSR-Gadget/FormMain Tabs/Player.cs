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
        private DSRPlayer Player;

        private void initPlayer()
        {
            cbxRestoreState.Checked = settings.RestoreState;

            foreach (DSRBonfire bonfire in DSRBonfire.All)
                cmbBonfire.Items.Add(bonfire);
            cmbBonfire.SelectedIndex = 0;
            cmbBonfire.SelectedIndexChanged += cmbBonfire_SelectedIndexChanged;

            foreach (DSRTeam team in DSRTeam.All)
                cmbChrSelect.Items.Add(team);
            cmbChrSelect.SelectedIndex = 0;
            cmbChrSelect.SelectedIndexChanged += cmbChrSelect_SelectedIndexChanged;

            foreach (DSRInvasion invasion in DSRInvasion.All)
                cmbInvasionSelect.Items.Add(invasion);
            cmbInvasionSelect.SelectedIndex = 0;
            cmbInvasionSelect.SelectedIndexChanged += cmbInvasionSelect_SelectedIndexChanged;

            foreach (DSRArea area in DSRArea.All)
            {
                cmbMPAreaID.Items.Add(area);
                cmbAreaID.Items.Add(area);
            }
            cmbMPAreaID.SelectedIndex = 0;
            cmbAreaID.SelectedIndex = 0;
            cmbMPAreaID.SelectedIndexChanged += cmbMPAreaID_SelectedIndexChanged;
            cmbAreaID.SelectedIndexChanged += cmbAreaID_SelectedIndexChanged;

            Player = Hook.GetPlayer();

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
                    Player.AnimSpeed = 1;
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
            nudHealth.Value = Player.Hp;
            nudHealthMax.Value = Player.MaxHp;
            nudStamina.Value = Player.Stamina;
            nudStaminaMax.Value = Player.MaxStamina;

            if (cbxFreezeChrType.Checked)
                Player.ChrType = (int)nudChrType.Value;
            else
                nudChrType.Value = Player.ChrType;

            if (cbxFreezeTeamType.Checked)
                Player.TeamType = (int)nudTeamType.Value;
            else
                nudTeamType.Value = Player.TeamType;

            if (cbxFreezeInvadeType.Checked)
                Player.InvadeType = (byte)nudInvadeType.Value;
            else
                nudInvadeType.Value = Player.InvadeType;

            try
            {
                DSRPlayer.Position pos = Player.GetPosition();
                nudPosX.Value = (decimal)pos.X;
                nudPosY.Value = (decimal)pos.Y;
                nudPosZ.Value = (decimal)pos.Z;
                nudPosAngle.Value = angleToDegree(pos.Angle);

                pos = Hook.GetStablePosition();
                nudStableX.Value = (decimal)pos.X;
                nudStableY.Value = (decimal)pos.Y;
                nudStableZ.Value = (decimal)pos.Z;
                nudStableAngle.Value = angleToDegree(pos.Angle);
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
                Player.AnimSpeed = (float)nudSpeed.Value;
   
            updateDropdown<DSRBonfire>(cmbBonfire, Hook.LastBonfire);
            updateDropdown<DSRInvasion>(cmbInvasionSelect, Player.InvadeType);

            updateTeam(cmbChrSelect, Player.ChrType, Player.TeamType);

 

            updateAreaID(cmbMPAreaID, cbxFreezeMPAreaID, Player.MPAreaID, value => Player.MPAreaID = value);
            updateAreaID(cmbAreaID, cbxFreezeAreaID, Player.AreaID, value => Player.AreaID = value);

        }

        private void nudHealth_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.Hp = (int)nudHealth.Value;
        }

        private void nudStamina_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.Stamina = (int)nudStamina.Value;
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
                Player.ChrType = (int)nudChrType.Value;
        }

        private void nudTeamType_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.TeamType = (int)nudTeamType.Value;
        }

        private void nudInvadeType_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Player.InvadeType = (byte)nudInvadeType.Value;
        }

        private void cmbChrSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRTeam item = cmbChrSelect.SelectedItem as DSRTeam;
                Player.ChrType = item.ChrType;
                Player.TeamType = item.TeamType;
            }
        }

        private void cmbInvasionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRInvasion item = cmbInvasionSelect.SelectedItem as DSRInvasion;
                Player.InvadeType = (byte)item.ID;
            }
        }

        private void cmbMPAreaID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRArea item = cmbMPAreaID.SelectedItem as DSRArea;
                Player.MPAreaID = item.AreaID;
            }
        }

        private void cmbAreaID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
            {
                DSRArea item = cmbAreaID.SelectedItem as DSRArea;
                Player.AreaID = item.AreaID;
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
            DSRPlayer.Position pos;
            pos.X = (float)nudStoredX.Value;
            pos.Y = (float)nudStoredY.Value;
            pos.Z = (float)nudStoredZ.Value;
            pos.Angle = degreeToAngle(nudStoredAngle.Value);
            Player.PosWarp(pos);

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
                Player.PosWarp(Hook.GetLastBloodstainPosition());
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
                Player.PosWarp(Hook.GetInitialPosition());
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
                Player.AnimSpeed = 1;
        }

        private decimal angleToDegree(float angle)
        {
            return (decimal)((angle + Math.PI) / (Math.PI * 2) * 360);
        }

        private float degreeToAngle(decimal degree)
        {
            return (float)((double)degree / 360 * (Math.PI * 2) - Math.PI);
        }

        private void updateTeam(ComboBox cmbChrSelect, int chrType, int teamType)
        {
            // TODO: fix changing team when nud is frozen
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

        private void updateAreaID(ComboBox cmbAreaID, CheckBox cbxFreezeAreaID, int areaID, Action<int> setHook)
        {
            DSRArea lastAreaID = cmbAreaID.SelectedItem as DSRArea;
            if (cbxFreezeAreaID.Checked)
            {
                if (lastAreaID != null)
                    setHook(lastAreaID.AreaID);
            }
            else
            {
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
