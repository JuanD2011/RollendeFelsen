using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {
    [SerializeField] Color[] colors;
    Image mImg;
    bool selectioned;

    public bool Selectioned1
    {
        get
        {
            return selectioned;
        }

        set
        {
            selectioned = value;
        }
    }

    private void Start()
    {
        mImg = GetComponent<Image>();
        selectioned = false;
    }

    private void Update()
    {
        if (selectioned)
            mImg.color = colors[1];
        else
            mImg.color = colors[0];
    }
}
