using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infinitecloud : MonoBehaviour
{
    public GameObject[] infinitecloudGameObject1;//0일반 구름 1느린구름
    public GameObject[] infinitecloudGameObject2;//0일반 구름 1느린구름
    public float cloud1Speed;
    public float cloud1SlowSpeed;

    public float cloud2Speed;
    public float cloud2SlowSpeed;
    void Update()
    {
        //구름 이동
        infinitecloudGameObject1[0].transform.Translate(new Vector2(cloud1Speed * Time.deltaTime,0));
        infinitecloudGameObject1[1].transform.Translate(new Vector2(cloud1SlowSpeed * Time.deltaTime, 0));
        infinitecloudGameObject2[0].transform.Translate(new Vector2(cloud2Speed * Time.deltaTime, 0));
        infinitecloudGameObject2[1].transform.Translate(new Vector2(cloud2SlowSpeed * Time.deltaTime, 0));

        //종료 위치로 갈시 시작 위치로 이동
        if(infinitecloudGameObject1[0].transform.localPosition.x <= -25.71429f)
        {
            infinitecloudGameObject1[0].transform.localPosition = new Vector2(28.2f, 0f);
        }
        if (infinitecloudGameObject2[0].transform.localPosition.x <= -25.71429f)
        {
            infinitecloudGameObject2[0].transform.localPosition = new Vector2(28.2f, 0f);
        }
        if (infinitecloudGameObject1[1].transform.localPosition.x <= -50.14f)
        {
            infinitecloudGameObject1[1].transform.localPosition = new Vector2(10.2f, 0f);
        }
        if (infinitecloudGameObject2[1].transform.localPosition.x <= -50.14f)
        {
            infinitecloudGameObject2[1].transform.localPosition = new Vector2(10.2f, 0f);
        }
    }
}
