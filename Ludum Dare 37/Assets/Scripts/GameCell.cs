using UnityEngine;

namespace Assets.Scripts
{
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
                _fortification.PlaceFortification(type);
            }
            else
            {
                _fortification = new Fortification(type);
            }
        }

        public void RemoveFortification()
        {
            if(_fortification != null)
            {
                _fortification.RemoveFortification();
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

        private void DrawFortification()
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

        public void UpdateDim(Vector2 dim)
        {
            _dim = dim;
        }
    }
}
