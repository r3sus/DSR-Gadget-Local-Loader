using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DSR_Gadget
{
    class SteamAPI
    {
        private static string steamAPI = "http://api.steampowered.com/";
        private static string steamFamilyShareApiURL = steamAPI + "IPlayerService/IsPlayingSharedGame/v1/";
        private static string steamUserInfoApiURL = steamAPI + "ISteamUser/GetPlayerSummaries/v2/";
        private static string steamResolveVanityUrlApi = steamAPI + "ISteamUser/ResolveVanityURL/v1/";
        private static string apiKey => Properties.Settings.Default.SteamAPIKey;
        private static string appID = "570940";
        private static string format = "json";

        private static HttpClient client = new HttpClient();
        public static async Task<long> IsPlayingSharedGame(long steamID)
        {
            string urlParameters =
                "?key=" + apiKey +
                "&steamid=" + steamID +
                "&appid_playing=" + appID +
                "&format=" + format;

            SteamResponseRoot response = await SteamAPIRequest(steamFamilyShareApiURL + urlParameters);

            long longResult = -1;
            bool success = false;
            try
            {
                success = long.TryParse(response.response.lender_steamid, out longResult);
            }
            catch (NullReferenceException)
            {
                success = false;
            }
            finally
            {
                if (!success)
                    longResult = -1;
            }

            return longResult;
        }

        public static async Task<Dictionary<long, SteamUserInfo>> GetPlayerSummaries(long[] steamIDs)
        {
            string steamIDsString = String.Join(",", steamIDs.Select(p => p.ToString()).ToArray());

            string urlParameters =
                "?key=" + apiKey +
                "&steamids=" + steamIDsString +
                "&format=" + format;

            SteamResponseRoot response = await SteamAPIRequest(steamUserInfoApiURL + urlParameters);

            if (response.Success && response.response.players.Count > 0)
            {
                Dictionary<long, SteamUserInfo> users = new Dictionary<long, SteamUserInfo>();

                foreach (SteamResponsePlayer player in response.response.players)
                {
                    long steamID;
                    if (!long.TryParse(player.steamid, out steamID))
                        steamID = -1;
                    string steamName = player.personaname;
                    string profileURl = player.profileurl;
                    string avatarURL = player.avatarfull;

                    users.Add(
                        steamID,
                        new SteamUserInfo
                        {
                            SteamID = steamID,
                            SteamName = steamName,
                            ProfileURL = profileURl,
                            AvatarURL = avatarURL,
                        });
                }
                return users;
            }
            else
                return null;
        }

        public static async Task<long> ResolveVanityUrl(string vanityUrl, string urlType)
        {
            string urlParameters =
                "?key=" + apiKey +
                "&vanityurl=" + vanityUrl +
                "&url_type=" + urlType +
                "&format=" + format;

            SteamResponseRoot response = await SteamAPIRequest(steamResolveVanityUrlApi + urlParameters);

            if (response.Success && response.response.success == 1)
            {
                long longResult = -1;
                bool success = false;
                try
                {
                    success = long.TryParse(response.response.steamid, out longResult);
                }
                catch (NullReferenceException)
                {
                    success = false;
                }
                finally
                {
                    if (!success)
                        longResult = -1;
                }
                return longResult;
            }
            else
                return -1;
        }

        private static async Task<SteamResponseRoot> SteamAPIRequest(string URI)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, URI);
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.SendAsync(request);

            SteamResponseRoot result;
            if (response.IsSuccessStatusCode)
            {
                string test = response.Content.ReadAsStringAsync().Result;
                var streamTask = response.Content.ReadAsStreamAsync();
                try
                {
                    result = await JsonSerializer.DeserializeAsync<SteamResponseRoot>(await streamTask);
                    result.Success = true;
                }
                catch (JsonException)
                {
                    result = new SteamResponseRoot(false);
                }

            }
            else
                result = new SteamResponseRoot(false);

            return result;
        }

        private class SteamResponseRoot
        {
            public SteamResponseResponse response { get; set; }
            public bool Success { get; set; }
            public SteamResponseRoot(bool success)
            {
                Success = success;
            }
        }

        private class SteamResponseResponse
        {
            public string lender_steamid { get; set; }
            public List<SteamResponsePlayer> players { get; set; }
            public string steamid { get; set; }
            public int success { get; set; }
        }

        private class SteamResponsePlayer
        {
            public string steamid { get; set; }
            public int communityvisibilitystate { get; set; }
            public int profilestate { get; set; }
            public string personaname { get; set; }
            public int lastlogoff { get; set; }
            public string profileurl { get; set; }
            public string avatar { get; set; }
            public string avatarmedium { get; set; }
            public string avatarfull { get; set; }
        }

        public struct SteamUserInfo
        {
            public long SteamID { get; set; }
            public string SteamName { get; set; }
            public string ProfileURL { get; set; }
            public string AvatarURL { get; set; }
        }
    }
}
