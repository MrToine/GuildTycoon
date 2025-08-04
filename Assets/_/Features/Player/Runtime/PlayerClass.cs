namespace Player.Runtime
{
    public class PlayerClass
    {
        public string GuildName
        {
            get { return _guildName; }
            set { _guildName = value; }
        }

        public int GuildLevel
        {
            get { return _guildLevel; }
            set { _guildLevel = value; }
        }

        public int Money
        {
            get { return _money; }
            set { _money = value; }
        }

        public int AdventurersMax
        {
            get { return _adventurersMax; }
            set { _adventurersMax = value; }
        }

        public int AdventurersCount
        {
            get { return _adventurersCount; }
            set { _adventurersCount = value; }
        }
        
        string _guildName;
        int _guildLevel;
        int _money;
        int _adventurersMax;
        int _adventurersCount;
     
        public PlayerClass(string guildName, int guildLevel, int money, int adventurersMax)
        {
            _guildName = guildName.Trim();
            _guildLevel = guildLevel;
            _money = money;
            _adventurersMax = adventurersMax;
        }
    }
}
