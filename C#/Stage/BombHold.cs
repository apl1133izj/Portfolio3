using UnityEngine;

public class BombHold : MonoBehaviour
{
    Player player;
    BoxCollider2D boxCollider2D;
    posion pos;
    Animator animator;
    public int[] boomCount;

    private void Awake()
    {
        pos = FindObjectOfType<posion>();
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        //플레이어가 점프시 isTrigger 로 바뀌면서 아래로 떨어짐
        if (player.jumpBool == true && pos.posionTypr == posion.PosionTypr.posion1)//점프했을경우
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;
        }
        else
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = false;
        }
        if (boomCount[3] == 1)
        {
            BoomCountDesTory();
        }
    }
    void BoomCountDesTory()
    {

        Destroy(gameObject);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //4번 부딪치면 폭발 애니메이션 활성화
        if (collision.gameObject.name == "Load4")
        {
            GetComponent<Animator>().enabled = true;
        }
        if (collision.gameObject.name == "Boom1")
        {
            boomCount[0] += 1;
        }
        if (collision.gameObject.name == "Boom2" && boomCount[0] == 1)
        {
            boomCount[1] += 1;
        }
        if (collision.gameObject.name == "Boom3" && boomCount[1] == 1)
        {
            boomCount[2] += 1;
        }
        if (collision.gameObject.name == "Boom4" && boomCount[2] == 1)
        {
            boomCount[3] += 1;
        }
    }

}
