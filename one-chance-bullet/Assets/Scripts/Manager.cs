using UnityEngine;

public class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
            Instance = this as T;
        else
            Destroy(gameObject);
    }   
}
