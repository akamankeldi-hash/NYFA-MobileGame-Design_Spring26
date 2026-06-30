using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Text;

// AI Generated script, cause debugging was taking too long, and dead line was coming :\

public class GlobalUIClickLogger : MonoBehaviour
{
    private void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (EventSystem.current != null)
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Mouse.current.position.ReadValue();

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);

                if (results.Count > 0)
                {
                    GameObject clickedUIObject = results[0].gameObject;
                    
                    string fullHierarchyPath = GetFullHierarchyPath(clickedUIObject.transform);

                    Debug.Log($"[UI Hierarchy Debug] Clicked Element Path:\n{fullHierarchyPath}", clickedUIObject);
                }
            }
        }
    }

    private string GetFullHierarchyPath(Transform currentTransform)
    {
        StringBuilder pathBuilder = new StringBuilder(currentTransform.name);

        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
            pathBuilder.Insert(0, currentTransform.name + " > ");
        }
        
        return pathBuilder.ToString();
    }
}