using GooglePlayGames;

using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
   #region EXPOSED_FIELDS
   [SerializeField] private Button playBtn;
   [SerializeField] private Button creditsBtn;
   [SerializeField] private Button leaderboardBtn;
   [SerializeField] private Button logsBtn;
   [SerializeField] private Button shopBtn;

   [SerializeField] private Transform creditsHolder;
   [SerializeField] private ShopUI shopUI;
   #endregion

   #region UNITY_CALLS
   private void Start()
   {
      playBtn.onClick.AddListener((() =>
      {
         Logger.Instance.LogButton("PLAY");
         GameManager.Instance.LoadScene(SCENES.GAMEPLAY);
      }));
      creditsBtn.onClick.AddListener((() =>
      {
         Logger.Instance.LogButton("CREDITS");
         creditsHolder.gameObject.SetActive(true);
      }));
      leaderboardBtn.onClick.AddListener((() =>
      {
         PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_top10);
         Logger.Instance.LogButton("LEADERBOARD");
      }));
      logsBtn.onClick.AddListener((() =>
      {
         Logger.Instance.LogButton("LOGS");
         GameManager.Instance.LoadScene(SCENES.LOGS);
      }));
      shopBtn.onClick.AddListener((() =>
      {
         Logger.Instance.LogButton("SHOP");
         shopUI.gameObject.SetActive(true);
         shopUI.Init();
      }));
   }
   #endregion
}