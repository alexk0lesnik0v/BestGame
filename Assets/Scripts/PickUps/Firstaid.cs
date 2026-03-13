using UnityEngine;

namespace PickUps
{
    public class Firstaid : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;
        
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