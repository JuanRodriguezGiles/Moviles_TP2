using System;

using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    private float hitCount = 0;
    private Action onBallFall;

    public float HitCount { get => hitCount; set => hitCount = value; }

    public void Init(Color color, Action onBallFall)
    {
        sprite.color = color;
        this.onBallFall = onBallFall;
    }

    public void LateUpdate()
    {
        if (transform.position.y <= -5.0f)
        {
            onBallFall?.Invoke();
            Destroy(gameObject);
        }
    }
}