using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageMapPlayer : MonoBehaviour
{
    public int playerSpeed;
    Vector2 playerPosition;
    public bool keyRight;
    public bool keyLeft;
    //점프 
    public Sprite spriteJump;
    public bool jumpBool;
    public int jumpPower;
    //플레이어 설정
    new Rigidbody2D rigidbody2D;
    public Animator animator;
    //박스 관련
    public bool boxPickingAni;
    public bool boxOn;
    public bool boxThrowing;
    public int boxint;
    GameObject box;
    public GameObject insBox;
    float throwForce; // 던질 힘의 크기
    float throwAngle;// 던질 각도 (0~90도)
    //아이템
    public int jumpCount;

    public bool stageSelectionBool;
    public bool exitSelectionBool;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Jump();
    }
    void Jump()
    {
        if (jumpCount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpCount -= 1;
                jumpBool = true;
                rigidbody2D.AddForce(new Vector2(0, 10 * jumpPower));
                animator.SetBool("Jump", true);
                Invoke("JumpOff", 0.85f);
            }

        }
    }

    void JumpOff()
    {
        animator.SetBool("Jump", false);
    }
    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) keyRight = true;
        else keyRight = false;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) keyLeft = true;
        else keyLeft = false;

        if (keyRight)//오른쪽
        {
            transform.Translate(new Vector2(playerSpeed * Time.deltaTime, 0));
            transform.localScale = new Vector3(-1, 1, 1);
            if (boxOn)//박스들고 뛸경우
            {
                animator.SetBool("BoxIdle", false);
                animator.SetBool("BoxRun", true);
            }
            else//박스 없이 그냥
            {
                animator.SetBool("Run", true);
            }

        }
        else if (keyLeft)//왼쪽
        {
            transform.Translate(new Vector2(-playerSpeed * Time.deltaTime, 0));
            transform.localScale = new Vector3(1, 1, 1);
            if (boxOn)//박스들고 뛸경우
            {
                animator.SetBool("BoxIdle", false);
                animator.SetBool("BoxRun", true);
            }
            else//박스 없이 그냥
            {
                animator.SetBool("Run", true);
            }
        }
        else//키누르지 안은상태
        {
            if (boxOn)
            {
                animator.SetBool("BoxIdle", true);
                animator.SetBool("BoxRun", false);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //스테이지 선택
        if (collision.gameObject.CompareTag("Stage"))
        {
            stageSelectionBool = true;
        }
        //게임 종료
        if (collision.gameObject.CompareTag("Exit"))
        {
            exitSelectionBool = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //스테이지 선택 벗어나기
        if (collision.gameObject.CompareTag("Stage"))
        {
            stageSelectionBool = false;
        }
        //게임 종료 벗어나기
        if (collision.gameObject.CompareTag("Exit"))
        {
            exitSelectionBool = false;
        }
    }
}
