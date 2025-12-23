using DG.Tweening;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 m_targetRotation = new Vector3(0, -100f, 0);
    [SerializeField] private float m_rotationSpeed = 1f;

    private bool m_isOpen = false;

    public bool CanInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        if (m_isOpen)
        {
            transform.DORotate(-m_targetRotation, m_rotationSpeed, RotateMode.WorldAxisAdd);
        }
        else
        {
            transform.DORotate(m_targetRotation, m_rotationSpeed, RotateMode.WorldAxisAdd);
        }
        m_isOpen = !m_isOpen;

        return true;
    }
}

//public class ButtonDoor
//{
//    [SerializeField] DoorInteraction d;'
//    public bool Interact(Interactor interactor)
//    {
//        d.Open();;
//    }
//}
