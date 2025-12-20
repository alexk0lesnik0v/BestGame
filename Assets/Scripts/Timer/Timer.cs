using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text m_timerText;
    [SerializeField] private float m_time = 300;

    void Update()
    {
        m_time -= Time.deltaTime;
        m_timerText.text = ((int)(m_time/60)).ToString() + " : " + ((int)(m_time % 60)).ToString();
    }
}
