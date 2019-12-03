using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiroGeneraterSquare : MonoBehaviour
{
    /*
     壁0
     スタート1
     ゴール2
     床3
     道4

         */

    //迷路縦横の初期サイズ
    private int meiroNeighborhood = 10;

    //迷路全体初期のサイズ
    private int meirosize = 0;

    //迷路の初期空間配列
    private int[] meiroInt = new int[400];

    //矩形の個数
    [SerializeField]
    private int squareSize = 0;

    //空間の配列を迷路空間配列に入れる前に保存する場所 (初期値)
    private int[] squareNumIndex=new int[400];

    //空間の配列を格納して道を繋げるための準備をするジャグ配列
    private int[][] squareIndexLoad;

    private bool creatSquareFinish = true;

    //迷路完成を表示
    private bool meiroKansei = false;

    //矩形の縦横のサイズ
    struct square
    {
       public int min;
       public int max;
    }

    private square xSquare;
    private square ySquare;
    private int start = 0;
    private int goal = 0;
    private GameObject cloneObject;
    private GameObject parentLbrynth;


    //UpdateIndexSerach_filed
    //空間の数
    private int creatLoadUpdate_squarenum;
    //空間配列だけある道である点の設定
    private int[] creatLoadUpdate_loadNumber;
    //配列番号
    private int creatLoadUpdate_index = 0;
    private int creatLoadUpdate_i = 0;
    private bool updateIndexSearch = true;
    private int creatLoadUpdate_nextIndex = 0;

    private bool creatload = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SetMeiro(this);
        //StartCoroutine(Generater());
        Generater();

    }

    //他のゲームオブジェクトから迷路作成を指定する時に利用するメソッド
    public void GeneratPublic()
    {
        //StartCoroutine(Generater());
         Generater();
    }

 

    //迷路の生成のメソッド
    private void Generater()
    {
        MeiroUpdate();
        CretateWall();
        CreatSpace();
        CreateLoad();
        StarttoGoalCreate();
       // CheakLoadDable();
        GameObjectInstanceMeiro();
        GameManagerUpdate();
        DebugMeiroArray(meiroInt);
        meiroKansei = true;
    }

    public bool MeiroKnasei()
    {
        return meiroKansei;
            
    }

    private void Update()
    {
        
        //CreateLoadUpdateVersion();
        /*if (creatload)
        {
            StarttoGoalCreate();
            GameObjectInstanceMeiro();
            GameManagerUpdate();
            DebugMeiroArray(meiroInt);
        }*/
        
    }

    //privateのオブジェクトを参照するためのメソッド
    private void GameManagerUpdate()
    {
        GameManager.instance.SetMap(meiroInt);
    }
    //迷路の大きさを初期化するメソッド
    private void MeiroUpdate()
    {
        //初期化設定
        squareSize = GameManager.instance.GetSquareSize();
        meiroNeighborhood = GameManager.instance.GetMeiroNeighborhood();
        meirosize = meiroNeighborhood * meiroNeighborhood;
        int level = GameManager.instance.GetFloor();
        meiroInt = new int[meirosize];
        meiroKansei = false;
        //矩形の初期化
        xSquare.min = GameManager.instance.GetSpaceMin();
        xSquare.max = GameManager.instance.GetSpaceMax();
        ySquare.min = GameManager.instance.GetSpaceMin();
        ySquare.max = GameManager.instance.GetSpaceMax(); ;

        squareIndexLoad = new int[squareSize][];

        if (parentLbrynth != null)
        {
            Destroy(parentLbrynth);
            parentLbrynth = new GameObject("parentMeiroObject");
        }
        else
        {
            parentLbrynth = new GameObject("parentMeiroObject");
        }
    }

   

    //壁を生成するメソッド
    private void CretateWall()
    {
        //壁作成
        for (int i = 0; i < meiroNeighborhood; i++)
        {
            for (int j = 0; j < meiroNeighborhood; j++)
            {
                meiroInt[i*meiroNeighborhood + j] = 0;

            }
        }
        DebugMeiroArray(meiroInt);
    }

    //矩形の範囲を決めるメソッド
    private void CreateSquare()
    {
        creatSquareFinish = true;
        while (creatSquareFinish)
        {
            //Squareの縦横の大きさをランダムで指定
            int x = Random.Range(xSquare.min, xSquare.max);
            int y = Random.Range(ySquare.min, ySquare.max);

            //Squareを作成する場所を指定する
            int meirox = Random.Range(1, meiroNeighborhood - 2);
            int meiroy = Random.Range(1, meiroNeighborhood - y);

            //Debug.Log("矩形の横" + x +"　"+ "矩形の縦" + y + "　" + "位置x" + meirox + "　" + "位置y" + meiroy);

            int indexNum = 0;
            squareNumIndex = new int[meirosize];
            int[] chackIndex = new int[(x + 9) * (y + 9)+1];

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    squareNumIndex[indexNum] = (meirox + j) +( meiroy * meiroNeighborhood + i * meiroNeighborhood);
                    indexNum++;
                }
            }
            squareNumIndex[indexNum] = -1;

            indexNum = 0;

            for (int i = -3; i < y + 3; i++)
            {
                for (int j = -3; j < x + 3; j++)
                {
                    if (meirox + j + meiroy * meiroNeighborhood + i * meiroNeighborhood < meiroNeighborhood * meiroNeighborhood)
                    {
                        chackIndex[indexNum] = (meirox + j) +( meiroy * meiroNeighborhood + i * meiroNeighborhood);
                       // Debug.Log("範囲は" + chackIndex[indexNum]);
                        indexNum++;
                    }
                }
            }

            chackIndex[indexNum] = -1;

            int index = IndexOf(chackIndex, -1);
            for (int i = 0; i < index; i++)
            {
                if (chackIndex[i] >= 0)
                {
                    if (meiroInt[chackIndex[i]] == 3)
                    {
                        //Debug.Log("空間生成失敗");
                        break;
                    }
                }

                if (chackIndex[i] >= 0)
                {
                    if (index == i + 1)
                    {
                        if (meiroInt[chackIndex[i]] != 3)
                        {
                            creatSquareFinish = false;
                        }
                    }
                }
            }

        }
     }

    //空間を作成するメソッド
    private void CreatSpace()
    {
        int createSpace = 0;
        int falldNum = 0;
        for(int i = 0; i < squareSize; i=createSpace)
        {
            CreateSquare();

            if (MeiroUse())
            {
                int index = IndexOf(squareNumIndex, -1);
                for(int j=0;j<index;j++)
                {
                    meiroInt[squareNumIndex[j]] = 3;
                }
                //Debug.Log("スペースを" + (createSpace+1) + "生成");
                DebugMeiroArray(meiroInt);
                createSpace++;
                squareIndexLoad[i] = squareNumIndex;
            }
            else
            {
                if (falldNum >= 100)
                {
                 //   Debug.Log("失敗回数を" + falldNum + "回超えたので空間生成を終了します");
                    break;
                }
                falldNum++;
            }
        }
           squareSize =createSpace;
    }

    //道の生成
    private void CreateLoad()
    {
        //空間の数
        int squarenum = squareSize;
        //空間配列だけある道である点の設定
        int[] loadNumber = new int[squarenum];
        //配列番号
        int index = 0;
        
        for(int i=0;i<squarenum;i++)
        {
            loadNumber[i] = squareIndexLoad[i][Random.Range(0, IndexOf(squareIndexLoad[i],-1))];
            //Debug.Log("迷路の起点位置" + loadNumber[i]);
            index++;
        }

        for (int i = 0; i < squarenum - 1; i++)
        {
            int nextIndex = loadNumber[i];

            while (nextIndex != loadNumber[i + 1])
            {
                int nowIndex = nextIndex;

                if (nextIndex % meiroNeighborhood > loadNumber[i + 1]%meiroNeighborhood&& nextIndex / meiroNeighborhood > loadNumber[i + 1] / meiroNeighborhood)
                {
                    
                    if (nextIndex%meiroNeighborhood>loadNumber[i + 1]%meiroNeighborhood)
                    {
                        nextIndex--;
                    }
                    else if (nextIndex / meiroNeighborhood > loadNumber[i + 1] / meiroNeighborhood)

                    {
                        nextIndex -= meiroNeighborhood;
                    }

                }

                else if(nextIndex % meiroNeighborhood < loadNumber[i + 1] % meiroNeighborhood && nextIndex / meiroNeighborhood > loadNumber[i + 1] / meiroNeighborhood)
                {
                    if( nextIndex % meiroNeighborhood < loadNumber[i + 1] % meiroNeighborhood)
                    {
                        nextIndex++;
                    }
                    else if (nextIndex / meiroNeighborhood > loadNumber[i + 1] / meiroNeighborhood)

                    {
                        nextIndex -= meiroNeighborhood;
                    }
                }

                else if (nextIndex % meiroNeighborhood > loadNumber[i + 1] % meiroNeighborhood && nextIndex / meiroNeighborhood < loadNumber[i + 1] / meiroNeighborhood)
                {
                    if (nextIndex % meiroNeighborhood > loadNumber[i + 1] % meiroNeighborhood)
                    {
                        nextIndex--;
                    }
                    else if ( nextIndex / meiroNeighborhood < loadNumber[i + 1] / meiroNeighborhood)

                    {
                        nextIndex += meiroNeighborhood;
                    }
                   
                }

                else if(nextIndex % meiroNeighborhood < loadNumber[i + 1] % meiroNeighborhood && nextIndex / meiroNeighborhood < loadNumber[i + 1] / meiroNeighborhood)
                {
                    if (nextIndex % meiroNeighborhood < loadNumber[i + 1] % meiroNeighborhood)
                    {
                        nextIndex++;
                    }
                    else if (nextIndex / meiroNeighborhood < loadNumber[i + 1] / meiroNeighborhood)

                    {
                        nextIndex += meiroNeighborhood;
                    }
                    
                }

                else if (nextIndex % meiroNeighborhood == loadNumber[i + 1] % meiroNeighborhood && nextIndex/meiroNeighborhood > loadNumber[i+1]/meiroNeighborhood)
                {
                    nextIndex -= meiroNeighborhood;
                }
                else if (nextIndex % meiroNeighborhood == loadNumber[i + 1] % meiroNeighborhood && nextIndex / meiroNeighborhood < loadNumber[i + 1] / meiroNeighborhood)
                {
                    nextIndex += meiroNeighborhood;
                }

                else if(nextIndex / meiroNeighborhood == loadNumber[i + 1] / meiroNeighborhood && nextIndex%meiroNeighborhood > loadNumber[i+1]%meiroNeighborhood)
                {
                    nextIndex--;   
                }
                else if (nextIndex / meiroNeighborhood == loadNumber[i + 1] / meiroNeighborhood && nextIndex % meiroNeighborhood < loadNumber[i + 1] % meiroNeighborhood)
                {
                    nextIndex++;
                }

                //道の値を作成する迷路配列に入れる
                //MeiroUse(squareIndexLoad, nextIndex);

               // nextIndex = MeiroIndexSearch(nextIndex, nowIndex, loadNumber[i+1]);
              //  SpaceIndexOnLoad(nextIndex, squareIndexLoad, loadNumber[i],loadNumber[i+1],loadNumber);
                //Debug.Log("meiroNextIndex" + nextIndex);
                if (loadNumber[i] != 0)
                {
                    if (meiroInt[nextIndex] != 3)
                    {
                        meiroInt[nextIndex] = 4;
                    }
                    else
                    {

                    }
                }
                
            }
            //DebugMeiroArray(meiroInt);
        }
       // Debug.Log("CretaLoad終了");

    }

    private void CheakLoadDable()
    {
        int y = meiroNeighborhood;
        int x = meiroNeighborhood;
        int indexNum = 0;
        int[] x1 = new int [meiroNeighborhood*meiroNeighborhood];
        int[] x2 = new int [meiroNeighborhood*meiroNeighborhood];

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                x1[indexNum] =  j +  meiroNeighborhood + i * meiroNeighborhood;
                x2[indexNum]=   j +  meiroNeighborhood + (i * meiroNeighborhood+meiroNeighborhood);
                //条件式
                if (x2[indexNum] >= meiroNeighborhood * meiroNeighborhood)
                {
                    break;
                }

                while (x1[indexNum] == 4 && x2[indexNum] == 4)
                {
                    indexNum++;
                    x1[indexNum] = j + meiroNeighborhood + i * meiroNeighborhood;
                    x2[indexNum] = j + meiroNeighborhood + (i * meiroNeighborhood + meiroNeighborhood);
                    //条件式
                    if (x2[indexNum] >= meiroNeighborhood * meiroNeighborhood)
                    {
                        break;
                    }
                }

                indexNum++;
                x1[indexNum] =-1;
                x2[indexNum] = -1;
                int length = IndexOf(x1, -1);
                if (length - 1 > 1)
                {
                    for(int k = 0; k < length; k++)
                    {
                        meiroInt[x1[k]] = 0;
                    }
                }

            }
        }

        y = meiroNeighborhood;
        x = meiroNeighborhood;
        indexNum = 0;
        x1 = new int[meiroNeighborhood];
        x2 = new int[meiroNeighborhood];
        //縦バージョン
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                x1[indexNum] = j + meiroNeighborhood + i * meiroNeighborhood;
                x2[indexNum] = j + meiroNeighborhood+1 + (i * meiroNeighborhood);
                //条件式
                if (x2[indexNum] >= meiroNeighborhood * meiroNeighborhood)
                {
                    break;
                }

                while (x1[indexNum] == 4 && x2[indexNum] == 4)
                {
                    indexNum++;
                    x1[indexNum] = j + meiroNeighborhood + i * meiroNeighborhood;
                    x2[indexNum] = j + meiroNeighborhood+1 + (i * meiroNeighborhood );
                    //条件式
                    if (x2[indexNum] >= meiroNeighborhood * meiroNeighborhood)
                    {
                        break;
                    }
                }
                indexNum++;
                Debug.Log("indexNum" +indexNum);
                x1[indexNum] = -1;
                x2[indexNum] = -1;
                int length = IndexOf(x1, -1);
                if (length - 1 > 1)
                {
                    for (int k = 0; k < length; k++)
                    {
                        meiroInt[x1[k]] = 0;
                    }
                }

            }
        }
    }



    /*

    private void CreateLoadUpdateVersion()
    {
        if (updateIndexSearch)
        {
            //空間の数
            creatLoadUpdate_squarenum = squareSize;
            //空間配列だけある道である点の設定
            creatLoadUpdate_loadNumber = new int[creatLoadUpdate_squarenum];
            //配列番号
            creatLoadUpdate_index = 0;
            creatLoadUpdate_i = 0;
            for (int j = 0; j < creatLoadUpdate_squarenum; j++)
            {
                creatLoadUpdate_loadNumber[j] = squareIndexLoad[j][Random.Range(0, IndexOf(squareIndexLoad[j], -1))];
                Debug.Log("迷路の起点位置" + creatLoadUpdate_loadNumber[j]);
                creatLoadUpdate_index++;
            }
            creatLoadUpdate_nextIndex = creatLoadUpdate_loadNumber[creatLoadUpdate_i];
            updateIndexSearch = false;
        }

        if (creatLoadUpdate_i != creatLoadUpdate_squarenum - 1)
        {

            if (creatLoadUpdate_nextIndex != creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1])
            {
                int nowIndex = creatLoadUpdate_nextIndex;
                //次のIndexの値とゴールの値を比較
                if (creatLoadUpdate_nextIndex % meiroNeighborhood > creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood && creatLoadUpdate_nextIndex / meiroNeighborhood > creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood)
                {

                    if (creatLoadUpdate_nextIndex % meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood > creatLoadUpdate_nextIndex / meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood)
                    {
                        creatLoadUpdate_nextIndex--;
                    }
                    else if(creatLoadUpdate_nextIndex % meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood < creatLoadUpdate_nextIndex / meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood)
                    {
                        creatLoadUpdate_nextIndex -= meiroNeighborhood;
                    }
                    else
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            creatLoadUpdate_nextIndex--;
                        }
                        else
                        {
                            creatLoadUpdate_nextIndex -= meiroNeighborhood;
                        }
                    }

                }
                else if (creatLoadUpdate_nextIndex % meiroNeighborhood < creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood && creatLoadUpdate_nextIndex / meiroNeighborhood > creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood)
                {
                    if (Mathf.Abs(creatLoadUpdate_nextIndex % meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood) > Mathf.Abs(creatLoadUpdate_nextIndex / meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood))
                    {
                        creatLoadUpdate_nextIndex++;
                    }
                    else if (Mathf.Abs(creatLoadUpdate_nextIndex % meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood) < Mathf.Abs(creatLoadUpdate_nextIndex / meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood))
                    {
                        creatLoadUpdate_nextIndex -= meiroNeighborhood;
                    }
                    else
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            creatLoadUpdate_nextIndex++;
                        }
                        else
                        {
                            creatLoadUpdate_nextIndex -= meiroNeighborhood;
                        }
                    }
                }

                else if (creatLoadUpdate_nextIndex % meiroNeighborhood > creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood && creatLoadUpdate_nextIndex / meiroNeighborhood < creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood)
                {
                    if (Mathf.Abs(creatLoadUpdate_nextIndex % meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood)> Mathf.Abs(creatLoadUpdate_nextIndex / meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood))
                    {
                        creatLoadUpdate_nextIndex--;
                    }
                    else if (Mathf.Abs(creatLoadUpdate_nextIndex % meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood) < Mathf.Abs(creatLoadUpdate_nextIndex / meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood))
                    {
                        creatLoadUpdate_nextIndex += meiroNeighborhood;
                    }
                    else
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            creatLoadUpdate_nextIndex--;
                        }
                        else
                        {
                            creatLoadUpdate_nextIndex += meiroNeighborhood;
                        }
                    }
                }
               
                else if (creatLoadUpdate_nextIndex % meiroNeighborhood < creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood && creatLoadUpdate_nextIndex / meiroNeighborhood < creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood)
                {
                    if (Mathf.Abs(creatLoadUpdate_nextIndex % meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood)> Mathf.Abs(creatLoadUpdate_nextIndex / meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood))
                    {
                        creatLoadUpdate_nextIndex++;
                    }
                    else if (Mathf.Abs(creatLoadUpdate_nextIndex % meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood) < Mathf.Abs(creatLoadUpdate_nextIndex / meiroNeighborhood - creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood))

                    {
                        creatLoadUpdate_nextIndex += meiroNeighborhood;
                    }
                    else
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            creatLoadUpdate_nextIndex++;
                        }
                        else
                        {
                            creatLoadUpdate_nextIndex += meiroNeighborhood;
                        }
                    }
                }

                else if (creatLoadUpdate_nextIndex % meiroNeighborhood == creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood && creatLoadUpdate_nextIndex / meiroNeighborhood > creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood)
                {
                    creatLoadUpdate_nextIndex -= meiroNeighborhood;
                }
                else if (creatLoadUpdate_nextIndex % meiroNeighborhood == creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood && creatLoadUpdate_nextIndex / meiroNeighborhood < creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood)
                {
                    creatLoadUpdate_nextIndex += meiroNeighborhood;
                }

                else if (creatLoadUpdate_nextIndex / meiroNeighborhood == creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood && creatLoadUpdate_nextIndex % meiroNeighborhood > creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood)
                {
                    creatLoadUpdate_nextIndex--;
                }
                else if (creatLoadUpdate_nextIndex / meiroNeighborhood == creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] / meiroNeighborhood && creatLoadUpdate_nextIndex % meiroNeighborhood < creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1] % meiroNeighborhood)
                {
                    creatLoadUpdate_nextIndex++;
                }


                creatLoadUpdate_nextIndex = MeiroIndexSearch(creatLoadUpdate_nextIndex, nowIndex, creatLoadUpdate_loadNumber[creatLoadUpdate_i + 1]);
                //  SpaceIndexOnLoad(nextIndex, squareIndexLoad, loadNumber[i],loadNumber[i+1],loadNumber);
                Debug.Log("変更されたmeiroNextIndex" + creatLoadUpdate_nextIndex);
                meiroInt[creatLoadUpdate_nextIndex] = 5;
                Debug.Log("現状のMeiroIntは");
                DebugMeiroArray(meiroInt);
                if (creatLoadUpdate_loadNumber[creatLoadUpdate_i] != 0)
                {
                    if (meiroInt[creatLoadUpdate_nextIndex] != 3)
                    {
                        meiroInt[creatLoadUpdate_nextIndex] = 4;
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                creatLoadUpdate_i++;
                creatLoadUpdate_nextIndex = creatLoadUpdate_loadNumber[creatLoadUpdate_i];
            }
            
        }
        else
        {

            Debug.Log("CretaLoad終了");
            DebugMeiroArray(meiroInt);
            creatload = true;
        }

    }
    */

    //スタートとゴールの位置を決めるメソッド
    private void StarttoGoalCreate()
    {
        int count = 1;
        while(count!=3)
        {
            int selectSpaceRandom = Random.Range(0, squareSize);
            int selectRandom = Random.Range(0, IndexOf(squareIndexLoad[selectSpaceRandom], -1));
            //Debug.Log("選択した空間の番号" + selectSpaceRandom + "選択した空間の要素番号" + selectRandom+"選択された要素の値"+ meiroInt[squareIndexLoad[selectSpaceRandom][selectRandom]]);
            meiroInt[squareIndexLoad[selectSpaceRandom][selectRandom]] = count;
            if (count == 1)
            {
                start =squareIndexLoad[selectSpaceRandom][selectRandom];
                GameManager.instance.SetStart(start);
            }else if (count == 2)
            {
                goal =squareIndexLoad[selectSpaceRandom][selectRandom];
            }
            count++;

        }
    }

    //迷路をゲームオブジェクトとして出力する
    private void GameObjectInstanceMeiro()
    {
        
        GameObject[] games = GameManager.instance.GetMeiroObject();
        int gamelength = games.Length;
        int x = 0, z = 0;
        for(int i = 0; i < meiroInt.Length; i++)
        {
            if (0<meiroInt[i] &&meiroInt[i]<5)
            {
                cloneObject = GameObject.Instantiate(games[meiroInt[i]], new Vector3(x, -1, z), Quaternion.identity);
            }
            else
            {
                cloneObject = GameObject.Instantiate(games[meiroInt[i]], new Vector3(x, 0, z), Quaternion.identity);
            }
            cloneObject.GetComponent<MeiroObjectID>().SetID(i);
            cloneObject.GetComponent<MeiroObjectID>().SetMeiroID(meiroInt[i]);
            //Player追加
            if (meiroInt[i] == 1)
            {
                GameManager.instance.GetPlayer().transform.position = cloneObject.transform.position+new Vector3(0,1,0);
                GameManager.instance.GetPlayer().GetComponent<MeiroObjectID>().SetID(i);
                GameManager.instance.GetPlayer().GetComponent<MeiroObjectID>().SetMeiroID(5);
            }
            cloneObject.transform.parent= parentLbrynth.transform;
            
            x+=2;
            if (x >= meiroNeighborhood*2)
            {
                z+=2;
                x = 0;
            }
        }
    }

    //生成中の迷路の配列の中身で使用しているかを確認するメソッド
    private bool MeiroUse()
    {
        int index = IndexOf(squareNumIndex, -1);
        bool flag = true;
        for (int i = 0; i < index; i++)
        {
                if (meiroInt[squareNumIndex[i]] != 0 || squareNumIndex[i] % meiroNeighborhood==0 ||squareNumIndex[i]%20==19)
                {
                    flag=false;
                    break;
                }
            
        }
        return flag;
    }

    //空間と起点が一致した場合その起点を削除
    private void SpaceIndexOnLoad(int nowLoad,int[][] space,int startIndex,int goalindex,int[] goalIndex )
    {
        int getIndex=0;
        for(int i = 0; i < space.Length; i++)
        {
            for(int j = 0; j < space[i].Length; j++)
            {
                if (nowLoad == space[i][j])
                {
                    getIndex = i;
                    if (startIndex != goalIndex[getIndex]&&goalindex!=goalIndex[getIndex])
                    {
                        //Debug.Log("削除された起点" + goalIndex[getIndex]);
                        goalIndex[getIndex] = 0;
                    }
                }
            }
        }
    }

    /*
    private void MeiroUse(int[][] vs,int index)
    {
        bool flag = false;
        int length = vs.Length;
        for(int i = 0; i < length; i++)
        {
            for(int j = 0; j <IndexOf( vs[i],-1); j++)
            {
                if (index == vs[i][j])
                {
                    flag = true;
                    vs[i][j] = num;
                    break;
                }
            }
            if (flag)
            {
                break;
            }
        }
    }
    */



    //次の要素番号が道として近くにあるならそちらに飛ぶ
    /*
    private int MeiroIndexSearch(int nextIndex, int nowIndex, int goalIndex)
    {
        Debug.Log("MeiroIndexsearchでnextIndex" + nextIndex + "です。" + "nowIndexは" + nowIndex + "です。" + "golaIndexは" + goalIndex + "です。");
        int[] changeNextindexArray = new int[3];
        int goIndex = -1;
        int returnIndex = 0;
        int goalY = goalIndex / meiroNeighborhood;
        int goalX = goalIndex % meiroNeighborhood;
        int nextIndexX = nextIndex % meiroNeighborhood;
        int nextIndexY = nextIndex / meiroNeighborhood;

        uint mask = 0b_000;
        const uint maskDefalutNextIndex = 0b_010;
        const uint maskRightAndLeftNextIndex = 0b_101;
        const uint maskLeftNextIndex = 0b_100;
        const uint maskRightNextIndex = 0b_001;

        uint mask2 = 0b_00;
        const uint mask2RightAndLeftNextIndex = 0b_11;
        const uint mask2RightNextIndex = 0b_01;
        const uint mask2LeftNextIndex = 0b_10;

        Debug.Log("nextIndexXは" + nextIndexX + "nextIndexYは" + nextIndexY + "goalXは" + goalX + "goalYは" + goalY);

        //進む方向で三つの先のインデックス番号を取得
        if (nextIndex - nowIndex == meiroNeighborhood)
        {
            goIndex = 0;
            for (int i = -1; i < changeNextindexArray.Length - 1; i++)
            {
                changeNextindexArray[i + 1] = nextIndex + i;
            }
        }
        else if (nextIndex - nowIndex == meiroNeighborhood * -1)
        {
            goIndex = 1;
            for (int i = -1; i < changeNextindexArray.Length - 1; i++)
            {
                changeNextindexArray[i + 1] = nextIndex + i;
            }
        }
        else if (nextIndex - nowIndex == 1)
        {
            goIndex = 2;
            for (int i = -1; i < changeNextindexArray.Length - 1; i++)
            {
                changeNextindexArray[i + 1] = nextIndex + meiroNeighborhood * i;
            }
        }
        else if (nextIndex - nowIndex == -1)
        {
            goIndex = 3;
            for (int i = -1; i < changeNextindexArray.Length - 1; i++)
            {
                changeNextindexArray[i + 1] = nextIndex + meiroNeighborhood * i;
            }
        }

        Debug.Log("goIndexは" + goIndex);
        //ここまでOK

        //mask調べる
        for (int i = 0; i < changeNextindexArray.Length; i++)
        {
            if (1 <= meiroInt[changeNextindexArray[i]] &&meiroInt[changeNextindexArray[i]] <= 4)
            {
                if (i == 0)
                {
                    mask += 0b_100;
                }
                else if (i == 1)
                {
                    mask += 0b_010;
                }
                else if (i == 2)
                {
                    mask += 0b_001;
                }
            }
        }
        //////
        ///条件式に含まれる値に変換
        ///


        if (goalY > nextIndexY && goalX > nextIndexX)
        {
            //右下に向かう中で右に動く場合
            if (goIndex == 2)
            {
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - 1] && meiroInt[changeNextindexArray[0] - 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] - 1] && meiroInt[changeNextindexArray[2] - 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[2];

                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - 1] && meiroInt[changeNextindexArray[0] - 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] - 1] && meiroInt[changeNextindexArray[2] - 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }

            //右下に進む場合で下に向かう動きをする場合
            else if (goIndex == 0)
            {
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - meiroNeighborhood] && meiroInt[changeNextindexArray[0] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] - meiroNeighborhood] && meiroInt[changeNextindexArray[2] - meiroNeighborhood] <= 4) {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[2];

                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - meiroNeighborhood] && meiroInt[changeNextindexArray[0] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] - meiroNeighborhood] && meiroInt[changeNextindexArray[2] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }

        }

        else if (goalY > nextIndexY && goalX < nextIndexX)
        {
            //左下に向かいながら下に動くとき

            if (goIndex == 0)
            {
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - meiroNeighborhood] && meiroInt[changeNextindexArray[0] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] - meiroNeighborhood] && meiroInt[changeNextindexArray[2] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - meiroNeighborhood] && meiroInt[changeNextindexArray[0] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] - meiroNeighborhood] && meiroInt[changeNextindexArray[2] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }

            //左下に進むときに左に動く場合
            else if (goIndex == 3)
            {
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + 1] && meiroInt[changeNextindexArray[0] + 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] + 1] && meiroInt[changeNextindexArray[2] + 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[2];

                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + 1] && meiroInt[changeNextindexArray[0] + 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] + 1] && meiroInt[changeNextindexArray[2] + 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }

        }

        //ゴールが右上の場合
        else if (goalY < nextIndexY && goalX > nextIndexX)
        {
            //ゴールが右上で上に進む場合
            if (goIndex == 1)
            {
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + meiroNeighborhood] && meiroInt[changeNextindexArray[0] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] + meiroNeighborhood] && meiroInt[changeNextindexArray[2] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[2];

                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + meiroNeighborhood] && meiroInt[changeNextindexArray[0] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] + meiroNeighborhood] && meiroInt[changeNextindexArray[2] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }

            //右上に進む中で右に進むとき
            else if (goIndex == 2)
            {
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - 1] && meiroInt[changeNextindexArray[0] - 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] - 1] && meiroInt[changeNextindexArray[2] - 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - 1] && meiroInt[changeNextindexArray[0] - 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] - 1] && meiroInt[changeNextindexArray[2] - 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }

        }

        //左上に進む場合
        else if (goalY < nextIndexY && goalX < nextIndexX)
        {
            //左上に進む中で上に進む場合
            if (goIndex == 1)
            {
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + meiroNeighborhood] && meiroInt[changeNextindexArray[0] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] + meiroNeighborhood] && meiroInt[changeNextindexArray[2] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + meiroNeighborhood] && meiroInt[changeNextindexArray[0] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] + meiroNeighborhood] && meiroInt[changeNextindexArray[2] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }

            //左上に進む中で左に進む場合
            else if (goIndex == 3)
            {
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + 1] && meiroInt[changeNextindexArray[0] + 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] + 1] && meiroInt[changeNextindexArray[2] + 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + 1] && meiroInt[changeNextindexArray[0] + 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] + 1] && meiroInt[changeNextindexArray[2] + 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }

        }

        //片方が一緒だった場合の条件式
        //上下が同じである場合
        else if (goalY == nextIndexY && goalX != nextIndexX)
        {
            //左に進む場合
            if (goIndex == 3)
            {
                Debug.Log("goindex3");
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + 1] && meiroInt[changeNextindexArray[0] + 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] + 1] && meiroInt[changeNextindexArray[2] + 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            returnIndex = changeNextindexArray[0];
                        }
                        else if (rand == 1)
                        {
                            returnIndex = changeNextindexArray[2];
                        }
                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + 1] && meiroInt[changeNextindexArray[0] + 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] + 1] && meiroInt[changeNextindexArray[2] + 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }
            //右上に進む中で右に進むとき
            else if (goIndex == 2)
            {
                Debug.Log("goindex2");
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - 1] && meiroInt[changeNextindexArray[0] - 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] - 1] && meiroInt[changeNextindexArray[2] - 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            returnIndex = changeNextindexArray[0];
                        }
                        else if (rand == 1)
                        {
                            returnIndex = changeNextindexArray[2];
                        }
                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - 1] && meiroInt[changeNextindexArray[0] - 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] - 1] && meiroInt[changeNextindexArray[2] - 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }
            //上に進む場合
            else if (goIndex == 1)
            {
                Debug.Log("goindex1");
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + meiroNeighborhood] && meiroInt[changeNextindexArray[0] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] + meiroNeighborhood] && meiroInt[changeNextindexArray[2] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            returnIndex = changeNextindexArray[0];
                        }
                        else if (rand == 1)
                        {
                            returnIndex = changeNextindexArray[2];
                        }

                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + meiroNeighborhood] && meiroInt[changeNextindexArray[0] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] + meiroNeighborhood] && meiroInt[changeNextindexArray[2] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }
            //下に動くとき

            else if (goIndex == 0)
            {
                Debug.Log("goindex0");
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - meiroNeighborhood] && meiroInt[changeNextindexArray[0] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] - meiroNeighborhood] && meiroInt[changeNextindexArray[2] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            returnIndex = changeNextindexArray[0];
                        }
                        else if (rand == 1)
                        {
                            returnIndex = changeNextindexArray[2];
                        }
                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - meiroNeighborhood] && meiroInt[changeNextindexArray[0] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] - meiroNeighborhood] && meiroInt[changeNextindexArray[2] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }
        }

        //左右が同じ場合
        else if (goalY != nextIndexY && goalX == nextIndexX)
        {
            //左に進む場合
            if (goIndex == 3)
            {
                Debug.Log("goindex3");
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + 1] && meiroInt[changeNextindexArray[0] + 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] + 1] && meiroInt[changeNextindexArray[2] + 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            returnIndex = changeNextindexArray[0];
                        }
                        else if (rand == 1)
                        {
                            returnIndex = changeNextindexArray[2];
                        }
                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + 1] && meiroInt[changeNextindexArray[0] + 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] + 1] && meiroInt[changeNextindexArray[2] + 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }
            //右上に進む中で右に進むとき
            else if (goIndex == 2)
            {
                Debug.Log("goindex2");
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - 1] && meiroInt[changeNextindexArray[0] - 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] - 1] && meiroInt[changeNextindexArray[2] - 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            returnIndex = changeNextindexArray[0];
                        }
                        else if (rand == 1)
                        {
                            returnIndex = changeNextindexArray[2];
                        }
                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - 1] && meiroInt[changeNextindexArray[0] - 1] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] - 1] && meiroInt[changeNextindexArray[2] - 1] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }
            //上に進む場合
            else if (goIndex == 1)
            {
                Debug.Log("goindex1");
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + meiroNeighborhood] && meiroInt[changeNextindexArray[0] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] + meiroNeighborhood] && meiroInt[changeNextindexArray[2] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            returnIndex = changeNextindexArray[0];
                        }
                        else if (rand == 1)
                        {
                            returnIndex = changeNextindexArray[2];
                        }

                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] + meiroNeighborhood] && meiroInt[changeNextindexArray[0] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] + meiroNeighborhood] && meiroInt[changeNextindexArray[2] + meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }
            //下に動くとき

            else if (goIndex == 0)
            {
                Debug.Log("goindex0");
                if ((mask & maskDefalutNextIndex) == maskDefalutNextIndex)
                {
                    returnIndex = nextIndex;
                }

                else if ((mask & maskRightAndLeftNextIndex) == maskRightAndLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - meiroNeighborhood] && meiroInt[changeNextindexArray[0] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (1 <= meiroInt[changeNextindexArray[2] - meiroNeighborhood] && meiroInt[changeNextindexArray[2] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }

                    if (mask2 == mask2RightAndLeftNextIndex)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            returnIndex = changeNextindexArray[0];
                        }
                        else if (rand == 1)
                        {
                            returnIndex = changeNextindexArray[2];
                        }
                    }

                    else if (mask2 == mask2LeftNextIndex)
                    {

                        returnIndex = changeNextindexArray[0];

                    }

                    else if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }

                }

                else if ((mask & maskLeftNextIndex) == maskLeftNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[0] - meiroNeighborhood] && meiroInt[changeNextindexArray[0] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_10;
                    }

                    if (mask2 == mask2LeftNextIndex)
                    {
                        returnIndex = changeNextindexArray[0];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }

                else if ((mask & maskRightNextIndex) == maskRightNextIndex)
                {
                    if (1 <= meiroInt[changeNextindexArray[2] - meiroNeighborhood] && meiroInt[changeNextindexArray[2] - meiroNeighborhood] <= 4)
                    {
                        mask2 += 0b_01;
                    }
                    if (mask2 == mask2RightNextIndex)
                    {
                        returnIndex = changeNextindexArray[2];
                    }
                    else
                    {
                        returnIndex = nextIndex;
                    }
                }
                else
                {
                    returnIndex = nextIndex;
                }
            }

        }

        else if (goalY == nextIndexY && goalX == nextIndexX)
        {
            returnIndex = nextIndex;

        }

        else
        {
            Debug.Log("条件に入らない場合になりました");
            returnIndex = nextIndex;

        }

        Debug.Log("maskは" + mask);
        return returnIndex;
        
    }
    */
    
    //デバック用コンソール上で数値で結果がどうなるかを見る時に利用する
    private void DebugMeiroArray(int[] vs)
    {
        string debug = "";
        int n = 0;
        for (int str = 0; str < vs.Length; str++)
        {
            debug += vs[str].ToString();
            if (str == (meiroNeighborhood-1) + n * meiroNeighborhood)
            {
                debug += "\n";
                n++;
            }
        }
       // Debug.Log(debug);
    }

    //空間の配列を昇順に直す。(使う機会があれば)
    private void IndexSourt(int[] vs)
    {
        string debug = "";
        for (int str = 0; str < vs.Length; str++)
        {
            debug += vs[str].ToString();
            debug += "\n";
        }
        Debug.Log(debug);

    }

    //配列のある数値が格納されている要素番号を探す

    private int IndexOf(int[] vs ,int num)
    {
        int indexNum = 0;

        foreach(var a in vs)
        {
            if (a == num)
            {
                return indexNum;
            }
            indexNum++;
        }
        return -1;

        
    }
    
    //ゴールとスタートの要素番号を返す
    public int GetStartIndex()
    {
        return start;
    }

    public int GetGoalIndex()
    {
        return goal;
    }

 
}
