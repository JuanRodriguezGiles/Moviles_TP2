using System;
using System.Collections.Generic;

using Facebook.Unity;

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
                Debug.Log("Failed gp login");
            }
        }));

        if (!FB.IsInitialized)
        {
            FB.Init(onInitComplete: () =>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
                else
                {
                    Debug.Log("Failed to init FB");
                }
            });
        }
        else
        {
            FB.ActivateApp();
        }

        money = PlayerPrefs.GetInt("Money", 0);
        money = 100;
    }

    public void LoadScene(SCENES scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public void ToggleInput(bool active)
    {
        inputEnabled = active;
    }

    public void FBLogin(Action onLogin)
    {
        List<string> permissions = new List<string>();

        permissions.Add("public_profile");

        permissions.Add("user_friends");

        FB.LogInWithReadPermissions(permissions, result =>
        {
            if (result.Error != null)
            {
                Debug.Log(result.Error);
            }
            else
            {
                if (FB.IsLoggedIn)
                {
                    Debug.Log("Logged in!");
                    onLogin?.Invoke();
                }
                else
                {
                    Debug.Log("Failed login!");
                }
            }
        });
    }

    public void FBShare(int score)
    {
        FB.ShareLink(new Uri("https://imgur.com/a/BIOl8XZ/"), contentTitle: "New Score!", contentDescription: "I just got a new score of " + score, callback: result =>
        {
            if (result.Cancelled || !String.IsNullOrEmpty(result.Error))
            {
                Debug.Log("ShareLink Error: " + result.Error);
            }
            else if (!String.IsNullOrEmpty(result.PostId))
            {
                // Print post identifier of the shared content
                Debug.Log(result.PostId);
            }
            else
            {
                // Share succeeded without postID
                Debug.Log("ShareLink success!");
            }
        });
    }
}