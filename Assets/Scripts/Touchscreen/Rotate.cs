using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.5f;
    private float rotationX;
    private float rotationY;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = touch.deltaPosition.x;
                float deltaY = touch.deltaPosition.y;
                transform.Rotate(Vector3.up, -deltaX * rotationSpeed, Space.World);
            }
        }
    }
}
