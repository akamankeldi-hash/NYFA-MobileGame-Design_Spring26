using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Text;

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
                    
                    // Get the full breadcrumb path
                    string fullHierarchyPath = GetFullHierarchyPath(clickedUIObject.transform);

                    // Log the structural path to the console
                    Debug.Log($"[UI Hierarchy Debug] Clicked Element Path:\n{fullHierarchyPath}", clickedUIObject);
                }
            }
        }
    }

    private string GetFullHierarchyPath(Transform currentTransform)
    {
        // Using StringBuilder is highly performant for string accumulation
        StringBuilder pathBuilder = new StringBuilder(currentTransform.name);

        // Keep stepping up to parent transforms until hitting the scene root
        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
            // Insert the parent name at the beginning of the path
            pathBuilder.Insert(0, currentTransform.name + " > ");
        }

        return pathBuilder.ToString();
    }
}