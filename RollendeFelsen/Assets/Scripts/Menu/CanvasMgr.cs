using UnityEngine;

public class CanvasMgr : MonoBehaviour {

    [SerializeField] GameObject[] canvas;
    ButtonMgr buttonMgr;
    byte count;

    private void Start()
    {
        buttonMgr = GetComponentInChildren<ButtonMgr>();
        buttonMgr.OnUnnpause += Unnpaused;
        canvas[1].SetActive(false);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
            if (canvas[1].activeInHierarchy && count >= 2)
            {
                Unnpaused();
            }
        }
    }

    public void Paused() {
        count++;
        canvas[0].SetActive(false);
        Time.timeScale = 0;
        canvas[1].SetActive(true);
    }

    public void Unnpaused() {
        count = 0;
        canvas[0].SetActive(true);
        Time.timeScale = 1;
        canvas[1].SetActive(false);
    }
}
