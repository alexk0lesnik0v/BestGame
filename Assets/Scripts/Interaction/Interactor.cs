using System;
using Enemies;
using Inventories;
using PickUps;
using QuestControllers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public event Action OnGostView;
    
    [SerializeField] private float m_castDistance = 3f;
    [SerializeField] private GameObject m_interactionUI;
    [SerializeField] private TMP_Text m_interactionText;
    [SerializeField] private Camera m_camera;
    [SerializeField] private QuestControllerTwo m_questControllerTwo;

    private bool m_questController = false;
    
    public bool m_isGrab = false;

    private void Update()
    {
        if (DoInteractionTest(out IInteractable interactable))
        {
            if (interactable.CanInteract())
            {
                m_interactionText.text = "Нажмите 'F' для взаимодействия";
                m_interactionUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactable.Interact(this);
                }
            }
            else
            {
                m_interactionText.text = "";
                m_interactionUI.SetActive(false);

                if (m_questController)
                {
                    int figurkasCount = (5 - m_questControllerTwo.m_figurkaAmount);

                    switch (figurkasCount)
                    {
                        case 5:
                            m_interactionText.text = "Не доступно! Вам нужно ещё 5 статуэток!"; 
                            break;
                        
                        case 4:
                        case 3: 
                        case 2:
                            m_interactionText.text = "Не доступно! Вам нужно ещё " + figurkasCount.ToString() + " статуэтки!";
                            break;
                        
                        case 1:
                            m_interactionText.text = "Не доступно! Вам нужна ещё 1 статуэтка!";
                            break;
                    }
                    
                    m_interactionUI.SetActive(true);
                }
            }
        }
        else if (DoPickUpTest())
        {
            if (!m_isGrab)
            {
                m_interactionText.text = "Нажмите 'E' чтобы взять";
            }
            else
            {
                m_interactionText.text = "Нажмите 'E' чтобы бросить";
            }
            
            m_interactionUI.SetActive(true);
        }
        else if (DoGostTest())
        {
            OnGostView?.Invoke();
        }
        else
        {
            m_interactionText.text = "";
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
                if (hitInfo.collider.TryGetComponent(out m_questControllerTwo))
                {
                    m_questController = true;
                }
                
                return true;
            }
        }
        return false;
    }

    private bool DoPickUpTest()
    {
        Ray ray = m_camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, m_castDistance))
        {
            if (_hit.collider.TryGetComponent(out Item item) || _hit.transform.TryGetComponent(out ObjectGrabbable objectGrabbable))
            {
                return true;
            }
        }
        return false;
    }
    
    private bool DoGostTest()
    {
        Ray ray = m_camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, 10f))
        {
            if (_hit.collider.TryGetComponent(out Gost gost))
            {
                return true;
            }
        }
        return false;
    }
}
