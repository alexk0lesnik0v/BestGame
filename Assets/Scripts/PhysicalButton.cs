using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalButton : MonoBehaviour
{

    [SerializeField] private UnityEvent onClick;
    [SerializeField] private float m_animationDuration;
    [SerializeField] private Transform m_movable;
    [SerializeField] private Vector3 m_targetLocalPosition;

    private bool m_isAnimating;

    private void OnMouseUpAsButton()
    {
        if (m_isAnimating)
        {
            return;
        }
        m_isAnimating = true;
        m_movable.DOLocalMove(m_targetLocalPosition, m_animationDuration / 2).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        onClick.Invoke();
        m_movable.DOLocalMove(Vector3.zero, m_animationDuration / 2).OnComplete(() => m_isAnimating = false);
    }

}
