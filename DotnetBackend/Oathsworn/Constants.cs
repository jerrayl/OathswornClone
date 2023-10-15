namespace Oathsworn
{
    public enum Might
    {
        White = 1,
        Yellow = 2,
        Red = 3,
        Black = 4
    }

    public enum BossPosition
    {
        Front,
        Back,
        Core,
        LeftFlank1,
        LeftFlank2,
        RightFlank1,
        RightFlank2
    }

    public enum Token
    {
        Battleflow,
        Redraw,
        Defence,
        Empower,
        Animus
    }

    public enum Class
    {
        Warbear,
        Exile,
        Cur,
        Priest,
        Harbinger,
        Blade,
        Witch,
        Ranger,
        Warden,
        Huntress,
        Penitent,
        GroveMaiden
    }

    public class Constants
    {
        // Game constants
        public const int EMPOWER_TOKEN_VALUE = 3;
        public const int ANIMUS_TOKEN_VALUE = 2;

        // SignalR Responses
        public const string SUCCESS = "SUCCESS";
    }
}
