using Guns;
using TMPro;
using UnityEngine;

namespace Inventories
{
    public class BarabanOfBullets : MonoBehaviour
    {
        [SerializeField] private Revolver m_revolver;
        [SerializeField] private TMP_Text m_bulletsCountText;
        [SerializeField] private GameObject[] m_bullets;
        
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
            
            switch (m_revolver.m_bulletCount)
            {
                case 6:
                    for (int i = 0; i <= 5; i++)
                    {
                        m_bullets[i].SetActive(true);
                    }
                    break;
                case 5:
                    for (int i = 1; i <= 5; i++)
                    {
                        m_bullets[i].SetActive(true);
                    }
                    m_bullets[0].SetActive(false);
                    break;
                case 4:
                    for (int i = 2; i <= 5; i++)
                    {
                        m_bullets[i].SetActive(true);
                    }
                    
                    for (int i = 0; i <= 1; i++)
                    {
                        m_bullets[i].SetActive(false);
                    }
                    break;
                case 3:
                    for (int i = 3; i <= 5; i++)
                    {
                        m_bullets[i].SetActive(true);
                    }
                    
                    for (int i = 0; i <= 2; i++)
                    {
                        m_bullets[i].SetActive(false);
                    }
                    break;
                case 2:
                    for (int i = 4; i <= 5; i++)
                    {
                        m_bullets[i].SetActive(true);
                    }
                    
                    for (int i = 0; i <= 3; i++)
                    {
                        m_bullets[i].SetActive(false);
                    }
                    break;
                case 1:
                    for (int i = 0; i <= 4; i++)
                    {
                        m_bullets[i].SetActive(false);
                    }
                    m_bullets[5].SetActive(true);
                    break;
                case 0:
                    for (int i = 0; i <= 5; i++)
                    {
                        m_bullets[i].SetActive(false);
                    }
                    break;
            }
        }
    }
}