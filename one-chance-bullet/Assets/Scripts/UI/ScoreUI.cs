using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] int playerID = 1;
    [SerializeField] TextMeshProUGUI scoreText;
    
    Points points;

    private void OnEnable() {
        FindPlayer();
        points.OnPointsChange += UpdateUI;
        UpdateUI(points.PointsValue);
    }

    private void FindPlayer() {
        var players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        foreach (var player in players)
        {
            if(player.PlayerID == playerID)
            {
                points = player.GetComponent<Points>();
                break;
            }
        }
    }

    private void OnDisable() {
        points.OnPointsChange -= UpdateUI;
    }

    void UpdateUI(int points)
    {
        scoreText.text = points.ToString();
    }
}
