using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] int playerID = 1;
    [SerializeField] HeartUI heartContainerPrefab;
    
    HeartUI[] hearts;
    Health health;

    private void OnEnable() {
        FindPlayer();
        health.OnHealthChange += UpdateUI;
        UpdateUI();
    }

    private void FindPlayer() {
        var players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        foreach (var player in players)
        {
            if(player.PlayerID == playerID)
            {
                health = player.GetComponent<Health>();
                break;
            }
        }
    }

    private void OnDisable() {
        health.OnHealthChange -= UpdateUI;
    }

    // Create hearts based on the health and empty hearts based on max health. The last heart is filled based on the remaining of health.
    void UpdateUI()
    {
        // Create new hearts if necessary
        if(hearts == null) {
            CreateHeartContainers();
        }
            
        
        // Expand the array if necessary
        if (Mathf.Ceil(health.MaxHealth) != hearts.Length)
        {
            foreach (HeartUI heart in hearts)
            {
                Destroy(heart.gameObject);
            }

            CreateHeartContainers();
        }

        float remainHealth = health.HealthValue;
        for (int i = 0; i < hearts.Length; i++)
        {
            if(remainHealth >= 1)
            {
                hearts[i].FillImage.fillAmount = 1;
                remainHealth -= 1;
            }
            else {
                hearts[i].FillImage.fillAmount = remainHealth;
                remainHealth = 0;
            }
                
        }
    }

    private void CreateHeartContainers()
    {
        hearts = new HeartUI[(int)Mathf.Ceil(health.MaxHealth)];
        for (int i = 0; i < Mathf.Ceil(health.MaxHealth); i++)
        {
            hearts[i] = Instantiate(heartContainerPrefab, transform);
        }
    }
}
