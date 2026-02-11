using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool _isInitialized;
    private static bool _isDestroyed;

    private static readonly object _lock = new object();

    public static T Instance
    {
        get
        {
            if (_isDestroyed)
            {
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = Object.FindAnyObjectByType<T>();

                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();

                         DontDestroyOnLoad(singletonObject);
                    }
                }

                if (!_isInitialized)
                {
                    if (_instance is MonoSingleton<T> monoSingleton)
                    {
                        monoSingleton.InternalInit();
                    }
                }

                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            InternalInit();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InternalInit()
    {
        if (!_isInitialized)
        {
            _isInitialized = true;
            _isDestroyed = false;
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _isInitialized = false;
            _isDestroyed = true;
            _instance = null;
        }
    }

    public virtual void OnInit() { }
    public virtual void OnCleanUp() { }
}