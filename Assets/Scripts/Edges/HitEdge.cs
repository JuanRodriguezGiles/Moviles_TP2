using UnityEngine;

public class HitEdge : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Ball")) return;
        Rigidbody2D rb = col.rigidbody;
        Ball ball = col.gameObject.GetComponent<Ball>();
        rb.velocity = new Vector2(Random.Range(-5, 5), rb.velocity.y + ball.HitCount / 8);
    }
}