using UnityEngine;

public class ShowComponents : MonoBehaviour
{
    [ContextMenu("Показать компоненты")]
    void PrintComponents()
    {
        var components = GetComponents<Component>();
        Debug.Log($"=== Компоненты на {gameObject.name} ===");
        foreach (var comp in components)
        {
            Debug.Log($"- {comp.GetType().FullName}");
        }
    }
}