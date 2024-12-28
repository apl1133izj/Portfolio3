using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class JewelryLevel : MonoBehaviour
{
    public GameObject jewelryAniAGameobjectTrue;
    public GameObject[] uijewelryFalse;
    GameManager gameManager;
    Animator animator;
    public GameObject textGame;
    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        
    }
    public void JewelryAniAGameobjectTrue()
    {
        jewelryAniAGameobjectTrue.SetActive(true);
        textGame.SetActive(true);
    }
    public void UijewelryFalse1()//초록보석
    {
        if (gameManager.clearTimeBool[0])
        {
            //uijewelryFalse[0].SetActive(false);
            //uijewelryFalse[3].SetActive(false);
            Debug.Log(1);
        }
        else
        {
            animator.enabled = false;
        }

    }
    public void UijewelryFalse2()//빨간보석
    {
        if (gameManager.clearTimeBool[1])
        {
            //uijewelryFalse[1].SetActive(false);
            //uijewelryFalse[4].SetActive(false);

            Debug.Log(2);
        }
        else
        {
            animator.enabled = false;
        }
    }
    public void UijewelryFalse3()//파란보석
    {
        if (gameManager.clearTimeBool[2])
        {
            //uijewelryFalse[2].SetActive(false);
            //uijewelryFalse[5].SetActive(false);
            
        }
        else
        {
            animator.enabled = false;
        }
    }
}
