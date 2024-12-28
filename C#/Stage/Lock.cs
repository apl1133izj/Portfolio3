using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    Door door;
    SpriteRenderer spriteRenderer;
    public Sprite[] lockSprites;
    new Rigidbody2D rigidbody2D;
    public bool openDoorAniBool;
    public GameObject instEffect;
    void Start()
    {
        door = GetComponentInParent<Door>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (door.boolLockOpen)
        {
            StartCoroutine(lockAni());
        }
        else
        {
            StopCoroutine(lockAni());
        }  
    }
    IEnumerator lockAni()
    {
        spriteRenderer.sprite = lockSprites[0];
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = lockSprites[1];
        rigidbody2D.gravityScale = 1;
        door.boolLockOpen = false;
        yield return new WaitForSeconds(0.4f);
        openDoorAniBool = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            GameObject effect = Instantiate(instEffect);
            effect.transform.position = new Vector2(transform.position.x, transform.position.y);
            Destroy(gameObject);
        }
    }
}
