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
        //�÷��̾ ������ isTrigger �� �ٲ�鼭 �Ʒ��� ������
        if (player.jumpBool == true && pos.posionTypr == posion.PosionTypr.posion1)//�����������
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
        //4�� �ε�ġ�� ���� �ִϸ��̼� Ȱ��ȭ
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
