using UnityEngine;

public class CreateBoundaries : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject topRight;
    [SerializeField] private GameObject bottomLeft;

    private void Start()
    {
        SetupBoundaries();
    }

    private void SetupBoundaries()
    {
        Vector3 point = new Vector3();

        point = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));
        point.y--;
        topRight.transform.position = point;

        point = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        bottomLeft.transform.position = point;
    }
}