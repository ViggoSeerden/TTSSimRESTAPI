namespace TTSSimRESTAPI.GameClasses
{
    [System.Serializable]
    public class PartyMemberData
    {
        public string name;
        public int HP;
        public int MaxHP;
        public int SP;
        public int MaxSP;
        public int level;
        public int XP;
        public int XPToNextLevel;

        public int Strength;
        public int Magic;
        public int Endurance;
        public int Agility;
        public int Luck;

        public List<string> skillNames;
    }
}
