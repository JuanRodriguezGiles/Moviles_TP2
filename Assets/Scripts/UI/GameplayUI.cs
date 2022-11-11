using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI scoreTxt;
   [SerializeField] private Transform loseHolder;
   [SerializeField] private Button mainMenuBtn;
   
   public void Init(ref Action<int> onUpdateScore)
   {
      onUpdateScore += UpdateScore;
      mainMenuBtn.onClick.AddListener(LoadMainMenu);
   }

   public void ToggleLosePanel()
   {
      loseHolder.gameObject.SetActive(true);
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