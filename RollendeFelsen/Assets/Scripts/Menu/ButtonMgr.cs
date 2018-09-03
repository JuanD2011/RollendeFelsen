using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMgr : MonoBehaviour {
    [SerializeField] Buttons[] menuButtons;
    int position;

    public delegate void PauseDelegate();
    public event PauseDelegate OnUnnpause;

    private void Start()
    {
        position = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            menuButtons[position].Selectioned1 = false;
            position++;

            if (position < 0) {
                position = menuButtons.Length - 1;
                menuButtons[position].Selectioned1 = true;
            }
            if (position > menuButtons.Length - 1) {
                position = 0;
                menuButtons[position].Selectioned1 = true;
            }

            menuButtons[position].Selectioned1 = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            menuButtons[position].Selectioned1 = false;
            position--;

            if (position < 0)
            {
                position = menuButtons.Length - 1;
                menuButtons[position].Selectioned1 = true;
            }
            if (position > menuButtons.Length - 1)
            {
                position = 0;
                menuButtons[position].Selectioned1 = true;
            }

            menuButtons[position].Selectioned1 = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) && menuButtons[position].Selectioned1 == true && SceneManager.GetActiveScene().name == "Menu")
        {
            switch (position) {
                case 0:
                    SceneManager.LoadScene("GameScene");
                    break;
                case 1:
                    Application.Quit();
                    Debug.Log("Quit");
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && menuButtons[position].Selectioned1 == true && SceneManager.GetActiveScene().name == "GameScene")
        {
            switch (position)
            {
                case 0:
                    OnUnnpause();
                    break;
                case 1:
                    SceneManager.LoadScene("Menu");
                    break;
                default:
                    break;
            }
        }
    }
}
