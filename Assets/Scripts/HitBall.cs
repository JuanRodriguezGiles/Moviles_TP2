using System;

using UnityEngine;

using Random = UnityEngine.Random;

public class HitBall : MonoBehaviour
{
    [SerializeField] private Vector2 bounceRange;
    private Action onHitBall;

    public void Init(Action onHitBall)
    {
        this.onHitBall = onHitBall;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Ball")) return;
        onHitBall?.Invoke();
        Rigidbody2D rb = col.rigidbody;
        Ball ball = col.gameObject.GetComponent<Ball>();
        ball.HitCount++;
        rb.velocity = new Vector2(Random.Range(bounceRange.x, bounceRange.y), rb.velocity.y + ball.HitCount / 4);
    }
}