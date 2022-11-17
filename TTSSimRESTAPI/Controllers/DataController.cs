using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using TTSSimRESTAPI.Data;
using TTSSimRESTAPI.GameClasses;

namespace TTSSimRESTAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private APIContext context;
        public DataController(APIContext context)
        {
            this.context = context;
        }

        [HttpPatch]
        public JsonResult BuyItems(string saveData, string items)
        {
            GameData? gameData = JsonSerializer.Deserialize<GameData>(saveData);
            JsonData? jsonData = JsonSerializer.Deserialize<JsonData>(items);

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

            string editedSaveData = JsonSerializer.Serialize(gameData);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult SellItems(string saveData, string items)
        {
            GameData? gameData = JsonSerializer.Deserialize<GameData>(saveData);
            JsonData? jsonData = JsonSerializer.Deserialize<JsonData>(items);

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

            string editedSaveData = JsonSerializer.Serialize(gameData);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult UpdateParty(string saveData, string party)
        {
            GameData? gameData = JsonSerializer.Deserialize<GameData>(saveData);
            JsonData? jsonData = JsonSerializer.Deserialize<JsonData>(party);

            gameData.currentMemberData = jsonData.partyMembers;

            string editedSaveData = JsonSerializer.Serialize(gameData);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult UpdateEquipment(string saveData, string json)
        {
            GameData? gameData = JsonSerializer.Deserialize<GameData>(saveData);
            JsonData? jsonData = JsonSerializer.Deserialize<JsonData>(json);

            string editedSaveData = JsonSerializer.Serialize(gameData);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult FusePersona(string saveData, string json)
        {
            GameData? gameData = JsonSerializer.Deserialize<GameData>(saveData);
            JsonData? jsonData = JsonSerializer.Deserialize<JsonData>(json);

            foreach (DemonData demonData in gameData.demonData)
            {
                foreach (DemonData demonData2 in gameData.demonData)
                {
                    if (demonData.name == jsonData.persona && demonData2.name == jsonData.fusedPersona)
                    {
                        Random rd = new Random();
                        if (rd.Next(5, 100) < 10)
                        {
                            double xp = (1000 * demonData.level) * (rd.Next(9, 10) / 10) * 1.25;

                            if (demonData.arcana == demonData2.arcana)
                            {
                                xp *= 1.5;
                            }

                            demonData2.XP += (int)xp;

                            if (demonData2.XP > demonData2.XPToNextLevel)
                            {
                                DoLevelUp(demonData2);
                            }

                            gameData.demonData.Remove(demonData);
                        }
                        else
                        {
                            double xp = (1000 * demonData2.level) * (rd.Next(9, 10) / 10);

                            if (demonData.arcana == demonData2.arcana)
                            {
                                xp *= 1.5;
                            }

                            demonData.XP += (int)xp;

                            if (demonData.XP > demonData.XPToNextLevel)
                            {
                                DoLevelUp(demonData);
                            }

                            for (int i = 0; i < jsonData.skillNames.Count; i++)
                            {
                                demonData.skillNames.Add(jsonData.skillNames[i]);
                            }

                            gameData.demonData.Remove(demonData2);
                        }
                        break;
                    }
                } 
            }

            string editedSaveData = JsonSerializer.Serialize(gameData);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult SacrificePersona(string saveData, string json)
        {
            GameData? gameData = JsonSerializer.Deserialize<GameData>(saveData);
            JsonData? jsonData = JsonSerializer.Deserialize<JsonData>(json);

            string editedSaveData = JsonSerializer.Serialize(gameData);

            return new JsonResult(Ok(editedSaveData));
        }

        [HttpPatch]
        public JsonResult RemoveSkills(string saveData, string json)
        {
            GameData? gameData = JsonSerializer.Deserialize<GameData>(saveData);
            JsonData? jsonData = JsonSerializer.Deserialize<JsonData>(json);

            foreach (DemonData demonData in gameData.demonData)
            {
                if (demonData.name == jsonData.persona)
                {
                    foreach(string skill in jsonData.skillNames)
                    {
                        demonData.skillNames.Remove(skill);
                    }
                    break;
                }
            }

            string editedSaveData = JsonSerializer.Serialize(gameData);

            return new JsonResult(Ok(editedSaveData));
        }


        private void DoLevelUp(DemonData demonData)
        {
            Random rd = new Random();
            int levelUpAmount = 0;
            while (demonData.XP >= demonData.XPToNextLevel)
            {
                levelUpAmount++;
                demonData.XP -= demonData.XPToNextLevel;
                demonData.level++;
                demonData.totalLevelUps++;
                demonData.XPToNextLevel = (100 * demonData.level + 10 * demonData.level) * (demonData.level / 10 + 1) * (demonData.totalLevelUps / 10 + 1) + rd.Next(21, 99);

                if (demonData.levelUpSkillLevels.Contains(demonData.level))
                {
                    demonData.skillNames.Add(demonData.levelUpSkillNames[demonData.levelUpSkillLevels.IndexOf(demonData.level)]);
                }

                bool strength = false;
                bool magic = false;
                bool endurance = false;
                bool agility = false;
                bool luck = false;

                int i = 0;
                int r = rd.Next(2, 4);

                while (i < r)
                {
                    int stat = rd.Next(0, 5);
                    if (stat == 0 && !strength)
                    {
                        demonData.Strength += rd.Next(1, 4);
                        strength = true;
                        i++;
                    }
                    if (stat == 1 && !magic)
                    {
                        demonData.Magic += rd.Next(1, 4);
                        magic = true;
                        i++;
                    }
                    if (stat == 2 && !endurance)
                    {
                        demonData.Endurance += rd.Next(1, 4);
                        endurance = true;
                        i++;
                    }
                    if (stat == 3 && !agility)
                    {
                        demonData.Agility += rd.Next(1, 4);
                        agility = true;
                        i++;
                    }
                    if (stat == 4 && !luck)
                    {
                        demonData.Luck += rd.Next(1, 4);
                        luck = true;
                        i++;
                    }
                }
            }
        }
    }
}