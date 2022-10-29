using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
   #region EXPOSED_FIELDS
   [SerializeField] private Button playBtn;
   [SerializeField] private Button creditsBtn;
   [SerializeField] private Button leaderboardBtn;
   [SerializeField] private Button logsBtn;
   #endregion

   #region UNITY_CALLS
   private void Start()
   {
      playBtn.onClick.AddListener((() =>
      {
         Logger.Instance.LogButton("PLAY");
      }));
      creditsBtn.onClick.AddListener((() =>
      {
         Logger.Instance.LogButton("CREDITS");
      }));
      leaderboardBtn.onClick.AddListener((() =>
      {
         Logger.Instance.LogButton("LEADERBOARD");
      }));
      logsBtn.onClick.AddListener((() =>
      {
         Logger.Instance.LogButton("LOGS");
         GameManager.Instance.LoadScene(SCENES.LOGS);
      }));
   }
   #endregion
}