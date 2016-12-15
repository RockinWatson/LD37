using UnityEngine;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Enemies;
using UnityEngine.SceneManagement;

public class GameBoard : MonoBehaviour {
    #region Initialize Vars
    [SerializeField]
    private Transform _origin;

    [SerializeField]
    private int _width = 5;
    
    [SerializeField]
    private int _height = 12;

    [SerializeField]
    private Vector2 _dim = new Vector2(1.06f, 1.06f);

    [SerializeField]
    GameObject _levelOverMenu;

    // Row[0]: [0] [1] [2] [3]
    // Row[1]: [0] [1] [2] [3]
    private List<List<GameCell>> _board = null;

    private Fortification.Type _selectedFortificationType = Fortification.Type.NONE;
    private BaseFortification _previewFortification = null;

    private int _score = 25;
    public Text _scoreText;
    public Text _daysTil;
    private static int currentDay;
    public static bool _levelOver = false;

    public static float _globalTimer = 400;

    static private GameBoard _singleton = null;
    #endregion

    #region Create Board
    private void Awake()
    {
        _singleton = this;
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
    #endregion

    static public GameBoard Get()
    {
        return _singleton;
    }

    private void Start()
    {
        _globalTimer = 400;
    }

    private void Update()
    {
        DrawBoard();

        UpdatePlayerMouse();

        UpdatePlayerKeyInput();

        _scoreText.text = _score.ToString();

        _globalTimer -= Time.deltaTime;

        _daysTil.text = GetCurrentDay().ToString();

    }
    private void FixedUpdate()
    {
        if (IsLevelOver())
        {
            LevelOverMenu();
        }
        if (GetCurrentDay() == 0)
        {
            //TODO GoToGameOver Scene
            SceneManager.LoadScene("WinScene");
        }
    }

    private void LevelOverClear()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var bullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (var item in enemies)
        {
            Destroy(item);
        }
        foreach (var item in bullets)
        {
            Destroy(item);
        }
    }

    private void LevelOverMenu()
    {
        Time.timeScale = 0.0f;
        Instantiate(_levelOverMenu);
        DestroyAllFortifications();
        LevelOverClear();
    }

    private enum Days
    {
        DayZero = 0,
        DayOne = 1,
        DayTwo = 2,
        DayThree = 3,
        DayFour = 4,
        DayFive = 5
    }

    public static int GetCurrentDay()
    {
        if (_globalTimer <= 400)
        {
           currentDay = (int)Days.DayFive;
        }
        if (_globalTimer <= 350)
        {
            currentDay = (int)Days.DayFour;
        }
        if (_globalTimer <= 275)
        {
            currentDay = (int)Days.DayThree;
        }
        if (_globalTimer <= 200)
        {
            currentDay = (int)Days.DayTwo;
        }
        if (_globalTimer <= 100)
        {
            currentDay = (int)Days.DayOne;
        }
        if ((int)_globalTimer == 0)
        {
            currentDay = (int)Days.DayZero;
        }
        return currentDay;
    }

    public static bool IsLevelOver()
    {
        if ((int)_globalTimer == 350)
        {
            _levelOver = true;
        }
        if ((int)_globalTimer == 275)
        {
            _levelOver = true;
        }
        if ((int)_globalTimer == 200)
        {
            _levelOver = true;
        }
        if ((int)_globalTimer == 100)
        {
            _levelOver = true;
        }
        return _levelOver;
    }

    static public Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

    private bool IsRemoveModifierActive()
    {
        return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
    }

    private int GetCostOfFortification(Fortification.Type type)
    {
        switch (type)
        {
            case Fortification.Type.TOWER1:
                return 5;
            case Fortification.Type.TOWER2:
                return 10;
            case Fortification.Type.TOWER3:
                return 15;
            case Fortification.Type.TOWER4:
                return 25;
            case Fortification.Type.TOWER5:
                return 30;
            default:
                Debug.LogError("UNRECOGNIZED TYPE!");
                return 0;
        }
    }

    private bool CanPlaceSelectedFortificationOnGameCell(GameCell cell)
    {
        return (
            !cell.IsSet() &&
            (_selectedFortificationType != Fortification.Type.NONE) &&
            (GetCostOfFortification(_selectedFortificationType) <= _score)
            );
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
                if (IsRemoveModifierActive())
                {
                    cell.RemoveFortification();
                }
                else if (CanPlaceSelectedFortificationOnGameCell(cell))
                {
                    DestroyPreviewFortification();

                    int cost = GetCostOfFortification(_selectedFortificationType);
                    _score -= cost;

                    cell.SetFortification(_selectedFortificationType);
                    _selectedFortificationType = Fortification.Type.NONE;
                }
            }
            else
            {
                //cell.Draw(true);

                // Update potential preview...
                if (_previewFortification != null)
                {
                    cell.SetPreviewFortification(_previewFortification);
                }
            }
        }
    }

    public void SetSelectedFortificationType(Fortification.Type type)
    {
        if (_selectedFortificationType != type)
        {
            _selectedFortificationType = type;

            CreatePreviewFortification(type);

        }
    }

    private void CreatePreviewFortification(Fortification.Type type)
    {
        DestroyPreviewFortification();
        _previewFortification = Fortification.GetPreviewPrefab(type);

        Vector3 mousePos = GetMouseWorldPos();
        GameCell nearestCell = GetGameCellOnWorldPos(mousePos, true);
        nearestCell.SetPreviewFortification(_previewFortification);
    }

    private void DestroyPreviewFortification()
    {
        if (_previewFortification != null)
        {
            GameObject.Destroy(_previewFortification.gameObject);
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            DestroyAllFortifications();
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
        return (xMin <= pos.x && pos.x <= xMax &&
            yMin <= pos.y && pos.y <= yMax);
    }

    private Rect getBoardRect()
    {
        return new Rect(_origin.position.x, _origin.position.y, _dim.x * _width, _dim.y * -_height);
    }

    public GameCell GetGameCellOnWorldPos(Vector3 pos, bool nearest=false)
    {
        Rect boardRect = getBoardRect();
        //DrawRect(boardRect, Color.magenta, false);
        //if (boardRect.Contains(pos))
        if (myRectContainsPoint(boardRect, pos) || nearest)
        {
            float realXMin = boardRect.xMin;
            float realYMin = boardRect.yMax;
            float realXMax = boardRect.xMax;
            float realYMax = boardRect.yMin;

            pos.x = Mathf.Clamp(pos.x, realXMin, realXMax);
            pos.y = Mathf.Clamp(pos.y, realYMin, realYMax);

            //Debug.Log("CONTAINS!");
            int xIndex = Mathf.FloorToInt((pos.x - realXMin) / (realXMax - realXMin) * _width);
            //int yIndex = _board.Count - Mathf.FloorToInt((pos.y - realYMin) / (realYMax - realYMin) * _height) - 1;
            int yIndex = GetRowIndexOnWorldPos(pos);

            //Debug.Log(string.Format("{0} {1}", xIndex, yIndex));
            return _board[yIndex][xIndex];
        }
        else
        {
            return null;
        }
    }

    private bool IsCellPosValid(Vector2 pos)
    {
        return (0 <= pos.x && pos.x < _width &&
            0 <= pos.y && pos.y < _height);
    }

    public GameCell GetGameCellOnCellPos(Vector2 pos)
    {
        if (IsCellPosValid(pos))
        {
            return _board[Mathf.FloorToInt(pos.y)][Mathf.FloorToInt(pos.x)];
        }
        else
        {
            return null;
        }
    }

    public int GetRowIndexOnWorldPos(Vector3 pos)
    {
        Rect boardRect = getBoardRect();
        float yMin = Mathf.Min(boardRect.yMin, boardRect.yMax);
        float yMax = Mathf.Max(boardRect.yMin, boardRect.yMax);
        if (yMin <= pos.y && pos.y <= yMax)
        {
            int yIndex = _board.Count - Mathf.FloorToInt((pos.y - yMin) / (yMax - yMin) * _height) - 1;
            return yIndex;
        }
        else
        {
            return -1;
        }
    }

    public GameCell GetGameCellRightOfPos(Vector3 pos)
    {
        GameCell cell = GetGameCellOnWorldPos(pos);
        if (cell != null)
        {
            Vector2 cellPos = cell.GetCellPos();
            cellPos.x += 1;
            return GetGameCellOnCellPos(cellPos);
        }
        else
        {
            // Based on position, find the proper row... then retrieve the first in the row.
            int rowIndex = GetRowIndexOnWorldPos(pos);
            if (rowIndex != -1)
            {
                Vector2 cellPos = new Vector2(0, rowIndex);
                return GetGameCellOnCellPos(cellPos);
            }
            else
            {
                return null;
            }
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

    // This will probably move somewhere more appropriate?
    #region Score
    public void AddScore(int score)
    {
        _score += score;
    }
    public bool SpendScore(int score)
    {
        if (_score < score)
        {
            return false;
        }
        else
        {
            _score -= score;
            return true;
        }
    }
    #endregion Score

    private void OnGUI()
    {
        Rect drawPos = new Rect(0, 0, 100, 50);
        //GUI.Label(drawPos, string.Format("SCORE: {0}", _score));
        //GUI.Label(drawPos, string.Format("SELECTED TYPE: {0}", _selectedFortificationType));
        //GUI.Label(drawPos, string.Format("Timer: {0}",_globalTimer));
        
        //Vector3 pos = GetMouseWorldPos();
        //GameCell cell = GetGameCellOnWorldPos(pos);
        //if (cell != null)
        //{
        //    Vector3 center = cell.getRect().center;
        //    GUI.Label(drawPos, string.Format("CELL CENTER: {0:0.00}, {1:0.00}", center.x, center.y));
        //}
        //GUI.Label(drawPos, string.Format("MOUSE POS: {0:0.00}, {1:0.00}", pos.x, pos.y));

        //Rect rect = getBoardRect();
        //drawPos.y += 50;
        //GUI.Label(drawPos, string.Format("RECT UL POS: {0:0.00}, {1:0.00}", rect.min.x, rect.min.y));
        //drawPos.y += 50;
        //GUI.Label(drawPos, string.Format("RECT LR POS: {0:0.00}, {1:0.00}", rect.max.x, rect.max.y));
    }

    private void DestroyAllFortifications()
    {
        for (int i = 0; i < _board.Count; ++i)
        {
            List<GameCell> row = _board[i];
            for (int j = 0; j < row.Count; ++j)
            {
                GameCell cell = row[j];
                Fortification fort = cell.GetFortification();
                if (fort != null && fort.IsSet())
                {
                    fort.RemoveFortification();
                }
            }
        }

        // Clear preview.
        DestroyPreviewFortification();
    }
}
