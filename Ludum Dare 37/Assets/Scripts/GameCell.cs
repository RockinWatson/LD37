using UnityEngine;

namespace Assets.Scripts
{
    public class GameCell
    {
        private Vector2 _cellPos;
        public Vector2 GetCellPos() { return _cellPos; }
        private Vector2 _dim;
        private Rect _rect;
        public Rect getRect() { return _rect; }

        private Fortification _fortification = null;
        public Fortification GetFortification() { return _fortification; }
        public bool IsSet() { return _fortification != null && _fortification.IsSet(); }

        public void SetFortification(Fortification.Type type)
        {
            if (_fortification != null)
            {
                _fortification.SetType(type);
            }
            else
            {
                _fortification = new Fortification(type, _rect.center);
            }
        }

        public void RemoveFortification()
        {
            if(_fortification != null)
            {
                _fortification.RemoveFortification();
            }
        }

        public void SetPreviewFortification(BaseFortification preview)
        {
            preview.transform.position = _rect.center;
        }
        
        public GameCell(Vector2 origin, int x, int y, Vector2 dim)
        {
            _cellPos.x = x;
            _cellPos.y = y;
            _dim = dim;

            Vector2 pos = origin + new Vector2(_cellPos.x * _dim.x, _cellPos.y * -_dim.y);
            _rect = new Rect(pos, new Vector2(_dim.x, -_dim.y));
        }

        //@TODO: Placeholder for now.
        public void Draw(bool debug = false)
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

        private void DrawFortification(bool debug=false)
        {
            if (debug)
            {
                if (_fortification != null)
                {
                    if (_fortification.IsSet())
                    {
                        Color color = _fortification.GetColor();
                        GameBoard.DrawRect(_rect, color);
                    }
                }
            }
        }

        public void UpdateDim(Vector2 dim)
        {
            _dim = dim;
        }
    }
}
