using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonHandler : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI progressText;

    [SerializeField] GameObject pauseScreen;

    void Start()
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseScreen != null)
            {
                Pause(!pauseScreen.activeSelf);
            }
        }
    }

    public void setDiff(int difficulty)
    {
        Global.diff = difficulty;
        Debug.Log("Difficulty set as: " + Global.diff);
    }

    public void setGameMode(int gamemode)
    {
        Global.mode = gamemode;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
        Global.reset();
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);

                slider.value = progress;
                progressText.text = progress * 100f + "%";

                yield return null;
            }
        }
    }

    public void HideObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void ShowObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void Pause(bool state)
    {
        if (state)
        {
            if (pauseScreen != null)
                pauseScreen.SetActive(true);

            Time.timeScale = 0; // Pause the game
        }
        else
        {
            if (pauseScreen != null)
                pauseScreen.SetActive(false);
            
            Time.timeScale = 1; // Resume the game
        }
    }
}