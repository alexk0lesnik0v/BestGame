using DG.Tweening;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 m_openDoorDown;
    [SerializeField] private Vector3 m_openDoorUp;
    [SerializeField] private float m_duration = 2.5f;
    [SerializeField] private AudioClip m_soundDuration;
    [SerializeField] private AudioSource m_audioSource;
    
    private bool m_isOpen = false;
    private Tweener m_tweener;

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
        m_tweener.Kill();
        if (m_isOpen)
        {
            return;
        }
        m_audioSource.PlayOneShot(m_soundDuration);
        m_tweener = transform.DOLocalMove(m_openDoorDown, m_duration);
        m_isOpen = true;
    }
    public void OpenDoorUp()
    {
        m_tweener.Kill();
        if (!m_isOpen)
        {
            return;
        }
        m_audioSource.PlayOneShot(m_soundDuration);
        m_tweener = transform.DOLocalMove(m_openDoorUp, m_duration);
        m_isOpen = false;
    }
}