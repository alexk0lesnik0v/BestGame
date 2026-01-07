using Players;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text m_timerText;
    [SerializeField] private float m_time = 301;
    
    [SerializeField] private GameObject m_deathUI;
    [SerializeField] private PlayerController m_player;

    private float m_restartTime;
    private bool m_isStart = false;
    private bool m_canStart = false;
    
    private void Start()
    {
        m_restartTime = m_time;
    }
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
        else
        {
            m_time = m_restartTime;
        }

        if (m_time <= 0)
        {
            m_time = 0;
            m_deathUI.SetActive(true);
            m_player.enabled = false;
            m_player.m_isDead = true;
                
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void StartTimer()
    {
        m_isStart = true;
        m_canStart = true;
    }
    public void RestartTimer()
    {
        m_isStart = false;
        m_time = m_restartTime;
    }
}
