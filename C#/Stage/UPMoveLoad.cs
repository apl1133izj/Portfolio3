using UnityEngine;

public class UPMoveLoad : MonoBehaviour
{
    public bool onUPBool;
    public bool onDownBool;
    bool moveLoadBool;
    public enum UpmoveNum { Upmoveload1, Upmoveload2, Upmoveload3, Upmoveload4, Upmoveload5 };
    public UpmoveNum upmoveNum;
    public enum LoadNum { load1, load2, load3, load4, load5 };
    public LoadNum loadNum;
    float desTime;
    Animator animator;
    void Update()
    {
        UpMove();
        GameObject jumItem = GameObject.Find("JumpItem");
        if (!jumItem)
        {
            desTime += Time.deltaTime;
            if (desTime >= 2)
            {
                animator = GetComponentInParent<Animator>();
                animator.enabled = true;
            }

        }
    }

    void UpMove()
    {

        if (upmoveNum == UpmoveNum.Upmoveload1)
        {

            if (onUPBool && transform.localPosition.y <= -4.71f)
            {
                transform.Translate(Vector2.up * 1.5f * Time.deltaTime);
            }
            if (transform.localPosition.y >= -4.71f)
            {
                onDownBool = true;
            }
            if (onDownBool && transform.localPosition.y >= -6.864f)
            {
                transform.Translate(Vector2.down * 1.5f * Time.deltaTime);
            }

        }
        if (upmoveNum == UpmoveNum.Upmoveload2)
        {

            if (onUPBool && transform.localPosition.y <= -2.701f)
            {
                transform.Translate(Vector2.up * 1.5f * Time.deltaTime);
            }
            if (transform.localPosition.y >= -2.701f)
            {
                onDownBool = true;
            }
            if (onDownBool && transform.localPosition.y >= -4.69f)
            {
                transform.Translate(Vector2.down * 1.5f * Time.deltaTime);
            }
        }
        if (upmoveNum == UpmoveNum.Upmoveload3)
        {

            if (onUPBool && transform.localPosition.y <= -0.694f)
            {
                transform.Translate(Vector2.up * 1.5f * Time.deltaTime);
            }
            if (transform.localPosition.y >= -0.694f)
            {
                onDownBool = true;
            }
            if (onDownBool && transform.localPosition.y >= -2.701f)
            {
                transform.Translate(Vector2.down * 1.5f * Time.deltaTime);
            }
        }
        if (upmoveNum == UpmoveNum.Upmoveload4)
        {

            if (onUPBool && transform.localPosition.y <= 1.298f)
            {
                transform.Translate(Vector2.up * 1.5f * Time.deltaTime);
            }
            if (transform.localPosition.y >= 1.298f)
            {
                onDownBool = true;
            }
            if (onDownBool && transform.localPosition.y >= -0.7f)
            {
                transform.Translate(Vector2.down * 1.5f * Time.deltaTime);
            }
        }

    }

    void moveLoad()
    {
        if (loadNum == LoadNum.load1)
        {
            if (moveLoadBool)
            {
                GetComponent<Animator>().enabled = true;
            }
        }
        if (loadNum == LoadNum.load2)
        {
            if (moveLoadBool)
            {
                GetComponent<Animator>().enabled = true;
            }
        }
        if (loadNum == LoadNum.load3)
        {
            if (moveLoadBool)
            {
                GetComponent<Animator>().enabled = true;
            }
        }
        if (loadNum == LoadNum.load4)
        {
            if (moveLoadBool)
            {
                GetComponent<Animator>().enabled = true;
            }
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onUPBool = true;
            onDownBool = false;
        }
        if (collision.gameObject.name == "Load1")
        {
            moveLoadBool = true;
        }
        if (collision.gameObject.name == "Load2")
        {
            moveLoadBool = true;
        }
        if (collision.gameObject.name == "Load3")
        {
            moveLoadBool = true;
        }
        if (collision.gameObject.name == "Load4")
        {
            moveLoadBool = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onUPBool = false;
        }
        if (collision.gameObject.name == "Load1")
        {
            moveLoadBool = false;
        }
        if (collision.gameObject.name == "Load2")
        {
            moveLoadBool = false;
        }
        if (collision.gameObject.name == "Load3")
        {
            moveLoadBool = false;
        }
        if (collision.gameObject.name == "Load4")
        {
            moveLoadBool = false;
        }
    }
}
