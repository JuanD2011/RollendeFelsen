using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LvlMgr : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] GameObject[] canvas;
    [SerializeField] Text txtFinish;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu") {
            GameController.instance.delLoose += Finish;
            GameController.instance.delWin += Finish;
        }
    }

    public void Levels(string levelName)
    {
        StartCoroutine(LoadAsynchronously(levelName));
    }

    IEnumerator LoadAsynchronously(string _sceneName)
    {
        Time.timeScale = 1;
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress * 0.9f);
            slider.value = progress;
            yield return null;
        }
    }

    public void Exit() {
        Application.Quit();
    }

    public void PauseGame() {
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
    }

    public void Restart() {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().name));
    }

    private void Finish(bool _conditionOfWin) {
        PauseGame();
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
        if (_conditionOfWin)
            txtFinish.text = "Winner!!!";
        else
            txtFinish.text = "Looooseeerr!!!!";

    }
}