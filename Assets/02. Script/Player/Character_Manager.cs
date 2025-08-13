public class Character_Manager : Singleton<Character_Manager>
{
    private Player player;

    public Player Player
    {
        get { return player; } 
        set {  player = value; }
    }
}
