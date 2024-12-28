using UnityEngine;

public class MoveLoad : MonoBehaviour
{
    Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentXPosition = transform.localPosition;
        currentXPosition.x = Mathf.Clamp(transform.localPosition.x,3f,13.5f);
        transform.localPosition = currentXPosition;

        if (player.keyRight)
        {
            transform.Translate(new Vector2(5 * Time.deltaTime, 0));
        }
        if (player.keyLeft)
        {
            transform.Translate(new Vector2(-5 * Time.deltaTime, 0));
        }
    }


}
