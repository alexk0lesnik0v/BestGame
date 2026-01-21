using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventories
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_UIPanel;
        [SerializeField] private Transform m_inventoryPanel;
        [SerializeField] private List<InventorySlot> m_slots  = new List<InventorySlot>();
        [SerializeField] private float m_reachDistance = 3f;
        
        public bool m_isOpened = false;
        
        private Camera m_mainCamera;

        private void Awake()
        {
            m_UIPanel.SetActive(true);
        }

        private void Start()
        {
            m_mainCamera = Camera.main;
            
            for (int i = 0; i < m_inventoryPanel.childCount; i++)
            {
                if (m_inventoryPanel.GetChild(i).GetComponent<InventorySlot>() is not null)
                {
                    m_slots.Add(m_inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
                }
            }
            
            m_UIPanel.SetActive(false);
            m_inventoryPanel.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (m_isOpened)
            {
                m_UIPanel.SetActive(true);
                m_inventoryPanel.gameObject.SetActive(true);
            }
            else
            {
                m_UIPanel.SetActive(false);
                m_inventoryPanel.gameObject.SetActive(false);
            }
            
            /*Ray ray = m_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, m_reachDistance))
            {
                if (hit.collider.TryGetComponent(out Item item))
                {
                    AddItem(item.m_item, item.m_amount);
                    Destroy(item.gameObject);
                }
            }*/
        }

        public void AddItem(ItemScriptableObject item, int amount)
        {
            foreach (InventorySlot slot in m_slots)
            {
                if (slot.m_item == item)
                {
                    if (slot.m_amount + amount <= item.m_maximumAmount)
                    {
                        slot.m_amount += amount;
                        slot.m_itemAmountText.text = slot.m_amount.ToString();
                        return;
                    }
                    continue;
                }
                else if (slot.m_isEmpty)
                {
                    slot.m_item = item;
                    slot.m_amount = amount;
                    slot.m_isEmpty = false;
                    slot.SetIcon(item.m_icon);
                    slot.m_itemAmountText.text = amount.ToString();
                    break;
                }
            }
        }
    }
}