using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public State state = State.TITLE; //ゲームの状態
    public StageType stageType = StageType.FLAT; //迷路の種類
    public LayerMask BlockLayer; //ブロックのレイヤーマスク
    [SerializeField]
    private int keyState = 0; //鍵の所持数

    // --- --- --- --- --- --- --- --- --- --- ---
    public Labyrinth labyrinth;
    public Player player;
    public MapSystem mapSystem;
    public ThroughMassage throughMassage;
    public BGeffect gbEffect;
    private const string stageName = "Stage";
    private Scene stageScene;

    [HideInInspector] public bool cursorOnUI = false;
    [HideInInspector] public int cursorInventory = 0;

    [HideInInspector] public List<Item> itemList;
    [HideInInspector] public int itemCapa = 5;
    [HideInInspector] public int golds = 0;
    [HideInInspector] public int bullets = 10;
    [HideInInspector] public float luck = 0;
    [HideInInspector] public float unluck = 0;
    [HideInInspector] public int currentFloor = 1;
    public GameObject GameOver;

    public enum StageType
    {
        FLAT, LABYRINTH, MERO
    }

    public enum State
    {
        TITLE, LOAD, GAME, CLEAR, GAMROVER, PAUSE
    }

    private void Awake()
    {
        //ゲームマネージャーは何処からでもアクセスできる。
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //シーンをまたいでもこのゲームオブジェクトを消去しない。
        } else {
            Destroy(gameObject);
        }

        stageScene = SceneManager.GetSceneByName(stageName);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //初期設定
        itemList = new List<Item>();
        var w = new Weapon() { Name = "最初のハンドガン" };
        w.Update_Desc();
        itemList.Add(w);
        player.weapon = w;


    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.GAME;
        Create_Labyrinth();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("cursorOnUI :"+ cursorOnUI);
    }

    public void Create_Labyrinth()
    {
        if (labyrinth != null && stageType == StageType.LABYRINTH)
        {
            labyrinth.Create_Labyrinth();
            
            Move_FirstPoss();
            labyrinth.Put_Enemy(6);
        }
    }

    /// <summary>
    /// ゲームオブジェクトをステージシーンに移動する。
    /// </summary>
    /// <param name="gameObject"></param>
    public void Move_ObjectToScene(GameObject gameObject)
    {
        SceneManager.MoveGameObjectToScene(gameObject ,stageScene);
    }

    public Vector3Int Get_PlayerPossToMap()
    {
        if (player == null) { Debug.LogError("playerが登録されていません"); return new Vector3Int(); }
        if (labyrinth == null) { Debug.LogError("labyrinthが登録されていません"); return new Vector3Int(); }

        Vector3 playerPoss = player.transform.position - labyrinth.baseposs_DG;
        
        Vector3 size = labyrinth.sizeBlock;
        Vector3Int playerPossToMap = new Vector3Int((int)Mathf.Round(playerPoss.x / size.x), (int)Mathf.Round(playerPoss.z / size.y), 0);
        return playerPossToMap;
    }

    /// <summary>
    /// 指定した座標にプレイヤーを転送する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Move_PlayerToMap(int x, int y)
    {
        if (player == null) { Debug.LogError("playerが登録されていません"); return false; }
        if (labyrinth == null) { Debug.LogError("labyrinthが登録されていません"); return false; }

        int id = labyrinth.Get_TileData(x, y).TileID;
        
        if (2 <= id && id <= 4)
        {
            player.Telport(new Vector3(x, 0.5f, y));
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 最初の位置を決定・設置
    /// </summary>
    public void Move_FirstPoss()
    {
        if (labyrinth == null) { Debug.LogError("labyrinthが登録されていません"); return; }
        var list = labyrinth.Get_RoomDatas();
        RoomData room = list[Random.Range(0, list.Count)];
        
        for (int i=1; i < room.height-2; i++)
        {
            for (int j=1; j < room.width-2; j++)
            {
                if(labyrinth.Get_TileData(room.x + i, room.y + j).TileID == 2)
                {
                    Move_PlayerToMap(room.x + i, room.y + j);
                }
            }
        }

    }

    /// <summary>
    /// アイテムをリストに追加する
    /// </summary>
    /// <param name="item"></param>
    public bool Add_Item(Item item)
    {
        if (itemList.Count < itemCapa)
        {
            itemList.Add(item);
            return true;
        }
        return false;
    }

    public void Add_Key(int value)
    {
        keyState += value;
    }

    public int Get_KeyState()
    {
        return keyState;
    }

    public void Clear_KeyState()
    {
        keyState = 0;
    }

    static public bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    /// <summary>
    /// 次のフロアへ
    /// </summary>
    public void NextStage()
    {
        StartCoroutine(IE_NextStage());

    }

    /// <summary>
    /// 次のフロアへ
    /// </summary>
    /// <returns></returns>
    private IEnumerator IE_NextStage()
    {
        state = State.LOAD;
        throughMassage.accese = null;
        gbEffect.gameObject.SetActive(true);
        keyState = 0;
        currentFloor++;

        gbEffect.textF.text ="F" + currentFloor.ToString();
        for (int i=0; i <= 36; i++)
        {
            yield return null;
            gbEffect.E_BG_Effect(i*10, true);
        }

        for (int i=0; i<=5; i++)
        {
            yield return null;
            gbEffect.E_Floor_Effect(i*20, true);
        }

        yield return new WaitForSeconds(2);

        labyrinth.Clear_Labyrinth(); //迷宮データを削除する
        Create_Labyrinth(); //迷宮データを作成する
        mapSystem.Reset_Map(); //マップを初期化

        for (int i = 0; i <= 5; i++)
        {
            yield return null;
            gbEffect.E_Floor_Effect(i*20, false);
        }

        for (int i = 0; i <= 36; i++)
        {
            yield return null;
            gbEffect.E_BG_Effect(i*10, false);
        }

        gbEffect.gameObject.SetActive(false);
        state = State.GAME;
    }

    
    /// <summary>
    /// アイテムを取得する
    /// </summary>
    public void GetItem()
    {
        if (player != null)
        {
            if(player.culletTarget != null)
            {

                player.culletTarget.Get_Item();
                cursorInventory = itemList.Count - 1;

            }
        }

    }

    public void Set_Weapon(int index)
    {
        if (player != null)
        {
            if (itemList[index].GetType() == typeof(Weapon))
            {
                player.weapon = itemList[index] as Weapon;
            }
        }
    }

    public void Set_Armor(int index)
    {
        if (player != null)
        {
            if (itemList[index].GetType() == typeof(Armor))
            {
                player.armor = itemList[index] as Armor;
            }
        }
    }

    /// <summary>
    /// インベントリの参照番号を進める
    /// </summary>
    /// <param name="upDOWN"></param>
    public void Scroll_Inventory(bool upDOWN)
    {
        if (upDOWN)
        {
            cursorInventory--;
            if(cursorInventory < 0)
            {
                cursorInventory = itemList.Count - 1;
            }
        }
        else
        {
            cursorInventory++;
            if (cursorInventory > itemList.Count - 1)
            {
                cursorInventory = 0;
            }
        }
        
        
    }

    public void UseItem()
    {
        if (itemList.Count > 0)
        {
            itemList[cursorInventory].Use();

            if (itemList[cursorInventory].GetType() == typeof(Drink)) ItemDelete();
        }
    }

    public void ItemDelete()
    {
        if (itemList.Count > 0)
        {
            itemList.RemoveAt(cursorInventory);
            if(itemList.Count-1 < cursorInventory)
            {
                cursorInventory = 0;
            }
        }
    }

}
