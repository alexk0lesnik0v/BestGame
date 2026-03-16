using Players;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text m_timerText;
    [SerializeField] private float m_time = 301;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_audioClip;
    
    [SerializeField] private Player m_player;

    private bool m_isStart = false;
    private bool m_canStart = false;
    private bool m_isPlaying = false;
    
    void Update()
    {
        if (m_isStart)
        {
            m_time -= Time.deltaTime;
            m_timerText.text = ((int)(m_time / 60)).ToString("D2") + " : " + ((int)(m_time % 60)).ToString("D2");
        }
        else if(m_canStart)
        {
            m_time -= Time.deltaTime;
            m_timerText.text = ((int)(m_time / 60)).ToString("D2") + " : " + ((int)(m_time % 60)).ToString("D2");
        }

        if (m_time <= 31f)
        {
            m_timerText.color = Color.red;
            if (!m_isPlaying)
            {
                m_audioSource.PlayOneShot(m_audioClip);
                m_isPlaying = true;
            }
        }
        else
        {
            m_timerText.color = Color.white;
            m_isPlaying = false;
            m_audioSource.Stop();
        }
        
        if (m_time <= 0)
        {
            m_time = 0;
            m_player.TakeDamage(100f);
        }
    }
    public void StartTimer(float time)
    {
        m_isStart = true;
        m_canStart = true;
        m_time = time;
    }
    public void RestartTimer(float restartTime)
    {
        m_isStart = false;
        m_time = restartTime;
    }
}
