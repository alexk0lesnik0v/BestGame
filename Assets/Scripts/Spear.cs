using DG.Tweening;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private Vector3 m_isActive;
    [SerializeField] private Vector3 m_isNoActive;
    [SerializeField] private float m_duration;

    public void IsActive()
    {
        transform.DOLocalMove(m_isActive, m_duration);
    }
    public void IsNoActive()
    {
        transform.DOLocalMove(m_isNoActive, m_duration);
    }
}
