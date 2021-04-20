﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary> マップ自動生成のクラス </summary>
public class MazeGanerator : MonoBehaviour
{
    /// <summary> 縦横のサイズ　※奇数 </summary>
    [Header("縦横のサイズ ※奇数")]
    [SerializeField] int mazeSize = 5;
    /// <summary> スタート地点とゴール地点のY軸の値調整用 </summary>
    [Header("スタート地点とゴール地点のY軸の値調整用")]
    [SerializeField] float startGoalPosY = 0.5f;
    /// <summary> 壁用のオブジェクト </summary>
    [Header("壁用のオブジェクト")]
    [SerializeField] GameObject wall = null;
    /// <summary> 床用のオブジェクト </summary>
    [Header("床用のオブジェクト")]
    [SerializeField] GameObject floor = null;
    /// <summary> スタート地点に配置するオブジェクト </summary>
    [Header("スタート地点に配置するオブジェクト")]
    [SerializeField] GameObject start = null;
    /// <summary> ゴール地点に配置するオブジェクト </summary>
    [Header("ゴール地点に配置するオブジェクト")]
    [SerializeField] GameObject goal = null;
    
    //内部パラメーター
    //セルの種類
    private enum CellType { Wall,Path };
    private CellType[,] cells;

    /// <summary> スタートの座標 </summary>
    private Vector2Int startPos;
    /// <summary> ゴールの座標 </summary>
    private Vector2Int goalPos;

    void Start()
    {
        //マップの初期化
        cells = new CellType[mazeSize, mazeSize];
        //スタート地点の取得
        startPos = GetStartPosition();
        //初回はゴール地点を設定
        goalPos = MakeMapInfo(startPos);

        //通路生成し袋小路を減らす
        var tmpStart = goalPos;
        for (int i = 0; i < mazeSize * 5; i++)
        {
            MakeMapInfo(tmpStart);
            tmpStart = GetStartPosition();
        }

        //マップの状態に応じて壁と通路を生成する
        BuildMaze();

        //スタート地点とゴール地点にオブジェクトを配置する
        var startObj = Instantiate(start, new Vector3(startPos.x, startGoalPosY, startPos.y), Quaternion.identity);
        var goalObj = Instantiate(goal, new Vector3(goalPos.x, startGoalPosY, goalPos.y), Quaternion.identity);

        startObj.transform.parent = this.transform;
        goalObj.transform.parent = this.transform;
    }

    /// <summary>
    /// スタート地点の取得
    /// </summary>
    /// <returns></returns>
    private Vector2Int GetStartPosition() 
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
    /// <param name="_startPos"></param>
    /// <returns></returns>
    private Vector2Int MakeMapInfo(Vector2Int _startPos)
    {
        //スタート位置の配列を複製
        var tmpStartPos = _startPos;

        //移動可能な座標リストを取得
        var movablePositions = GetMovablePositions(tmpStartPos);

        //移動可能な座標がなくなるまで探索を繰り返す
        while (movablePositions != null)
        {
            //移動可能な座標からランダムで１つ取得し通路にする
            var tmpPos = movablePositions[Random.Range(0, movablePositions.Count)];
            cells[tmpPos.x, tmpPos.y] = CellType.Path;

            //元の地点と通路にした座標の間を通路にする
            var xPos = tmpPos.x + (tmpStartPos.x - tmpPos.x) / 2;
            var yPos = tmpPos.y + (tmpStartPos.y - tmpPos.y) / 2;
            cells[xPos, yPos] = CellType.Path;

            //移動後の座標を一時、変数に入れて、もう一度移動可能な座標を探索する
            tmpStartPos = tmpPos;
            movablePositions = GetMovablePositions(tmpStartPos);
        }

        //探索終了まで繰り返す
        return tmpStartPos;
    }

    /// <summary>
    /// 移動可能な座標リストを取得する
    /// </summary>
    /// <param name="_startPos"></param>
    /// <returns></returns>
    private List<Vector2Int> GetMovablePositions (Vector2Int _startPos) 
    {
        //見やすくするために座標を変数に入れる
        var x = _startPos.x;
        var y = _startPos.y;

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
                    var wallObj = Instantiate(wall, new Vector3(i, 0, k), Quaternion.identity);
                    wallObj.transform.parent = this.transform;
                }

                //全ての場所に床オブジェクトを生成する
                var floorObj = Instantiate(floor, new Vector3(i, -1, k), Quaternion.identity);
                floorObj.transform.parent = this.transform;
            }
        }
    }
}
