using DG.Tweening;
using UnityEngine;

public class BtnInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorInteraction m_doorInteraction;
    [SerializeField] private Lever m_lever;
    [SerializeField] private Timer m_timer;
    private bool m_isOpen = false;
    public bool CanInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        m_timer.RestartTimer();
        if (m_isOpen)
        {
            m_doorInteraction.OpenDoorUp();
            m_lever.LeverUp();
        }
        else
        {
            m_timer.StartTimer();
            m_doorInteraction.OpenDoorDown();
            m_lever.LeverDown();
        }
        m_isOpen = !m_isOpen;
        return true;
    }
}
