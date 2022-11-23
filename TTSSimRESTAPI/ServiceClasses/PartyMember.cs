using Newtonsoft.Json;
using System.Text.Json;
using TTSSimRESTAPI.GameClasses;

namespace TTSSimRESTAPI.ServiceClasses
{
    public class PartyMember
    {
        public static string UpdateParty(string saveData, string party)
        {
            GameData? gameData = JsonConvert.DeserializeObject<GameData>(saveData);
            JsonData? jsonData = JsonConvert.DeserializeObject<JsonData>(party);

            return(JsonConvert.SerializeObject(gameData));
        }

        public static string UpdateEquipment(string saveData, string json)
        {
            GameData? gameData = JsonConvert.DeserializeObject<GameData>(saveData);
            JsonData? jsonData = JsonConvert.DeserializeObject<JsonData>(json);

            return(JsonConvert.SerializeObject(gameData));
        }
    }
}
