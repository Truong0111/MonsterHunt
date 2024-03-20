using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool _instantiated;
    protected bool isDestroy;

    public virtual void Awake()
    {
        isDestroy = false;
        var objects = FindObjectsOfType<T>();

        if (objects.Length > 1)
        {
            isDestroy = true;
            Destroy(gameObject);
        }

        OnAwake();
    }

    protected virtual void OnAwake()
    {
    }

    public static T Instance
    {
        get
        {
            if (_instantiated) return _instance;

            var type = typeof(T);
            var attribute = Attribute.GetCustomAttribute(type, typeof(SingletonAttribute)) as SingletonAttribute;

            var objects = FindObjectsOfType<T>();

            if (objects.Length > 0)
            {
                _instance = objects[0];
                if (objects.Length > 1)
                    for (var i = 1; i < objects.Length; i++)
                        DestroyImmediate(objects[i].gameObject);

                if (attribute != null && attribute.IsDontDestroy) DontDestroyOnLoad(_instance.gameObject);

                _instantiated = true;
                return _instance;
            }

            if (attribute == null)
                return null;
            if (string.IsNullOrEmpty(attribute.Name))
                return null;

            var prefab = Resources.Load(attribute.Name) as GameObject;
            if (prefab == null)
                return null;

            var gameObject = Instantiate(prefab);
            _instance = gameObject.GetComponent<T>();
            gameObject.name = type.ToString();

            _instantiated = true;
            return _instance;
        }

        private set
        {
            _instance = value;
            _instantiated = value != null;
        }
    }

    public static bool Instantiated => _instantiated;

    public virtual void Init()
    {
    }

    private void OnDestroy()
    {
        _instantiated = false;
    }
}