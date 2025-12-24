using DG.Tweening;
using UnityEngine;

public class BtnInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorInteraction m_doorInteraction;
    private bool m_isOpen = false;
    public bool CanInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        if (m_isOpen)
        {
            m_doorInteraction.OpenDoorDown();
        }
        else
        {
            m_doorInteraction.OpenDoorUp();
        }
        m_isOpen = !m_isOpen;
        return true;
    }
}
