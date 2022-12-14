using Newtonsoft.Json;
using System.Text.Json;
using TTSSimRESTAPI.GameClasses;

namespace TTSSimRESTAPI.ServiceClasses
{
    public class Shop
    {
        public static string BuyItems(string saveData, string items)
        {
            GameData? gameData = JsonConvert.DeserializeObject<GameData>(saveData);
            JsonData? jsonData = JsonConvert.DeserializeObject<JsonData>(items);

            bool exists = false;
            foreach (ItemData itemdata in gameData.itemData)
            {
                if (itemdata.name == jsonData.itemName)
                {
                    itemdata.amount += jsonData.amount;
                    gameData.playerData.yen -= jsonData.price * jsonData.amount;
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                gameData.itemData.Add(new ItemData()
                {
                    name = jsonData.itemName,
                    amount = jsonData.amount,
                });
                gameData.playerData.yen -= jsonData.price * jsonData.amount;
            }

            return(JsonConvert.SerializeObject(gameData));
        }

        public static string SellItems(string saveData, string items)
        {
            GameData? gameData = JsonConvert.DeserializeObject<GameData>(saveData);
            JsonData? jsonData = JsonConvert.DeserializeObject<JsonData>(items);

            foreach (ItemData itemdata in gameData.itemData.ToArray())
            {
                if (itemdata.name == jsonData.itemName)
                {
                    itemdata.amount -= jsonData.amount;
                    gameData.playerData.yen += jsonData.price * jsonData.amount;

                    if (itemdata.amount < 1)
                    {
                        gameData.itemData.Remove(itemdata);
                    }

                    break;
                }
            }

            return(JsonConvert.SerializeObject(gameData));
        }
    }
}
