using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Inventories
{
    public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public InventorySlot m_oldSlot;
        private Transform m_player;

        private void Start()
        {
            m_player = FindAnyObjectByType<Player>().transform;
            m_oldSlot = transform.GetComponentInParent<InventorySlot>();
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (m_oldSlot.m_isEmpty)
                return;
            GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (m_oldSlot.m_isEmpty)
             return;
            GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
            GetComponentInChildren<Image>().raycastTarget = false;
            transform.SetParent(transform.parent.parent.parent);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (m_oldSlot.m_isEmpty)
                return;
            GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
            GetComponentInChildren<Image>().raycastTarget = true;

            transform.SetParent(m_oldSlot.transform);
            transform.position = m_oldSlot.transform.position;
            
            if (eventData.pointerCurrentRaycast.gameObject.name == "UIPanel") // renamed to UIBG
            {
                // Выброс объектов из инвентаря - Спавним префаб обекта перед персонажем
                GameObject itemObject = Instantiate(m_oldSlot.m_item.m_itemPrefab, m_player.position + Vector3.up + m_player.forward, Quaternion.identity);
                // Устанавливаем количество объектов такое какое было в слоте
                itemObject.GetComponent<Item>().m_amount = m_oldSlot.m_amount;
                // убираем значения InventorySlot
                NullifySlotData();
            }
            else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent is null)
            {
                return;
            }
            else if(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() is not null)
            {
                //Перемещаем данные из одного слота в другой
                ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>());
            }
        }
        
        public void NullifySlotData() // made public 
        {
            // убираем значения InventorySlot
            m_oldSlot.m_item = null;
            m_oldSlot.m_amount = 0;
            m_oldSlot.m_isEmpty = true;
            m_oldSlot.m_iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            m_oldSlot.m_iconGO.GetComponent<Image>().sprite = null;
            m_oldSlot.m_itemAmountText.text = "";
        }
        void ExchangeSlotData(InventorySlot newSlot)
        {
            // Временно храним данные newSlot в отдельных переменных
            ItemScriptableObject item = newSlot.m_item;
            int amount = newSlot.m_amount;
            bool isEmpty = newSlot.m_isEmpty;
            GameObject iconGO = newSlot.m_iconGO;
            TMP_Text itemAmountText = newSlot.m_itemAmountText;

            // Заменяем значения newSlot на значения oldSlot
            newSlot.m_item = m_oldSlot.m_item;
            newSlot.m_amount = m_oldSlot.m_amount;
            if (!m_oldSlot.m_isEmpty)
            {
                newSlot.SetIcon(m_oldSlot.m_iconGO.GetComponent<Image>().sprite);
                newSlot.m_itemAmountText.text = m_oldSlot.m_amount.ToString();
            }
            else
            {
                newSlot.m_iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                newSlot.m_iconGO.GetComponent<Image>().sprite = null;
                newSlot.m_itemAmountText.text = "";
            }
            
            newSlot.m_isEmpty = m_oldSlot.m_isEmpty;

            // Заменяем значения oldSlot на значения newSlot сохраненные в переменных
            m_oldSlot.m_item = item;
            m_oldSlot.m_amount = amount;
            if (!isEmpty)
            {
                m_oldSlot.SetIcon(iconGO.GetComponent<Image>().sprite);
                m_oldSlot.m_itemAmountText.text = amount.ToString();
            }
            else
            {
                m_oldSlot.m_iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                m_oldSlot.m_iconGO.GetComponent<Image>().sprite = null;
                m_oldSlot.m_itemAmountText.text = "";
            }
            
            m_oldSlot.m_isEmpty = isEmpty;
        }
    }
}