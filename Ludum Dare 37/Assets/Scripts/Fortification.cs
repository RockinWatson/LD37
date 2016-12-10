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

        public bool IsSet()
        {
            return (_type != Type.NONE);
        }

        public Fortification(Type type)
        {
            SetType(type);
        }

        public void PlaceFortification(Type type)
        {
            //@TODO: Check to see if something already exists here...
            if (!IsSet())
            {
                SetType(type);
            }
        }

        private void SetType(Type type)
        {
            if (_type != Type.NONE)
            {
                if (_go != null)
                {
                    GameObject.Destroy(_go);
                }
            }

            //_go = GetPrefab(type);
            _type = type;
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
                    return (GameObject)GameObject.Instantiate(Resources.Load("coal_shot"));
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
