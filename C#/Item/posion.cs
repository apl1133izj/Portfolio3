using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class posion : MonoBehaviour
{
    Player player;
    public enum PosionTypr { posion1, posion2, posion3 }
    public PosionTypr posionTypr;
    public GameObject posionOnOffGameObject;
    public GameObject posionChangeEffect;
    GameManager manager;
    public GameObject[] posionImage;
    Color color;
    public int buttoncount;
    public TextMeshProUGUI[] posionText;
    public Tilemap[] bluePosionTileMapChange;
    public TilemapCollider2D[] bluePosionCollider;
    public bool bluePosionOn;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();

    }
    private void LateUpdate()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        if (player.playerScaleX2Posion)
        {
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.4f);
            transform.localScale = new Vector2(1.5f, 1.5f);
        }
        else
        {
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
            transform.localScale = new Vector2(1f, 1f);
        }

    }
    void Update()
    {
        OnOffPosionGameObject();
        PosionChangeEffect();
        Posionpresence();
        OfftilelMapChangePosionBool();
    }
    void OnOffPosionGameObject()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            buttoncount += 1;
        }
        if (buttoncount == 1)
        {
            posionOnOffGameObject.SetActive(true);
        }
        if (buttoncount == 2)
        {
            posionOnOffGameObject.SetActive(false);
            buttoncount = 0;
        }
    }
    void OfftilelMapChangePosionBool()
    {
        if (player.tilelMapChangePosionBool)
        {
            if (Input.GetKey(KeyCode.Q) )
            {
                bluePosionCollider[1].enabled = true;
                bluePosionCollider[0].enabled = false;
                bluePosionTileMapChange[0].color = new Color(255, 255, 255, 1);
                bluePosionTileMapChange[1].color = new Color(255, 255, 255, 0f);
                player.tilelMapChangePosionBool = false;
            }
        }

    }
    void PosionChangeEffect()
    {
        if (buttoncount == 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                posionTypr = PosionTypr.posion1;
                posionChangeEffect.transform.localPosition = new Vector2(-1.085f, 1.165f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                posionTypr = PosionTypr.posion2;
                bluePosionOn = true;
                player.playerScaleX2PosionTime = 0f;
                posionChangeEffect.transform.localPosition = new Vector2(-0.018f, 1.165f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                posionTypr = PosionTypr.posion3;
                posionChangeEffect.transform.localPosition = new Vector2(0.909f, 1.165f);
            }
        }
    }

    void Posionpresence()
    {
        player = FindObjectOfType<Player>();
        posionTime();
        //포션1
        if (player.jumpCount >= 1)
        {
            posionImage[0].SetActive(true);
            posionImage[3].SetActive(false);
        }
        if (player.jumpCount <= 0)
        {
            posionImage[0].SetActive(false);
            posionImage[3].SetActive(true);
        }
        //포션2

        if (player.tilelMapChangePosionBool)
        {
            if (!bluePosionOn)
            {
                Debug.Log("??");
                bluePosionTileMapChange[0].color = new Color(1f, 1f, 1f, 1f);
                bluePosionTileMapChange[1].color = new Color(1f, 1f, 1f, 0.2f);
            }
            else
            {
                Debug.Log("?1");
                bluePosionTileMapChange[0].color = new Color(1f, 1f, 1f, 0.2f);
                bluePosionTileMapChange[1].color = new Color(1f, 1f, 1f, 1f);
                bluePosionCollider[1].enabled = true;
                bluePosionCollider[0].enabled = false;
            }
            posionImage[1].SetActive(true);
            posionImage[4].SetActive(false);
        }
        if (!player.tilelMapChangePosionBool)
        {
            posionImage[1].SetActive(false);
            posionImage[4].SetActive(true);
            bluePosionCollider[0].enabled = true;
            bluePosionCollider[1].enabled = false;
        }
        //포션3
        if (player.playerScaleX2Posion)
        {
            posionImage[2].SetActive(true);
            posionImage[5].SetActive(false);

        }
        if (!player.playerScaleX2Posion)
        {
            posionImage[2].SetActive(false);
            posionImage[5].SetActive(true);
        }
    }
    void posionTime()
    {
        posionText[0].text = "" + player.jumpCount;
        posionText[2].text = "" + math.round(player.playerScaleX2PosionTime);
        if (player.playerScaleX2PosionTime >= 9.9f)
        {
            posionText[2].transform.localPosition = new Vector3(1.91f, 0.6651001f);
        }
        else
        {
            posionText[2].transform.localPosition = new Vector2(2.028198f, 0.6651001f);
        }
    }
}
