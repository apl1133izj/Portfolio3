using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using static GameManager;

public class Player : MonoBehaviour
{
    public bool playerGameOver;
    //플레이어 이동
    public int playerSpeed;
    Vector2 playerPosition;
    public bool keyRight;
    public bool keyLeft;
    public float throwingCoolTime;
    public bool canBoxThrow;
    bool canBoomThrow = true;
    public float sptiteAlpha = 1;
    public Vector2 playerStartPos;
    /******************************행동start******************************/
    //점프 
    public Sprite spriteJump;
    public bool jumpBool;
    public int jumpPower;
    public bool isGround;//땅에 발이 닳았을경우에만 true
    public int maxJump;
    //플레이어 설정
    new Rigidbody2D rigidbody2D;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    Color spriteColor;
    public Door door;
    Lock lockscpr;
    //박스 관련
    public bool boxPickingAni;
    public bool boxOn;
    public bool boxThrowing;
    public int boxint;
    GameObject box;
    public GameObject insBox;
    float throwForce; // 던질 힘의 크기
    float throwAngle;// 던질 각도 (0~90도)
    //폭탄 관련 
    public bool boomPickingAni;
    public bool boomOn;
    public bool boomThrowing;
    public int boomint;
    GameObject boom;
    public GameObject insBoom;
    public bool boomtype;
    GameManager gameManager;
    /******************************행동end******************************/
    //아이템
    public int jumpCount;
    public bool keyOn;
    Tilemap tilemap;
    bool isBoxPickingRunning;
    bool isBomPickingRunning;
    StageGameManager stageGameManager;

    //포션
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

            // 플레이어 행동
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

                    // 알파값 변경
                    spriteColor.a = sptiteAlpha;

                    // 변경된 색상을 스프라이트에 적용
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
    /* 행동 */
    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) keyRight = true;
        else keyRight = false;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) keyLeft = true;
        else keyLeft = false;

        if (keyRight)//오른쪽
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
            if (boxOn)//박스들고 뛸경우
            {
                animator.SetBool("BoxIdle", false);
                animator.SetBool("BoxRun", true);
            }
            else if (boomOn)
            {
                animator.SetBool("BoomIdle", false);
                animator.SetBool("BoomRun", true);
            }
            else//박스 없이 그냥
            {
                animator.SetBool("Run", true);
            }

        }
        else if (keyLeft)//왼쪽
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
            if (boxOn)//박스들고 뛸경우
            {
                animator.SetBool("BoxIdle", false);
                animator.SetBool("BoxRun", true);
            }
            else if (boomOn)//폭탄 들고 띄기
            {
                animator.SetBool("BoomIdle", false);
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
    // 박스 던지기
    void BoxThrowing()
    {
        if (Input.GetKey(KeyCode.E) && canBoxThrow) // GetKeyDown으로 변경하고 canThrow 조건 추가
        {

            boxint += 1;
            throwingCoolTime = 0;
            boxThrowing = true;
            canBoxThrow = false; // 재생 중일 때는 재생 방지 변수를 false로 설정
            StartCoroutine(BoxThrowingCoroutine());
        }

    }
    //폭탄 던지기
    void BoomThrowing()
    {
        throwingCoolTime += Time.deltaTime;
        if (throwingCoolTime >= 1.5f)
        {

            if (Input.GetKey(KeyCode.E) && canBoomThrow) // GetKeyDown으로 변경하고 canThrow 조건 추가
            {
                boomint += 1;

                throwingCoolTime = 0;
                boomThrowing = true;
                canBoomThrow = false; // 재생 중일 때는 재생 방지 변수를 false로 설정
                StartCoroutine(BoomThrowingCoroutine());
            }
        }

    }
    // 점프
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

    //폭탄 잡기
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
        // 박스를 들었을 때
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
     행동 코루틴 Start
    *******************************************************************************************************************/
    // 박스 잡을 때
    IEnumerator BoxPickingAni()
    {
        canBoxThrow = true;
        isBoxPickingRunning = true; // 코루틴이 실행 중임을 나타내는 변수를 true로 설정
        animator.SetBool("BoxPicking", true);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("BoxPicking", false);
        animator.SetBool("BoxIdle", true);
        animator.SetBool("Run", false);
        boxOn = true;
        boxPickingAni = false;
        isBoxPickingRunning = false; // 코루틴이 종료되었음을 나타내는 변수를 false로 설정
    }

    // 박스 던질 때
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

        canBoxThrow = true; // 애니메이션이 끝나면 재생 방지 변수를 true로 설정
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
        Debug.Log("던지기 false");
        canBoomThrow = true; // 애니메이션이 끝나면 재생 방지 변수를 true로 설정
    }
    /******************************************************************************************************************
    행동 코루틴 End
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

            // 힘을 가해 박스를 던짐
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

            // 힘을 가해 박스를 던짐
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

                // 힘을 가해 박스를 던짐
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

                // 힘을 가해 폭탄을 던짐
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

