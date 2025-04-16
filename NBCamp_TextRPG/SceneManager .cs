//using TRPG.GM;

//namespace TRPG.SM;

//public enum Location
//{
//    Title = 1,
//    Village = 2
//}
//enum TitleChoice
//{
//    GameStart = 1,
//    GameEnd = 2,
//    DataReset = 3
//}

//public class SceneManager
//{
//    public Location location;
//    TitleChoice titleChoice;

//    GameManager gm = new GameManager();

//    string userChoiceInput; // 유저 선택지 입력

//    int userChoice; // 유저 선택지

//    bool isString = true; // 영어 입력 판별
//    bool isChoice = false;

//	public void TitleScene()
//	{
//        Console.WriteLine("\n야맛치 TRPG\n\n");
//        location = Location.Title; // 현재 위치를 Title로

//        // Console.WriteLine($"SM / sm.location = {location}");
//    }
//    public void ShowTitleChoice() // 타이틀 화면 선택지
//    {
//        GameManager gm = new GameManager();

//        CurrentLocation();
//        do
//        {
//            Console.Write("\n\n원하시는 행동을 입력해주세요\n>> ");
//            userChoiceInput = Console.ReadLine();
//            isString = int.TryParse(userChoiceInput, out userChoice);
//            titleChoice = (TitleChoice)userChoice; // int -> enum 캐스팅

//            HandleError();
//        }
//        while (!isChoice);

//        HandleTitle();
//    }
//    void HandleTitle() // 타이틀 화면 선택지 선택 했을 때
//    {
//        switch (titleChoice)
//        {
//            case TitleChoice.GameStart:
//                Console.WriteLine("게임을 시작합니다.");
//                location = Location.Village; // 데이터 저장으로 현재 location 값 받아오기
//                break;
//            case TitleChoice.GameEnd:
//                Console.Write("\n게임을 종료합니다.\n");
//                Environment.Exit(0);
//                break;
//            case TitleChoice.DataReset:
//                Console.WriteLine("\n데이터를 초기화합니다.\n게임을 다시 실행해주세요.\n");
//                Environment.Exit(0);
//                // 데이터 리셋
//                break;
//        }
//    }
//    public void CurrentLocation() // 현재 장소 (씬)
//    {
//        switch (location)
//        {
//            case Location.Title: // 메인 화면
//                Console.WriteLine("1. 게임 시작\n2. 게임 종료\n3. 데이터 초기화");
//                break;
//            case Location.Village: // 마을
//                Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점");
//                break;
//        }
//    }

//    public void HandleError() // 예외 처리
//    {
//        // 숫자만 = false, 영어, null = true
//        if (!isString || userChoiceInput == null || !(0 < userChoice && userChoice < 4))
//        {
//            Console.WriteLine("형식 오류: 잘못된 입력값입니다.");
//        }
//        else
//        {
//            isChoice = true;
//        }
//    }
//}
