using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator animator;
    public GameObject key;
    public GameObject chestEffect;
    new BoxCollider2D collider2D;
    new Rigidbody2D rigidbody2D;
    void Start()
    {
        animator = GetComponent<Animator>();
        collider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void insKey()
    {
        GameObject itemKey = Instantiate(key);
        itemKey.transform.position = new Vector2(transform.position.x, transform.position.y);
        collider2D.isTrigger = true;
        rigidbody2D.gravityScale = 0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(chestEffect);   
            effect.transform.position = new Vector2(transform.position.y, transform.position.y);
            animator.enabled = true;
        }
    }
}
