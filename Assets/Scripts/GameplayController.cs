using System;

using UnityEngine;

using GooglePlayGames;

using UnityEngine.SocialPlatforms;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private GameObject ball;
    [SerializeField] private HitBall pallet;
    [SerializeField] private Lose lose;

    private Action onHitBall;
    private Action onBallFall;
    private Action<int> onScoreChange;

    private int score;
    private int activeBalls;

    private ShopData shopData;

    private void Start()
    {
        GameManager.Instance.ToggleInput(false);

        if (PlayerPrefs.HasKey("ShopData"))
        {
            string json = PlayerPrefs.GetString("ShopData");
            shopData = JsonUtility.FromJson<ShopData>(json);
        }

        activeBalls = 0;

        onHitBall += OnHitBall;
        onBallFall += OnBallFall;

        pallet.Init(onHitBall);
        lose.Init(onBallFall);

        gameplayUI.Init(ref onScoreChange, ReStart, StartGame);
    }

    private void StartGame()
    {
        GameManager.Instance.ToggleInput(true);

        SpawnBall();
    }

    private void ReStart()
    {
        score = 0;
        onScoreChange?.Invoke(score);

        activeBalls = 0;
        pallet.Reset();

        GameManager.Instance.ToggleInput(true);
        SpawnBall();
    }

    private void OnHitBall()
    {
        score++;

        if (score % 5 == 0)
        {
            SpawnBall();
        }

        onScoreChange?.Invoke(score);
    }

    private void SpawnBall()
    {
        GameObject go = Instantiate(ball);
        Ball ballComp = go.GetComponent<Ball>();

        if (shopData != null)
        {
            int index = UnityEngine.Random.Range(0, shopData.purchasedIds.Count);
            string id = shopData.purchasedIds[index];
            switch (id)
            {
                case "black":
                    ballComp.Init(Color.black);
                    break;
                case "blue":
                    ballComp.Init(Color.blue);
                    break;
                case "cyan":
                    ballComp.Init(Color.cyan);
                    break;
                case "green":
                    ballComp.Init(Color.green);
                    break;
                case "magenta":
                    ballComp.Init(Color.magenta);
                    break;
                case "red":
                    ballComp.Init(Color.red);
                    break;
                case "yellow":
                    ballComp.Init(Color.yellow);
                    break;
            }
        }
        else
        {
            ballComp.Init(Color.white);
        }

        activeBalls++;
    }

    private void OnBallFall()
    {
#if UNITY_ANDROID
        Handheld.Vibrate();
#endif
        activeBalls--;
        Debug.Log("Ball fell");
        if (activeBalls <= 0)
        {
            GameManager.Instance.ToggleInput(false);
            GameManager.Instance.money += score;
            PlayerPrefs.SetInt("Money", GameManager.Instance.money);
            gameplayUI.ToggleLosePanel(true, score);
            Debug.Log("Game Over");

            Social.ReportScore(score, GPGSIds.leaderboard_top10, (bool success) => { Debug.Log(success ? "Updated leaderboard" : "Failed to update leaderboard"); });
            
            PlayGamesPlatform.Instance.IncrementAchievement(
                GPGSIds.achievement_bounce_beginner, score, success =>
                {
                    if (success)
                    {
                        Debug.Log("Incremented achievement");
                    }
                    else
                    {
                        Debug.Log("Failed to increment achievement");
                    }
                });
            PlayGamesPlatform.Instance.IncrementAchievement(
                GPGSIds.achievement_bounce_intermediate, score, success =>
                {
                    if (success)
                    {
                        Debug.Log("Incremented achievement");
                    }
                    else
                    {
                        Debug.Log("Failed to increment achievement");
                    }
                });
            PlayGamesPlatform.Instance.IncrementAchievement(
                GPGSIds.achievement_bounce_expert, score, success =>
                {
                    if (success)
                    {
                        Debug.Log("Incremented achievement");
                    }
                    else
                    {
                        Debug.Log("Failed to increment achievement");
                    }
                });
        }
    }
}