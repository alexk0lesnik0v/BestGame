using Guns;
using TMPro;
using UnityEngine;

namespace Inventories
{
    public class BarabanOfBullets : MonoBehaviour
    {
        [SerializeField] private Revolver m_revolver;
        [SerializeField] private TMP_Text m_bulletsCountText;
        
        private void Awake()
        {
            this.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (this.gameObject.activeSelf)
            {
                m_bulletsCountText.text = m_revolver.m_bulletCount.ToString() + " / " + (m_revolver.m_bulletCount + m_revolver.m_bulletItemCount).ToString();
            }
        }
    }
}