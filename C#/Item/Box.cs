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
            GameObject piece = Instantiate(piecePrefab, transform.position, Quaternion.identity);
            Rigidbody2D pieceRigidbody = piece.GetComponent<Rigidbody2D>();
            float randomForceX = Random.Range(-explosionForce, explosionForce);
            float randomForceY = Random.Range(-explosionForce, explosionForce);
            pieceRigidbody.AddForce(new Vector2(randomForceX, randomForceY), ForceMode2D.Impulse);
            Destroy(piece, 1f);
        }
        yield return new WaitForSeconds(1);
        Destroy(piece);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(boxHitEvent());
        }
    }
}
