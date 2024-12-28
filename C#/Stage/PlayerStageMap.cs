using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerStageMap : MonoBehaviour
{
    public int playerSpeed;
    Vector2 playerPosition;
    public bool keyRight;
    public bool keyLeft;
    //���� 
    public Sprite spriteJump;
    public bool jumpBool;
    public int jumpPower;
    //�÷��̾� ����
    new Rigidbody2D rigidbody2D;
    public Animator animator;
    //�ڽ� ����
    public bool boxPickingAni;
    public bool boxOn;
    public bool boxThrowing;
    public int boxint;
    GameObject box;
    public GameObject insBox;
    float throwForce; // ���� ���� ũ��
    float throwAngle;// ���� ���� (0~90��)
    //������
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

        if (keyRight)//������
        {
            transform.Translate(new Vector2(playerSpeed * Time.deltaTime, 0));
            transform.localScale = new Vector3(-1, 1, 1);
            if (boxOn)//�ڽ���� �۰��
            {
                animator.SetBool("BoxIdle", false);
                animator.SetBool("BoxRun", true);
            }
            else//�ڽ� ���� �׳�
            {
                animator.SetBool("Run", true);
            }

        }
        else if (keyLeft)//����
        {
            transform.Translate(new Vector2(-playerSpeed * Time.deltaTime, 0));
            transform.localScale = new Vector3(1, 1, 1);
            if (boxOn)//�ڽ���� �۰��
            {
                animator.SetBool("BoxIdle", false);
                animator.SetBool("BoxRun", true);
            }
            else//�ڽ� ���� �׳�
            {
                animator.SetBool("Run", true);
            }
        }
        else//Ű������ ��������
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
        if (collision.gameObject.CompareTag("Stage"))
        {
            stageSelectionBool = true;
        }
        if (collision.gameObject.CompareTag("Exit"))
        {
            exitSelectionBool = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            stageSelectionBool = false;
        }
        if (collision.gameObject.CompareTag("Exit"))
        {
            exitSelectionBool = false;
        }
    }
}
