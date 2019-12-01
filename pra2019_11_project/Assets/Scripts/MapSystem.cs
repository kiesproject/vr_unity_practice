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
    private List<GameObject> listObject = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        labyrinth = GameManager.instance.labyrinth;
        rectTransform = transform as RectTransform;
        sizeTile = new Vector2Int((int)(tile.transform as RectTransform).sizeDelta.x ,(int)(tile.transform as RectTransform).sizeDelta.y);

        //Set_MapRoomAll();
    }

    // Update is called once per frame
    void Update()
    {
        if ((labyrinth != null) && labyrinth.isCreatedData() && GameManager.instance.stageType == GameManager.StageType.LABYRINTH)
        {
            Display_Player();
            MapSystem_Update();
        }
    }

    public void Set_MapTile(int x, int y, Color color)
    {
        if (!tileList.Contains(x + labyrinth.horizontal_size * y))
        {
            tileList.Add(x + labyrinth.horizontal_size * y);
            var c = new Color(color.r, color.g, color.b, aColor);
            GameObject o = Instantiate(tile, Vector3.zero, Quaternion.identity);
            listObject.Add(o);

            o.transform.parent = this.transform;
            (o.transform as RectTransform).localPosition = new Vector3(startPoss.x - x * sizeTile.x, startPoss.y - y * sizeTile.y, 0);
            
            o.GetComponent<Image>().color = c;
        }
    }

    public void Set_MapRoomAll()
    {
        if (!labyrinth.isCreatedData()) return;

        for (int i=0; i < labyrinth.vertical_size-1; i++)
        {
            for (int j=0; j < labyrinth.horizontal_size-1; j++)
            {
                switch(labyrinth.Get_TileData(i, j).TileID)
                {
                    case 2:
                        TileData data = labyrinth.Get_TileData(i, j);
                        if (data.ItemID == 1)
                        {
                            Set_MapTile(i, j, Color.yellow);
                        }
                        else if (data.ItemID == 2 || data.ItemID == 4)
                        {
                            Set_MapTile(i, j, Color.cyan);
                        }
                        else if (data.ItemID == 3)
                        {
                            Set_MapTile(i, j, Color.green);
                        }
                        else
                        {
                            Set_MapTile(i, j, Color.blue);
                        }
                        break;
                    case 3:
                        Set_MapTile(i, j, Color.grey);
                        break;
                    case 4:
                        Set_MapTile(i, j, Color.white);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void Set_MapRoom(int x, int y, Color color)
    {
        TileData data = labyrinth.Get_TileData(x, y);
        if (data.ItemID == 1)
        {
            Set_MapTile(x, y, Color.yellow);
        }
        else if (data.ItemID == 2 || data.ItemID == 4)
        {
            Set_MapTile(x, y, Color.cyan);
        }
        else if (data.ItemID == 3)
        {
            Set_MapTile(x, y, Color.green);
        }
        else
        {
            Set_MapTile(x, y, color);
        }


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
            //Debug.Log(string.Format("ID: {0} X: {1} Y: {2}", id, poss.x, poss.y));
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

    public void Reset_Map()
    {
        tileList.Clear();
        foreach(var o in listObject)
        {
            Destroy(o);
        }

    }
}
