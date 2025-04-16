using TRPG.Character;
using TRPG.IM;
using TRPG.IEM;
using TRPG.ShopM;

namespace TRPG.UM;

// 현재는 HandleError 쪽에 받는 값을 정수로 해서 관리가 힘듬
// 해당 UI에 값에 맞게 알아서 늘어나고 줄어들도록
// HandleError함수에 있는 if문 숫자 1~3까지만 받는 조건 
// 숫자 -> 변수로 바꿔서 관리하기 편하게

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
enum TitleChoice
{
    GameStart = 1,
    GameEnd = 2,
    DataReset = 3
}

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
        
    public void MapUI() // 메인 실행
    {
        //Console.Clear();
        villageChoice = VillageChoice.IDLE;

        LocationDiscription();
        ShowChoice();
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
    void ShowChoice() // 타이틀 화면 선택지
    {
        CurrentLocation();
        iem.HandleError();

        switch (location)
        {
            case Location.Title:
                titleChoice = (TitleChoice)iem.userChoice; // int -> enum 캐스팅
                break;
            case Location.Village:
                villageChoice = (VillageChoice)iem.userChoice; // int -> enum 캐스팅
                break;
        }
        Console.WriteLine($"\ntitleChoice = {titleChoice}\nvillageChoice = {villageChoice}\n");
    }
    void HandleUI() // 타이틀 선택지 UI
    {
        bool handleUIOUT = false;
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
        else if (location == Location.Village)
        {
            switch (villageChoice) // 마을 동일 UI, 선택지 확장 가능
            {
                case VillageChoice.CharacterStats:
                    while (!handleUIOUT)
                    {
                        UIDiscription();
                        p.ShowStats();
                        villageChoice = VillageChoice.CharacterStats;
                        ShowChoice();
                        if (villageChoice == 0)
                        {
                            villageChoice = VillageChoice.Back; // 나가기 선택 시 마을 메뉴로 복귀
                            break;
                        }
                    }
                    break;
                case VillageChoice.Inventory:
                    while (!handleUIOUT)
                    {
                        UIDiscription();
                        ItemManager.Inventory();
                        villageChoice = VillageChoice.Inventory;
                        ShowChoice();
                        if (villageChoice == 0)
                        {
                            villageChoice = VillageChoice.Back; // 나가기 선택 시 마을 메뉴로 복귀
                            break;
                        }
                    }
                    break;
                case VillageChoice.Store:
                    while (!handleUIOUT)
                    {
                        UIDiscription();
                        shopm.BaseShop();
                        villageChoice = VillageChoice.Store;
                        ShowChoice();
                        if (villageChoice == 0)
                        {
                            villageChoice = VillageChoice.Back; // 나가기 선택 시 마을 메뉴로 복귀
                            break;
                        }
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
    }

    void UIDiscription() // UI 설명
    {
        switch (villageChoice)
        {
            case VillageChoice.CharacterStats:
                Console.WriteLine("캐릭터 스테이터스\n");
                break;
            case VillageChoice.Inventory:
                Console.Write("인벤토리\n");
                break;
            case VillageChoice.Store:
                Console.WriteLine("상점\n");
                break;
        }
    }
    void CurrentLocation() // 현재 장소 (씬)
    {
        switch (location) // 마을 확장 가능
        {
            case Location.Title: // 타이틀
                Console.WriteLine("1. 게임 시작\n2. 게임 종료\n3. 데이터 초기화 ( 미구현 )");
                break;
            case Location.Village: // 마을
                CurrentUI();
                break;
        }
    }
    void CurrentUI() // 현재 UI
    {
        if (villageChoice == VillageChoice.IDLE)
        {
            // 마을에 있을 때
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n0. 나가기");
        }
        else if (villageChoice == VillageChoice.CharacterStats)
        {
            Console.WriteLine("0. 나가기");
        }
        else if (villageChoice == VillageChoice.Inventory)
        {
            // 인벤토리 UI
            Console.WriteLine("1. 장착 관리\n0. 나가기");
        }
        else if (villageChoice == VillageChoice.Store)
        {
            // 상점 UI
            Console.WriteLine("1. 아이템 구매\n0. 나가기");
        }
    }
}