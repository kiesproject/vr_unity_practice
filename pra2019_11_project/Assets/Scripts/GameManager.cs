using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public State state = State.TITLE;
    public Labyrinth labyrinth;
    public Player player;

    private const string stageName = "Stage";
    private Scene stageScene;

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

    }

    // Start is called before the first frame update
    void Start()
    {
        if (labyrinth != null)
        {
            labyrinth.Create_Labyrinth();
            Move_FirstPoss();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
