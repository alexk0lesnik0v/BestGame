using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Inventories
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_UIBG;
        [SerializeField] private Transform m_inventoryPanel;
        [SerializeField] private Transform m_quickslotPanel;
        [SerializeField] private QuickslotInventory m_quickslotInventory;
        [SerializeField] private List<InventorySlot> m_slots  = new List<InventorySlot>();
        [SerializeField] private float m_reachDistance = 3f;
        
        public bool m_isOpened = false;
        
        private Camera m_mainCamera;

        private void Awake()
        {
            m_UIBG.SetActive(true);
        }

        private void Start()
        {
            m_mainCamera = Camera.main;
            
            for (int i = 0; i < m_quickslotPanel.childCount; i++)
            {
                if (m_quickslotPanel.GetChild(i).GetComponent<InventorySlot>() is not null)
                {
                    m_slots.Add(m_quickslotPanel.GetChild(i).GetComponent<InventorySlot>());
                }
            }
            
            for (int i = 0; i < m_inventoryPanel.childCount; i++)
            {
                if (m_inventoryPanel.GetChild(i).GetComponent<InventorySlot>() is not null)
                {
                    m_slots.Add(m_inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
                }
            }
            
            m_UIBG.SetActive(false);
            m_inventoryPanel.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (m_isOpened)
            {
                m_UIBG.SetActive(true);
                m_inventoryPanel.gameObject.SetActive(true);
            }
            else
            {
                m_UIBG.SetActive(false);
                m_inventoryPanel.gameObject.SetActive(false);
            }
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
                }
            }

            foreach (InventorySlot slot in m_slots)
            {
                if (slot.m_isEmpty)
                {
                    slot.m_item = item;
                    slot.m_amount = amount;
                    slot.m_isEmpty = false;
                    slot.SetIcon(item.m_icon);
                    if (slot.m_item.m_maximumAmount != 1)
                    {
                        slot.m_itemAmountText.text = amount.ToString();
                    }
                    break;
                }
            }
            
            m_quickslotInventory.CheckItemInHand();
        }

        public void UseBulletItem(int amount)
        {
            foreach (InventorySlot slot in m_slots)
            {
                if (!slot.m_isEmpty)
                {
                    if (slot.m_item.m_itemType == ItemType.Bullet)
                    {
                        slot.m_amount -= amount;
                        slot.m_itemAmountText.text = slot.m_amount.ToString();
                        if (slot.m_amount <= 0)
                        {
                            slot.m_item = null;
                            slot.m_amount = 0;
                            slot.m_isEmpty = true;
                            slot.m_iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                            slot.m_iconGO.GetComponent<Image>().sprite = null;
                            slot.m_itemAmountText.text = "";
                        }
                    }
                }
            }
        }
    }
}