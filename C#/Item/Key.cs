using UnityEngine;
using System.Collections;
public class Key : MonoBehaviour
{
    public GameObject itemEffect;
    GameManager gameManager;
    GameObject[] load;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager.stageSatting == GameManager.StageSatting.stage3)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameObject.Find("Load").transform.Find("Load1").gameObject.SetActive(true);
                GameObject.Find("Load").transform.Find("Load2").gameObject.SetActive(true);
                GameObject.Find("Load").transform.Find("Load3").gameObject.SetActive(true);
                GameObject.Find("Load").transform.Find("tilelMapChangePosion").gameObject.SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(itemEffect);
            effect.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }
}
