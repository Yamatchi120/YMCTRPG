using System.Net.Http.Headers;
using TRPG.UM;

namespace TRPG.IEM;

//enum InputContext
//{
//    OnlyZero,       // 0만 가능
//    ZeroToOne,      // 0~1 가능
//    ZeroToTwo,      // 0~2 가능
//    ZeroToThree     // 0~3 가능
//}
public class InputErrorManager
{
    //InputContext currentInputContext;
    char handleYesNo; // handleYesNoInput[0] 분리 Y or N 확인

    string userChoiceInput; // 유저 선택 입력
    string upperInput; // upper 변환용 변수

    //int minChoice = 0;
    //int maxChoice = 0;
    public int userChoice; // 유저 선택지

    bool isString = true; // 영어 입력 판별
    bool isChoice = false;

    public void HandleError()
    {
        if (UIManager.Instance.location == Location.Title)
        {
            // 숫자 1~3 값
            HandleErrorInt();
        }
        else if (UIManager.Instance.villageChoice >= (VillageChoice)1 && (VillageChoice)3 >= UIManager.Instance.villageChoice)
        {
            // 숫자 0 값
            HandleErrorOnlyzero();
        }
        else if (UIManager.Instance.location == Location.Village)
        {
            // 숫자 0~4 값
            HandleErrorIntAddzero();
        }
    }


    //public void HandleError()
    //{
    //    currentInputContext = (InputContext)userChoice;
    //    switch (currentInputContext)
    //    {
    //        case InputContext.OnlyZero:
    //            minChoice = 0;
    //            maxChoice = 0;
    //            break;
    //        case InputContext.ZeroToOne:
    //            minChoice = 0;
    //            maxChoice = 1;
    //            break;
    //        case InputContext.ZeroToTwo:
    //            minChoice = 0;
    //            maxChoice = 2;
    //            break;
    //        case InputContext.ZeroToThree:
    //            minChoice = 0;
    //            maxChoice = 3;
    //            break;
    //    }
    //    do
    //    {
    //        HandleErrorInput();

    //        // 숫자만 = false, 영어, null = true, 0 포함 x
    //        if (string.IsNullOrWhiteSpace(userChoiceInput))
    //        {
    //            Console.WriteLine("형식 오류: 값을 입력해주세요.");
    //            Console.ReadLine();
    //        }
    //        else if (!isString || !(minChoice < userChoice && userChoice < maxChoice))
    //        {
    //            Console.WriteLine("형식 오류: 잘못된 입력값입니다.");
    //            Console.ReadLine();
    //        }
    //        else
    //            isChoice = true;
    //    }
    //    while (!isChoice);
    //}
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
            if (!isString || string.IsNullOrWhiteSpace(userChoiceInput) || !(0 < userChoice && userChoice < 4))
            {
                Console.WriteLine("형식 오류: 잘못된 입력값입니다.");
                Console.ReadLine();
                userChoice = -1; // ReadLine 이후 Back되지 않기 위한 초기화값
            }
            else
                isChoice = true;
            Console.WriteLine("ErrorInt 발동");
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
                userChoice = -1; // ReadLine 이후 Back되지 않기 위한 초기화값
            }
            else
                isChoice = true;
            Console.WriteLine("ErrorAddzero 발동");
        }
        while (!isChoice);
    }
    public void HandleErrorOnlyzero()
    {
        do
        {
            HandleErrorInput();

            // 숫자만 = false, 영어, null = true, 0 포함 o
            if (!isString || string.IsNullOrWhiteSpace(userChoiceInput) || 0 != userChoice)
            {
                Console.WriteLine("형식 오류: 잘못된 입력값입니다.");
                Console.ReadLine();
                userChoice = -1; // ReadLine 이후 Back되지 않기 위한 초기화값
            }
            else
                isChoice = true;
            Console.WriteLine("ErrorOnlyZero 발동");
        }
        while (!isChoice);
    }
    public void HandleErrorYesNo() // 메인화면 돌아갈 때 질문하는 함수
    {
        do
        {
            Console.Write("\n메인화면으로 돌아가시겠습니까? (Yes or No)\n>> ");
            userChoiceInput = Console.ReadLine();

            isString = int.TryParse(userChoiceInput, out userChoice);

            if (isString || string.IsNullOrWhiteSpace(userChoiceInput))
            {
                Console.WriteLine("형식 오류: 잘못된 입력값입니다.");
                userChoice = -1; // ReadLine 이후 Back되지 않기 위한 초기화값
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
                    UIManager.Instance.location = Location.Title;
                    break;
                case 'N':
                    UIManager.Instance.location = Location.Village;
                    // 현재 장소 기억 다시 돌아와야함
                    break;
            }
        } while (!isChoice);
    }
}
