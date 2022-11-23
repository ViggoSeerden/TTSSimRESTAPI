using Newtonsoft.Json;
using System.Text.Json;
using TTSSimRESTAPI.GameClasses;

namespace TTSSimRESTAPI.ServiceClasses
{
    public class Persona
    {
        public static string FusePersona(string saveData, string json)
        {
            GameData? gameData = JsonConvert.DeserializeObject<GameData>(saveData);
            JsonData? jsonData = JsonConvert.DeserializeObject<JsonData>(json);

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

            return(JsonConvert.SerializeObject(gameData));
        }

        public static string SacrificePersona(string saveData, string json)
        {
            GameData? gameData = JsonConvert.DeserializeObject<GameData>(saveData);
            JsonData? jsonData = JsonConvert.DeserializeObject<JsonData>(json);

            return (JsonConvert.SerializeObject(gameData));
        }

        public static string RemoveSkills(string saveData, string json)
        {
            GameData? gameData = JsonConvert.DeserializeObject<GameData>(saveData);
            JsonData? jsonData = JsonConvert.DeserializeObject<JsonData>(json);

            foreach (DemonData demonData in gameData.demonData)
            {
                if (demonData.name == jsonData.persona)
                {
                    foreach (string skill in jsonData.skillNames)
                    {
                        demonData.skillNames.Remove(skill);
                    }
                    break;
                }
            }

            return (JsonConvert.SerializeObject(gameData));
        }


        private static void DoLevelUp(DemonData demonData)
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
