using Unity.VisualScripting;
using UnityEngine;

public class Click : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float rotationSpeed = 0.5f;
    private float rotationX;
    private float rotationY;
    void Start()
    {
        
    }

    // Update is called once per frame
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
