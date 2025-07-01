using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] private PlayerController player;
    [SerializeField] private Slider speedometerSlider;
    

    private void Start()
    {
        speedometerSlider.maxValue = player.MaxSpeed;
    }

    private void Update()
    {
        speedText.text = (int)player.CurrentSpeed + "\n<size=40%>km/h</size>";
        speedometerSlider.value = player.CurrentSpeed;
    }
}
