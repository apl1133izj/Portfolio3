using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    Door door;

    public GameObject[] ui; //0:time 1:clear 2:game Over;
    public enum StageSatting
    {
        stage1, stage2, stage3, stage4, stage5, stage6, stage7, stage8, stage9, stage10,
        stage11, stage12, stage13, stage14, stage15, stageSatingMap
    };
    public StageSatting stageSatting;

    //UI
    public Slider timeSlider;
    public GameObject timeUI;
    public int reStartCount;
    public float timeAttack;
    public float[] clearAttack; //0:초록보석 1단계 1:빨간보석 2단계 2:파란보석 3단계
    public GameObject menu;
    public bool menuOn;
    public int buttonCount;
    public GameObject[] stageMapPrefab;
    GameObject stageMap;
    public int reStartButtonPushCount;//restart버튼을 눌렀을경우 결과 창에서 시도한횟수에추가
    public Text reStartButtonPushCountText;

    //시간당 점수
    public Image[] timeLevelJulGame; //특정 시간내 스테이지 클리어 
    public bool[] clearTimeBool;
    //게임 상태
    public bool gameClear;
    public bool gameOver;
    public bool Restart;
    public bool regame = false;
    //맵이동
    public GameObject stageSelectionMap;
    public GameObject gameManager;
    //클리어시 맵열림
    public bool[] stageClearUnLock;//2~10스테이지 까지(0이2스테이지 8이10스테이지)
    public void Start()
    {
        player = FindObjectOfType<Player>();
        door = FindObjectOfType<Door>();

        if (stageSatting == StageSatting.stage1)
        {
            player = FindObjectOfType<Player>();

            if (door == null)
            {
                return;
            }
            timeAttack = 60;
            player.sptiteAlpha = 1.0f;
            player.playerSpeed = 5;
            door.plyerKeycontrolBool = false;
            timeSlider.maxValue = 60;
            clearAttack[0] = 40;
            clearAttack[1] = 20;
            clearAttack[2] = 1;
            Restart = false;
            player.keyOn = false;
            player.jumpCount = 0;
        }
        if (stageSatting == StageSatting.stage2)
        {
            player = FindObjectOfType<Player>();
            if (door == null)
            {
                return;
            }
            timeAttack = 100;
            player.sptiteAlpha = 1.0f;
            player.playerSpeed = 5;
            door.plyerKeycontrolBool = false;
            timeSlider.maxValue = 100;
            clearAttack[0] = 66.7f;
            clearAttack[1] = 34;
            clearAttack[2] = 1;
            Restart = false;
            player.keyOn = false;
            player.jumpCount = 0;
        }
        if (stageSatting == StageSatting.stage3)
        {
            player = FindObjectOfType<Player>();
            if (door == null)
            {
                return;
            }
            timeAttack = 80;
            player.sptiteAlpha = 1.0f;
            player.playerSpeed = 5;
            door.plyerKeycontrolBool = false;
            timeSlider.maxValue = 80;
            clearAttack[0] = 53.3462f;
            clearAttack[1] = 27.15182f;
            clearAttack[2] = 1;
            Restart = false;
            player.keyOn = false;
            player.jumpCount = 0;
        }
    }
    void Update()
    {
        if (door == null)
        {
            return;
        }

        if (door.uiOnBool)
        {
            ui[1] = GameObject.FindWithTag("ClearGame");
            // ui 배열의 길이가 2 이상인지 확인하고, 유효한 인덱스에만 접근합니다.
            if (ui.Length >= 2)
            {
                // 해당 인덱스의 UI를 활성화합니다.
                ui[1]?.gameObject.SetActive(true);

            }
        }
        //위의 수정된 코드에서는 Start 함수에서 Door 객체를 찾아서 door 변수에 할당하고, Update 함수에서 door 객체의 null 여부를 확인하는 로직이 추가되었습
        timeClear();
        mainMenu();
        GameOver();
    }

    void timeClear()
    {

        if (!gameClear || regame)
        {
            if (stageSatting != StageSatting.stageSatingMap)
            {
                timeAttack -= Time.deltaTime;
                timeSlider.value = timeAttack;
            }

        }
        //타임어택에 클리어 속도 보석 표시
        if (timeAttack <= clearAttack[0])//파란보석
        {
            timeLevelJulGame[2].enabled = false;
            clearTimeBool[2] = false;
        }
        if (timeAttack <= clearAttack[1])//빨간보석
        {
            timeLevelJulGame[1].enabled = false;
            clearTimeBool[1] = false;
        }
        if (timeAttack <= clearAttack[2])//초록보석
        {
            timeLevelJulGame[0].enabled = false;
            clearTimeBool[0] = false;
        }
    }
    public void gameClearButton()
    {

        if (stageSatting == StageSatting.stage1)
        {
            gameClearDefault();
            stageClearUnLock[0] = true;
        }
        if (stageSatting == StageSatting.stage2)
        {
            gameClearDefault();
            stageClearUnLock[1] = true;
        }
        if (stageSatting == StageSatting.stage3)
        {
            gameClearDefault();
            stageClearUnLock[2] = true;
        }
    }
    public void gameClearDefault()
    {
        string stageRoundStriing;
        stageRoundStriing = stageSatting.ToString();
        GameObject stageObject = GameObject.Find(stageRoundStriing);
        //하이어라키창에서 Stage1,Stage1(Clone)찾기
        GameObject stageInst = GameObject.Find($"{stageRoundStriing}(Clone)");
        Destroy(stageObject);
        Destroy(stageInst);
        //게임클리어시 스테이지 선택map 으로 이동
        stageSelectionMap.SetActive(true);
        gameManager.SetActive(false);
    }
    public void ReStart()
    {
        string stageRoundStriing;
        reStartCount += 1;
        stageRoundStriing = stageSatting.ToString();
        GameObject stageObject = GameObject.Find(stageRoundStriing);
        //하이어라키창에서 Stage1,Stage1(Clone)찾기
        GameObject stageInst = GameObject.Find($"{stageRoundStriing}(Clone)");
        Destroy(stageObject);
        Destroy(stageInst);
        regame = true;
        for (int i = 0; i <= 2; i++)
        {
            timeLevelJulGame[i].enabled = true;
            clearTimeBool[i] = true;
        }
        switch (stageSatting)
        {
            case StageSatting.stage1:
                stageMap = Instantiate(stageMapPrefab[0]);
                timeAttack = 60;
                timeSlider.maxValue = 60;
                clearAttack[0] = 40;
                clearAttack[1] = 20;
                clearAttack[2] = 1;
                StageDefaultSet();
                break;
            case StageSatting.stage2:
                stageMap = Instantiate(stageMapPrefab[1]);
                timeAttack = 100;
                timeSlider.maxValue = 100;
                clearAttack[0] = 66.7f;
                clearAttack[1] = 34;
                clearAttack[2] = 1;
                StageDefaultSet();
                break;
            case StageSatting.stage3:
                stageMap = Instantiate(stageMapPrefab[2]);
                timeAttack = 80;
                timeSlider.maxValue = 80;
                clearAttack[0] = 53.3462f;
                clearAttack[1] = 27.15182f;
                clearAttack[2] = 1;
                StageDefaultSet();
                break;
        }


    }
    void StageDefaultSet()
    {
        player = FindObjectOfType<Player>();
        door = FindObjectOfType<Door>();
        GameObject jumpItem = GameObject.Find("JumpItem(Clone)");
        GameObject KeyItem = GameObject.Find("Key(Clone)");
        Destroy(jumpItem);
        Destroy(KeyItem);
        gameManager.SetActive(true);
        BackGame();
        timeUI.gameObject.SetActive(true);
        player.sptiteAlpha = 1.0f;
        player.playerSpeed = 5;
        door.plyerKeycontrolBool = false;

        //player.transform.position = player.playerStartPos;
        Restart = false;
        player.keyOn = false;
        player.jumpCount = 0;
        menu.gameObject.SetActive(false);
    }
    public void BackGame()
    {
        buttonCount = 0;
        menuOn = false;
    }
    public void stage()
    {
        stageSelectionMap.SetActive(true);
        string stageRoundStriing;
        stageRoundStriing = stageSatting.ToString();
        GameObject stageObject = GameObject.Find(stageRoundStriing);
        //하이어라키창에서 Stage1,Stage1(Clone)찾기
        GameObject stageInst = GameObject.Find($"{stageRoundStriing}(Clone)");
        stageSatting = StageSatting.stageSatingMap;
        BackGame();
        //있을경우 삭제(한번에 2개의 스테이지 가 존재 하면 안되기 때문)
        if (stageObject != null)
        {
            stageObject.SetActive(false);
        }
        if (stageInst != null)
        {
            stageMap.SetActive(false);
        }
        if (stageSatting == StageSatting.stageSatingMap)
        {
            menu.gameObject.SetActive(false);
            timeUI.gameObject.SetActive(false);
        }
    }
    public void mainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && buttonCount == 0)
        {
            buttonCount += 1;
            menuOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && buttonCount == 1)
        {
            buttonCount = 0;
            menuOn = false;
        }
        if (menuOn)
        {
            menu.gameObject.SetActive(true);
        }
        else
        {
            menu.gameObject.SetActive(false);
        }
    }

    public void GameOver()
    {
        player = FindObjectOfType<Player>();
        if (player.playerGameOver)
        {
            gameOver = true;
        }
        else if (timeAttack <= 0f)
        {
            gameOver = true;
        }
        else
        {
            gameOver = false;
        }
    }
}
