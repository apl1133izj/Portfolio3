using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageGameManager : MonoBehaviour
{
    public GameObject stageMenu;
    public GameObject gameManagerGameObject;
    public GameObject stageSelectionGameObject;
    public GameObject[] Stage;
    public GameObject stageMap;
    public PlayerStageMap player;
    public bool  StageMapmove;
    GameManager gameManagers;
    public bool[] lockAnimatior;   
    //Lock애니메이션(스테이지 클리시 스테이지 선택ui에서 실행됨)
    public Animator[] animator;
    private void Start()
    {
        gameManagers = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (player.stageSelectionBool)
        {
            stageMenu.SetActive(true);
        }
        else
        {
            stageMenu.SetActive(false); 
        }
        if (player.exitSelectionBool)
        {

        }
        stageLockAnimator();
    }
    
    public void stageLockAnimator()
    {
        if(gameManagers.stageClearUnLock[0] == true && lockAnimatior[0] == false)
        {
            Debug.Log("?");
            animator[0].enabled = true;
            lockAnimatior[0] = true;
        }
        if (gameManagers.stageClearUnLock[1] == true && lockAnimatior[1] == false)
        {
            Debug.Log("?");
            animator[1].enabled = true;
            lockAnimatior[1] = true;
        }
    }
    public void stage1Button()
    {
        gameManagers.stageSatting = GameManager.StageSatting.stage1;
        stageSelectionGameObject.gameObject.SetActive(false);
        gameManagers.ReStart();
    }
    public void stage2Button()
    {
        if (gameManagers.stageClearUnLock[0] == true)
        {
            gameManagers.stageSatting = GameManager.StageSatting.stage2;
            stageSelectionGameObject.gameObject.SetActive(false);
            gameManagers.ReStart();
            gameManagerGameObject.SetActive(true);
        }
    }
    public void stage3Button()
    {


    }
    public void stage4Button()
    {



    }
    public void stage5Button()
    {


    }

    public void stage6Button()
    {


    }
    public void stage7Button()
    {


    }
    public void stage8Button()
    {


    }
    public void stage9Button()
    {


    }
    public void stage10Button()
    {


    }

}
