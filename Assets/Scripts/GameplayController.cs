using System;

using UnityEngine;

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
    
    private void Start()
    {
        activeBalls = 1;
        
        onHitBall += OnHitBall;
        onBallFall += OnBallFall;
        
        pallet.Init(onHitBall);
        lose.Init(onBallFall);
        
        gameplayUI.Init(ref onScoreChange);
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
        Instantiate(ball);
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
            gameplayUI.ToggleLosePanel();
            Debug.Log("Game Over");
        }
    }
}