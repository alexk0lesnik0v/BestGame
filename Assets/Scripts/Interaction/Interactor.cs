using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float m_castDistance = 5f;
    [SerializeField] private GameObject m_interactionUI;
    [SerializeField] private Camera m_camera;

    private void Update()
    {
        if (DoInteractionTest(out IInteractable interactable))
        {
            if (interactable.CanInteract())
            {
                m_interactionUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactable.Interact(this);
                }
            }
            else
            {
                m_interactionUI.SetActive(false);
            }
        }
        else
        {
            m_interactionUI.SetActive(false); 
        }
    }

    private bool DoInteractionTest(out IInteractable interactable)
    {
        interactable = null;

        Ray ray = new Ray(m_camera.transform.position, m_camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, m_castDistance))
        {
            interactable = hitInfo.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
