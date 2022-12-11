using Facebook.Unity;

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
   [SerializeField] private Button fbLoginBtn;

   [SerializeField] private Transform creditsHolder;
   [SerializeField] private ShopUI shopUI;

   [SerializeField] private Image fbProfileImg = null;
   [SerializeField] private Text fbProfileName = null;
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
      fbLoginBtn.onClick.AddListener((() =>
      {
         GameManager.Instance.FBLogin(onLogin: () =>
         {
            FB.API("/me?fields=first_name", HttpMethod.GET, result =>
            {
               if (result.Error != null) return;
               
               string name = "" + result.ResultDictionary["first_name"];
               
               fbProfileName.gameObject.SetActive(true);
               fbProfileName.text = name;
               Debug.Log("" + name);
            });

            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, result =>
            {
               if (result.Texture == null) return;
               Debug.Log("Profile Pic");

               fbProfileImg.gameObject.SetActive(true);
               fbProfileImg.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
            });
         });
      }));
   }
   #endregion
}