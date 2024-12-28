using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Boom : MonoBehaviour
{
    public Transform moveLoad;
    public bool ONOff;
    Player player;
    new BoxCollider2D collider2D;

    public Tilemap destructibleTilemap;
    public Tile destroyedTile;
    public LayerMask projectileLayer;
    public string[] boomTag;
    new Rigidbody2D rigidbody2D;
    public int boomCount;
    Animator animator;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // 타일맵 초기화
        if (destructibleTilemap != null)
        {
            destructibleTilemap.CompressBounds();
        }
    }
    void Update()
    {
        if (ONOff)
        {
            transform.position = new Vector3(moveLoad.position.x, moveLoad.position.y + 0.8f, 0);
        }
        else return;
        player = FindObjectOfType<Player>();
        if (player.jumpBool == true)
        {
            ONOff = false;
            collider2D = GetComponent<BoxCollider2D>();
            collider2D.isTrigger = true;
        }
        else
        {
            collider2D = GetComponent<BoxCollider2D>();
            collider2D.isTrigger = false;
        }
    }
    public void exAni()
    {
        animator.SetBool("exBool", true);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            if (destructibleTilemap != null && collision.gameObject.CompareTag(boomTag[0]))
            {
                boomCount += 1;
                Debug.Log("OnCollisionEnter2D" + boomCount);
                Vector3 contactPoint = collision.GetContact(0).point;
                Vector3Int tilePos = destructibleTilemap.WorldToCell(contactPoint);
                // 파괴 가능한 타일을 파괴 처리
                destructibleTilemap.SetTile(tilePos, destroyedTile);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception in OnCollisionEnter2D: " + ex.Message);
        }

        if (boomCount == 4)
        {
            animator.SetInteger("BoomCount", boomCount);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            animator.enabled = true;
            Destroy(gameObject, 0.5f);
            rigidbody2D.gravityScale = 0;
        }    
        if (collision.gameObject.CompareTag("JumpItem"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            animator.enabled = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MoveLoad"))
        {
            ONOff = true;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            animator.enabled = true;
            rigidbody2D.gravityScale = 0;
            Destroy(gameObject,0.5f);
        }
    
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(boomTag[0]))
        {
            Vector3 contactPoint = collision.ClosestPoint(transform.position);
            Vector3Int tilePos = destructibleTilemap.WorldToCell(contactPoint);
            boomCount += 1;
            // 파괴 가능한 타일을 파괴 처리
            destructibleTilemap.SetTile(tilePos, destroyedTile);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MoveLoad"))
        {
            collider2D = GetComponent<BoxCollider2D>();
            //animator.enabled = true;
            boomCount += 1;
            collider2D.isTrigger = false;
        }
    }
}
