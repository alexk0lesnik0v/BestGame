using System.Collections;
using Players;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class DeathUI : MonoBehaviour
    {
        [SerializeField] private Image[] m_images;
        [SerializeField] private PlayerController m_player;

        public void SetImage(float health)
        {
            this.gameObject.SetActive(true);
            foreach (var image in m_images)
            {
                image.gameObject.SetActive(false);
            }
            
            if (health > 80f)
            {
                this.gameObject.SetActive(false);
                return;
            }
            
            if (health <= 0f)
            {
                m_images[0].gameObject.SetActive(true);
                m_player.Dead();
                StartCoroutine(WaitRestart(3f));
            }
            
            if (health <= 20f)
            {
                m_images[1].gameObject.SetActive(true);
                return;
            }

            if (health <= 40f)
            {
                m_images[2].gameObject.SetActive(true);
                return;
            }

            if (health <= 60f)
            {
                m_images[3].gameObject.SetActive(true);
                return;
            }
            
            if (health <= 80f)
            {
                m_images[4].gameObject.SetActive(true);
            }
        }
        
        IEnumerator WaitRestart(float time)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("Restart Level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}