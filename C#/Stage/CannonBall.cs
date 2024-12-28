using UnityEngine;

public class CannonBall : MonoBehaviour
{
    Animator animator;
    int speed;
    void Start()
    {
        speed = 20;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, speed * Time.deltaTime));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MoveLoad"))
        {
            animator.enabled = true;
            speed = 0;
            Destroy(gameObject, 0.5f);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(collision.gameObject);
            transform.rotation = Quaternion.Euler(0, 0, 180);
            Destroy(gameObject, 0.5f);
            animator.enabled = true;
            speed = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.5f);
            animator.enabled = true;
            speed = 0;
        }
    }
}
