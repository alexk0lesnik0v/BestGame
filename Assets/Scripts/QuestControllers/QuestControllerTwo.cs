using Inventories;
using UnityEngine;

namespace QuestControllers
{
    public class QuestControllerTwo : MonoBehaviour, IInteractable
    {
        [SerializeField] private DoorInteraction m_doorInteraction;
        [SerializeField] private Lever m_lever;
        [SerializeField] private Timer m_timer;
        
        
        private int m_figurkaAmount = 0;
        private bool m_doorCanOpen = false;
        private bool m_isOpen = false;
        
        public bool CanInteract()
        {
            if (m_doorCanOpen)
            {
                return true;
            }
            else
            {
                Debug.Log("Can't Interact! You need " + (5 - m_figurkaAmount)  + " figurkas!");
                return false;
            }
        }

        public bool Interact(Interactor interactor)
        {
            m_timer.RestartTimer();
            if (m_isOpen)
            {
                m_doorInteraction.OpenDoorUp();
                m_lever.LeverUp();
            }
            else
            {
                m_timer.StartTimer();
                m_doorInteraction.OpenDoorDown();
                m_lever.LeverDown();
            }
            m_isOpen = !m_isOpen;
            return true;
        }
        
        public void AddFigurka(ItemScriptableObject figurka)
        {
            m_figurkaAmount++;
        }

        private void Update()
        {
            if (m_figurkaAmount == 5)
            {
                m_doorCanOpen = true;
            }
        }
    }
}