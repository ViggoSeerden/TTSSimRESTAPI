namespace TTSSimRESTAPI.GameClasses
{
    public enum Affinity
    {
        Physical,
        Fire,
        Wind,
        Ice,
        Electric,
        Psy,
        Nuke,
        Bless,
        Curse,
        Almighty,
        Heal,
        Ailment,
        Support,
        Passive,
        None
    }

    [System.Serializable]
    public class DemonData
    {
        public string name;
        public int arcana;
        public int level;
        public int XP;
        public int XPToNextLevel;
        public int totalLevelUps;

        public int Strength;
        public int Magic;
        public int Endurance;
        public int Agility;
        public int Luck;

        public List<string> skillNames;
        public List<string> levelUpSkillNames;
        public List<int> levelUpSkillLevels;
        public Affinity doesntInherit;
    }
}
