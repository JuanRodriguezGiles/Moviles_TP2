using UnityEngine.SceneManagement;

public enum SCENES
{
    MAIN_MENU,
    LOGS,
    GAMEPLAY,
    LEADERBOARD
}

class GameManager : Singleton<GameManager>
{
    public void LoadScene(SCENES scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}