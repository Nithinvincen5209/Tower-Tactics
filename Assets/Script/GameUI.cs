using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
   public  AK.Wwise.Event Music;
    public AK.Wwise.Event UIClick;

    private void Start()
    {
           Music.Post(gameObject);
    }
    public void  RetryButton()
    {
        Music.Stop(gameObject);

        Debug.Log("Retry button clicked");
        UIClick.Post(gameObject);
        Time.timeScale = 1f; // Resume the game if it was paused
        SceneManager.LoadScene(1);
    }
   public void  QuitButton()
    {
        Debug.Log("Quit button clicked");
        UIClick.Post(gameObject);
        Application.Quit();
    }
   public  void HomeButton()
    {
        Music.Stop(gameObject);
        Debug.Log("Home button clicked");
        UIClick.Post(gameObject);
        SceneManager.LoadScene(0);
    }
    
}
