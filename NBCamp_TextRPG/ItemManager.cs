namespace TRPG.IM;

public enum ItemType
{
    Weapon = 1,
    Armor = 2
}
public static class ItemManager
{
    public static List<Item> itemList = new List<Item>();

    public static void Init()
    {
        // 아이템 등록
        itemList.Add(new Item(1, ItemType.Armor, 1000, "구매 완료", 0, 5, "수련자 갑옷", "수련에 도움을 주는 갑옷입니다."));
        itemList.Add(new Item(2, ItemType.Armor, 2000, "구매 완료", 0, 9, "무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다."));
        itemList.Add(new Item(3, ItemType.Armor, 3500, "구매 완료", 0, 15, "스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."));
        itemList.Add(new Item(4, ItemType.Weapon, 600, "구매 완료", 2, 0, "낡은 검", "쉽게 볼 수 있는 낡은 검 입니다."));
        itemList.Add(new Item(5, ItemType.Weapon, 1500, "구매 완료", 5, 0, "청동 도끼", "어디선가 사용됐던거 같은 도끼입니다."));
        itemList.Add(new Item(6, ItemType.Weapon, 3500, "구매 완료", 7, 0, "스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다."));
    }
}

public struct Item
{
    public int id;
    public string name;
    public ItemType type;
    public int atk;
    public int def;
    public string desc;
    public int value;
    public string buy;

    // 생성자
    public Item(int id, ItemType type, int value, string buy, int atk, int def, string name, string desc)
    {
        this.id = id;
        this.type = type;
        this.value = value;
        this.buy = buy;
        this.atk = atk;
        this.def = def;
        this.name = name;
        this.desc = desc;
    }
    // 복사 생성자
    public Item(Item other)
    {
        this.id = other.id;
        this.type = other.type;
        this.value = other.value;
        this.buy = other.buy;
        this.atk = other.atk;
        this.def = other.def;
        this.name = other.name;
        this.desc = other.desc;
    }

    public void PrintInfo()
    {
        if(ItemType.Weapon == type)
        {
            Console.WriteLine($" - [ {id} ] {value} G |  공격력 : +{atk} | {name} | {desc}");
        }
        else if(ItemType.Armor == type)
        {
            Console.WriteLine($" - [ {id} ] {value} G |  방어력 : +{def} | {name} | {desc}");
        }
    }
}