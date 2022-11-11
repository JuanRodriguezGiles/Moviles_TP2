using System;

using UnityEngine;

public class Lose : MonoBehaviour
{
    private Action onBallFall;
    
    public void Init(Action onBallFall)
    {
        this.onBallFall = onBallFall;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        onBallFall?.Invoke();
    }
}