namespace TTSSimRESTAPI.GameClasses
{
    [System.Serializable]
    public class GameData
    {
        public PlayerData playerData;
        public List<ItemData> itemData;
        public List<DemonData> demonData;
        public List<PartyMemberData> partyMemberData;
        public List<string> currentMemberData;

        public GameData(string name)
        {
            playerData = new PlayerData(name);
            itemData = new List<ItemData>();
            demonData = new List<DemonData>();
            partyMemberData = new List<PartyMemberData>();
            currentMemberData = new List<string>();
        }

        public GameData() { }
    }
}
