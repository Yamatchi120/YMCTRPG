using TRPG.IM;
using TRPG.UM;

namespace TRPG.GM;

public class GameManager
{
    static GameManager gm = new GameManager();
    public static void Main()
    {
        UIManager.Instance.location = Location.Title;
        ItemManager.Init(); // 아이템 등록
        gm.RunGame();
    }

    void RunGame()
    {
        while (true)
        {
            //Console.WriteLine("RunGame 루프 도는중...\n");
            //Console.WriteLine($"GM / location 현재 장소 {UIManager.Instance.location}\n");
            UIManager.Instance.UIRun();
        }
    }
}