using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    public AK.Wwise.Event Music;
    public AK.Wwise.Event UIClick;
    public GameObject SettingsCanvas;

    private void Start()
    {
        Music.Post(gameObject);
        SettingsCanvas.SetActive(false);
    }
    public void StartButton()
    {
        Time.timeScale = 1f;
        Music.Stop(gameObject);
        UIClick.Post(gameObject);
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        UIClick.Post(gameObject);
        Application.Quit();
    }

    public void SettingsButton()
    {
        Debug.Log("Settings button clicked");
        UIClick.Post(gameObject);
        SettingsCanvas.SetActive(true);
    }
    public void CloseSettingsButton()
    {
        UIClick.Post(gameObject);
        SettingsCanvas.SetActive(false);
    }
}
