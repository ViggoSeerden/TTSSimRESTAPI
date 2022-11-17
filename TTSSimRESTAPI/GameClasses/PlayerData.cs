namespace TTSSimRESTAPI.GameClasses
{
    [Serializable]
    public class PlayerData
    {
        public string name;
        public int HP;
        public int MaxHP;
        public int SP;
        public int MaxSP;
        public int XP;
        public int XPToNextLevel;
        public int level;
        public int yen;

        public int maxFloorsClimbed;
        public bool clearedTartarus;
        public bool clearedAbyss;
        public bool clearedBonus;

        public PlayerData(string name)
        {
            this.name = name;
            HP = 84;
            MaxHP = 84;
            SP = 26;
            MaxSP = 26;
            XP = 0;
            XPToNextLevel = 100;
            level = 1;
            yen = 1000;
            maxFloorsClimbed = 0;
            clearedTartarus = false;
            clearedAbyss = false;
            clearedBonus = false;
        }
    }
}
