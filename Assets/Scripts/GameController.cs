using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    private void OnGUI()
    {
        int m_size = 12;
        float m_posX = m_camera.pixelWidth / 2 - m_size / 4;
        float m_posY = m_camera.pixelHeight / 2 - m_size / 2;
        GUI.Label(new Rect(m_posX, m_posY, m_size, m_size), "*");
    }
}
