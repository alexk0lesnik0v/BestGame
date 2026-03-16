using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SimpleGost : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_audioClip;
        [SerializeField] private Transform m_targetPosition;
        
        private Animator m_animator;
        
        private NavMeshAgent m_agent;

        private void Start()
        {
            m_agent = GetComponent<NavMeshAgent>();
            m_animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (this.gameObject.activeSelf)
            {
                m_agent.SetDestination(m_targetPosition.position);
                m_agent.speed = 10f;
                
                Destroy(this.gameObject, 2f);
            }
        }
    }
}