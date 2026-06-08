using UnityEngine;
using UnityEngine.InputSystem;

public class InspectSystem : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    [SerializeField] private float mouseSensitivity = 0.2f;
    [SerializeField] private float touchSensitivity = 0.1f;

    private Camera _mainCamera;
    private Collider _characterCollider;
    private bool _isDragging = false;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _characterCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        Vector2 screenPosition = Vector2.zero;
        Vector2 delta = Vector2.zero;
        float currentSensitivity = mouseSensitivity;
        bool isInteractionStarting = false;
        bool isInteractionActive = false;

        if (Mouse.current != null)
        {
            screenPosition = Mouse.current.position.ReadValue();
            delta = Mouse.current.delta.ReadValue();
            currentSensitivity = mouseSensitivity;
            
            isInteractionStarting = Mouse.current.leftButton.wasPressedThisFrame;
            isInteractionActive = Mouse.current.leftButton.isPressed;
        }
        
        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
        {
            var touch = Touchscreen.current.touches[0];
            screenPosition = touch.position.ReadValue();
            delta = touch.delta.ReadValue();
            currentSensitivity = touchSensitivity;

            isInteractionStarting = touch.press.wasPressedThisFrame;
            isInteractionActive = touch.press.isPressed;
        }

        if (isInteractionStarting)
        {
            Ray ray = _mainCamera.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider == _characterCollider)
                {
                    _isDragging = true;
                }
            }
        }

        if (!isInteractionActive)
        {
            _isDragging = false;
        }

        if (_isDragging && delta.sqrMagnitude > 0.01f)
        {
            float horizontalInput = delta.x * currentSensitivity;
            transform.Rotate(Vector3.up * -horizontalInput, Space.World); 
        }
    }

    // public void ResetRotation()
    // {
    //     _isDragging = false;
    //     _resetStartRotation = transform.rotation;
    //     _resetTimer = 0f;
    // }
}