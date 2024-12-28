using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CamaraLimite : MonoBehaviour
{
    public float[] xMinLimite;
    public float[] xMaxLimite;
    public float[] yMinLimite;
    public float[] yMaxLimite;

    public Transform target;
    public GameManager manager;
    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }
    private void LateUpdate()
    {
        if(manager.stageSatting == GameManager.StageSatting.stage1)
        {
            transform.position = Vector3.Lerp(transform.position,target.position,Time.deltaTime * 5);
            transform.position = new Vector3(transform.position.x, 2f , -10f);
        }
        if (manager.stageSatting == GameManager.StageSatting.stage2)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 5);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
        if (manager.stageSatting == GameManager.StageSatting.stage3)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 5);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }

    }
}
