using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Sprite hitSprite; // 변경할 스프라이트
    private SpriteRenderer spriteRenderer;
    private Player player;
    public GameObject[] hitGameObject;

    public float explosionForce = 10f;
    GameObject piece;
    public GameObject[] item;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }
    IEnumerator boxHitEvent()
    {
        spriteRenderer.sprite = hitSprite;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
        GameObject itemJump = Instantiate(item[0], transform.position, Quaternion.identity);
        foreach (GameObject piecePrefab in hitGameObject)
        {
            //박스 파편 생성
            GameObject piece = Instantiate(piecePrefab, transform.position, Quaternion.identity);
            Rigidbody2D pieceRigidbody = piece.GetComponent<Rigidbody2D>();
            //힘 값을 랜덤으로
            float randomForceX = Random.Range(-explosionForce, explosionForce);
            float randomForceY = Random.Range(-explosionForce, explosionForce);
            //회전은 없이 방향은 랜덤한 힘으로
            pieceRigidbody.AddForce(new Vector2(randomForceX, randomForceY), ForceMode2D.Impulse);
            Destroy(piece, 1f);//1초뒤 파편 삭제
        }
        yield return new WaitForSeconds(1);
        Destroy(piece);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //박스가 벽이 부딪침
            StartCoroutine(boxHitEvent());
        }
    }
}
