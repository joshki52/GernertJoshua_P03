using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sources:
// https://gist.github.com/NickMercer/60b13551aaf8e3b86129c6a3ee35bc67
// https://www.reddit.com/r/Unity3D/comments/vyl3mr/ispointerovergameobject_equivalent_for/

public static class UIToolkitRaycastChecker
{
    private static HashSet<VisualElement> _blockingElements = new HashSet<VisualElement>();

    public static void RegisterBlockingElement(VisualElement blockingElement) =>
        _blockingElements.Add(blockingElement);

    public static void UnregisterBlockingElement(VisualElement blockingElement) =>
        _blockingElements.Remove(blockingElement);

    public static bool IsBlockingRaycasts(VisualElement element)
    {
        return _blockingElements.Contains(element) &&
               element.visible &&
               element.resolvedStyle.display == DisplayStyle.Flex;
    }

    public static bool IsPointerOverUI()
    {
        foreach (var element in _blockingElements)
        {
            if (IsBlockingRaycasts(element) == false)
                continue;

            if (ContainsMouse(element))
                return true;
        }

        return false;
    }

    private static bool ContainsMouse(VisualElement element)
    {
        var mousePosition = Mouse.current.position.ReadValue();
        var scaledMousePosition = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);

        var flippedPosition = new Vector2(scaledMousePosition.x, 1 - scaledMousePosition.y);
        var adjustedPosition = flippedPosition * element.panel.visualTree.layout.size;

        var localPosition = element.WorldToLocal(adjustedPosition);

        return element.ContainsPoint(localPosition);
    }

#if UNITY_EDITOR
    //This is used to reset the blocking elements set on playmode enter
    //to fix a bug if you have the quick enter playmode settings turned on
    //and don't unregister all your blocking elements before leaving playmode.
    [InitializeOnEnterPlayMode]
    private static void ResetBlockingElements()
    {
        _blockingElements = new HashSet<VisualElement>();
    }
#endif
}