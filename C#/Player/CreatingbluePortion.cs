using UnityEngine;

public class CreatingbluePortion : MonoBehaviour
{
    public GameObject insbluePosion;
    public float CreatingbluePosionTime;
    public Vector2 insPos;
    // Update is called once per frame
    private void Start()
    {//new Vector2(8.277f, -4.537f)
        GameObject bluePosion = Instantiate(insbluePosion, insPos, Quaternion.identity);
    }
    void Update()
    {
        CreatingbluePosionTime += Time.deltaTime;
        if (CreatingbluePosionTime >= 10)
        {
            GameObject RedPosionFind = GameObject.Find("redPosion");
            GameObject RedPosionCloneFind = GameObject.Find("redPosion(Clone)");
            if (RedPosionFind)
            {
                CreatingbluePosionTime = 0;
                
            }
            else if (RedPosionCloneFind)
            {
                CreatingbluePosionTime = 0;
            }
            else
            {
                GameObject bluePosion = Instantiate(insbluePosion, insPos, Quaternion.identity);
                CreatingbluePosionTime = 0;
            }
        }

    }
}
