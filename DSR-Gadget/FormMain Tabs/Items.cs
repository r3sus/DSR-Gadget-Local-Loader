using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSR_Gadget
{
    public partial class FormMain : Form
    {
        private void initItems()
        {
            DSRItemCategory.GetItemCategories();
            foreach (DSRItemCategory category in DSRItemCategory.All)
                cmbCategory.Items.Add(category);
            cmbCategory.SelectedIndex = 0;
        }

        private void saveItems() { }

        private void resetItems() { }

        private void reloadItems() { }

        private void updateItems() { }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbxItems.Items.Clear();
            DSRItemCategory category = cmbCategory.SelectedItem as DSRItemCategory;
            foreach (DSRItem item in category.Items)
                lbxItems.Items.Add(item);
            lbxItems.SelectedIndex = 0;
        }

        private void cmbInfusion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSRInfusion infusion = cmbInfusion.SelectedItem as DSRInfusion;
            nudUpgrade.Maximum = infusion.MaxUpgrade;

            HandleMaxItemCheckbox();
        }

        private void lbxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSRItem item = lbxItems.SelectedItem as DSRItem;
            if (cbxRestrict.Checked)
            {
                if (item.StackLimit == 1)
                    nudQuantity.Enabled = false;
                else
                    nudQuantity.Enabled = true;
                nudQuantity.Maximum = item.StackLimit;
            }
            switch (item.UpgradeType)
            {
                case DSRItem.Upgrade.None:
                    cmbInfusion.Enabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.Enabled = false;
                    nudUpgrade.Maximum = 0;
                    break;
                case DSRItem.Upgrade.Unique:
                    cmbInfusion.Enabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.Maximum = 5;
                    nudUpgrade.Enabled = true;
                    break;
                case DSRItem.Upgrade.Armor:
                    cmbInfusion.Enabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.Maximum = 10;
                    nudUpgrade.Enabled = true;
                    break;
                case DSRItem.Upgrade.Infusable:
                    cmbInfusion.Items.Clear();
                    foreach (DSRInfusion infusion in DSRInfusion.All)
                        cmbInfusion.Items.Add(infusion);
                    cmbInfusion.SelectedIndex = 0;
                    cmbInfusion.Enabled = true;
                    nudUpgrade.Enabled = true;
                    break;
                case DSRItem.Upgrade.InfusableRestricted:
                    cmbInfusion.Items.Clear();
                    foreach (DSRInfusion infusion in DSRInfusion.All)
                        if (!infusion.Restricted)
                            cmbInfusion.Items.Add(infusion);
                    cmbInfusion.SelectedIndex = 0;
                    cmbInfusion.Enabled = true;
                    nudUpgrade.Enabled = true;
                    break;
                case DSRItem.Upgrade.PyroFlame:
                    cmbInfusion.Enabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.Maximum = 15;
                    nudUpgrade.Enabled = true;
                    break;
                case DSRItem.Upgrade.PyroFlameAscended:
                    cmbInfusion.Enabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.Maximum = 5;
                    nudUpgrade.Enabled = true;
                    break;
            }

            HandleMaxItemCheckbox();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (loaded)
                createItem();
        }

        private void lbxItems_DoubleClick(object sender, EventArgs e)
        {
            if (loaded)
                createItem();
        }

        private void createItem()
        {
            //Check if the button is enabled and the selected item isn't null
            if (btnCreate.Enabled && lbxItems.SelectedItem != null)
            {
                _ = ChangeColor(Color.DarkGray);
                DSRItem item = lbxItems.SelectedItem as DSRItem;
                int id = item.ID;
                if (item.UpgradeType == DSRItem.Upgrade.PyroFlame || item.UpgradeType == DSRItem.Upgrade.PyroFlameAscended)
                    id += (int)nudUpgrade.Value * 100;
                else
                    id += (int)nudUpgrade.Value;
                if (item.UpgradeType == DSRItem.Upgrade.Infusable || item.UpgradeType == DSRItem.Upgrade.InfusableRestricted)
                {
                    DSRInfusion infusion = cmbInfusion.SelectedItem as DSRInfusion;
                    id += infusion.Value;
                }
                Hook.GetItem(item.CategoryID, id, (int)nudQuantity.Value);
            }
        }

        private void cbxRestrict_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbxRestrict.Checked)
            {
                nudQuantity.Enabled = true;
                nudQuantity.Maximum = Int32.MaxValue;
            }
            else if (lbxItems.SelectedIndex != -1)
            {
                DSRItem item = lbxItems.SelectedItem as DSRItem;
                nudQuantity.Maximum = item.StackLimit;
                if (item.StackLimit == 1)
                    nudQuantity.Enabled = false;
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            FilterItems();
        }

        //Clear items and add the ones that match text in search box
        private void FilterItems()
        {

            lbxItems.Items.Clear();

            if (SearchAllCheckbox.Checked && searchBox.Text != "")
            {
                //search every item category
                foreach (DSRItemCategory category in cmbCategory.Items)
                {
                    foreach (DSRItem item in category.Items)
                    {
                        if (item.ToString().ToLower().Contains(searchBox.Text.ToLower()))
                            lbxItems.Items.Add(item);
                    }
                }
            }
            else
            {
                //only search selected item category
                DSRItemCategory category = cmbCategory.SelectedItem as DSRItemCategory;
                foreach (DSRItem item in category.Items)
                {
                    if (item.ToString().ToLower().Contains(searchBox.Text.ToLower()))
                        lbxItems.Items.Add(item);
                }
            }

            if (lbxItems.Items.Count > 0)
                lbxItems.SelectedIndex = 0;

            HandleSearchLabel();
        }

        private void HandleSearchLabel()
        {
            if (searchBox.Text == "")
                lblSearch.Visible = true;
            else
                lblSearch.Visible = false;
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                searchBox.Clear();
                return;
            }

            //Create selected index as item
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; //Do not pass keypress along
                createItem();
                return;
            }

            //Return if sender is cmbInfusion so that arrow keys are handled correctly
            if (sender == cmbInfusion)
                return;
            //Prevents up and down keys from moving the cursor left and right when nothing in item box
            if (lbxItems.Items.Count == 0)
            {
                if (e.KeyCode == Keys.Up)
                    e.Handled = true; //Do not pass keypress along
                if (e.KeyCode == Keys.Down)
                    e.Handled = true; //Do not pass keypress along
                return;
            }

            ScrollListbox(e);
        }

        //Changes the color of the Apply button
        private async Task ChangeColor(Color new_color)
        {
            btnCreate.BackColor = new_color;

            await Task.Delay(TimeSpan.FromSeconds(.25));

            btnCreate.BackColor = default(Color);
        }

        //handles up and down scrolling
        private void ScrollListbox(KeyEventArgs e)
        {
            //Scroll down through Items listbox and go back to bottom at end
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;//Do not pass keypress along
                //Check is there's still items to go through
                if (lbxItems.SelectedIndex > 0)
                {
                    lbxItems.SelectedIndex -= 1;
                    return;
                }

                //Check if last item or "over" for safety
                if (lbxItems.SelectedIndex <= 0)
                {
                    lbxItems.SelectedIndex = lbxItems.Items.Count - 1; //-1 because Selected Index is 0 based and Count isn't
                    return;
                }

                //One liner meme that does the exact same thing as the code above
                //lbxItems.SelectedIndex = ((lbxItems.SelectedIndex - 1) + lbxItems.Items.Count) % lbxItems.Items.Count;
                //return;
            }

            //Scroll down through Items listbox and go back to top at end
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;//Do not pass keypress along
                //Check is there's still items to go through
                if (lbxItems.SelectedIndex < lbxItems.Items.Count - 1) //-1 because Selected Index is 0 based and Count isn't
                {
                    lbxItems.SelectedIndex += 1;
                    return;
                }

                //Check if last item or "over" for safety
                if (lbxItems.SelectedIndex >= lbxItems.Items.Count - 1) //-1 because Selected Index is 0 based and Count isn't
                {
                    lbxItems.SelectedIndex = 0;
                    return;
                }

                //One liner meme that does the exact same thing as the code above
                //lbxItems.SelectedIndex = (lbxItems.SelectedIndex + 1) % lbxItems.Items.Count;
                //return;

            }


        }

        private void maxUpgrade_CheckedChanged(object sender, EventArgs e)
        {
            //HandleMaxItemCheckbox
            if (maxUpgrade.Checked)
            {
                nudUpgrade.Value = nudUpgrade.Maximum;
                nudQuantity.Value = nudQuantity.Maximum;
            }
            else
            {
                nudUpgrade.Value = nudUpgrade.Minimum;
                nudQuantity.Value = nudQuantity.Minimum;
            }
        }

        private void HandleMaxItemCheckbox()
        {
            //Set upgrade nud to max if max checkbox is ticked
            if (maxUpgrade.Checked)
            {
                nudUpgrade.Value = nudUpgrade.Maximum;
                nudQuantity.Value = nudQuantity.Maximum;
            }
        }

        //Give focus and select all
        private void searchBox_Click(object sender, EventArgs e)
        {
            searchBox.SelectAll();
            searchBox.Focus();
        }

        private void SearchAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //checkbox changed, refresh search filter (if searchBox is not empty)
            if (searchBox.Text != "")
                FilterItems();
        }

    }
}
