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
        Social.localUser.Authenticate(result =>
        {
            if (result)
            {
                Debug.Log("Login");
            }
            else
            {
                Debug.Log("Failed login");
            }
        });

        PlayGamesPlatform.Instance.Authenticate((status =>
        {
            if (status == SignInStatus.Success)
            {
                Debug.Log("Login");
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