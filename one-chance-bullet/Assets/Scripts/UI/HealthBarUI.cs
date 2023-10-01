using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] RectTransform healthBar;
    [SerializeField] Slider slider;
    [SerializeField] float deadZone = 0.15f;
    [SerializeField] float durationWhenChanged = 0.5f;

    private void Start() {
        healthBar.gameObject.SetActive(false);
    }

    private void OnEnable() {
        health.OnHealthChange += Show;
    }

    private void OnDisable() {
        health.OnHealthChange -= Show;
    }

    private void Show()
    {
        var healthPercent = health.HealthValue / health.MaxHealth;
        healthPercent = Mathf.Max(healthPercent, deadZone);
        slider.value = healthPercent;
        healthBar.gameObject.SetActive(true);
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), durationWhenChanged);
    }

    private void Hide()
    {
        healthBar.gameObject.SetActive(false);
    }


}
