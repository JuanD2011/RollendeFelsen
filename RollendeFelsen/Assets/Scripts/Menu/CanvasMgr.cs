using UnityEngine;
using UnityEngine.UI;

public class CanvasMgr : MonoBehaviour {

    [SerializeField] GameObject[] canvas;
    [SerializeField] Text txtFinish;

    ButtonMgr buttonMgr;
    byte count;

    public GameObject[] Canvas
    {
        get
        {
            return canvas;
        }

        set
        {
            canvas = value;
        }
    }
    Player player;

    private void Start()
    {
        player = (Player)FindObjectOfType(typeof(Player));
        buttonMgr = GetComponentInChildren<ButtonMgr>();
        buttonMgr.OnUnnpause += Unnpaused;
        Canvas[1].SetActive(false);

        GameController gameController = (GameController)FindObjectOfType(typeof(GameController));
        gameController.OnWin += WinCanvas;
        gameController.OnLooser += LooserCanvas;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
            if (Canvas[1].activeInHierarchy && count >= 2)
            {
                Unnpaused();
            }
        }
    }

    private void Paused() {
        count++;
        Canvas[0].SetActive(false);
        Time.timeScale = 0;
        Canvas[1].SetActive(true);
    }

    private void Unnpaused() {
        count = 0;
        Canvas[0].SetActive(true);
        Time.timeScale = 1;
        Canvas[1].SetActive(false);
    }

    private void GameOverCanvas() {
        Canvas[0].SetActive(false);
        Canvas[2].SetActive(true);
    }

    private void WinCanvas() {
        canvas[2].SetActive(true);
        txtFinish.text = "Winner";
    }

    private void LooserCanvas() {
        canvas[2].SetActive(true);
        txtFinish.text = "Looser";
    }
}
