using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventories
{
    public class InventorySlot : MonoBehaviour
    {
        public ItemScriptableObject m_item;
        public int m_amount;
        public bool m_isEmpty = true;
        public GameObject m_iconGO;
        public TMP_Text m_itemAmountText;

        private void Awake()
        {
            m_iconGO = transform.GetChild(0).GetChild(0).gameObject;
            m_itemAmountText = transform.GetChild(0).GetChild(1).gameObject.GetComponent<TMP_Text>();
        }

        public void SetIcon(Sprite icon)
        {
            m_iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_iconGO.GetComponent<Image>().sprite = icon;
        }
    }
}