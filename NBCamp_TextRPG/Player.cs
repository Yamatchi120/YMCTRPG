using TRPG.IM;

namespace TRPG.Character;

public class Player
{
	int level = 01;
	string name = "yamatchi";
	string job = "전사";
	int damage = 10;
	int armor = 5;
	int hp = 100;
	public int gold = 1500;

	public void ShowStats()
	{
		Console.WriteLine($"Lv. {level}\n" +
						  $"{name} ( {job} )\n" + 
						  $"공격력 : {damage}\n" +
						  $"방어력 : {armor}\n" +
						  $"체  력 : {hp}\n" +
						  $"소지금 : {gold}");
	}

    public void Inventory()
    {
        Console.WriteLine("[아이템 목록]");
        
    }

    public void Equip()
	{

	}
}
