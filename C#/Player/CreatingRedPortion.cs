using UnityEngine;

public class CreatingRedPortion : MonoBehaviour
{
    public GameObject insRedPosion;
    public float CreatingRedPortionTime;
    public Vector2 insPos;
    // Update is called once per frame
    private void Start()
    {//new Vector2(8.277f, -4.537f)
        GameObject RedPosion = Instantiate(insRedPosion, new Vector2(8.277f, -4.537f), Quaternion.identity);
    }
    void Update()
    {
        CreatingRedPortionTime += Time.deltaTime;
        if (CreatingRedPortionTime >= 15)
        {
            GameObject RedPosionFind = GameObject.Find("redPosion");
            GameObject RedPosionCloneFind = GameObject.Find("redPosion(Clone)");
            if (RedPosionFind)
            {
                CreatingRedPortionTime = 0;
                
            }
            else if (RedPosionCloneFind)
            {
                CreatingRedPortionTime = 0;
            }
            else
            {
                GameObject RedPosion = Instantiate(insRedPosion, new Vector2(8.277f, -4.537f) , Quaternion.identity);
                CreatingRedPortionTime = 0;
            }
        }

    }
}
