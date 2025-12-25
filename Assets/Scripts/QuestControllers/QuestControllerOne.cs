using System;
using UnityEngine;

namespace QuestControllers
{
    public class QuestControllerOne : MonoBehaviour
    {
        [SerializeField] private GameObject m_doorLight1;
        [SerializeField] private GameObject m_doorLight2;
        [SerializeField] private GameObject m_doorLight3;
        [SerializeField] private GameObject m_doorLight4;
        [SerializeField] private DoorInteraction m_doorInteraction;

        private void Update()
        {
            if (!m_doorLight1.activeSelf && !m_doorLight2.activeSelf && !m_doorLight3.activeSelf &&
                !m_doorLight4.activeSelf)
            {
                m_doorInteraction.OpenDoorUp();
            }
            else
            {
                m_doorInteraction.OpenDoorDown();
            }
        }
    }
}