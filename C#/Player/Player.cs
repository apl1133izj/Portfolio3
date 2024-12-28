using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using static GameManager;

public class Player : MonoBehaviour
{
    public bool playerGameOver;
    //�÷��̾� �̵�
    public int playerSpeed;
    Vector2 playerPosition;
    public bool keyRight;
    public bool keyLeft;
    public float throwingCoolTime;
    public bool canBoxThrow;
    bool canBoomThrow = true;
    public float sptiteAlpha = 1;
    public Vector2 playerStartPos;
    /******************************�ൿstart******************************/
    //���� 
    public Sprite spriteJump;
    public bool jumpBool;
    public int jumpPower;
    public bool isGround;//���� ���� �������쿡�� true
    public int maxJump;
    //�÷��̾� ����
    new Rigidbody2D rigidbody2D;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    Color spriteColor;
    public Door door;
    Lock lockscpr;
    //�ڽ� ����
    public bool boxPickingAni;
    public bool boxOn;
    public bool boxThrowing;
    public int boxint;
    GameObject box;
    public GameObject insBox;
    float throwForce; // ���� ���� ũ��
    float throwAngle;// ���� ���� (0~90��)
    //��ź ���� 
    public bool boomPickingAni;
    public bool boomOn;
    public bool boomThrowing;
    public int boomint;
    GameObject boom;
    public GameObject insBoom;
    public bool boomtype;
    GameManager gameManager;
    /******************************�ൿend******************************/
    //������
    public int jumpCount;
    public bool keyOn;
    Tilemap tilemap;
    bool isBoxPickingRunning;
    bool isBomPickingRunning;
    StageGameManager stageGameManager;

    //����
    public bool tilelMapChangePosionBool;
    public bool playerScaleX2Posion;
    public float playerScaleX2PosionTime;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lockscpr = FindObjectOfType<Lock>();
        stageGameManager = FindObjectOfType<StageGameManager>();
    }

    void Update()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager.gameOver)
        {
            door = FindObjectOfType<Door>();
            if (door == null)
            {
                return;
            }

            // �÷��̾� �ൿ
            if (!door.plyerKeycontrolBool)
            {
                PlayerMove();
            }
            else
            {
                if (lockscpr.openDoorAniBool)
                {
                    playerSpeed = 2;
                    transform.Translate(new Vector2(playerSpeed * Time.deltaTime, 0));
                    animator.SetBool("Run", true);
                }
                else
                {
                    playerSpeed = 0;
                    animator.SetBool("Run", false);
                    sptiteAlpha -= Time.deltaTime;

                    Color spriteColor = spriteRenderer.color;

                    // ���İ� ����
                    spriteColor.a = sptiteAlpha;

                    // ����� ������ ��������Ʈ�� ����
                    spriteRenderer.color = spriteColor;
                }

            }
            if (boxOn)
            {
                BoxThrowing();
            }
            if (boomOn)
            {
                BoomThrowing();
            }
            else
            {
                // throwingCoolTime = 0;
            }
            Jump();
            BoomAction();
            BoxAction();
        }
    }
    private void FixedUpdate()
    {
        playerScaleX2PosionOff();
    }
    /* �ൿ */
    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) keyRight = true;
        else keyRight = false;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) keyLeft = true;
        else keyLeft = false;

        if (keyRight)//������
        {
            transform.Translate(new Vector2(playerSpeed * Time.deltaTime, 0));
            if (!playerScaleX2Posion)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-2, 2, 2);
            }
            if (boxOn)//�ڽ���� �۰��
            {
                animator.SetBool("BoxIdle", false);
                animator.SetBool("BoxRun", true);
            }
            else if (boomOn)
            {
                animator.SetBool("BoomIdle", false);
                animator.SetBool("BoomRun", true);
            }
            else//�ڽ� ���� �׳�
            {
                animator.SetBool("Run", true);
            }

        }
        else if (keyLeft)//����
        {
            transform.Translate(new Vector2(-playerSpeed * Time.deltaTime, 0));
            if (!playerScaleX2Posion)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(2, 2, 2);
            }
            if (boxOn)//�ڽ���� �۰��
            {
                animator.SetBool("BoxIdle", false);
                animator.SetBool("BoxRun", true);
            }
            else if (boomOn)//��ź ��� ���
            {
                animator.SetBool("BoomIdle", false);
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
            if (boomOn)
            {
                animator.SetBool("BoomIdle", true);
                animator.SetBool("BoomRun", false);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }
    }
    void playerScaleX2PosionOff()
    {
        if (playerScaleX2Posion)
        {
            playerScaleX2PosionTime -= Time.deltaTime;
            if (playerScaleX2PosionTime <= 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                playerScaleX2Posion = false;
            }
        }
    }
    // �ڽ� ������
    void BoxThrowing()
    {
        if (Input.GetKey(KeyCode.E) && canBoxThrow) // GetKeyDown���� �����ϰ� canThrow ���� �߰�
        {

            boxint += 1;
            throwingCoolTime = 0;
            boxThrowing = true;
            canBoxThrow = false; // ��� ���� ���� ��� ���� ������ false�� ����
            StartCoroutine(BoxThrowingCoroutine());
        }

    }
    //��ź ������
    void BoomThrowing()
    {
        throwingCoolTime += Time.deltaTime;
        if (throwingCoolTime >= 1.5f)
        {

            if (Input.GetKey(KeyCode.E) && canBoomThrow) // GetKeyDown���� �����ϰ� canThrow ���� �߰�
            {
                boomint += 1;

                throwingCoolTime = 0;
                boomThrowing = true;
                canBoomThrow = false; // ��� ���� ���� ��� ���� ������ false�� ����
                StartCoroutine(BoomThrowingCoroutine());
            }
        }

    }
    // ����
    void Jump()
    {
        if (jumpCount >= 1 && isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space) && maxJump <= 20)
            {
                maxJump += 1;
                jumpCount -= 1;
                jumpBool = true;
                rigidbody2D.AddForce(new Vector2(0, 10 * jumpPower));
                animator.SetBool("Jump", true);
                Invoke("JumpOff", 0.85f);
            }
        }
        if (maxJump == 20)
        {
            maxJump = 0;
            isGround = false;
        }
    }
    void JumpOff()
    {
        jumpBool = false;
        animator.SetBool("Jump", false);
    }

    //��ź ���
    void BoomAction()
    {
        if (boomPickingAni && !isBomPickingRunning)
        {
            StartCoroutine(BooomPickingAni());
        }
        else if (!boomPickingAni && isBomPickingRunning)
        {
            StopCoroutine(BooomPickingAni());
            isBomPickingRunning = false;
        }
    }
    void BoxAction()
    {
        // �ڽ��� ����� ��
        if (boxPickingAni && !isBoxPickingRunning)
        {
            StartCoroutine(BoxPickingAni());
        }
        else if (!boxPickingAni && isBoxPickingRunning)
        {
            StopCoroutine(BoxPickingAni());
            isBoxPickingRunning = false;
        }
    }
    /******************************************************************************************************************
     �ൿ �ڷ�ƾ Start
    *******************************************************************************************************************/
    // �ڽ� ���� ��
    IEnumerator BoxPickingAni()
    {
        canBoxThrow = true;
        isBoxPickingRunning = true; // �ڷ�ƾ�� ���� ������ ��Ÿ���� ������ true�� ����
        animator.SetBool("BoxPicking", true);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("BoxPicking", false);
        animator.SetBool("BoxIdle", true);
        animator.SetBool("Run", false);
        boxOn = true;
        boxPickingAni = false;
        isBoxPickingRunning = false; // �ڷ�ƾ�� ����Ǿ����� ��Ÿ���� ������ false�� ����
    }

    // �ڽ� ���� ��
    IEnumerator BoxThrowingCoroutine()
    {
        boxOn = false;
        animator.SetInteger("boxCount", boxint);
        animator.SetBool("BoxThrowing", true);
        boxThrowing = false;
        yield return new WaitForSeconds(0.4f);
        boxint = 0;
        animator.SetInteger("boxCount", boxint);
        animator.SetBool("BoxIdle", false);
        animator.SetBool("BoxRun", false);
        animator.SetBool("BoxThrowing", false);

        canBoxThrow = true; // �ִϸ��̼��� ������ ��� ���� ������ true�� ����
    }

    IEnumerator BooomPickingAni()
    {
        isBomPickingRunning = true;
        animator.SetBool("BoomPicking", true);
        yield return new WaitForSeconds(0.35f);
        animator.SetBool("BoomPicking", false);
        animator.SetBool("BoomIdle", true);
        animator.SetBool("Run", false);
        isBomPickingRunning = false;
    }

    IEnumerator BoomThrowingCoroutine()
    {
        boomPickingAni = false;
        animator.SetInteger("boomCount", boomint);
        animator.SetBool("BoomThrowing", true);
        boomThrowing = false;
        yield return new WaitForSeconds(0.4f);
        boomint = 0;
        animator.SetBool("BoomThrowing", false);
        animator.SetBool("BoomIdle", false);
        animator.SetBool("BoomRun", false);
        animator.SetBool("BoomThrowing", false);
        boomOn = false;
        Debug.Log("������ false");
        canBoomThrow = true; // �ִϸ��̼��� ������ ��� ���� ������ true�� ����
    }
    /******************************************************************************************************************
    �ൿ �ڷ�ƾ End
    *******************************************************************************************************************/

    public void InsBox()
    {
        if (transform.localScale == new Vector3(-1, 1, 1))
        {
            throwAngle = 15;
            throwForce = 60;
            box = Instantiate(insBox, transform.position, Quaternion.identity);
            Rigidbody2D boxRigidbody = box.GetComponent<Rigidbody2D>();

            float throwAngleRad = throwAngle * Mathf.Deg2Rad;

            Vector2 throwDirection = new Vector2(Mathf.Cos(throwAngleRad), Mathf.Sin(throwAngleRad));

            // ���� ���� �ڽ��� ����
            boxRigidbody.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        }
        if (transform.localScale == new Vector3(1, 1, 1))
        {
            throwAngle = -15;
            throwForce = 60;
            box = Instantiate(insBox, transform.position, Quaternion.identity);
            Rigidbody2D boxRigidbody = box.GetComponent<Rigidbody2D>();

            float throwAngleRad = throwAngle * Mathf.Deg2Rad;

            Vector2 throwDirection = new Vector2(Mathf.Cos(throwAngleRad), Mathf.Sin(throwAngleRad));

            // ���� ���� �ڽ��� ����
            boxRigidbody.AddForce(-throwDirection * throwForce, ForceMode2D.Impulse);
        }
    }

    public void InsBoom()
    {
        if (boomtype)
        {
            if (transform.localScale == new Vector3(-1, 1, 1) || transform.localScale == new Vector3(-2, 2, 2))
            {
                throwAngle = 15;
                throwForce = 30;
                boom = Instantiate(insBoom, new Vector2(transform.position.x + 1, transform.position.y), Quaternion.identity);
                if (playerScaleX2Posion)
                {
                    boom.transform.localScale = new Vector2(2, 2);
                }
                Rigidbody2D boomRigidbody = boom.GetComponent<Rigidbody2D>();

                float throwAngleRad = throwAngle * Mathf.Deg2Rad;

                Vector2 throwDirection = new Vector2(Mathf.Cos(throwAngleRad), Mathf.Sin(throwAngleRad));

                // ���� ���� �ڽ��� ����
                boomRigidbody.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
            }
            if (transform.localScale == new Vector3(1, 1, 1) || transform.localScale == new Vector3(2, 2, 2))
            {
                throwAngle = -15;
                throwForce = 30;
                boom = Instantiate(insBoom, new Vector2(transform.position.x - 1, transform.position.y), Quaternion.identity);
                if (playerScaleX2Posion)
                {
                    boom.transform.localScale = new Vector2(2, 2);
                }
                Rigidbody2D boxRigidbody = boom.GetComponent<Rigidbody2D>();

                float throwAngleRad = throwAngle * Mathf.Deg2Rad;

                Vector2 throwDirection = new Vector2(Mathf.Cos(throwAngleRad), Mathf.Sin(throwAngleRad));

                // ���� ���� ��ź�� ����
                boxRigidbody.AddForce(-throwDirection * throwForce, ForceMode2D.Impulse);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            keyOn = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("NoTriggetChangePosion"))
        {
            tilelMapChangePosionBool = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PlayerScaleX2"))
        {
            playerScaleX2PosionTime = 15f;
            playerScaleX2Posion = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("JumpItem"))
        {
            if (playerScaleX2Posion)
            {
                jumpCount += 2;
            }
            else
            {
                jumpCount += 1;
            }

            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("playerKeyControllFalse"))
        {
            lockscpr.openDoorAniBool = false;
            //door.plyerKeycontrolBool = false;
        }
        if (collision.gameObject.CompareTag("Box") && boxOn == false)
        {
            boxPickingAni = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Boom") && !boomOn)
        {
            boomtype = true;
            boomPickingAni = true;
            boomOn = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("gameover"))
        {
            Debug.Log("playerGameOver");
            playerGameOver = true;
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("foothold") || collision.gameObject.CompareTag("UPMoveLoad") || collision.gameObject.CompareTag("Load"))
        {
            isGround = true;
        }
        if (gameManager.stageSatting == StageSatting.stage1)
        {
            if (collision.gameObject.CompareTag("JumpItem"))
            {
                jumpCount += 1;
                Destroy(collision.gameObject);
            }
        }
    }
}

