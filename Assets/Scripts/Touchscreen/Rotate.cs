using Unity.VisualScripting;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.5f;
    private float rotationX;
    private float rotationY;
    void Start()
    {
        
    }

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
