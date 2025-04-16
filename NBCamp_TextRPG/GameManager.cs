using TRPG.UM;

namespace TRPG.GM;

public class GameManager
{
    static GameManager gm = new GameManager();

    bool isRunGame = false;
    public static void Main()
    {
        UIManager ui = new UIManager();
        UIManager.Instance.location = Location.Title;
        gm.RunGame();
    }

    void RunGame()
    {
        // Console.WriteLine($"Run / sm.location = {sm.location}");
        //ui.MapUI();

        while (!isRunGame)
        {
            Console.WriteLine("RunGame 루프 도는중...\n");
            Console.WriteLine($"GM / location 현재 장소 {UIManager.Instance.location}\n");
            UIManager.Instance.MapUI();
        }
    }
}