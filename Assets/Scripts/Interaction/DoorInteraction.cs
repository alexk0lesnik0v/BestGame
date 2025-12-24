using DG.Tweening;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 m_openDoorDown;
    [SerializeField] private Vector3 m_openDoorUp;
    [SerializeField] private float m_duration = 2f;
    public bool CanInteract()
    {
        return false;
    }

    public bool Interact(Interactor interactor)
    {
        return true;
    }
    public void OpenDoorDown()
    {
        transform.DOLocalMove(m_openDoorDown, m_duration);
    }
    public void OpenDoorUp()
    {
        transform.DOLocalMove(m_openDoorUp, m_duration);
    }
}