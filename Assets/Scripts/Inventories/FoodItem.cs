using UnityEngine;

namespace Inventories
{
    [CreateAssetMenu(fileName = "Food Item", menuName = "Inventory/Items/Food Item")]
    public class FoodItem : ItemScriptableObject
    {
        [SerializeField] private float m_healAmount;

        private void Start()
        {
            m_itemType =  ItemType.Food;
        }
    }
}