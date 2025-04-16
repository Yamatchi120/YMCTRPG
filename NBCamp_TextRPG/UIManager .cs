using TRPG.Character;
using TRPG.GM;

namespace TRPG.UM;
// TitleUIManager
// VillageUIManager
// BattleUIManager
// UIManager 세분화 시키고 UIManager는 통합 관리자로 관리
// UIManager -> GameManager
// HandleError함수에 있는 if문 숫자 1~3까지만 받는 조건 
// 숫자 -> 변수로 바꿔서 관리하기 편하게
public enum VillageChoice
{
    CharacterState = 1,
    Inventory = 2,
    Store = 3,
    Back = 0
    //Dungeon = 4
}
public enum Location
{
    Title = 1,
    Village = 2
}
enum TitleChoice
{
    GameStart = 1,
    GameEnd = 2,
    DataReset = 3,
    Back = 0
}
public class UIManager
{
    TitleChoice titleChoice;
    VillageChoice villageChoice;
    public Location location;

    GameManager gm = new GameManager();
    Player p = new Player();

    char handleYesNo; // handleYesNoInput[0] 분리 Y or N 확인

    string userChoiceInput; // 유저 선택 입력
    string upperInput; // upper 변환용 변수

    int userChoice; // 유저 선택지

    bool isString = true; // 영어 입력 판별
    bool isChoice = false;

    public void MapUI()
    {
        //Console.Clear();

        MapDiscriptionUI();
        ShowChoice();
        HandleTitleUI();
    }
    void MapDiscriptionUI() // 맵 설명
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
    void CurrentLocation() // 현재 장소 (씬)
    {
        switch (location) // 마을 확장 가능
        {
            case Location.Title: // 타이틀
                Console.WriteLine("1. 게임 시작\n2. 게임 종료\n3. 데이터 초기화");
                break;
            case Location.Village: // 마을
                if (villageChoice >= VillageChoice.CharacterState && VillageChoice.Store >= villageChoice)
                {
                    Console.WriteLine("0. 나가기");
                }
                else
                {
                    Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n0. 나가기");
                }
                break;
        }
    }
    void ShowChoice() // 타이틀 화면 선택지
    {
        CurrentLocation();
        HandleError();

        switch (location)
        {
            case Location.Title:
                titleChoice = (TitleChoice)userChoice; // int -> enum 캐스팅
                break;
            case Location.Village:
                villageChoice = (VillageChoice)userChoice; // int -> enum 캐스팅
                break;
        }
        Console.WriteLine($"\ntitleChoice = {titleChoice}\nvillageChoice = {villageChoice}\n");
    }
    void HandleTitleUI() // 타이틀 선택지 UI
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
        else if (location == Location.Village)
        {
            switch (villageChoice) // 마을 동일 UI / 선택지 확장 가능
            {
                case VillageChoice.CharacterState:
                    Console.WriteLine("캐릭터 스테이터스");
                    p.ShowState();
                    ShowChoice();
                    break;
                case VillageChoice.Inventory:
                    Console.Write("인벤토리");
                    ShowChoice();
                    break;
                case VillageChoice.Store:
                    Console.WriteLine("상점");
                    ShowChoice();
                    break;
                case VillageChoice.Back:
                    Console.WriteLine("나가기");
                    HandleErrorYesNo();
                    break;
            }
        }

    }
    void HandleError()
    {
        if (location == Location.Title)
        {
            HandleErrorInt();
        }
        else if (location == Location.Village)
        {
            HandleErrorIntAddzero();
        }
        
    }
    void HandleErrorInput()
    {
        Console.Write("\n\n원하시는 행동을 입력해주세요\n>> ");
        userChoiceInput = Console.ReadLine();
        isString = int.TryParse(userChoiceInput, out userChoice);
    }
    void HandleErrorInt() // 예외 처리
    {
        do
        {
            HandleErrorInput();

            // 숫자만 = false, 영어, null = true, 0 포함 x
            if (!isString || userChoiceInput == null || !(0 < userChoice && userChoice < 4))
            {
                Console.WriteLine("형식 오류: 잘못된 입력값입니다.");
                Console.ReadLine();
            }
            else
                isChoice = true;
        }
        while (!isChoice);
    }
    void HandleErrorIntAddzero() // 예외 처리
    {
        do
        {
            HandleErrorInput();

            // 숫자만 = false, 영어, null = true, 0 포함 o
            if (!isString || userChoiceInput == null || !(0 <= userChoice && userChoice < 4))
            {
                Console.WriteLine("형식 오류: 잘못된 입력값입니다.");
                Console.ReadLine();
            }
            else
            {
                isChoice = true;
            }
        }
        while (!isChoice);
    }
    void HandleErrorYesNo() // 메인화면 돌아갈 때 질문하는 함수
    {
        do
        {
            Console.Write("\n메인화면으로 돌아가시겠습니까? (Yes or No)\n>> ");
            userChoiceInput = Console.ReadLine();

            isString = int.TryParse(userChoiceInput, out userChoice);

            if (isString || userChoiceInput == null)
            {
                Console.WriteLine("형식 오류: 잘못된 입력값입니다.");
            }
            else if (userChoiceInput[0] == 'y' || 'Y' == userChoiceInput[0]) // 소문자 or 앞자리 y, n 대문자 변환
            {
                upperInput = userChoiceInput;
                handleYesNo = upperInput.ToString().ToUpper()[0];
            }
            else if (userChoiceInput[0] == 'n' || 'N' == userChoiceInput[0]) // 소문자 or 앞자리 y, n 대문자 변환
            {
                upperInput = userChoiceInput;
                handleYesNo = upperInput.ToString().ToUpper()[0];
            }

            switch (handleYesNo)
            {
                case 'Y':
                    // village 일 때 종료 하시겠습니까
                    location = Location.Title;
                    break;
                case 'N':
                    location = Location.Village;
                    // 현재 장소 기억 다시 돌아와야함
                    break;
            }
        } while (!isChoice);
    }
}