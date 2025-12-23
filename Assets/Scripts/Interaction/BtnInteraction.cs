using UnityEngine;

public class BtnInteraction : MonoBehaviour, IInteractable
{
    //[SerializeField] private DoorInteraction m_doorInteraction;
    public bool CanInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        //m_doorInteraction.Interact(interactor);
        return true;
    }
}
