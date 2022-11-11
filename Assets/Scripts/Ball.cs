using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    private float hitCount = 0;

    public float HitCount { get => hitCount; set => hitCount = value; }

    public void Init(Color color)
    {
        sprite.color = color;
    }
}