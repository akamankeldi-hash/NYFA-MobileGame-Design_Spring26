using UnityEngine;
using UnityEngine.InputSystem;

public class Clicks : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Touchscreen.current != null)
            {
                if(Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
                {
                    Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log($"Tapped on object: {hit.transform.name}");
        }
        
        ///DoSomething(hit.point);
                }
            }
    }
}