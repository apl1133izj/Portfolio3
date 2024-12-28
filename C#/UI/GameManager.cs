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
    public float[] clearAttack; //0:�ʷϺ��� 1�ܰ� 1:�������� 2�ܰ� 2:�Ķ����� 3�ܰ�
    public GameObject menu;
    public bool menuOn;
    public int buttonCount;
    public GameObject[] stageMapPrefab;
    GameObject stageMap;
    public int reStartButtonPushCount;//restart��ư�� ��������� ��� â���� �õ���Ƚ�����߰�
    public Text reStartButtonPushCountText;

    //�ð��� ����
    public Image[] timeLevelJulGame; //Ư�� �ð��� �������� Ŭ���� 
    public bool[] clearTimeBool;
    //���� ����
    public bool gameClear;
    public bool gameOver;
    public bool Restart;
    public bool regame = false;
    //���̵�
    public GameObject stageSelectionMap;
    public GameObject gameManager;
    //Ŭ����� �ʿ���
    public bool[] stageClearUnLock;//2~10�������� ����(0��2�������� 8��10��������)
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
            // ui �迭�� ���̰� 2 �̻����� Ȯ���ϰ�, ��ȿ�� �ε������� �����մϴ�.
            if (ui.Length >= 2)
            {
                // �ش� �ε����� UI�� Ȱ��ȭ�մϴ�.
                ui[1]?.gameObject.SetActive(true);

            }
        }
        //���� ������ �ڵ忡���� Start �Լ����� Door ��ü�� ã�Ƽ� door ������ �Ҵ��ϰ�, Update �Լ����� door ��ü�� null ���θ� Ȯ���ϴ� ������ �߰��Ǿ���
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
        //Ÿ�Ӿ��ÿ� Ŭ���� �ӵ� ���� ǥ��
        if (timeAttack <= clearAttack[0])//�Ķ�����
        {
            timeLevelJulGame[2].enabled = false;
            clearTimeBool[2] = false;
        }
        if (timeAttack <= clearAttack[1])//��������
        {
            timeLevelJulGame[1].enabled = false;
            clearTimeBool[1] = false;
        }
        if (timeAttack <= clearAttack[2])//�ʷϺ���
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
        //���̾��Űâ���� Stage1,Stage1(Clone)ã��
        GameObject stageInst = GameObject.Find($"{stageRoundStriing}(Clone)");
        Destroy(stageObject);
        Destroy(stageInst);
        //����Ŭ����� �������� ����map ���� �̵�
        stageSelectionMap.SetActive(true);
        gameManager.SetActive(false);
    }
    public void ReStart()
    {
        string stageRoundStriing;
        reStartCount += 1;
        stageRoundStriing = stageSatting.ToString();
        GameObject stageObject = GameObject.Find(stageRoundStriing);
        //���̾��Űâ���� Stage1,Stage1(Clone)ã��
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
        //���̾��Űâ���� Stage1,Stage1(Clone)ã��
        GameObject stageInst = GameObject.Find($"{stageRoundStriing}(Clone)");
        stageSatting = StageSatting.stageSatingMap;
        BackGame();
        //������� ����(�ѹ��� 2���� �������� �� ���� �ϸ� �ȵǱ� ����)
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
