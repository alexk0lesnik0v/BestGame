using UnityEngine;

namespace Inventories
{
    public class ItemScriptableObject : ScriptableObject
    {
        [SerializeField] private string m_itemName;
        [SerializeField] private int m_maximumAmount;
        [SerializeField] private string m_itemDescription;
    }
}