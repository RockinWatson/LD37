using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortification
{
    enum Type
    {
        TOWER1 = 0,
        TOWER2 = 1,
        TOWER3 = 2,
        TOWER4 = 3,
        TOWER5 = 4,
    };
}

public class GameCell
{
    private int _x;
    private int _y;
    private Vector2 _dim;

    private Fortification _fortification = null;
    public Fortification getFortification() { return _fortification; }
    public void setFortification(Fortification fortification) { _fortification = fortification; }

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

        GameBoard.DrawSquare(UL, UR, LL, LR, color);
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
    private int _width = 5;
    
    [SerializeField]
    private int _height = 12;

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
        for (int j = 0; j < _height; ++j)
        {
            List<GameCell> row = new List<GameCell>();
            for (int i = 0; i < _width; ++i)
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

    private Vector3 getMouseWorldPos()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        return pz;
    }

    private void UpdatePlayerMouse()
    {
        Vector3 pz = getMouseWorldPos();
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

    private bool myRectContainsPoint(Rect rect, Vector3 pos)
    {
        return (rect.min.x < pos.x && pos.x < rect.max.x &&
            rect.min.y > pos.y && pos.y > rect.max.y);
    }

    private Rect getBoardRect()
    {
        return new Rect(_origin.position.x, _origin.position.y, _dim.x * _width, _dim.y * -_height);
    }

    public GameCell GetGameCellOnWorldPos(Vector3 pos)
    {
        Rect boardRect = getBoardRect();
        DrawRect(boardRect, Color.magenta);
        //if (boardRect.Contains(pos))
        if (myRectContainsPoint(boardRect, pos))
        {
            float realXMin = boardRect.xMin;
            float realYMin = boardRect.yMax;
            float realXMax = boardRect.xMax;
            float realYMax = boardRect.yMin;

            float yRange = realYMax - realYMin;
            float xRange = realXMax - realXMin;

            Debug.Log("CONTAINS!");
            int xIndex = (int)((pos.x - realXMin) / (realXMax - realXMin) * xRange);
            int yIndex = _board.Count - (int)((pos.y - realYMin) / (realYMax - realYMin) * yRange) - 1;

            Debug.Log(string.Format("{0} {1}", xIndex, yIndex));
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
        Vector3 UL = rect.min;
        Vector3 UR = new Vector3(rect.xMax, rect.yMin);
        Vector3 LL = new Vector3(rect.xMin, rect.yMax);
        Vector3 LR = rect.max;
        DrawSquare(UL, UR, LL, LR, color);
    }

    static public void DrawSquare(Vector3 UL, Vector3 UR, Vector3 LL, Vector3 LR, Color color, bool drawX=true)
    {
        Debug.DrawLine(UL, UR, color);
        Debug.DrawLine(UL, LL, color);
        Debug.DrawLine(LL, LR, color);
        Debug.DrawLine(UR, LR, color);

        // X
        if (drawX)
        {
            Debug.DrawLine(UL, LR, color);
            Debug.DrawLine(LL, UR, color);
        }

        // Sanity
        Debug.DrawRay(UL, Vector3.left * 3, Color.green);
        Debug.DrawRay(LR, Vector3.right * 3, Color.cyan);
    }

    private void OnGUI()
    {
        Rect drawPos = new Rect(0, 0, 100, 50);

        Vector3 pz = getMouseWorldPos();
        GUI.Label(drawPos, string.Format("MOUSE POS: {0:0.00}, {1:0.00}", pz.x, pz.y));

        Rect rect = getBoardRect();
        drawPos.y += 50;
        GUI.Label(drawPos, string.Format("RECT UL POS: {0:0.00}, {1:0.00}", rect.min.x, rect.min.y));
        drawPos.y += 50;
        GUI.Label(drawPos, string.Format("RECT LR POS: {0:0.00}, {1:0.00}", rect.max.x, rect.max.y));
    }
}
