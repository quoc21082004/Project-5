using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public abstract class BaseInteractable : MonoBehaviour, IInteract
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerInteract>(out var interact))
            PlayerInteract.Assign(this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerInteract>(out var interact))
            PlayerInteract.CancelAssign(this);
    }
    protected virtual void OnDisable()
    {
        PlayerInteract.CancelAssign(this);
    }
    public virtual void StartAssign()
    {
        //gameObject.SetActive(true);
    }
    public virtual void EndAssign()
    {
        //gameObject.SetActive(false);
    }
    public abstract void Interact();
}
