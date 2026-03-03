using DG.Tweening;
using UnityEngine;

public class CameraMoveForward : MonoBehaviour
{
    public void MoveForward()
    {
        transform.DOLocalMove(new Vector3(-1.722f, 2.09500003f, -2.83599997f), 2);
    }
}
