using GooglePlayGames;
using GooglePlayGames.BasicApi;

using UnityEngine;
using UnityEngine.SceneManagement;

public enum SCENES
{
    MAIN_MENU,
    LOGS,
    GAMEPLAY
}

class GameManager : Singleton<GameManager>
{
    private bool inputEnabled = true;
    public bool InputEnabled { get => inputEnabled; }
    public int money = 0;

    private void Start()
    {
        PlayGamesPlatform.Activate();
        
        PlayGamesPlatform.Instance.Authenticate((status =>
        {
            if (status == SignInStatus.Success)
            {
                Debug.Log("Login");
                Social.ReportProgress(GPGSIds.achievement_welcome, 100.0f, success =>
                {
                    if (success)
                    {
                        Debug.Log("Unlocked achievement");
                    }
                    else
                    {
                        Debug.Log("Failed to unlock achievement");
                    }
                });
            }
            else
            {
                Debug.Log("Failed login");
            }
        }));

        money = PlayerPrefs.GetInt("Money", 0);
    }

    public void LoadScene(SCENES scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public void ToggleInput(bool active)
    {
        inputEnabled = active;
    }
}