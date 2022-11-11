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
    
    public void LoadScene(SCENES scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public void ToggleInput(bool active)
    {
        inputEnabled = active;
    }
}