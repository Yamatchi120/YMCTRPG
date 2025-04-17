using TRPG.UM;

namespace TRPG.IEM;

public enum InputContext
{
    OnlyZero,       // 0만 가능
    ZeroToOne,      // 0~1 가능
    ZeroToTwo,      // 0~2 가능
    ZeroToThree     // 0~3 가능
}
public class InputErrorManager
{
    public InputContext inputContext;
    char handleYesNo; // handleYesNoInput[0] 분리 Y or N 확인

    string userChoiceInput; // 유저 선택 입력
    string upperInput; // upper 변환용 변수

    int minChoice = 0;
    int maxChoice = 0;
    public int userChoice; // 유저 선택지

    bool isString = true; // 영어 입력 판별
    bool isChoice = false;

    public void HandleError(InputContext context)
    {
        inputContext = context;
        switch (inputContext)
        {
            case InputContext.OnlyZero:
                minChoice = 0;
                maxChoice = 0;
                break;
            case InputContext.ZeroToOne:
                minChoice = 0;
                maxChoice = 1;
                break;
            case InputContext.ZeroToTwo:
                minChoice = 0;
                maxChoice = 2;
                break;
            case InputContext.ZeroToThree:
                minChoice = 0;
                maxChoice = 3;
                break;
        }
        do
        {
            HandleErrorInput();

            // 숫자만 = false, 영어, null = true, 0 포함 x
            if (string.IsNullOrWhiteSpace(userChoiceInput))
            {
                Console.WriteLine("형식 오류: 값을 입력해주세요.");
                Console.ReadLine();
            }
            else if (!isString || userChoice < minChoice || userChoice > maxChoice)
            {
                Console.WriteLine("형식 오류: 잘못된 입력값입니다.");
                Console.ReadLine();
            }
            else
                isChoice = true;
        }
        while (!isChoice);
    }
    void HandleErrorInput()
    {
        Console.Write("\n\n원하시는 행동을 입력해주세요\n>> ");
        userChoiceInput = Console.ReadLine();
        isString = int.TryParse(userChoiceInput, out userChoice);
    }
    public void HandleErrorYesNo() // 메인화면 돌아갈 때 질문하는 함수
    {
        isChoice = false;
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
