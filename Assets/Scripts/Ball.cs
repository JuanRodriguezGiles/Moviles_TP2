using UnityEngine;

public class Ball : MonoBehaviour
{
    private float hitCount = 0;

    public float HitCount { get => hitCount; set => hitCount = value; }
}