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

        var startObj = Instantiate(start, new Vector3(startPos.x, 0, startPos.y), Quaternion.identity);
        var goalObj = Instantiate(goal, new Vector3(goalPos.x, 0, goalPos.y), Quaternion.identity);

        startObj.transform.parent = this.transform;
        goalObj.transform.parent = this.transform;
    }

    private Vector2Int GetStartPosition() 
    {
        int randomX = Random.Range(0, mazeSize);
        int randomY = Random.Range(0, mazeSize);

        while (!(randomX % 2 == 0 && randomY % 2 == 0))
        {
            randomX = Mathf.RoundToInt(Random.Range(0, mazeSize));
            randomY = Mathf.RoundToInt(Random.Range(0, mazeSize));
        }

        return new Vector2Int(randomX, randomY);
    }

    private Vector2Int MakeMapInfo(Vector2Int _startPos)
    {
        var tmpStartPos = _startPos;

        var movablePositions = GetMovablePositions(tmpStartPos);

        while (movablePositions != null)
        {
            var tmpPos = movablePositions[Random.Range(0, movablePositions.Count)];
            cells[tmpPos.x, tmpPos.y] = CellType.Path;

            var xPos = tmpPos.x + (tmpStartPos.x - tmpPos.x) / 2;
            var yPos = tmpPos.y + (tmpStartPos.y - tmpPos.y) / 2;
            cells[xPos, yPos] = CellType.Path;

            tmpStartPos = tmpPos;
            movablePositions = GetMovablePositions(tmpStartPos);
        }

        return tmpStartPos;
    }

    private List<Vector2Int> GetMovablePositions (Vector2Int _startPos) 
    {
        var x = _startPos.x;
        var y = _startPos.y;

        var positions = new List<Vector2Int>
        {
            new Vector2Int(x, y + 2),
            new Vector2Int(x, y - 2),
            new Vector2Int(x + 2, y),
            new Vector2Int(x - 2, y)
        };

        var movablePositions = positions.Where(p => !IsOutOfBounds(p.x, p.y) && cells[p.x, p.y] == CellType.Wall);

        return movablePositions.Count() != 0 ? movablePositions.ToList() : null; 
    }

    private bool IsOutOfBounds(int x, int y) => (x < 0 || y < 0 || x >= mazeSize || y >= mazeSize);

    private void BuildMaze() 
    {
        for (int i = -1; i <= mazeSize; i++)
        {
            for (int k = -1; k <= mazeSize; k++)
            {
                if (IsOutOfBounds(i,k) || cells[i,k] == CellType.Wall)
                {
                    var wallObj = Instantiate(wall, new Vector3(i, 0, k), Quaternion.identity);
                    wallObj.transform.parent = this.transform;
                }

                var floorObj = Instantiate(floor, new Vector3(i, -1, k), Quaternion.identity);
                floorObj.transform.parent = this.transform;
            }
        }
    }
}
