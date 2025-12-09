using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public Camera _camera;
    void Start()
    {
      
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");

    }

}
