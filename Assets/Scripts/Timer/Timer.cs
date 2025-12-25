using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text m_timerText;
    [SerializeField] private float m_time = 301;

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
            m_timerText.text = ((int)(m_time / 60)).ToString() + " : " + ((int)(m_time % 60)).ToString();

        }
        else if(m_canStart)
        {
            
            m_time -= Time.deltaTime;
            m_timerText.text = ((int)(m_time / 60)).ToString() + " : " + ((int)(m_time % 60)).ToString();
        }
        else
        {
            m_time = m_restartTime;
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
