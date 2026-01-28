using UnityEngine;

namespace Inventories
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items/Item")]
    public class ItemCreator : ItemScriptableObject
    {
        [SerializeField] private float m_healAmount;

        private void Start()
        {
            m_itemType =  ItemType.Food;
        }
    }
}