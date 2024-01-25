using UnityEngine;
public class PlayerInteract : MonoBehaviour
{
    private static IInteract currentAssigned;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            InteractWithCurrent();
    }
    public static void Assign<T>(T interactable) where T : MonoBehaviour, IInteract
    {
        if (currentAssigned != null)
        {
            currentAssigned.EndAssign();
        }
        currentAssigned = interactable;
        interactable.StartAssign();
    }
    public static void CancelAssign<T>(T interactable) where T : MonoBehaviour, IInteract
    {
        if (IsCurrentAssined(interactable))
        {
            currentAssigned.EndAssign();
            currentAssigned = null;
        }
    }
    public static bool IsCurrentAssined<T>(T interactable) where T : MonoBehaviour, IInteract
    {
        return interactable == currentAssigned;
    }
    public void InteractWithCurrent()
    {
        if (currentAssigned != null)
            currentAssigned.Interact();
    }
}
