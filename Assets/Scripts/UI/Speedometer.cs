using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] private PlayerController player;

    private void Update()
    {
        speedText.text = (int)player.CurrentSpeed + " km/h";
    }
}
