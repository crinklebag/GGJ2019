using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviour Singleton Class.
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object _objLock = new Object();
    private static T _instance;

    public static T Instance
    {
        get
        {
            lock (_objLock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    // Make sure for whatever reason there isn't multiple singletons already
                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        //Debug.LogError("[MonoSingleton] More than one _instance of '" + typeof(T).ToString() +
                        //"' was found");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        var singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "[Singleton] " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);

                        //Debug.Log("[MonoSingleton] An _instance of '" + typeof(T).ToString() + "' is needed, " +
                        //singleton + " was created");
                    }
                    else
                    {
                        //Debug.Log("[MonoSingleton] An _instance of '" + typeof(T).ToString() + "' is needed, " +
                        //_instance.gameObject.name + " has already been created");
                    }
                }

                return _instance;
            }
        }
    }

}
