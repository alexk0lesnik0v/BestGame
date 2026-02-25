using System;
using System.Collections;
using DG.Tweening;
using Players;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace UI
{
    public class DamageEffect : MonoBehaviour
    {
        [SerializeField] private Volume m_damageVolume;
        [SerializeField] private CanvasGroup m_damageCanvasGroup;
        [SerializeField] private PlayerController m_player;
        private Tween m_weightTween;
        private Tween m_fadeTween;
        private Tweener m_healTweener;
        private float m_damageVolumeValueMin;
        private float m_damageVolumeValueMax;

        private void EnableEffect()
        {
            m_healTweener.Kill();
            m_weightTween = DOTween.To(() => m_damageVolume.weight, x => m_damageVolume.weight = x, m_damageVolumeValueMax, 1f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo)
                .From(m_damageVolumeValueMin)
                .Pause();
                
            m_weightTween.Play();
                
            m_fadeTween = m_damageCanvasGroup.DOFade(m_damageVolumeValueMax, 1f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo)
                .From(m_damageVolumeValueMin)
                .Pause();
                
            m_fadeTween.Play();
        }

        private void DisableEffect()
        {
            m_healTweener.Kill();
            m_weightTween.Pause();
            m_healTweener = DOTween.To(() => m_damageVolume.weight, x => m_damageVolume.weight = x, 0f, 1f);
                
            m_fadeTween.Pause();
            m_fadeTween = m_damageCanvasGroup.DOFade(0f, 1f);
        }

        public void ToggleEffect(bool activate, float health)
        {
            DisableEffect();
            
            if (activate)
            {
                if (health > 80f)
                {
                    return;
                }
            
                if (health <= 0f)
                {
                    m_damageVolumeValueMax = 1f;
                    m_damageVolumeValueMin = 1f;
                    EnableEffect();
                    m_player.Dead();
                    StartCoroutine(WaitRestart(3f));
                }
            
                if (health <= 20f)
                {
                    m_damageVolumeValueMax = 0.9f;
                    m_damageVolumeValueMin = 0.7f;
                    EnableEffect();
                    return;
                }

                if (health <= 40f)
                {
                    m_damageVolumeValueMax = 0.7f;
                    m_damageVolumeValueMin = 0.5f;
                    EnableEffect();
                    return;
                }

                if (health <= 60f)
                {
                    m_damageVolumeValueMax = 0.5f;
                    m_damageVolumeValueMin = 0.3f;
                    EnableEffect();
                    return;
                }
            
                if (health <= 80f)
                {
                    m_damageVolumeValueMax = 0.3f;
                    m_damageVolumeValueMin = 0.15f;
                    EnableEffect();
                }
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