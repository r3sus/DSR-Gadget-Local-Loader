using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSR_Gadget.SubForms
{
    public partial class FamilyShareInputForm : Form
    {
        //private static Regex accessoryEntryRx = new Regex(@"^\s*(?<ID>\S+)\s+(?<limit>\S+)\s+(?<upgrade>\S+)\s+(?<Name>.+)$");
        private static Regex steamIDLinkRX = new Regex(@"^.*" + Regex.Escape("steamcommunity.com/") + "(?<Type>[^/]+)/(?<ID>[^/]+).*$");
        private static Regex steamID64 = new Regex(@"^.*(?<ID>[0-9]{17}).*$");

        public FamilyShareInputForm()
        {
            InitializeComponent();
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            txtSteamProfileUrl.ReadOnly = true;
            Match match = steamID64.Match(txtSteamProfileUrl.Text);
            long id = -1;
            if (!match.Success)
            {
                match = steamIDLinkRX.Match(txtSteamProfileUrl.Text);
                if (match.Success)
                {
                    string name = match.Groups["ID"].Value;
                    id = await SteamAPI.ResolveVanityUrl(name, "1");
                }
                else
                {
                    id = await SteamAPI.ResolveVanityUrl(txtSteamProfileUrl.Text, "1");
                }
            }
            else
                id = Convert.ToInt64(match.Groups["ID"].Value);

            if (id != -1)
            {
                FamilyShareForm familyShareForm = new FamilyShareForm();
                familyShareForm.SteamID = id;
                familyShareForm.StartPosition = FormStartPosition.CenterScreen;
                familyShareForm.Show();
                await familyShareForm.LoadFamilyShareInfo();
                Close();
            }
            else
            {
                lblError.Visible = true;
                txtSteamProfileUrl.ReadOnly = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
