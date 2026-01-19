using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventories
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_UIPanel;
        [SerializeField] private Transform m_inventoryPanel;
        [SerializeField] private List<InventorySlot> m_slots  = new List<InventorySlot>();
        
        public bool m_isOpened = false;

        private void Start()
        {
            for (int i = 0; i < m_inventoryPanel.childCount; i++)
            {
                if (m_inventoryPanel.GetChild(i).GetComponent<InventorySlot>() is not null)
                {
                    m_slots.Add(m_inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
                }
            }
            
            m_UIPanel.SetActive(false);
        }

        private void Update()
        {
            if (m_isOpened)
            {
                m_UIPanel.SetActive(true);
            }
            else
            {
                m_UIPanel.SetActive(false);
            }
        }
    }
}