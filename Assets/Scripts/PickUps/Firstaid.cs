using UnityEngine;

namespace PickUps
{
    public class Firstaid : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private AudioSource m_audioSource;
        
        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_animator.Play("FirstaidOnHand");
        }
        
        public void FirstaidOnHand()
        {
            m_animator.Play("FirstaidOnHand");
        }
    }
}