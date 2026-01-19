using UnityEngine;

namespace Inventories
{
    public class InventorySlot : MonoBehaviour
    {
        public ItemScriptableObject m_item;
        public int m_amount;
        public bool m_isEmpty = true;
    }
}