using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private void initCheats() 
        {
            Timer.Elapsed += RefillHP; // Timer elapsed method to call when triggered
            Timer.AutoReset = false; // Do not reset the timer when it is finished
            nudHealInterval.Value = settings.HealInterval;
        }

        private void saveCheats() 
        {
            nudHealInterval.Value = settings.HealInterval;
        }

        private void resetCheats()
        {
            if (cbxAllNoMagicQty.Checked)
                Hook.AllNoMagicQty = false;
            if (cbxPlayerNoDead.Checked)
                Hook.PlayerNoDead = false;
            if (cbxPlayerExterminate.Checked)
                Hook.PlayerExterminate = false;
            if (cbxAllNoStamina.Checked)
                Hook.AllNoStamina = false;
            if (cbxAllNoArrow.Checked)
                Hook.AllNoArrow = false;
            if (cbxPlayerHide.Checked)
                Hook.PlayerHide = false;
            if (cbxPlayerSilence.Checked)
                Hook.PlayerSilence = false;
            if (cbxAllNoDead.Checked)
                Hook.AllNoDead = false;
            if (cbxAllNoDamage.Checked)
                Hook.AllNoDamage = false;
            if (cbxAllNoHit.Checked)
                Hook.AllNoHit = false;
            if (cbxAllNoAttack.Checked)
                Hook.AllNoAttack = false;
            if (cbxAllNoMove.Checked)
                Hook.AllNoMove = false;
            if (cbxAllNoUpdateAI.Checked)
                Hook.AllNoUpdateAI = false;
            if (cbxInfDurability.Checked)
                Hook.Durability = false;
            if (cbxInfDurabilitySpecial.Checked)
                Hook.DurabilitySpecial = false;

            if (loaded)
            {
                if (cbxPlayerDeadMode.Checked)
                    Hook.PlayerDeadMode = false;
                if (cbxPlayerDisableDamage.Checked)
                    Hook.PlayerDisableDamage = false;
                if (cbxPlayerNoHit.Checked)
                    Hook.PlayerNoHit = false;
                if (cbxPlayerNoStamina.Checked)
                    Hook.PlayerNoStamina = false;
                if (cbxPlayerSuperArmor.Checked)
                    Hook.PlayerSuperArmor = false;
                if (cbxPlayerNoGoods.Checked)
                    Hook.PlayerNoGoods = false;
            }
        }

        private void reloadCheats()
        {
            if (cbxPlayerDeadMode.Checked)
                Hook.PlayerDeadMode = true;
            if (cbxPlayerDisableDamage.Checked)
                Hook.PlayerDisableDamage = true;
            if (cbxPlayerNoHit.Checked)
                Hook.PlayerNoHit = true;
            if (cbxPlayerNoStamina.Checked)
                Hook.PlayerNoStamina = true;
            if (cbxPlayerSuperArmor.Checked)
                Hook.PlayerSuperArmor = true;
            if (cbxPlayerNoGoods.Checked)
                Hook.PlayerNoGoods = true;
            if (cbxAllNoMagicQty.Checked)
                Hook.AllNoMagicQty = true;
            if (cbxPlayerNoDead.Checked)
                Hook.PlayerNoDead = true;
            if (cbxPlayerExterminate.Checked)
                Hook.PlayerExterminate = true;
            if (cbxAllNoStamina.Checked)
                Hook.AllNoStamina = true;
            if (cbxAllNoArrow.Checked)
                Hook.AllNoArrow = true;
            if (cbxPlayerHide.Checked)
                Hook.PlayerHide = true;
            if (cbxPlayerSilence.Checked)
                Hook.PlayerSilence = true;
            if (cbxAllNoDead.Checked)
                Hook.AllNoDead = true;
            if (cbxAllNoDamage.Checked)
                Hook.AllNoDamage = true;
            if (cbxAllNoHit.Checked)
                Hook.AllNoHit = true;
            if (cbxAllNoAttack.Checked)
                Hook.AllNoAttack = true;
            if (cbxAllNoMove.Checked)
                Hook.AllNoMove = true;
            if (cbxAllNoUpdateAI.Checked)
                Hook.AllNoUpdateAI = true;
            if (cbxInfDurability.Checked)
                Hook.Durability = true;
            if (cbxInfDurabilitySpecial.Checked)
                Hook.DurabilitySpecial = true;
        }

        System.Timers.Timer Timer = new System.Timers.Timer();
        private void updateCheats()
        {
            if (cbxPlayerDeadMode.Checked && !Hook.PlayerDeadMode)
                Hook.PlayerDeadMode = true;

            // Only start refill timer if enabled, health is lower than max and the timer isn't already going
            if (cbxRefill.Checked && (Player.Hp < Player.MaxHp) && !Timer.Enabled)
            {
                _ = Task.Run(() => RefillTimer());
            }
        }

        private void RefillTimer()
        {
            double time = (double)nudHealInterval.Value;

            //Set interval in ms, record hp and start the timer
            Timer.Interval = time * 1000;
            var hp = Player.Hp;
            Timer.Start();

            while (Timer.Enabled)
            {
                // If the recorded hp variable is over Hook.Health, set the timer interval again (resetting it) and set the recorded hp value
                if (hp > Player.Hp)
                {
                    Timer.Interval = time * 1000;
                    hp = Player.Hp;
                }
            }
        }

        private void RefillHP(object sender, ElapsedEventArgs e)
        {
            Player.Hp = Player.MaxHp;
        }

        private void cbxPlayerDeadMode_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                Hook.PlayerDeadMode = cbxPlayerDeadMode.Checked;
        }

        private void cbxPlayerNoDead_CheckedChanged(object sender, EventArgs e)
        {
            Hook.PlayerNoDead = cbxPlayerNoDead.Checked;
        }

        private void cbxPlayerDisableDamage_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                Hook.PlayerDisableDamage = cbxPlayerDisableDamage.Checked;
        }

        private void cbxPlayerNoHit_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                Hook.PlayerNoHit = cbxPlayerNoHit.Checked;
        }

        private void cbxPlayerNoStamina_CheckedChanged(object sender, EventArgs e)
        {
            Hook.PlayerNoStamina = cbxPlayerNoStamina.Checked;
        }

        private void cbxPlayerSuperArmor_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                Hook.PlayerSuperArmor = cbxPlayerSuperArmor.Checked;
        }

        private void cbxPlayerHide_CheckedChanged(object sender, EventArgs e)
        {
            Hook.PlayerHide = cbxPlayerHide.Checked;
        }

        private void cbxPlayerSilence_CheckedChanged(object sender, EventArgs e)
        {
            Hook.PlayerSilence = cbxPlayerSilence.Checked;
        }

        private void cbxPlayerExterminate_CheckedChanged(object sender, EventArgs e)
        {
            Hook.PlayerExterminate = cbxPlayerExterminate.Checked;
        }

        private void cbxPlayerNoGoods_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
                Hook.PlayerNoGoods = cbxPlayerNoGoods.Checked;
        }

        private void cbxAllNoArrow_CheckedChanged(object sender, EventArgs e)
        {
            Hook.AllNoArrow = cbxAllNoArrow.Checked;
        }

        private void cbxAllNoMagicQty_CheckedChanged(object sender, EventArgs e)
        {
            Hook.AllNoMagicQty = cbxAllNoMagicQty.Checked;
        }

        private void cbxAllNoDead_CheckedChanged(object sender, EventArgs e)
        {
            Hook.AllNoDead = cbxAllNoDead.Checked;
        }

        private void cbxAllNoDamage_CheckedChanged(object sender, EventArgs e)
        {
            Hook.AllNoDamage = cbxAllNoDamage.Checked;
        }

        private void cbxAllNoHit_CheckedChanged(object sender, EventArgs e)
        {
            Hook.AllNoHit = cbxAllNoHit.Checked;
        }

        private void cbxAllNoStamina_CheckedChanged(object sender, EventArgs e)
        {
            Hook.AllNoStamina = cbxAllNoStamina.Checked;
        }

        private void cbxAllNoAttack_CheckedChanged(object sender, EventArgs e)
        {
            Hook.AllNoAttack = cbxAllNoAttack.Checked;
        }

        private void cbxAllNoMove_CheckedChanged(object sender, EventArgs e)
        {
            Hook.AllNoMove = cbxAllNoMove.Checked;
        }

        private void cbxAllNoUpdateAI_CheckedChanged(object sender, EventArgs e)
        {
            Hook.AllNoUpdateAI = cbxAllNoUpdateAI.Checked;
        }

        private void cbxInfDurability_CheckedChanged(object sender, EventArgs e)
        {
            Hook.Durability = cbxInfDurability.Checked;
        }

        private void cbxInfDurabilitySpecial_CheckedChanged(object sender, EventArgs e)
        {
            Hook.DurabilitySpecial = cbxInfDurabilitySpecial.Checked;
        }
    }
}
