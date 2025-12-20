using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float m_castDistance = 5f;
    [SerializeField] private Vector3 m_raycastOffset = new Vector3(0, 1f, 0);
    [SerializeField] private GameObject m_interactionUI;

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

        Ray ray = new Ray(transform.position + m_raycastOffset, transform.forward);

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
