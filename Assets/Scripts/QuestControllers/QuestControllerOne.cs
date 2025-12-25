using System;
using Interaction;
using UnityEngine;

namespace QuestControllers
{
    public class QuestControllerOne : MonoBehaviour
    {
        [SerializeField] private GameObject m_doorLight1;
        [SerializeField] private GameObject m_doorLight2;
        [SerializeField] private GameObject m_doorLight3;
        [SerializeField] private GameObject m_doorLight4;
        
        [SerializeField] private OnePlatformController m_button1;
        [SerializeField] TestButtonPush m_button2;
        [SerializeField] TestButtonPush m_button3;
        [SerializeField] TestButtonPush m_button4;
        
        [SerializeField] private DoorInteraction m_doorInteraction;

        private int m_winIndex = 0;

        private void Update()
        {
            if (m_doorLight1.activeSelf && m_doorLight2.activeSelf && m_doorLight3.activeSelf &&
                m_doorLight4.activeSelf)
            {
                m_winIndex = 0;
            }

            if (m_button1.m_isPushed && !m_button2.m_isPushed && m_button3.m_isPushed)
            {
                m_winIndex = 0;
            }

            if (m_button1.m_isPushed && !m_button3.m_isPushed && m_button4.m_isPushed)
            {
                m_winIndex = 0;
            }
            
            if (!m_doorLight1.activeSelf && m_doorLight2.activeSelf && m_doorLight3.activeSelf &&
                m_doorLight4.activeSelf && m_winIndex == 0)
            {
                m_winIndex = 1;
            }
            
            if (!m_doorLight1.activeSelf && !m_doorLight2.activeSelf && m_doorLight3.activeSelf &&
                m_doorLight4.activeSelf  && m_winIndex == 1)
            {
                m_winIndex = 2;
            }
            
            if (!m_doorLight1.activeSelf && !m_doorLight2.activeSelf && !m_doorLight3.activeSelf &&
                m_doorLight4.activeSelf  && m_winIndex == 2)
            {
                m_winIndex = 3;
            }
            
            if (!m_doorLight1.activeSelf && !m_doorLight2.activeSelf && !m_doorLight3.activeSelf &&
                !m_doorLight4.activeSelf && m_winIndex == 3)
            {
                m_winIndex = 4;
            }
            
            if (m_winIndex ==4)
            {
                m_doorInteraction.OpenDoorUp();
            }
        }
    }
}