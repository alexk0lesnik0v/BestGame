using Guns;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Inventories
{
    public class QuickslotInventory : MonoBehaviour
    {
        [SerializeField] private Revolver m_revolver;
        [SerializeField] private BarabanOfBullets m_baraban;
        [SerializeField] private Axe m_axe;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_itemSounds;
        [SerializeField] private Transform m_objectGrabPointTransform;
        
        public Transform m_quickslotParent;
        public InventoryManager m_inventoryManager;
        public int m_currentQuickslotID = 0;
        public Sprite m_selectedSprite;
        public Sprite m_notSelectedSprite;
        public InventorySlot m_activeSlot =  null;
        
        private Player m_player;
        private GameObject m_itemPrefab;
        
        private void Start()
        {
            m_player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            float mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw < -0.1)
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
            
            if (mw > 0.1)
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
                            ShowItemInHand();
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

        public void MakeNotSelectedSlots()
        {
            for (int i = 0; i < m_quickslotParent.childCount; i++)
            {
                m_quickslotParent.GetChild(i).GetComponent<Image>().sprite = m_notSelectedSprite;

                if (m_quickslotParent.GetChild(i).GetComponent<InventorySlot>() == m_activeSlot)
                {
                    m_currentQuickslotID = i;
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

            CheckItemInHand();
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
            float heal = m_quickslotParent.GetChild(m_currentQuickslotID).GetComponent<InventorySlot>().m_item.m_changeHealth;
            m_player.Heal(heal);
                
            Debug.Log("Health " + m_player.health);
        }

        private void ShowItemInHand()
        {
            if (m_activeSlot is null)
            {
                HideItemInHand();
                return;
            }
            
            if (m_activeSlot.m_item is not null)
            {
                if (m_itemPrefab is not null)
                {
                    Destroy(m_itemPrefab);
                }
                
                if (m_activeSlot.m_item.m_itemType == ItemType.Weapon)
                {
                    if (m_activeSlot.m_item.m_itemName == "Revolver")
                    {
                        m_axe.gameObject.SetActive(false);

                        if (!m_revolver.gameObject.activeSelf)
                        {
                            m_revolver.gameObject.SetActive(true);
                            m_revolver.RevolverOnHand();
                            m_baraban.gameObject.SetActive(true);
                            
                            m_audioSource.PlayOneShot(m_itemSounds);
                        }
                    }
                    else if (m_activeSlot.m_item.m_itemName == "Axe")
                    {
                        m_revolver.gameObject.SetActive(false);
                        m_baraban.gameObject.SetActive(false);

                        if (!m_axe.gameObject.activeSelf)
                        {
                            m_axe.gameObject.SetActive(true);
                            m_axe.AxeOnHand();
                            
                            m_audioSource.PlayOneShot(m_itemSounds);
                        }
                    }
                }
                else if (m_activeSlot.m_item.m_itemType == ItemType.Firstaid)
                {
                    HideItemInHand();
                    var obj = Instantiate(m_activeSlot.m_item.m_itemPrefab, m_objectGrabPointTransform);
                    obj.GetComponent<Rigidbody>().isKinematic = true;
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localEulerAngles = new Vector3(20f, 180f, 0f);
                    obj.gameObject.layer = 9;
                    m_audioSource.PlayOneShot(m_itemSounds);

                    m_itemPrefab = obj;
                }
                else
                {
                   HideItemInHand();
                }
            }
            else
            {
                HideItemInHand();
            }
        }

        public void HideItemInHand()
        {
            m_revolver.gameObject.SetActive(false);
            m_baraban.gameObject.SetActive(false);
            m_axe.gameObject.SetActive(false);
            if (m_itemPrefab is not null)
            {
                Destroy(m_itemPrefab);
            }
            
            m_audioSource.PlayOneShot(m_itemSounds);
        }
    }
}