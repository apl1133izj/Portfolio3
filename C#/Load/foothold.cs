using UnityEngine;
using UnityEngine.Tilemaps;

public class foothold : MonoBehaviour
{

    TilemapCollider2D tilemapCollider2D;
    Player player;
    BoxCollider2D boxCollider2D;
    posion pos;
    GameManager gameManager;
    private void Awake()
    {
        
        gameManager = FindObjectOfType<GameManager>();
        pos = FindObjectOfType<posion>();
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {tilemapCollider2D = FindObjectOfType<TilemapCollider2D>();
        if (pos == null)
        {
            pos = FindObjectOfType<posion>();
        }
        if (pos == null)
        {
            player = FindObjectOfType<Player>();
        }

        if (gameManager.stageSatting == GameManager.StageSatting.stage2 && player.jumpBool == true)//점프했을경우 타일맵이 트리거로 바뀜
        {
            tilemapCollider2D = GetComponent<TilemapCollider2D>();
            tilemapCollider2D.isTrigger = true;

        }
        else
        {
            tilemapCollider2D = GetComponent<TilemapCollider2D>();
            tilemapCollider2D.isTrigger = false;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tilemapCollider2D.isTrigger = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Put"))
        {
            player.jumpBool = false;
        }
    }
}
