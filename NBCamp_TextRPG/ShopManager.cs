using TRPG.Character;
using TRPG.IM;

namespace TRPG.ShopM;
public class ShopManager
{
    Player p = new Player();
    public void BaseShop()
    {
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{p.gold} G\n");

        foreach (Item item in ItemManager.itemList)
        {
            item.PrintInfo();
        }
    }
}
