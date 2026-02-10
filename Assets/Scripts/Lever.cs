using DG.Tweening;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public void LeverDown()
    {
        transform.DOLocalRotate(new Vector3(-114, 0, 0), 1, RotateMode.Fast);
    }
    public void LeverUp()
    {
        transform.DOLocalRotate(new Vector3(-33, 0, 0), 1, RotateMode.Fast);
    }

}
