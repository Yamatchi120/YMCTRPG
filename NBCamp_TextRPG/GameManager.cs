using TRPG.UM;

namespace TRPG.GM;

public class GameManager
{
    static GameManager gm = new GameManager();
    static UIManager ui = new UIManager();

    bool isTitleUI = false;
    public static void Main()
    {
        ui.location = Location.Title;
        gm.RunGame();
    }

    void RunGame()
    {
        // Console.WriteLine($"Run / sm.location = {sm.location}");
        //ui.MapUI();

        while (!isTitleUI)
        {
            Console.WriteLine("RunGame 루프 도는중...\n");
            Console.WriteLine($"GM / location 현재 장소 {ui.location}\n");
            //ui.TitleUI();
            ui.MapUI();
        }
    }
}