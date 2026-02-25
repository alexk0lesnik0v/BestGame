using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

namespace UI
{
    public class DamageEffect : MonoBehaviour
    {
        [SerializeField] private Volume m_damageVolume;
        [SerializeField] private CanvasGroup m_damageCanvasGroup;
        private Tween m_weightTween;
        private Tween m_fadeTween;
        private Tweener m_healTweener;
        
        private bool m_isEffectEnabled = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ToggleEffect(true);
            }
            
            if (Input.GetKeyDown(KeyCode.Y))
            {
                ToggleEffect(false);
            }
        }

        private void EnableEffect()
        {
            if (!m_isEffectEnabled)
            {
                m_healTweener.Kill();
                m_weightTween = DOTween.To(() => m_damageVolume.weight, x => m_damageVolume.weight = x, 0.9f, 2f)
                    .SetEase(Ease.InOutQuad)
                    .SetLoops(-1, LoopType.Yoyo)
                    .From(0.15f)
                    .Pause();
                
                m_weightTween.Play();
                
                m_fadeTween = m_damageCanvasGroup.DOFade(0.9f, 2f)
                    .SetEase(Ease.InOutQuad)
                    .SetLoops(-1, LoopType.Yoyo)
                    .From(0.15f)
                    .Pause();
                
                m_fadeTween.Play();
                
                m_isEffectEnabled = true;
            }
        }

        private void DisableEffect()
        {
            if (m_isEffectEnabled)
            {
                m_healTweener.Kill();
                m_weightTween.Pause();
                m_healTweener = DOTween.To(() => m_damageVolume.weight, x => m_damageVolume.weight = x, 0f, 1f);
                
                m_fadeTween.Pause();
                m_fadeTween = m_damageCanvasGroup.DOFade(0f, 1f);
                
                m_isEffectEnabled = false;
            }
        }

        public void ToggleEffect(bool activate)
        {
            if (activate)
            {
                EnableEffect();
            }
            else
            {
                DisableEffect();
            }
        }
    }
}