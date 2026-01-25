using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Inventories
{
    public class QuickslotInventory : MonoBehaviour
    {
        public Transform m_quickslotParent;
        public InventoryManager m_inventoryManager;
        public int m_currentQuickslotID = 0;
        public Sprite m_selectedSprite;
        public Sprite m_notSelectedSprite;
        
        private Player m_player;

        private void Start()
        {
            m_player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            float mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw > 0.1)
            {
                m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_notSelectedSprite;
                
                if (m_currentQuickslotID >= m_quickslotParent.childCount-1)
                {
                    m_currentQuickslotID = 0;
                }
                else
                {
                    m_currentQuickslotID++;
                }
                
                m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_selectedSprite;
            }
            
            if (mw < -0.1)
            {
                m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_notSelectedSprite;
                if (m_currentQuickslotID <= 0)
                {
                    m_currentQuickslotID = m_quickslotParent.childCount-1;
                }
                else
                {
                    m_currentQuickslotID--;
                }
                
                m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_selectedSprite;
            }
            
            for(int i = 0; i < m_quickslotParent.childCount; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString())) 
                {
                    if (m_currentQuickslotID == i)
                    {
                        if (m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite == m_notSelectedSprite)
                        {
                            m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_selectedSprite;
                        }
                        else
                        {
                            m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_notSelectedSprite;
                        }
                    }
                    else
                    {
                        m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_notSelectedSprite;
                        m_currentQuickslotID = i;
                        m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_selectedSprite;
                    }
                }
            }
        }

        public void UseItem()
        { 
            if (m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>().m_item is not null)
            {
                if (m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>().m_item.m_isConsumeable && !m_inventoryManager.m_isOpened && m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite == m_selectedSprite)
                {
                    ChangeCharacteristics();

                    if (m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>().m_amount <= 1)
                    {
                        m_quickslotParent.GetChild(m_currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
                    }
                    else
                    {
                        m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>().m_amount--;
                        m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>().m_itemAmountText.text = m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>().m_amount.ToString();
                    }
                }
            }
        }

        private void ChangeCharacteristics()
        {
            if(m_player.health + m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>().m_item.m_changeHealth <= 100)
            {
                float heal = m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>().m_item.m_changeHealth;
                m_player.Heal(heal);
                
                Debug.Log("Health " + m_player.health);
            }
            else
            {
                m_player.Heal(100);
                
                Debug.Log("Health " + m_player.health);
            }
        }
    }
}