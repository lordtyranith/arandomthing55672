using UnityEngine;


// Its necessary if we need change scene, but keep some objects and carry it to other scene with te local changes 

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static PersistentObject Instance
    {
        get { return _instance; }
    }
}
