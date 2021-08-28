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
    public partial class FamilyShareForm : Form
    {
        public long SteamID { get; set; }
        public long ShareSteamID { get; set; }
        private List<Control> SharedComponents;
        private List<Control> SharedFromComponents;

        public FamilyShareForm()
        {
            InitializeComponent();
        }

        public async Task LoadFamilyShareInfo()
        {
            txtSteamID.Text = SteamID.ToString();
            ShareSteamID = await SteamAPI.IsPlayingSharedGame(SteamID);
            Dictionary<long, SteamAPI.SteamUserInfo> userInfo;
            bool visible;
            if (ShareSteamID != -1)
            {
                if (ShareSteamID > 0)
                {
                    txtShareSteamID.Text = ShareSteamID.ToString();

                    userInfo = await SteamAPI.GetPlayerSummaries(new[] { SteamID, ShareSteamID });
                    visible = true;
                    lblShared.Text = "Shared";
                }
                else
                {
                    userInfo = await SteamAPI.GetPlayerSummaries(new[] { SteamID });
                    visible = false;
                    lblShared.Text = "Not Shared or Not In-Game";
                }

                SteamAPI.SteamUserInfo steamUser;
                if (userInfo.TryGetValue(SteamID, out steamUser))
                {
                    txtSteamName.Text = steamUser.SteamName;
                    txtSteamProfileURL.Text = steamUser.ProfileURL;
                    pbxSteamAvatar.LoadAsync(steamUser.AvatarURL);
                }
                if (userInfo.TryGetValue(ShareSteamID, out steamUser))
                {
                    txtShareSteamName.Text = steamUser.SteamName;
                    txtShareSteamProfileURL.Text = steamUser.ProfileURL;
                    pbxShareSteamAvatar.LoadAsync(steamUser.AvatarURL);
                }

                foreach (Control control in SharedFromComponents)
                {
                    control.Visible = visible;
                }

                foreach (Control control in SharedComponents)
                {
                    control.Visible = true;
                }
            }
            else
            {
                lblFailed.Visible = true;
            }

        }

        private void FamilyShareForm_Load(object sender, EventArgs e)
        {
            SharedComponents = new List<Control>
            {
                lblShared, lblSteamName, lblSteamID, txtSteamName, txtSteamID,
                txtSteamProfileURL, pbxSteamAvatar, btnSteamVisitProfile,
            };

            SharedFromComponents = new List<Control>
            {
                lblSharedFrom, lblShareSteamName, lblShareSteamID, txtShareSteamName, txtShareSteamID,
                txtShareSteamProfileURL, pbxShareSteamAvatar, btnShareSteamVisitProfile,
            };
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSteamVisitProfile_Click(object sender, EventArgs e)
        {
            Util.Util.OpenUrl(txtSteamProfileURL.Text);
        }

        private void btnShareSteamVisitProfile_Click(object sender, EventArgs e)
        {
            Util.Util.OpenUrl(txtShareSteamProfileURL.Text);
        }
    }
}
