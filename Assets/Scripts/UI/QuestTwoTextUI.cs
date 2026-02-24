using QuestControllers;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class QuestTwoTextUI : MonoBehaviour
    {
        [SerializeField] private QuestControllerTwo m_questControllerTwo;
        [SerializeField] private TMP_Text m_figurkaCountText;
        
        private void Awake()
        {
            this.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (this.gameObject.activeSelf)
            {
                m_figurkaCountText.text = "Can't Interact! You need " + (5 - m_questControllerTwo.m_figurkaAmount).ToString() + " more figurkas!";
            }
        }
    }
}