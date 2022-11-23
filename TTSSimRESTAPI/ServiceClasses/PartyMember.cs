using System.Text.Json;
using TTSSimRESTAPI.GameClasses;

namespace TTSSimRESTAPI.ServiceClasses
{
    public class PartyMember
    {
        public static string UpdateParty(string saveData, string party)
        {
            GameData? gameData = JsonSerializer.Deserialize<GameData>(saveData);
            JsonData? jsonData = JsonSerializer.Deserialize<JsonData>(party);

            return(JsonSerializer.Serialize(gameData));
        }

        public static string UpdateEquipment(string saveData, string json)
        {
            GameData? gameData = JsonSerializer.Deserialize<GameData>(saveData);
            JsonData? jsonData = JsonSerializer.Deserialize<JsonData>(json);

            return(JsonSerializer.Serialize(gameData));
        }
    }
}
