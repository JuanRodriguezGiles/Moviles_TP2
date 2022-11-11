using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI loseScoreTxt;
    [SerializeField] private Transform loseHolder;
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button playAgainBtn;

    public void Init(ref Action<int> onUpdateScore, Action reStart)
    {
        onUpdateScore += UpdateScore;
        mainMenuBtn.onClick.AddListener(LoadMainMenu);
        playAgainBtn.onClick.AddListener((() =>
        {
            ToggleLosePanel(false);
            reStart?.Invoke();
        }));
    }

    public void ToggleLosePanel(bool active, int score = 0)
    {
        if (active)
        {
            loseScoreTxt.text = "SCORE " + score;
        }
        loseHolder.gameObject.SetActive(active);
    }

    private void LoadMainMenu()
    {
        Logger.Instance.LogButton("MAIN MENU");
        GameManager.Instance.LoadScene(SCENES.MAIN_MENU);
    }

    private void UpdateScore(int score)
    {
        scoreTxt.text = score.ToString();
    }
}