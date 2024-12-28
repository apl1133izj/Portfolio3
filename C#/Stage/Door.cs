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
    IEnumerator ClearUI()
    {
        animator.SetBool("Close", true);
        yield return new WaitForSeconds(0.5f);
        uiOnBool = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
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