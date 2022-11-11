using UnityEngine;

public class MovePallet : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed = 0.1f;
    private float xMin, xMax;

    private void Start()
    {
        float spriteSize = GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        float camWidth = mainCamera.orthographicSize * mainCamera.aspect;

        xMin = -camWidth + spriteSize;
        xMax = camWidth - spriteSize;
    }

    private void Update()
    {
        if (GameManager.Instance.InputEnabled && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Transform currentTransform = transform;

            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            currentTransform.Translate(touchDeltaPosition.x * speed * Time.deltaTime, 0, 0);

            if (transform.position.x > xMax)
            {
                currentTransform.position = new Vector3(xMax, currentTransform.position.y, 0);
            }
            else if (transform.position.x < xMin)
            {
                currentTransform.position = new Vector3(xMin, currentTransform.position.y, 0);
            }
        }
    }
}