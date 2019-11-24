using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSystem : MonoBehaviour
{
    public GameObject tile;

    [SerializeField]
    private Vector2 startPoss; //マップ上のスタート座標

    [SerializeField]
    private GameObject playerTile;

    private Vector2Int sizeTile = new Vector2Int(); //タイルのサイズ
    private float aColor = 0.5f;

    private RectTransform rectTransform;
    private Labyrinth labyrinth;

    private List<int> tileList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        labyrinth = GameManager.instance.labyrinth;
        rectTransform = transform as RectTransform;
        sizeTile = new Vector2Int((int)(tile.transform as RectTransform).sizeDelta.x ,(int)(tile.transform as RectTransform).sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (labyrinth == null) return;
        Display_Player();
        MapSystem_Update();
    }

    public void Set_MapTile(int x, int y, Color color)
    {
        if (!tileList.Contains(x + labyrinth.horizontal_size * y))
        {
            tileList.Add(x + labyrinth.horizontal_size * y);
            var c = new Color(color.r, color.g, color.b, aColor);
            GameObject o = Instantiate(tile, Vector3.zero, Quaternion.identity);
            o.transform.parent = this.transform;
            (o.transform as RectTransform).localPosition = new Vector3(startPoss.x - x * sizeTile.x, startPoss.y - y * sizeTile.y, 0);
            //(o.transform as RectTransform).anchoredPosition = new Vector3(startPoss.x - x * sizeTile.x, startPoss.y - y * sizeTile.y, 0);
            //Debug.Log((o.transform as RectTransform).localPosition);
            
            o.GetComponent<Image>().color = c;
        }
    }

    void Set_MapRoom(int x, int y, Color color)
    {
        Set_MapTile(x, y, color);

        Vector2[] dir =
        {
            new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1)
        };

        foreach (Vector2 d in dir) {
            int dx = x + (int)d.x;
            int dy = y + (int)d.y;

            var up = labyrinth.Get_TileData(dx, dy);
            if (2 == up.TileID)
            {
                if (!tileList.Contains(dx + labyrinth.horizontal_size * dy))
                {
                    Set_MapRoom(dx, dy, color);
                }
            }

            if (3 == up.TileID)
            {
                Set_MapTile(dx, dy, Color.gray);
            }
        }
    }

    void MapSystem_Update()
    {
        if (GameManager.instance != null)
        {
            Vector3Int poss = GameManager.instance.Get_PlayerPossToMap();
            int id = labyrinth.Get_TileData(poss.x, poss.y).TileID;
            Debug.Log(string.Format("ID: {0} X: {1} Y: {2}", id, poss.x, poss.y));
            switch (id)
            {
                case 4:   
                    {
                        Set_MapTile(poss.x, poss.y, Color.white);
                    }
                    break;
                case 3:
                case 2:
                    {
                        Set_MapRoom(poss.x, poss.y, Color.blue);
                    }
                    break;
                default:
                    break;
            }


        }

    }

    void Display_Player()
    {
        if (playerTile == null) return;
        Vector3Int poss = GameManager.instance.Get_PlayerPossToMap();
        (playerTile.transform as RectTransform).localPosition = new Vector3(
                startPoss.x - poss.x * sizeTile.x, 
                startPoss.y - poss.y * sizeTile.y, 
                playerTile.transform.localPosition.z);
    }
}
