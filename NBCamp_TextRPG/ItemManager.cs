using System;

namespace TRPG.IM;

public enum ItemType
{
    Werpon = 1,
    Armor = 2
}
public static class ItemManager
{
    public static List<Item> itemList = new List<Item>();

    public static void Init()
    {
        // 아이템 등록
        itemList.Add(new Item(0, "Bronz Sword", ItemType.Werpon, 100));
        itemList.Add(new Item(1, "Iron Sword", ItemType.Werpon, 150));

    }
}

public struct Item
{
    public int id;
    public string name;
    public ItemType type;
    public int value;

    // 생성자
    public Item(int id, string name, ItemType type, int value)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.value = value;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"[{id}] {name} ({type}) - Value: {value}");
    }
}