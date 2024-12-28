using System.Collections;
using UnityEngine;
public class Door : MonoBehaviour
{
    public Player player;
    Animator animator;
    Lock lockscript;
    new Rigidbody2D rigidbody2D;
    public bool plyerKeycontrolBool;//플레이어 컨트롤 여부;
    public bool boolLockOpen;
    public bool uiOnBool;

    public GameManager gameManager;
    void Start()
    {
        rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lockscript = GetComponentInChildren<Lock>();
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        //문에 닿을시 플레이어 조작 금지
        if (lockscript.openDoorAniBool)
        {
            player.keyLeft = false;
            player.keyRight = false;
            animator.enabled = true;
            player.playerSpeed = 0;
            plyerKeycontrolBool = true;
        }
        if (player.sptiteAlpha <= 0)
        {
            StartCoroutine(ClearUI());
        }
    }
    //게임 클리어 UI활성화
    IEnumerator ClearUI()
    {
        animator.SetBool("Close", true);
        yield return new WaitForSeconds(0.5f);
        uiOnBool = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
    //문에 플레이어가 닿으면 게임 클리어
        if (collision.gameObject.CompareTag("Player"))
        {
            //클리어 조건 키를 먹었는지
            if (player.keyOn)
            {
                gameManager.gameClear = true;
                gameManager.stageClearUnLock[0] = true;
                gameManager.regame = false;
                //lock스크립트연동
                boolLockOpen = true;
                Debug.Log(boolLockOpen);
            }
        }
    }
}
