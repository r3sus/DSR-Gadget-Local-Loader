using DSR_Gadget.List_Items;
using PropertyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSR_Gadget.SubForms
{
    internal partial class EnemyInsForm : Form
    {
        private DSRHook Hook;
        private bool loaded;
        private bool reading;
        private PHPointer EnemyInsPtr;
        private DSREnemy Enemy;

        public EnemyInsForm()
        {
            InitializeComponent();
        }

        // TODO: fix accesibility 
        public void InitForm(DSRHook hook, PHPointer enemyInsPtr, string title)
        {
            Text = title;
            Hook = hook;
            EnemyInsPtr = enemyInsPtr;
            Enemy = new DSREnemy(enemyInsPtr, hook);
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (Hook.Hooked)
            {
                if (Hook.Loaded)
                {
                    if (!loaded)
                    {
                        loaded = true;
                    }
                    else
                    {
                        reading = true;
                        UpdateForm();
                        reading = false;
                    }
                }
                else if (loaded)
                {
                    loaded = false;
                }
            }
        }

        private void UpdateForm()
        {
            if (!Enemy.IsIntPtrZero)
            {
                if (cbxKill.Checked && Enemy.Hp > 0)
                    Enemy.Hp = 0;
                try
                {
                    nudHealth.Value = Enemy.Hp;
                    nudHealthMax.Value = Enemy.MaxHp;
                    nudStamina.Value = Enemy.Stamina;
                    nudStaminaMax.Value = Enemy.MaxStamina;

                    if (cbxFreezeChrType.Checked)
                        Enemy.ChrType = (int)nudChrType.Value;
                    else
                        nudChrType.Value = Enemy.ChrType;

                    if (cbxFreezeTeamType.Checked)
                        Enemy.TeamType = (int)nudTeamType.Value;
                    else
                        nudTeamType.Value = Enemy.TeamType;

                }
                catch (ArgumentOutOfRangeException)
                {

                }
            }
        }


        private void nudHealth_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Enemy.Hp = (int)nudHealth.Value;
        }

        private void nudStamina_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Enemy.Stamina = (int)nudStamina.Value;
        }

        private void nudChrType_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Enemy.ChrType = (int)nudChrType.Value;
        }

        private void nudTeamType_ValueChanged(object sender, EventArgs e)
        {
            if (loaded && !reading)
                Enemy.TeamType = (int)nudTeamType.Value;
        }
    }
}
