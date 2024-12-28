using System.Collections;
using TMPro;
using UnityEngine;

public class barricade : MonoBehaviour
{
    public int barricadeCount;//ÆøÅºÀÌ ºÎµúÈù È½¼ö
    public Animator barricadeAnimator;
    public Rigidbody2D[] rigidbody2Ds;
    public BoxCollider2D boxCollider2D;

    public TextMeshProUGUI posionText;
    GameObject boomON;
    void Update()
    {
        boomON = GameObject.Find("Bomb1(Clone)");
        if (barricadeCount == 8)
        {
            StartCoroutine("barricadeCoroutine");
        }
        posionText.text = "X" + (8 - barricadeCount);
    }
    IEnumerator barricadeCoroutine()
    {
        boxCollider2D.enabled = false;
        barricadeAnimator.enabled = true;
        for (int i = 0; i <= 2; i++)
        {
            rigidbody2Ds[i].bodyType = RigidbodyType2D.Dynamic;
        }
        yield return new WaitForSeconds(2f);
        Player player = FindObjectOfType<Player>();
        player.keyOn = true;
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boom"))
        {
            Player player = FindObjectOfType<Player>();
            if (boomON != null && boomON.transform.localScale == new Vector3(2, 2, 0))
            {
                barricadeCount += 2;
            }
            else
            {
                barricadeCount += 1;
            }
        }
    }
}
