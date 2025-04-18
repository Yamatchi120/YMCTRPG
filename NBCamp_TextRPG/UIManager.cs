using TRPG.Character;
using TRPG.IEM;
using TRPG.ShopM;

namespace TRPG.UM;

enum TitleChoice
{
    GameStart = 1,
    DataReset = 2,
    GameEnd = 0
}
public enum Location
{
    Title = 1,
    Village = 2
}
public enum VillageChoice
{
    IDLE = 9999,
    CharacterStats = 1,
    Inventory = 2,
    Store = 3,
    Back = 0
}
public enum InventoryChoice
{
    Equip = 1,
    Back = 0
}
public enum StoreChoice
{
    Buy = 1,
    Back = 0
}
enum ChoiceCount
{
    Count0,
    Count1,
    Count2,
    Count3
}

// 추후 Discription, UI 기능별 클래스 쪼개기
public class UIManager
{
    // 싱글톤
    public static UIManager Instance { get; private set; } = new UIManager();

    Player p = new Player();
    ShopManager shopm;
    InputErrorManager iem = new InputErrorManager();

    TitleChoice titleChoice;
    public VillageChoice villageChoice;
    public Location location = new Location();
    InventoryChoice inventoryChoice;
    StoreChoice storeChoice;
    ChoiceCount choiceCount;
        
    public void UIRun() // 메인 실행
    {
        // Console.Clear();
        villageChoice = VillageChoice.IDLE;

        LocationDiscription();
        HandleLocation();
        HandleUI();
    }
    void LocationDiscription() // 맵 설명
    {
        switch (location) // 맵 확장 가능
        {
            case Location.Title:
                Console.WriteLine("\nNBCamp TRPG\n\n");
                break;
            case Location.Village:
                Console.WriteLine("시작의 마을에 방문을 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n");
                break;
        }
    }
    void HandleLocation() // 타이틀 화면 선택지
    {
        LocationChoice();
        iem.HandleError((InputContext)choiceCount);

        switch (location)
        {
            case Location.Title:
                titleChoice = (TitleChoice)iem.userChoice; // int -> enum 캐스팅
                break;
            case Location.Village:
                villageChoice = (VillageChoice)iem.userChoice; // int -> enum 캐스팅
                break;
        }
        if (location != Location.Village)
            return;

        if (villageChoice == VillageChoice.Inventory)
        {
            inventoryChoice = (InventoryChoice)iem.userChoice;
        }
        else if (villageChoice == VillageChoice.Store)
        {
            storeChoice = (StoreChoice)iem.userChoice;
        }
        else
        {
            villageChoice = (VillageChoice)iem.userChoice;
        }
        Console.WriteLine($"InventoryChoice = {inventoryChoice}"); // Test Debug
        Console.WriteLine($"\ntitleChoice = {titleChoice}\nvillageChoice = {villageChoice}"); // Test Debug
        Console.WriteLine($"StoreChoice = {storeChoice}\n"); // Test Debug
    }
    void HandleUI() // 타이틀 선택지 UI
    {
        if (location == Location.Title)
        {
            switch (titleChoice) // 선택지 확장 가능
            {
                case TitleChoice.GameStart:
                    location = Location.Village; // 데이터 저장으로 현재 location 값 받아오기
                    break;
                case TitleChoice.GameEnd:
                    Console.Write("\n게임을 종료합니다.\n");
                    Environment.Exit(0);
                    break;
            }
        }
        if (location == Location.Village)
        {
            switch (villageChoice) // 마을 동일 UI, 선택지 확장 가능
            {
                case VillageChoice.CharacterStats:
                    while (true)
                    {
                        // Console.Clear();
                        UIDiscription();
                        p.ShowStats();
                        HandleLocation();
                        if (villageChoice == 0)
                        {
                            villageChoice = VillageChoice.IDLE; // 나가기 선택 시 마을 메뉴로 복귀
                            break;
                        }
                    }
                    break;
                case VillageChoice.Inventory:
                    while (true)
                    {
                        // Console.Clear();
                        UIDiscription();
                        p.Inventory();
                        HandleLocation();
                        if (villageChoice == 0)
                        {
                            villageChoice = VillageChoice.IDLE; // 나가기 선택 시 마을 메뉴로 복귀
                            break;
                        }
                    }
                    break;
                case VillageChoice.Store:
                    while (true)
                    {
                        // Console.Clear();
                        UIDiscription();
                        shopm.BaseShop();
                        HandleLocation();
                        if (villageChoice == 0)
                        {
                            villageChoice = VillageChoice.IDLE; // 나가기 선택 시 마을 메뉴로 복귀
                            break;
                        }
                    }
                    break;
                case VillageChoice.Back:
                    if (location == Location.Title)
                    {
                        titleChoice = (TitleChoice)iem.userChoice; // int -> enum 캐스팅
                    }    
                    else if(location == Location.Village)
                    {
                        villageChoice = (VillageChoice)iem.userChoice; // int -> enum 캐스팅
                    }
                    villageChoice = VillageChoice.IDLE;
                    iem.HandleErrorYesNo();
                    break;
            }
        }
    }

    void UIDiscription() // UI 설명
    {
        switch (villageChoice)
        {
            case VillageChoice.CharacterStats:
                Console.WriteLine("캐릭터 스테이터스");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                break;
            case VillageChoice.Inventory:
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                break;
            case VillageChoice.Store:
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                break;
        }
        switch (inventoryChoice)
        {
            case InventoryChoice.Equip:
                Console.WriteLine("인벤토리 - 장착관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                break;
        }
        switch (storeChoice)
        {
            case StoreChoice.Buy:
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                break;
        }
    }
    void LocationChoice() // 현재 장소 (씬)
    {
        switch (location) // 마을 확장 가능
        {
            case Location.Title: // 타이틀
                Console.WriteLine("1. 게임 시작\n2. 데이터 초기화 ( 미구현 )\n0. 게임 종료");
                choiceCount = ChoiceCount.Count1;
                break;
            case Location.Village: // 마을
                UIChoice();
                break;
        }
    }
    void UIChoice() // 현재 UI
    {
        if (villageChoice == VillageChoice.IDLE)
        {
            // 마을에 있을 때
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n0. 나가기");
            choiceCount = ChoiceCount.Count3;
        }
        else if (villageChoice == VillageChoice.CharacterStats)
        {
            Console.WriteLine("\n0. 나가기");
            choiceCount = ChoiceCount.Count0;
        }
        else if (villageChoice == VillageChoice.Inventory)
        {
            // 인벤토리 UI
            Console.WriteLine("\n1. 장착 관리\n0. 나가기");
            if(iem.userChoice == 1)
            {
                villageChoice = VillageChoice.Inventory;
                inventoryChoice = InventoryChoice.Equip;
            }
            choiceCount = ChoiceCount.Count1;
        }
        else if (villageChoice == VillageChoice.Store)
        {
            // 상점 UI
            Console.WriteLine("\n1. 아이템 구매\n0. 나가기");
            choiceCount = ChoiceCount.Count1;
        }
    }
}