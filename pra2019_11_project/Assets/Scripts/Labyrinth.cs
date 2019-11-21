using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blockPrefabs;

    [SerializeField, Tooltip("ブロックのサイズ")]
    private Vector3 sizeBlock = new Vector3(1, 1, 1);


    //--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---

    //===ダンジョン===
    private int horizontal_size = 0;                    //ダンジョンのサイズ
    private int vertical_size = 0;                      //ダンジョンのサイズ
    private Vector3 baseposs_DG = Vector3.zero;         //ダンジョンの中心座標

    //===矩形===
    private int time_Ganaration_Rect = 10;
    private int x_Size_MinRect = 20;
    private int y_Size_MinRect = 20;

    //タイルマップ
    private List<RoomData> listRooms = new List<RoomData>();

    private TileData[] tileDate;        //タイルデータ

    //---
    private Rect originRect;            //根の矩形データ

    private void Start()
    {
        //タイルデータを作成
        //Create_TileData(100, 100);


        /*
        Rect rect = new Rect(0, 0, 10, 10);
        rect.Create_ChildRect(1);
        rect.rects[0].Create_ChildRect(0);
        rect.rects[1].Create_ChildRect(0);
        Test_RectTree(rect, 0);


        var v = Rect.Check_pointUpRect(5, 0, rect);
        Rect.Test_Rect(v);

        Debug.Log("--- --- --- --- --- --- --- ---");
        foreach(Rect r in v.Get_AdjaRect(rect))
        {
            Rect.Test_Rect(r);
        }
        */

        Create_Labyrinth();

        Test_TileMap();
        //Test_RectTree(originRect,0);
    }

    //  ◇========================================================================◇
    //   ||〇　〇　タ　イ　ル　デ　ー　タ　操　作　系　の　メ　ソ　ッ　ド　〇　〇 ||
    //  ◇========================================================================◇

    private void Create_TileData(int sizeX, int sizeY)
    {
        tileDate = new TileData[sizeX * sizeY];
        horizontal_size = sizeX;
        vertical_size = sizeY;

        for(int i=0; i < sizeX * sizeY; i++)
        {
            tileDate[i] = new TileData();
        }
    }

    /// <summary>
    /// 指定した座標のタイルIDを上書きする
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="id"></param>
    private void Set_TileData(int x, int y, int id)
    {
        Get_TileData(x, y).TileID = id;
    }

    /// <summary>
    /// 指定した範囲内のタイルデータを塗りつぶす
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="id"></param>
    private void Fill_TileData(int x, int y, int width, int height, int id)
    {
        for (int i = 0; i < width-1; i++)
        {
            for (int j = 0; j < height-1; j++)
            {
                //Debug.Log(string.Format("x: {0}, y: {1}", x+i, y+j));
                Set_TileData(x + i, y + j, id);
            }
        }
    }

    private void Fillp_TileData(int sx, int sy, int lx, int ly, int id)
    {
        int x_ad = (lx > sx) ? 1 : -1;
        int y_ad = (ly > sy) ? 1 : -1;

        for (int i = 0; sx + i + (-1 * x_ad) != lx; i += x_ad)
        {
            for(int j=0; sy + j + (-1 * y_ad) != ly; j += y_ad)
            {
                Set_TileData(sx + i, sy + j, id);

            }
        }

    }

    

    //====================================================

    /// <summary>
    /// 指定されたindexのタイルデータを取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private TileData Get_TileData(int index)
    {
        if (tileDate.Length != 0)
        {
            if ((0 <= index) && (index < tileDate.Length))
            {
                return tileDate[index];
            }
        }
        Debug.Log("index: "+index);
        Debug.LogError("[Labyrinyh] タイルデータの取得に失敗 : タイルデータが存在しない or 指定されたindexが不適切");
        return null;
    }

    /// <summary>
    /// 指定された座標のタイルデータを取得する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private TileData Get_TileData(int x, int y)
    {
        return Get_TileData(Get_TileIndex(x, y));
    }

    /// <summary>
    /// タイル座標をタイルデータ配列の要素数に変換する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private int Get_TileIndex(int x, int y)
    {
        return y * horizontal_size + x;
    }

    private bool Create_Room(int x_max, int y_max, int x_min, int y_min, Rect rectBase)
    {
        if ((x_max < x_min) || (y_max < y_min))
        {
            Debug.LogError("最大値または最低値が不適切です。");
            return false;
        }

        if ((x_min <= rectBase.width) && (y_min <= rectBase.height) && (x_max <= rectBase.width) && (y_max <= rectBase.height))
        {
            var dx = Random.Range(x_min, x_max);
            var dy = Random.Range(y_min, y_max);

            var x = Random.Range(0, rectBase.width - dx);
            var y = Random.Range(0, rectBase.height - dy);

            var rd = new RoomData(rectBase, x, y, dx, dy);
            rd.Set_RoundRect(originRect);

            listRooms.Add(rd);
            Fill_TileData(x + rectBase.x, y + rectBase.y, dx, dy, 1);
            Fill_TileData(x + rectBase.x + 1, y + rectBase.y + 1, dx - 2, dy - 2, 2);
            return true;
        }

        return false;
    }


    //  ◇================================================================◇
    //   ||◇　◇　ブ　ロ　ッ　ク　に　関　す　る　メ　ソ　ッ　ド　◇　◇ ||
    //  ◇================================================================◇

    /// <summary>
    /// ブロック(Prefab)を設置する(立体)
    /// </summary>
    /// <param name="x">ブロックを置く座標X</param>
    /// <param name="y">ブロックを置く座標Y</param>
    /// <param name="h">ブロックを置く座標・高さ</param>
    /// <param name="id">登録してあるブロックのID</param>
    private GameObject SetBlock(int x, int y,int h, int id)
    {
        if (Chack_BlockID(id))
        {
            return Instantiate(blockPrefabs[id], baseposs_DG + new Vector3(x * sizeBlock.x, h * sizeBlock.z, y * sizeBlock.y), Quaternion.identity);
        }
        return null;
    }

    /// <summary>
    /// ブロック(Prefab)を設置する(平面)
    /// </summary>
    /// <param name="x">ブロックを置く座標X</param>
    /// <param name="y">ブロックを置く座標Y</param>
    /// <param name="h">ブロックを置く座標・高さ</param>
    /// <returns></returns>
    private GameObject SetBlock(int x, int y, int id)
    {
        return SetBlock(x, y, 0, id);
    }

    /// <summary>
    /// ブロック(Prefab)を敷き詰める
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="h"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="id"></param>
    private void FillBlock(int x, int y, int h, int width, int height, int id)
    {
        for (int i=0; i < width-1; i++)
        {
            for (int j=0; j < height-1; j++)
            {
                SetBlock(x + i ,y + j, h, id);
            }
        }
    }

    /// <summary>
    /// ブロック(Prefab)を敷き詰める
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="id"></param>
    private void FillBlock(int x, int y, int width, int height, int id)
    {
        FillBlock(x, y, 0, width, height, id);
    }

    /// <summary>
    /// ブロックのIDが適切か
    /// </summary>
    /// <returns></returns>
    private bool Chack_BlockID(int id)
    {
        return (0 <= id && id <= blockPrefabs.Length - 1);
    }

    private void Put_TileTrace()
    {

    }


    //  ◇=========================================================◇
    //   ||☆　☆　迷　宮　に　関　す　る　メ　ソ　ッ　ド　☆　☆ ||
    //  ◇=========================================================◇

    /// <summary>
    /// 迷宮の生成
    /// </summary>
    void Create_Labyrinth()
    {
        int time_gene = 10;
        int x_size_laby = 50;
        int y_size_laby = 50;
        int x_size_minRect = 10;
        int y_size_minRect = 10;

        int roomSkip = 0;
        int x_max_room = x_size_minRect;
        int y_max_room = y_size_minRect;
        int x_min_room = 5;
        int y_min_room = 5;

        int route_addRate = 100;

        //タイルデータを生成・初期化
        Create_TileData(x_size_laby, y_size_laby);
        //矩形を自動生成
        Create_rect_Labyrinth_f(time_gene, x_size_laby, y_size_laby, x_size_minRect, y_size_minRect);
        Test_RectTree(originRect, 0);

        var list_rect_top = Get_TopRect(originRect);

        foreach(Rect r in list_rect_top)
        {
            if(!(1 + Random.Range(0, 100) <= roomSkip))
            {
                //部屋を生成する
                Create_Room(x_max_room, y_max_room, x_min_room, y_min_room, r);
            }
        }

        var routeData = new Dictionary<RoomData ,List<RoomData>>(); //ルートデータ(要素数: 部屋番号 , 値: 接続する部屋番号s)
        SettingRoute(route_addRate, ref routeData);

        //Test_Show_RoutingList(routeData);

        FixRoutingList(ref routeData);

        Test_Show_RoutingList(routeData);
        //---- ---- ---- ---- ---- ---- ---- ---- ---- ----- ---- ---- ----

        
        //Debug.Log(tileDate[0].TileID);

    }

    /// <summary>
    /// 矩形群を生成(幅不定)
    /// </summary>
    /// <param name="gene">Rect生成数</param>
    /// <param name="xSize_O">生成する根RectのサイズX</param>
    /// <param name="ySize_O">生成する根RectのサイズY</param>
    /// <param name="minX">生成するRectの最小面積X座標</param>
    /// <param name="minY">生成するRectの最小面積Y座標</param>
    /// <returns>実際に生成したRectの数</returns>
    private int Create_rect_Labyrinth(int gene, int xSize_O, int ySize_O, int minX, int minY)
    {
        int count = 0;
        originRect = new Rect(0, 0, xSize_O, ySize_O); //ベースとなる矩形を作成
        Rect rect = originRect;
        var rectList = new List<Rect>();
        rectList.Add(rect);

        for (int i=0; i<gene; i++)
        {
            Debug.Log(string.Format("生成: {0}\nスケール: {1}  width: {2} height: {3}", i, rect.scale, rect.width, rect.height));
            int q = 0;
            int length = -1;
            int d = Random.Range(0, 2);

            //分割作成が可能かどうかの判定
            if (rect.width < 2 * minX || rect.height < 2 * minY)
            {
                if (!(rect.width < 2 * minX))
                {
                    d = 0;
                }
                else if(!(rect.height < 2 * minY))
                {
                    d = 1;
                }
                else
                {
                    Debug.Log("\t作成不可");
                    rectList.Remove(rect);
                    if (rectList.Count == 0) continue;
                    rect = rectList[Random.Range(0, rectList.Count)];
                    continue;
                }
            }

            //length決定
            if (d == 0)
            {
                length = Random.Range(minX, rect.width - minX);
            }
            else
            {
                length = Random.Range(minY, rect.height - minY);
            }

            //テスト
            /*
            Rect testRect = new Rect(rect.x, rect.y, rect.width, rect.height);
            testRect.Create_ChildRect(length, d);
            foreach(Rect r in testRect.rects)
            {
                if (r.width < minX) Debug.LogError("\tXの最小値を下回っています");
                if (r.height < minY) Debug.LogError("\tYの最小値を下回っています");
            }
            */

            //生成したRectをリストに登録　→　次のRectを決定
            if (rectList.Count != 0)
            {
                rect.Create_ChildRect(length, d);
                count++;
                rectList.AddRange(rect.rects);
                rectList.Remove(rect);
                rect = rectList[Random.Range(0, rectList.Count)];
            }
            else
            {
                break; //作成可能のRectが無くなったので終了
            }
        }

        return count;
    }

    /// <summary>
    /// 矩形群を生成(幅均等)
    /// </summary>
    /// <param name="gene">Rect生成数</param>
    /// <param name="xSize_O">生成する根RectのサイズX</param>
    /// <param name="ySize_O">生成する根RectのサイズY</param>
    /// <param name="minX">生成するRectの最小面積X座標</param>
    /// <param name="minY">生成するRectの最小面積Y座標</param>
    /// <returns>実際に生成したRectの数</returns>
    private int Create_rect_Labyrinth_f(int gene, int xSize_O, int ySize_O, int minX, int minY)
    {
        int count = 0;
        originRect = new Rect(0, 0, xSize_O, ySize_O); //ベースとなる矩形を作成
        Rect rect = originRect;
        var rectList = new List<Rect>();
        rectList.Add(rect);

        for (int i = 0; i < gene; i++)
        {
            Debug.Log(string.Format("生成: {0}\nスケール: {1}  width: {2} height: {3}", i, rect.scale, rect.width, rect.height));
            int d = Random.Range(0, 2);

            //分割作成が可能かどうかの判定
            if (rect.width < 2 * minX || rect.height < 2 * minY)
            {
                if (!(rect.width < 2 * minX))
                {
                    d = 0;
                }
                else if (!(rect.height < 2 * minY))
                {
                    d = 1;
                }
                else
                {
                    Debug.Log("\t作成不可");
                    rectList.Remove(rect);
                    if (rectList.Count == 0) continue;
                    rect = rectList[Random.Range(0, rectList.Count)];
                    continue;
                }
            }

            //テスト
            /*
            Rect testRect = new Rect(rect.x, rect.y, rect.width, rect.height);
            testRect.Create_ChildRect(length, d);
            foreach(Rect r in testRect.rects)
            {
                if (r.width < minX) Debug.LogError("\tXの最小値を下回っています");
                if (r.height < minY) Debug.LogError("\tYの最小値を下回っています");
            }
            */

            //生成したRectをリストに登録　→　次のRectを決定
            if (rectList.Count != 0)
            {
                rect.Create_ChildRect(d);
                count++;
                rectList.AddRange(rect.rects);
                rectList.Remove(rect);
                rect = rectList[Random.Range(0, rectList.Count)];
            }
            else
            {
                break; //作成可能のRectが無くなったので終了
            }
        }

        return count;
    }

    /// <summary>
    /// 部屋をどの部屋に繋げるのかルートを設定する
    /// </summary>
    /// <param name="originAddRate"></param>
    /// <param name="dict"></param>
    private void SettingRoute(int originAddRate, ref Dictionary<RoomData, List<RoomData>> dict)
    {
        int addRate = 0;
        Debug.Log("[Labyrinth.SettingRoute()] ルートの設定を行う");
        foreach(RoomData room in listRooms)
        {
            addRate = 0;
            if (!dict.ContainsKey(room)) //roomキーが無ければ生成する
                dict.Add(room, new List<RoomData>());

            var listRect = new List<Rect>(room.roundRects); //隣接したRectのリストを複製する

            while (1 + Random.Range(0, 100) > addRate) //一定の確立でもう一度処理を行う
            {
                if (listRect.Count == 0) break; //リストが空になったら終了

                Rect focus = listRect[Random.Range(0, listRect.Count)]; //隣接したRectから選択する
                foreach (Rect r in focus.rects)
                {
                    if (r.GetType() == typeof(RoomData))
                    {
                        var rr = (RoomData)r; //RectをRoomDataにキャスト
                        //Debug.Log(string.Format("room: {0} -> add: {1}", room.label, rr.label));

                        dict[room].Add(rr); //辞書に登録
                        room.routeRoom.Add(rr);

                        listRect.Remove(focus); //隣接リストから除外
                    }
                }
                addRate += originAddRate;
            }
        }

        SortingRouteList(ref dict);

        //RoomData自体にコピー
        foreach (var item in dict)
        {
            item.Key.routeRoom = new List<RoomData>(item.Value);
        }

        
    }

    /// <summary>
    /// 部屋のルートに相互関係を作る
    /// </summary>
    /// <param name="dict"></param>
    private void SortingRouteList(ref Dictionary<RoomData, List<RoomData>> dict)
    {
        foreach(var item in dict)
        {
            foreach(var l in item.Value)
            {
                if (!dict[l].Contains(item.Key))
                {
                    dict[l].Add(item.Key);
                }
            }
        }
    }

    /// <summary>
    /// 部屋のルートを表示させる
    /// </summary>
    /// <param name="dict"></param>
    private void Test_Show_RoutingList(Dictionary<RoomData, List<RoomData>> dict)
    {
        foreach(var item in dict)
        {
            string s = "";
            foreach(var room in item.Value)
            {
                s += room.label;
            }
            Debug.Log(string.Format("room: {0} -> {1}", item.Key.label, s));
        }
    }

    /// <summary>
    /// 通路情報を修正する
    /// </summary>
    /// <param name="dict"></param>
    private void FixRoutingList(ref Dictionary<RoomData, List<RoomData>> dict)
    {

        var table = new Dictionary<RoomData, int>();
        List<int> labels = new List<int>();
        int label = 1;

        //初期化
        foreach(var item in dict)
        {
            table.Add(item.Key, 0);
        }

        //エリア別探索
        foreach(var item in dict)
        {
            Route(item.Key, 0 ,label);
            label++;
        }

        foreach (var item in table)
        {
            //Debug.Log(string.Format("room: {0} label: {1}", item.Key.label, item.Value));
        }

        int q=0;
        var rooms = new List<RoomData>(table.Keys);
        while (labels.Count != 1)
        {
            Debug.Log("countの数: " + labels.Count);
            if (q++ == 40) { Debug.LogError("異常な回数の繰り返しがありました"); break; }

            Debug.Log("rooms.Count: " + rooms.Count);
            var room = rooms[Random.Range(0, rooms.Count)];
            var culletLabel = table[room];
            RoomData rRoom;
            rooms.Remove(room);

            bool isEnd = false;

            Debug.Log("入ってない？: " + room.roundRects.Count) ;
            foreach(var rRect in room.roundRects)
            {
                
                if (!isEnd)
                {
                    if (rRect.rects[0].GetType() == typeof(RoomData))
                    {
                        Debug.Log("型が同じ");
                        rRoom = (RoomData)rRect.rects[0];
                        if (table[rRoom] != culletLabel /*&& labels.Contains(table[rRoom])*/)
                        {
                            Debug.Log("");
                            room.routeRoom.Add(rRoom);
                            rRoom.routeRoom.Add(room);
                            dict[room].Add(rRoom);
                            dict[rRoom].Add(room);
                            labels.Remove(table[rRoom]);

                            JoinRoute(rRoom, table[rRoom], table[room]);
                            
                            break;
                        }
                    }
                }
            }

            
        }

        foreach(var item in table)
        {
            Debug.Log(string.Format("room: {0} label: {1}", item.Key.label, item.Value));
        }
        
        return;

        void Route(RoomData room, int parentLabel , int setLabel)
        {
            if (table[room] == 0)
            {
                //現在のラベルを設定
                table[room] = setLabel;

                //リストに登録
                if (!labels.Contains(setLabel)) labels.Add(setLabel);

                //Debug.Log(string.Format("room: {0} label: {1}", room.label, setLabel));

                foreach (var a in room.routeRoom)
                {
                    Route(a, table[room], setLabel);
                }
            }
        }

        void JoinRoute(RoomData room, int goLabel, int setLabel)
        {
            if (table[room] == goLabel)
            {
                table[room] = setLabel;
                foreach (var a in room.routeRoom)
                    JoinRoute(a, goLabel, setLabel);
            }

        }

    }

    private void Create_Route(Dictionary<RoomData, List<RoomData>> dict)
    {
        var routingTable = new Dictionary<RoomData, List<RoomData>>(dict);

        Vector3Int[] dir = {
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, -1, 0)} ;

        var rooms = new List<RoomData>(dict.Keys);
        foreach(var room in rooms)
        {
            foreach(var routeRoom in room.routeRoom)
            {
                //通路伸ばし
                int dirID = Get_Dir(room, routeRoom);
                Vector3Int dirV3 = dir[dirID];
                Vector3Int point1 = RandamPoint(room, dirID);
                //ExtendRoute(dirID, point1, )
            }

        }

        return;

        void ExtendRoute(int dirID, Vector3Int start, RoomData targetRoom)
        {
            switch (dirID)
            {
                case 0:
                    {
                        Fill_TileData(start.x, start.y, targetRoom.parent.x - start.x + 1, 1, 4);
                        Set_TileData(start.x, start.y, 3);
                    }
                    break;
                case 1:
                    {
                        Fill_TileData(start.x, targetRoom.parent.y, 1, start.y - targetRoom.parent.y , 4);
                        Set_TileData(start.x, start.y, 3);
                    }
                    break;
                case 2:
                    {
                        Fill_TileData(targetRoom.parent.x, start.y, start.x - targetRoom.parent.x, 1, 4);
                        Set_TileData(start.x, start.y, 3);
                    }
                    break;
                case 3:
                    {
                        Fill_TileData(start.x, start.y, 1, start.y - targetRoom.parent.y + 1, 4);
                        Set_TileData(start.x, start.y, 3);
                    }
                    break;
                default:
                    break;
            }

        }

        Vector3Int RandamPoint(RoomData room, int direction)
        {
            Vector3Int outV3 = Vector3Int.zero;

            switch (direction)
            {
                case 0:
                    {
                        var len = Random.Range(1, room.height - 1);
                        int x = room.x + room.width - 1;
                        int y = room.y + len;
                        outV3 = new Vector3Int(x, y, 0);
                        break;
                    }
                case 1:
                    {
                        var len = Random.Range(1, room.width - 1);
                        int x = room.x + len;
                        int y = room.y;
                        outV3 = new Vector3Int(x, y, 0);
                        break;
                    }
                case 2:
                    {
                        var len = Random.Range(1, room.height - 1);
                        int x = room.x;
                        int y = room.y + len;
                        outV3 = new Vector3Int(x, y, 0);
                        break;
                    }
                case 3:
                    {
                        int len = Random.Range(1, room.width - 1);
                        int x = room.x + len;
                        int y = room.y + room.height - 1;
                        outV3 = new Vector3Int(x, y, 0);
                        break;
                    }
                default:
                    break;
            }
            return outV3;
        }

    }

    private int Get_Dir(RoomData origin, RoomData next)
    {
        int dx = next.parent.x - origin.parent.x;
        int dy = next.parent.y - origin.parent.y;

        if (Mathf.Abs(dx) >= Mathf.Abs(dy))
        {
            if (dx > 0)
            {
                return 0;
            }
            else if (dx < 0)
            {
                return 2;
            }
        }
        else if (Mathf.Abs(dx) < Mathf.Abs(dy))
        {
            if (dy > 0)
            {
                return 1;
            }
            else if (dy < 0)
            {
                return 3;
            }
        }
        return -1;
    }

    /// <summary>
    /// 最子供Rectをリスト化
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    private List<Rect> Get_TopRect(Rect origin)
    {
        var rList = new List<Rect>();

        minerRect(origin);

        return rList;

        void minerRect(Rect rect)
        {
            //Rect.Test_Rect(rect);
            if (rect.rects.Count == 0)
            {
                //Debug.Log("追加Rect : " + rect.Test_RS());
                rList.Add(rect);
            }

            foreach(Rect r in rect.rects)
            {
                if (r.GetType() == typeof(Rect))
                    minerRect(r);
            }
        }
    }

    /// <summary>
    /// Rectの階層を表示する
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="i"></param>
    private void Test_RectTree(Rect rect, int i)
    {
        //階層の深さの設定
        i++;
        if (rect.parent == null) i = 0;
        
        //文字列宣言(リセット)
        string ts = "";

        //タブを複数追加
        for (int j=0; j<i; j++)
            ts += "----";

        //文字列出力
        ts += string.Format("〇 x: {0} y: {1} width: {2} height: {3}", rect.x, rect.y, rect.width, rect.height);
        Debug.Log(ts + ((rect.rects.Count==0) ? " ☆" : ""));

        //再帰的に実行する
        foreach (Rect r in rect.rects)
        {
            Test_RectTree(r, i);
        }

    }

    /// <summary>
    /// タイルマップを出力する
    /// </summary>
    private void Test_TileMap()
    {
        string ss = "";
        for (int i=0; i<horizontal_size-1; i++)
        {
            string s="";
            for (int j=0; j<vertical_size-1; j++)
            {
                //Debug.Log(string.Format("x: {0}, y: {1}", j, i));
                s += Get_TileData(j, i).TileID.ToString();
            }
            ss += (s + "\n");
        }

        Debug.Log("「タイルマップ」\n" + ss);
    }
}

/// <summary>
/// タイルデータ
/// </summary>
public class TileData
{
    /*
        0: 外壁
        1: 内壁
        2: 部屋
        3: 玄関
        4: 通路
    */
    public int TileID = 0;
    public int BlockID = 0;
}

public class RoomData : Rect
{
    public static int num;
    public string label = "";

    public List<Rect> roundRects; //隣接したRect
    public List<RoomData> routeRoom = new List<RoomData>(); //どの部屋に繋がっているのか

    public RoomData(Rect rect, int x, int y, int w, int h) : base(x,y,w,h)
    {
        this.x = x;
        this.y = y;
        this.width = w;
        this.height = h;
        this.parent = rect;
        this.parent.rects.Add(this);

        label = "{" + num++.ToString() + "}";
    }

    public void Set_RoundRect(Rect rootR)
    {
        roundRects = parent.Get_AdjaRect(rootR);
    }
}
