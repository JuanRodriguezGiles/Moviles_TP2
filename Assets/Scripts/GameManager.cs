using UnityEngine.SceneManagement;

public enum SCENES
{
    MAIN_MENU,
    LOGS,
    GAMEPLAY
}

class GameManager : Singleton<GameManager>
{
    public void LoadScene(SCENES scene)
    {
        SceneManager.LoadScene((int)scene);
    }
}