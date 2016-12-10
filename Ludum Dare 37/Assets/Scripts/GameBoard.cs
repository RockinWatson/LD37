using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortification
{
    public enum Type
    {
        TOWER1 = 0,
        TOWER2 = 1,
        TOWER3 = 2,
        TOWER4 = 3,
        TOWER5 = 4,
    };
    private Type _type;
    public Type GetType() { return _type; }
    public void SetType(Type type) { _type = type; }

    public Fortification(Type type)
    {
        _type = type;
    }

    //@TEMP: Until we get SpriteRenderers going...
    public Color GetColor()
    {
        //@TODO: Draw the legit tower sprite er whatever.
        switch (_type)
        {
            case Type.TOWER1:
                return Color.red;
            case Type.TOWER2:
                return Color.cyan;
            case Type.TOWER3:
                return Color.green;
            case Type.TOWER4:
                return Color.yellow;
            case Type.TOWER5:
                return Color.blue;
            default:
                return Color.black;
        }
    }
}

public class GameCell
{
    private int _x;
    private int _y;
    private Vector2 _dim;
    private Rect _rect;

    private Fortification _fortification = null;
    public Fortification GetFortification() { return _fortification; }
    public void SetFortification(Fortification.Type type)
    {
        if (_fortification != null)
        {
            _fortification.SetType(type);
        }
        else
        {
            _fortification = new Fortification(type);
        }
    }

    public GameCell(Vector2 origin, int x, int y, Vector2 dim)
    {
        _x = x;
        _y = y;
        _dim = dim;

        Vector2 pos = origin + new Vector2(_x * _dim.x, _y * -_dim.y);
        _rect = new Rect(pos, new Vector2(_dim.x, -_dim.y));
    }

    //@TODO: Placeholder for now.
    public void Draw(bool debug=false)
    {
        if (debug)
        {
            //DrawSquare(center, Color.yellow);
            GameBoard.DrawRect(_rect, Color.yellow);
        }
        DrawFortification();
    }

    private void DrawSquare(Vector3 pos, Color color)
    {
        Vector3 UL = pos;
        Vector3 UR = pos + Vector3.right * _dim.x;
        Vector3 LL = pos + Vector3.down * _dim.y;
        Vector3 LR = pos + Vector3.right * _dim.x + Vector3.down * _dim.y;

        GameBoard.DrawSquare(UL, UR, LL, LR, color);
    }

    private void DrawFortification()
    {
        if (_fortification != null)
        {
            Color color = _fortification.GetColor();
            GameBoard.DrawRect(_rect, color);
        }
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

    private Fortification.Type _selectedFortificationType = Fortification.Type.TOWER1;

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
                GameCell newCell = new GameCell(_origin.position, i, j, _dim);
                row.Add(newCell);
            }
            _board.Add(row);
        }
    }

    private void Update()
    {
        DrawBoard();

        UpdatePlayerMouse();

        UpdatePlayerKeyInput();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

    private void UpdatePlayerMouse()
    {
        Vector3 pos = GetMouseWorldPos();
        //DrawMousePos(pos);

        GameCell cell = GetGameCellOnWorldPos(pos);
        if (cell != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cell.SetFortification(_selectedFortificationType);
            }
            else
            {
                cell.Draw(true);
            }
        }
    }

    private void UpdatePlayerKeyInput()
    {
        //@TEMP: Mostly TEMP / DEBUG to set Fortification Types quickly.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _selectedFortificationType = Fortification.Type.TOWER1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _selectedFortificationType = Fortification.Type.TOWER2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _selectedFortificationType = Fortification.Type.TOWER3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _selectedFortificationType = Fortification.Type.TOWER4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _selectedFortificationType = Fortification.Type.TOWER5;
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
        float xMin = Mathf.Min(rect.min.x, rect.max.x);
        float xMax = Mathf.Max(rect.min.x, rect.max.x);
        float yMin = Mathf.Min(rect.min.y, rect.max.y);
        float yMax = Mathf.Max(rect.min.y, rect.max.y);
        return (xMin < pos.x && pos.x < xMax &&
            yMin < pos.y && pos.y < yMax);
    }

    private Rect getBoardRect()
    {
        return new Rect(_origin.position.x, _origin.position.y, _dim.x * _width, _dim.y * -_height);
    }

    public GameCell GetGameCellOnWorldPos(Vector3 pos)
    {
        Rect boardRect = getBoardRect();
        //DrawRect(boardRect, Color.magenta, false);
        //if (boardRect.Contains(pos))
        if (myRectContainsPoint(boardRect, pos))
        {
            float realXMin = boardRect.xMin;
            float realYMin = boardRect.yMax;
            float realXMax = boardRect.xMax;
            float realYMax = boardRect.yMin;

            //Debug.Log("CONTAINS!");
            int xIndex = Mathf.FloorToInt((pos.x - realXMin) / (realXMax - realXMin) * _width);
            int yIndex = _board.Count - Mathf.FloorToInt((pos.y - realYMin) / (realYMax - realYMin) * _height) - 1;

            //Debug.Log(string.Format("{0} {1}", xIndex, yIndex));
            return _board[yIndex][xIndex];
        }
        else
        {
            return null;
        }
    }

    private void DrawBoard(bool debug=false)
    {
        for (int i = 0; i < _board.Count; ++i)
        {
            List<GameCell> row = _board[i];
            for (int j = 0; j < row.Count; ++j)
            {
                GameCell cell = row[j];
                if (debug)
                {
                    cell.UpdateDim(_dim);
                }
                cell.Draw(debug);
            }
        }
    }

    static public void DrawRect(Rect rect, Color color, bool drawX=true)
    {
        Vector3 UL = rect.min;
        Vector3 UR = new Vector3(rect.xMax, rect.yMin);
        Vector3 LL = new Vector3(rect.xMin, rect.yMax);
        Vector3 LR = rect.max;
        DrawSquare(UL, UR, LL, LR, color, drawX);
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
        //Debug.DrawRay(UL, Vector3.left * 3, Color.green);
        //Debug.DrawRay(LR, Vector3.right * 3, Color.cyan);
    }

    private void OnGUI()
    {
        Rect drawPos = new Rect(0, 0, 100, 50);
        GUI.Label(drawPos, string.Format("SELECTED TYPE: {0}", _selectedFortificationType));

        //Vector3 pos = getMouseWorldPos();
        //GUI.Label(drawPos, string.Format("MOUSE POS: {0:0.00}, {1:0.00}", pos.x, pos.y));

        //Rect rect = getBoardRect();
        //drawPos.y += 50;
        //GUI.Label(drawPos, string.Format("RECT UL POS: {0:0.00}, {1:0.00}", rect.min.x, rect.min.y));
        //drawPos.y += 50;
        //GUI.Label(drawPos, string.Format("RECT LR POS: {0:0.00}, {1:0.00}", rect.max.x, rect.max.y));
    }
}
