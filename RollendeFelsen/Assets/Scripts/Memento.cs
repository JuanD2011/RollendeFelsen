using UnityEngine;

public class Memento : MonoBehaviour
{
    private Memento instance;

    public Memento Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void Save(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public float Load(string key)
    {
        float value = 0;

        if(PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.GetFloat(key);
        }

        return value;
    }
}
