using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace YAAS
{
    public class AbilityUI : MonoBehaviour
    {
        [SerializeField] private Image _abilityArt;
        [SerializeField] private Image _cooldownOverlay;
        [SerializeField] private TMP_Text _abilityTimer;
        [SerializeField] private TMP_Text _keybind;
        [SerializeField] private Button _abilityButton;

        private RuntimeAbility _ability;
        private float _lastUseTime = 0.0f;
        private float _cooldownDuration;
        private bool _isOnCooldown;
        
        public string AbilityID => _ability.Ability.AbilityID;

   
        public void Setup(RuntimeAbility ability, Action onClick = null)
        {
            _ability = ability;
            _abilityArt.sprite = ability.Ability.AbilityIcon;

            _abilityButton.enabled = onClick != null;
            if (onClick != null)
            {
                _abilityButton.onClick.AddListener(() =>
                {
                    onClick?.Invoke();
                    PutOnCooldown();
                });
            }
        }

        public void SetKeyBind(string val)
        {
            _keybind.text = val;
        }

        public void SimulateTap()
        {
            _abilityButton.onClick.Invoke();
        }
        
        public void PutOnCooldown()
        {
            _cooldownDuration = _ability.AbilityCooldownSeconds;
            _lastUseTime = Time.time;
            _isOnCooldown = true;
            _abilityButton.interactable = false;
            _cooldownOverlay.fillAmount = 1.0f;
            UpdateAbilityTimerText();
        }

        private void Update()
        {
            if (_isOnCooldown)
            {
                float timeLeft = Mathf.Max(0.0f, _cooldownDuration - (Time.time - _lastUseTime));
                _cooldownOverlay.fillAmount = timeLeft / _cooldownDuration;
                
                // Update UI text
                UpdateAbilityTimerText(timeLeft);
                
                // If cooldown is over, enable the ability button and reset
                if (timeLeft <= 0.0f)
                {
                    _isOnCooldown = false;
                    _abilityButton.interactable = true;
                    _abilityTimer.text = string.Empty; // Clear the timer text
                }
            }
        }

        private void UpdateAbilityTimerText(float timeLeft = 0.0f)
        {
            if (_isOnCooldown)
                _abilityTimer.text = timeLeft.ToString("0.0");
        }
    }

}
