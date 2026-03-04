using DG.Tweening;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private Vector3 m_isActive;
    [SerializeField] private Vector3 m_isNoActive;
    [SerializeField] private float m_duration;

    private Tweener m_tweener;

    public void IsActive()
    {
        m_tweener.Kill();
        m_tweener = transform.DOLocalMove(m_isActive, m_duration);
    }

    public void IsNoActive()
    {
        m_tweener.Kill();
        m_tweener = transform.DOLocalMove(m_isNoActive, m_duration);
    }

}
