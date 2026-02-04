using Guns;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Inventories
{
    public class QuickslotInventory : MonoBehaviour
    {
        [SerializeField] private Revolver m_revolver;
        [SerializeField] private Axe m_axe;
        public Transform m_quickslotParent;
        public InventoryManager m_inventoryManager;
        public int m_currentQuickslotID = 0;
        public Sprite m_selectedSprite;
        public Sprite m_notSelectedSprite;
        public InventorySlot m_activeSlot =  null;
        
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
                m_activeSlot = m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>();
                ShowItemInHand();
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
                m_activeSlot = m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>();
                ShowItemInHand();
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
                            m_activeSlot = m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>();
                            ShowItemInHand();
                        }
                        else
                        {
                            m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_notSelectedSprite;
                            m_activeSlot = null;
                        }
                    }
                    else
                    {
                        m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_notSelectedSprite;
                        m_currentQuickslotID = i;
                        m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<Image>().sprite = m_selectedSprite;
                        m_activeSlot = m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>();
                        ShowItemInHand();
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

        public void CheckItemInHand()
        {
            if (m_activeSlot is not null)
            {
                ShowItemInHand();
            }
            else
            {
                HideItemInHand();
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
                m_player.Heal(0);
                
                Debug.Log("Health " + m_player.health);
            }
        }

        private void ShowItemInHand()
        {
            if (m_activeSlot.m_item is not null)
            {
                if (m_activeSlot.m_item.m_itemType == ItemType.Weapon)
                {
                    if (m_activeSlot.m_item.m_itemName == "Revolver")
                    {
                        m_axe.gameObject.SetActive(false);
                        m_revolver.gameObject.SetActive(true);
                    }
                    else if (m_activeSlot.m_item.m_itemName == "Axe")
                    {
                        m_revolver.gameObject.SetActive(false);
                        m_axe.gameObject.SetActive(true);
                    }
                }
                else
                {
                    m_revolver.gameObject.SetActive(false);
                    m_axe.gameObject.SetActive(false);
                }
            }
            else
            {
                m_revolver.gameObject.SetActive(false);
                m_axe.gameObject.SetActive(false);
            }
        }

        private void HideItemInHand()
        {
            m_revolver.gameObject.SetActive(false);
            m_axe.gameObject.SetActive(false);
        }
    }
}