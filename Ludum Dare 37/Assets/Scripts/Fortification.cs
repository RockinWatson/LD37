using UnityEngine;

namespace Assets.Scripts
{
    public class Fortification
    {
        public enum Type
        {
            NONE = 0,
            TOWER1 = 1,
            TOWER2 = 2,
            TOWER3 = 3,
            TOWER4 = 4,
            TOWER5 = 5,
        };
        private Type _type = Type.NONE;
        public new Type GetType() { return _type; }
        private GameObject _go = null;
        private Vector3 _pos;
        
        public bool IsSet()
        {
            return (_type != Type.NONE);
        }

        public Fortification(Type type, Vector3 pos)
        {
            _pos = pos;
            SetType(type);
        }

        public void SetType(Type type)
        {
            if (!IsSet())
            {
                _go = GetPrefab(type);
                _go.gameObject.transform.position = _pos;
                _type = type;
            }
            else
            {
                Debug.Log("Fortification Already Set.");
            }
        }

        public void RemoveFortification()
        {
            if (IsSet())
            {
                if (_go != null)
                {
                    GameObject.Destroy(_go);
                }
                _type = Type.NONE;
            }
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

        //@NOTE: Used to retrieve Prefab of the logic contained within the Fortification - e.g., Elf Sniper's game code, Mine behavior, etc etc.
        static public GameObject GetPrefab(Type type)
        {
            switch (type)
            {
                case Type.TOWER1:
                    return (GameObject)GameObject.Instantiate(Resources.Load("spirit_gen"));
                case Type.TOWER2:
                    return (GameObject)GameObject.Instantiate(Resources.Load("coal_elf"));
                case Type.TOWER3:
                    return (GameObject)GameObject.Instantiate(Resources.Load("mine"));
                case Type.TOWER4:
                    return (GameObject)GameObject.Instantiate(Resources.Load("elf_sniper"));
                case Type.TOWER5:
                    return (GameObject)GameObject.Instantiate(Resources.Load("candy_cane"));
                default:
                    Debug.LogError("ERROR!: We don't recognize your authority heeeyah. This type is fucked.");
                    return null;
            }
        }
    }
}
