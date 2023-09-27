using System;


    [Serializable] 
    public class GameData
    {
        public int level;
        public int gold;
        public GameData()
        {
            level = 1;
            gold = 0;
        }
    }
