using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell
{
    private int _x;
    private int _y;
    private Vector2 _dim;

    public GameCell(int x, int y, Vector2 dim)
    {
        _x = x;
        _y = y;
        _dim = dim;
    }

    //@TODO: Placeholder for now.
    public void Draw(Transform origin)
    {
        Vector3 center = origin.position + new Vector3(_x * _dim.x, _y * -_dim.y, 0);
        //Gizmos.color = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
        //Gizmos.DrawCube(center, Vector3.up * DIM);

        DrawSquare(center, Color.yellow);
    }

    private void DrawSquare(Vector3 pos, Color color)
    {
        Vector3 UL = pos;
        Vector3 UR = pos + Vector3.right * _dim.x;
        Vector3 LL = pos + Vector3.down * _dim.y;
        Vector3 LR = pos + Vector3.right * _dim.x + Vector3.down * _dim.y;

        Debug.DrawLine(UL, UR, color);
        Debug.DrawLine(UL, LL, color);
        Debug.DrawLine(LL, LR, color);
        Debug.DrawLine(UR, LR, color);

        // X
        Debug.DrawLine(UL, LR, color);
        Debug.DrawLine(LL, UR, color);
    }

    public void UpdateDim(Vector2 dim)
    {
        _dim = dim;
    }
}

public class GameBoard : MonoBehaviour {

    [SerializeField]
    private Transform _origin;

    [SerializeField]
    private int _width = 12;
    
    [SerializeField]
    private int _height = 5;

    [SerializeField]
    private Vector2 _dim = new Vector2(1.06f, 1.06f);

    // Row[0]: [0] [1] [2] [3]
    // Row[1]: [0] [1] [2] [3]
    private List<List<GameCell>> _board = null;

    private void Awake()
    {
        CreateBoard();
    }

    private void CreateBoard()
    {
        _board = new List<List<GameCell>>();
        for (int i = 0; i < _height; ++i)
        {
            List<GameCell> row = new List<GameCell>();
            for (int j = 0; j < _width; ++j)
            {
                GameCell newCell = new GameCell(i, j, _dim);
                row.Add(newCell);
            }
            _board.Add(row);
        }
    }

    private void Update()
    {
        //DebugDrawBoard();

        UpdatePlayerMouse();
    }

    private void UpdatePlayerMouse()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        DrawMousePos(pz);
        GameCell cell = GetGameCellOnWorldPos(pz);
        if (cell != null)
        {
            cell.Draw(_origin);
        }
    }

    private void DrawMousePos(Vector3 pos)
    {
        float crosshair = 3;

        Debug.DrawRay(pos, Vector3.up * crosshair);
        Debug.DrawRay(pos, Vector3.right * crosshair);
        Debug.DrawRay(pos, Vector3.down * crosshair);
        Debug.DrawRay(pos, Vector3.left * crosshair);
    }

    public GameCell GetGameCellOnWorldPos(Vector3 pos)
    {
        Rect boardRect = new Rect(_origin.position.x, _origin.position.y, _dim.x * _height, _dim.y * -_width);
        DrawRect(boardRect, Color.magenta);
        if (boardRect.Contains(pos))
        {
            int xIndex = (int)((pos.x - boardRect.xMin) / (boardRect.xMax - boardRect.xMin));
            int yIndex = (int)((pos.y - boardRect.yMin) / (boardRect.yMax - boardRect.yMin));

            return _board[yIndex][xIndex];
        }
        else
        {
            return null;
        }
    }

    private void DebugDrawBoard()
    {
        for (int i = 0; i < _board.Count; ++i)
        {
            List<GameCell> row = _board[i];
            for (int j = 0; j < row.Count; ++j)
            {
                GameCell cell = row[j];
                cell.UpdateDim(_dim);
                cell.Draw(_origin);
            }
        }
    }

    static public void DrawRect(Rect rect, Color color)
    {
        Vector3 UL = new Vector3(rect.xMin, rect.yMin);
        Vector3 UR = new Vector3(rect.xMax, rect.yMin);
        Vector3 LL = new Vector3(rect.xMin, rect.yMax);
        Vector3 LR = new Vector3(rect.xMax, rect.yMax);
        DrawSquare(UL, UR, LL, LR, color);
    }

    static public void DrawSquare(Vector3 UL, Vector3 UR, Vector3 LL, Vector3 LR, Color color)
    {
        Debug.DrawLine(UL, UR, color);
        Debug.DrawLine(UL, LL, color);
        Debug.DrawLine(LL, LR, color);
        Debug.DrawLine(UR, LR, color);

        // X
        Debug.DrawLine(UL, LR, color);
        Debug.DrawLine(LL, UR, color);
    }
}
