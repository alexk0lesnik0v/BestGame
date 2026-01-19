using UnityEngine;

namespace Inventories
{
    public enum ItemType {Default, Food, Weapon, Instrument}
    public class ItemScriptableObject : ScriptableObject
    {
        [SerializeField] private string m_itemName;
        [SerializeField] private int m_maximumAmount;
        [SerializeField] private GameObject m_itemPrefab;
        public Sprite m_icon;
        public ItemType m_itemType;
        [SerializeField] private string m_itemDescription;
    }
}