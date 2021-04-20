using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary> マップ自動生成のクラス </summary>
public class MazeGanerator : MonoBehaviour
{
    /// <summary> 縦横のサイズ　※奇数 </summary>
    [Header("縦横のサイズ ※奇数")]
    [SerializeField] int mazeSize = 5;
    /// <summary> プレイヤーのY軸の値調整用の変数 </summary>
    private float playerPosY = -0.5f;
    /// <summary> ゴール地点のY軸の値調整用の変数 </summary>
    private float goalPosY = -0.5f;
    /// <summary> 壁用のオブジェクトの変数 </summary>
    [Header("壁用のオブジェクト")]
    [SerializeField] GameObject _Wall = null;
    /// <summary> 床用のオブジェクトの変数 </summary>
    [Header("床用のオブジェクト")]
    [SerializeField] GameObject _Floor = null;
    /// <summary> プレイヤープレハブの変数 </summary>
    [Header("プレイヤープレハブ")]
    [SerializeField] GameObject _Player = null;
    /// <summary> ゴール地点に配置するオブジェクトの変数 </summary>
    [Header("ゴール地点に配置するオブジェクト")]
    [SerializeField] GameObject _Goal = null;
    
    //内部パラメーター
    //セルの種類
    private enum CellType { Wall,Path };
    private CellType[,] cells;

    /// <summary> スタートの座標 </summary>
    private Vector2Int playerPos;
    /// <summary> ゴールの座標 </summary>
    private Vector2Int goalPos;

    void Start()
    {
        //マップの初期化
        cells = new CellType[mazeSize, mazeSize];
        //スタート地点の取得
        playerPos = GetPlayerPosition();
        //初回はゴール地点を設定
        goalPos = MakeMapInfo(playerPos);

        //通路生成し袋小路を減らす
        var tmpStart = goalPos;
        for (int i = 0; i < mazeSize * 5; i++)
        {
            MakeMapInfo(tmpStart);
            tmpStart = GetPlayerPosition();
        }

        //マップの状態に応じて壁と通路を生成する
        BuildMaze();

        //プレイヤーの地点とゴール地点にオブジェクトを配置する
        var startObj = Instantiate(_Player, new Vector3(playerPos.x, playerPosY, playerPos.y), Quaternion.identity);
        var goalObj = Instantiate(_Goal, new Vector3(goalPos.x, goalPosY, goalPos.y), Quaternion.identity);

        startObj.transform.parent = this.transform;
        goalObj.transform.parent = this.transform;
    }

    /// <summary>
    /// プレイヤーの地点の取得
    /// </summary>
    /// <returns></returns>
    private Vector2Int GetPlayerPosition() 
    {
        //ランダムにX,Yを設定する
        int randomX = Random.Range(0, mazeSize);
        int randomY = Random.Range(0, mazeSize);

        //X,Yが偶数になるなで繰り返す
        while (!(randomX % 2 == 0 && randomY % 2 == 0))
        {
            randomX = Mathf.RoundToInt(Random.Range(0, mazeSize));
            randomY = Mathf.RoundToInt(Random.Range(0, mazeSize));
        }

        return new Vector2Int(randomX, randomY);
    }

    /// <summary>
    /// マップ生成
    /// </summary>
    /// <param name="_playerPos"></param>
    /// <returns></returns>
    private Vector2Int MakeMapInfo(Vector2Int _playerPos)
    {
        //スタート位置の配列を複製
        var tmpPlayerPos = _playerPos;

        //移動可能な座標リストを取得
        var movablePositions = GetMovablePositions(tmpPlayerPos);

        //移動可能な座標がなくなるまで探索を繰り返す
        while (movablePositions != null)
        {
            //移動可能な座標からランダムで１つ取得し通路にする
            var tmpPos = movablePositions[Random.Range(0, movablePositions.Count)];
            cells[tmpPos.x, tmpPos.y] = CellType.Path;

            //元の地点と通路にした座標の間を通路にする
            var xPos = tmpPos.x + (tmpPlayerPos.x - tmpPos.x) / 2;
            var yPos = tmpPos.y + (tmpPlayerPos.y - tmpPos.y) / 2;
            cells[xPos, yPos] = CellType.Path;

            //移動後の座標を一時、変数に入れて、もう一度移動可能な座標を探索する
            tmpPlayerPos = tmpPos;
            movablePositions = GetMovablePositions(tmpPlayerPos);
        }

        //探索終了まで繰り返す
        return tmpPlayerPos;
    }

    /// <summary>
    /// 移動可能な座標リストを取得する
    /// </summary>
    /// <param name="_playerPos"></param>
    /// <returns></returns>
    private List<Vector2Int> GetMovablePositions (Vector2Int _playerPos) 
    {
        //見やすくするために座標を変数に入れる
        var x = _playerPos.x;
        var y = _playerPos.y;

        //移動方向毎に2つ先のx,y座標を計算する
        var positions = new List<Vector2Int>
        {
            new Vector2Int(x, y + 2),
            new Vector2Int(x, y - 2),
            new Vector2Int(x + 2, y),
            new Vector2Int(x - 2, y)
        };

        //移動方向毎に移動先の座標が範囲内かつ壁であるかを判定する。また、真だったら、返却用リストに追加する　
        var movablePositions = positions.Where(p => !IsOutOfBounds(p.x, p.y) && cells[p.x, p.y] == CellType.Wall);

        return movablePositions.Count() != 0 ? movablePositions.ToList() : null; 
    }

    /// <summary>
    /// 与えられたx,y座標が範囲外の場合[真]を返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool IsOutOfBounds(int x, int y) => (x < 0 || y < 0 || x >= mazeSize || y >= mazeSize);

    /// <summary>
    /// パラメーターに応じてオブジェクトを生成する
    /// </summary>
    private void BuildMaze() 
    {
        //縦横1マスずつループさせて外壁にする
        for (int i = -1; i <= mazeSize; i++)
        {
            for (int k = -1; k <= mazeSize; k++)
            {
                //範囲外又は壁のオブジェクトを生成する
                if (IsOutOfBounds(i,k) || cells[i,k] == CellType.Wall)
                {
                    var wallObj = Instantiate(_Wall, new Vector3(i, 0, k), Quaternion.identity);
                    wallObj.transform.parent = this.transform;
                }

                //全ての場所に床オブジェクトを生成する
                var floorObj = Instantiate(_Floor, new Vector3(i, -1, k), Quaternion.identity);
                floorObj.transform.parent = this.transform;
            }
        }
    }
}
