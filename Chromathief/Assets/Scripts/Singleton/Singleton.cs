using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance = default(T);

    public static T Instance => instance;

    protected virtual void Awake()
    {
        if (instance && instance != this) Destroy(this);
        else instance = this as T;
    }
}
