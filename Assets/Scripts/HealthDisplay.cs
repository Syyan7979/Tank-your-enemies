using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    // Initialization
    TextMeshProUGUI healthTextGUI;
    Player playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthTextGUI = GetComponent<TextMeshProUGUI>();
        playerHealth = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthTextGUI.text = $"{playerHealth.GetPlayerHealth()}";
    }
}
