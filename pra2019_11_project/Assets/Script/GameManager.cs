using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //*** ===============================
    //*** [改善]コメントを書いてください
    //*** ===============================

    [SerializeField] private int floor = 0;
    [SerializeField] private int squareSize = 0;
    [SerializeField] private int meiroNeighborhood = 0;
    [SerializeField] private int spaceMax = 0, spaceMin = 0;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private GameObject[] labyrinthObject;
    [SerializeField] private MeiroGeneraterSquare mgs;
    [SerializeField] private int[] MeiroInt;
    [SerializeField] private int[][] spaceInt;
    [SerializeField] private int start, goal;
    [SerializeField] private bool Meirokansei;
    [SerializeField] private int PlayerKoudou;
    [SerializeField] private MeiroGeneraterSquare meiro;
    private int[] meiroInt;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    /*
    void Start()
    {
        if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
		DontDestroyOnLoad(this.gameObject);
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }

    //*** ===================================
    //*** カプセル化を意識していていいだろう
    //*** ===================================

    public int GetFloor()
    {
        return floor;
    }

    public GameObject[] GetMeiroObject()
    {
        return labyrinthObject;
    }

    public int GetSquareSize()
    {
        return squareSize;
    }

    public int GetSpaceMin()
    {
        return spaceMin;
    }

    public int GetSpaceMax()
    {
        return spaceMax;
    }

    public int GetMeiroNeighborhood()
    {
        return meiroNeighborhood;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void SetPlayer(GameObject game)
    {
        player = game;
    }

    public void SetStart(int s)
    {
        start = s;
    }

    public void SetMap(int[] a)
    {
        MeiroInt = a;
    }

    public int[] GetMap()
    {
        return MeiroInt;
    }
    public void SetPlayerKoudou(int a)
    {
        PlayerKoudou = a;
    }
    public int GetPlayerKoudou()
    {
        return PlayerKoudou;
    }

    public GameObject[] GetEnemyObject()
    {
        return enemy;
    }
    public void SetSpace(int[][] vs)
    {
        spaceInt = vs;
    }

    public int[][] GetSpace()
    {
        return spaceInt;
    }

    public void SetMeiro(MeiroGeneraterSquare a)
    {
        meiro = a;
    }

    public MeiroGeneraterSquare GetMeiro()
    {
        return meiro;
    }
}
