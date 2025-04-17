using TRPG.Character;
using TRPG.IM;
using TRPG.IEM;
using TRPG.ShopM;
using System;

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
    //Dungeon = 4
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
// 추후 Discription, UI 기능별 클래스 쪼개기
public class UIManager
{
    // 싱글톤
    public static UIManager Instance { get; private set; } = new UIManager();

    Player p = new Player();
    ShopManager shopm = new ShopManager();
    InputErrorManager iem = new InputErrorManager();

    TitleChoice titleChoice;
    public VillageChoice villageChoice;
    public Location location = new Location();
    InventoryChoice inventoryChoice;
    StoreChoice storeChoice;
        
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
                Console.WriteLine("\n야맛치 TRPG\n\n");
                break;
            case Location.Village:
                Console.WriteLine("시작의 마을에 방문을 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n");
                break;
        }
    }
    void HandleLocation() // 타이틀 화면 선택지
    {
        LocationChoice();
        Console.WriteLine("HandleLocation HandleError 시작 전");
        iem.HandleError();
        Console.WriteLine("HandleLocation HandleError 시작 후");

        switch (location)
        {
            case Location.Title:
                titleChoice = (TitleChoice)iem.userChoice; // int -> enum 캐스팅
                break;
            case Location.Village:
                villageChoice = (VillageChoice)iem.userChoice; // int -> enum 캐스팅
                break;
        }
        switch (villageChoice)
        {
            case VillageChoice.Inventory:
                inventoryChoice = (InventoryChoice)iem.userChoice;
                break;
            case VillageChoice.Store:
                storeChoice = (StoreChoice)iem.userChoice;
                break;
        }
        Console.WriteLine($"\ntitleChoice = {titleChoice}\nvillageChoice = {villageChoice}"); // Test Debug
        Console.WriteLine($"InventoryChoice = {inventoryChoice}"); // Test Debug
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
                case TitleChoice.DataReset:
                    Console.WriteLine("\n데이터를 초기화합니다.\n게임을 다시 실행해주세요.\n");
                    Environment.Exit(0);
                    // 데이터 리셋
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
                        UIDiscription();
                        p.ShowStats();
                        villageChoice = VillageChoice.CharacterStats;
                        HandleLocation();
                        if (villageChoice == 0)
                        {
                            villageChoice = VillageChoice.Back; // 나가기 선택 시 마을 메뉴로 복귀
                            break;
                        }
                        Console.WriteLine("Clear"); // Test Debug
                    }
                    break;
                case VillageChoice.Inventory:
                    while (true)
                    {
                        UIDiscription();
                        p.Inventory();
                        HandleLocation();
                        if (villageChoice == 0)
                        {
                            villageChoice = VillageChoice.Back; // 나가기 선택 시 마을 메뉴로 복귀
                            break;
                        }
                        Console.WriteLine("Clear"); // Test Debug
                    }
                    break;
                case VillageChoice.Store:
                    while (true)
                    {
                        UIDiscription();
                        shopm.BaseShop();
                        villageChoice = VillageChoice.Store;
                        HandleLocation();
                        if (villageChoice == 0)
                        {
                            villageChoice = VillageChoice.Back; // 나가기 선택 시 마을 메뉴로 복귀
                            break;
                        }
                        Console.WriteLine("Clear"); // Test Debug
                    }
                    break;
                case VillageChoice.Back:
                    Console.WriteLine("HandleUI_BACK");
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
        if(inventoryChoice == InventoryChoice.Equip)
        {

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
    }
    void LocationChoice() // 현재 장소 (씬)
    {
        switch (location) // 마을 확장 가능
        {
            case Location.Title: // 타이틀
                Console.WriteLine("1. 게임 시작\n2. 데이터 초기화 ( 미구현 )\n0. 게임 종료");
                iem.inputContext = InputContext.ZeroToThree;
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
            iem.inputContext = InputContext.ZeroToThree;
        }
        else if (villageChoice == VillageChoice.CharacterStats)
        {
            Console.WriteLine("\n0. 나가기");
            iem.inputContext = InputContext.OnlyZero;
        }
        else if (villageChoice == VillageChoice.Inventory)
        {
            // 인벤토리 UI
            Console.WriteLine("\n1. 장착 관리\n0. 나가기");
            iem.inputContext = InputContext.ZeroToTwo;
        }
        else if (villageChoice == VillageChoice.Store)
        {
            // 상점 UI
            Console.WriteLine("\n1. 아이템 구매\n0. 나가기");
            iem.inputContext = InputContext.ZeroToTwo;
        }
    }
}