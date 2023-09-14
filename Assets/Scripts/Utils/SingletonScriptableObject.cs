using UnityEngine;

namespace Utils
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    T[] assets = Resources.LoadAll<T>("");
                    if(assets == null || assets.Length < 1)
                    {
                        throw new System.Exception($"Not found Singleton Scriptable Object of type: {typeof(T).ToString()}");
                    } else if (assets.Length > 1)
                    {
                        throw new System.Exception($"More than 1 instance of Singleton Scriptable Object of type: {typeof(T).ToString()} found");
                    }
                    instance = assets[0];
                }

                return instance;
            }
        }
    }
}