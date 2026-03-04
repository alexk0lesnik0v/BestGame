using DG.Tweening;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Tweener m_tweener;
    public void LeverDown()
    {
        m_tweener.Kill();
        m_tweener = transform.DOLocalRotate(new Vector3(-114, 0, 0), 1, RotateMode.Fast);
    }
    public void LeverUp()
    {
        m_tweener.Kill();
        m_tweener = transform.DOLocalRotate(new Vector3(-33, 0, 0), 1, RotateMode.Fast);
    }

}
