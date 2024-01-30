using UnityEngine;
public class NPC : BaseInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    public override void Interact()
    {
        DialogueManager.instance.ShowDialogue(dialogueObject);
    }
    public override void StartAssign()
    {
        base.StartAssign();
    }
    public override void EndAssign()
    {
        base.EndAssign();
    }
}
