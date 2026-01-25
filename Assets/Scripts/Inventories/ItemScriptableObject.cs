using UnityEngine;

namespace Inventories
{
    public enum ItemType {Default, Food, Weapon, Instrument}
    public class ItemScriptableObject : ScriptableObject
    {
        [SerializeField] private string m_itemName;
        public GameObject m_itemPrefab;
        public int m_maximumAmount;
        public Sprite m_icon;
        public ItemType m_itemType;
        [SerializeField] private string m_itemDescription;
        public bool m_isConsumeable;
        
        [Header("Consumable Characteristics")]
        public float m_changeHealth;
        public float m_changeHunger;
        public float m_changeThirst;
    }
}