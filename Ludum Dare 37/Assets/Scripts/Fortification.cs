using UnityEngine;

namespace Assets.Scripts
{
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
        public new Type GetType()
        { return _type; }
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
}
