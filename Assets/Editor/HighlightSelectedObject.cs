using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class HighlightSelectedObject
{
    private static GameObject lastSelectedObject;
    private static Color originalColor;

    static HighlightSelectedObject()
    {
        Selection.selectionChanged += OnSelectionChanged;
    }

    private static void OnSelectionChanged()
    {
        if (lastSelectedObject != null)
        {
            RestoreOriginalColor();
        }

        if (Selection.activeGameObject != null)
        {
            ApplyHighlight(Selection.activeGameObject);
        }
    }

    private static void ApplyHighlight(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            lastSelectedObject = obj;
            originalColor = renderer.sharedMaterial.color;
            renderer.sharedMaterial.color = Color.green;
        }
    }

    private static void RestoreOriginalColor()
    {
        if (lastSelectedObject != null)
        {
            Renderer renderer = lastSelectedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial.color = originalColor;
            }
            lastSelectedObject = null;
        }
    }
}
