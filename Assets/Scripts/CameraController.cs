using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private Vector3 m_cameraPoint = new Vector3(0f, 0.5f, 0.5f);

    private Transform m_playerPosition;

    private void Start()
    {
        m_playerPosition = m_playerController.transform;
    }

    void LateUpdate()
    {
        this.transform.position = m_playerPosition.TransformPoint(m_cameraPoint);
    }

}
