using UnityEngine;
using Unity.Cinemachine;
using System;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CameraPoint[] m_cameras;
    [SerializeField] private int m_activePriority;
    [SerializeField] private int m_inactivePriority;

    private int m_current;

    private void Awake()
    {
        foreach(var item  in m_cameras)
        {
            item.camera.Priority = m_inactivePriority;
        }
        m_cameras[0].camera.Priority = m_activePriority;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_cameras[m_current].previousCamera != null)
            {
                for (int i = 0; i < m_cameras.Length; i++)
                {
                    if (m_cameras[i].camera == m_cameras[m_current].previousCamera)
                    {
                        SetCamera(i);
                        return;
                    }
                }
            }
        }
    }

    public void SetCamera(int id)
    {
        if (id < 0 || id >= m_cameras.Length || id == m_current)
        {
            return;
        }

        m_cameras[m_current].camera.Priority = m_inactivePriority;
        m_cameras[m_current].onClose.Invoke();

        m_cameras[id].camera.Priority = m_activePriority;
        m_current = id;

        m_cameras[id].onOpen.Invoke();
    }

    [Serializable]
    private struct CameraPoint
    {
        public CinemachineCamera camera;
        public CinemachineCamera previousCamera;
        public UnityEvent onOpen;
        public UnityEvent onClose;
    }
}
