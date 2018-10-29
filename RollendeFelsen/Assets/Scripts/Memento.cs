using UnityEngine;

public class Memento : MonoBehaviour
{
    private static Memento instance;

    public static Memento Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void Save(string key, int value)
    {
        PlayerPrefs.SetInt(key, Inventario.Instancia.Billetera[TypeCurrency.firstCurrency] + value);
    }

    public void Load(string key)
    {
        int value = 0;

        if (PlayerPrefs.HasKey(key))
        {
            value = PlayerPrefs.GetInt(key);
            Inventario.Instancia.Billetera[TypeCurrency.firstCurrency] = value;
        }
        else {
            PlayerPrefs.SetInt(key, 0);
        }
    }
}
