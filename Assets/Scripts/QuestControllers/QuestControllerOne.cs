using System;
using Interaction;
using Players;
using UnityEngine;

namespace QuestControllers
{
    public class QuestControllerOne : MonoBehaviour
    {
        [SerializeField] private GameObject m_doorLight1;
        [SerializeField] private GameObject m_doorLight2;
        [SerializeField] private GameObject m_doorLight3;
        [SerializeField] private GameObject m_doorLight4;
        
        [SerializeField] private GameObject m_doorGreenLight1;
        [SerializeField] private GameObject m_doorGreenLight2;
        [SerializeField] private GameObject m_doorGreenLight3;
        [SerializeField] private GameObject m_doorGreenLight4;
        
        [SerializeField] private OnePlatformController m_button1;
        [SerializeField] TestButtonPush m_button2;
        [SerializeField] TestButtonPush m_button3;
        [SerializeField] TestButtonPush m_button4;
        
        [SerializeField] private DoorInteraction m_doorQuestInteraction;
        [SerializeField] private DoorInteraction m_doorInteraction;
        
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_audioClip;

        private bool m_isOne = false;
        private bool m_isTue = false;
        private bool m_isThree = false;
        private bool m_isFour = false;
        
        private int m_winIndex = 0;
        
        private void Start()
        {
            m_doorLight1.SetActive(true);
            m_doorLight2.SetActive(true);
            m_doorLight3.SetActive(true);
            m_doorLight4.SetActive(true);
            
            m_doorGreenLight1.SetActive(false);
            m_doorGreenLight2.SetActive(false);
            m_doorGreenLight3.SetActive(false);
            m_doorGreenLight4.SetActive(false);
        }

        private void Update()
        {
            if (!m_button1.m_isPushed)
            {
                m_winIndex = 0;
            }
            
            if (m_button1.m_isPushed && !m_button2.m_isPushed && m_button3.m_isPushed)
            {
                m_winIndex = 1;
            }

            if (m_button1.m_isPushed && !m_button3.m_isPushed && m_button4.m_isPushed)
            {
                if (m_winIndex == 2)
                {
                    m_winIndex = 2;
                }
                else if (m_winIndex == 1)
                {
                    m_winIndex = 1;
                }
            }

            if (m_winIndex == 2 && !m_button2.m_isPushed)
            {
                m_winIndex = 1;
                m_isTue = false;
                m_doorLight2.SetActive(true);
                m_doorGreenLight2.SetActive(false);
            }
            
            if (m_winIndex == 3 && !m_button3.m_isPushed)
            {
                m_winIndex = 2;
                m_isThree = false;
            }
            
            if (m_button1.m_isPushed && !m_button2.m_isPushed && !m_button3.m_isPushed &&
                !m_button4.m_isPushed && m_winIndex == 0)
            {
                m_winIndex = 1;
                
                if (!m_isOne)
                {
                    m_audioSource.PlayOneShot(m_audioClip);
                    m_isOne = true;
                }
            }
            
            if (m_button1.m_isPushed && m_button2.m_isPushed && !m_button3.m_isPushed &&
                !m_button4.m_isPushed && m_winIndex == 1)
            {
                m_winIndex = 2;
                
                if (!m_isTue)
                {
                    m_audioSource.PlayOneShot(m_audioClip);
                    m_isTue = true;
                }
            }
            
            if (m_button1.m_isPushed && m_button2.m_isPushed && m_button3.m_isPushed &&
                !m_button4.m_isPushed && m_winIndex == 2)
            {
                m_winIndex = 3;
                
                if (!m_isThree)
                {
                    m_audioSource.PlayOneShot(m_audioClip);
                    m_isThree = true;
                }
            }
            
            if (m_button1.m_isPushed && m_button2.m_isPushed && m_button3.m_isPushed &&
                m_button4.m_isPushed && m_winIndex == 3)
            {
                m_winIndex = 4;
                
                if (!m_isFour)
                {
                    m_audioSource.PlayOneShot(m_audioClip);
                    m_isFour = true;
                }
            }
            
            if (m_winIndex ==4)
            {
                m_doorQuestInteraction.OpenDoorDown();
            }

            if (m_winIndex == 1 &&  m_button1.m_isPushed)
            {
                m_doorLight1.SetActive(false);
                m_doorGreenLight1.SetActive(true);
            }
            
            if (m_winIndex == 2 && m_button2.m_isPushed)
            {
                m_doorLight2.SetActive(false);
                m_doorGreenLight2.SetActive(true);
            }
            
            if (m_winIndex == 3 && m_button3.m_isPushed || m_winIndex ==4 && m_button4.m_isPushed)
            {
                m_doorLight3.SetActive(false);
                m_doorGreenLight3.SetActive(true);
            }
            else
            {
                m_doorLight3.SetActive(true);
                m_doorGreenLight3.SetActive(false);
            }
            
            if (m_winIndex == 4  && m_button4.m_isPushed)
            {
                m_doorLight4.SetActive(false);
                m_doorGreenLight4.SetActive(true);
            }

            if (m_winIndex == 0)
            {
                m_doorLight1.SetActive(true);
                m_doorLight2.SetActive(true);
                m_doorLight3.SetActive(true);
                m_doorLight4.SetActive(true);
                
                m_doorGreenLight1.SetActive(false);
                m_doorGreenLight2.SetActive(false);
                m_doorGreenLight3.SetActive(false);
                m_doorGreenLight4.SetActive(false);
                
                m_isOne = false;
                m_isTue = false;
                m_isThree = false;
                m_isFour = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                m_doorInteraction.OpenDoorUp();
            }
        }
    }
}