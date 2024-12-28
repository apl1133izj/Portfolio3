using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    Animator animator;
    public GameObject Boom;
    public Transform insPos;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void InvokeAniFalse()
    {
        animator.enabled = false;
    }
    void InsBoom()
    {
        GameObject boom1 = Instantiate(Boom);
        boom1.transform.position = insPos.position; 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boom"))
        {
            animator.enabled = true;

            Destroy(collision.gameObject);
        }
        
    }
}
