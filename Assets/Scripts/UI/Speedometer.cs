using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] private PlayerController player;
    [SerializeField] private Slider speedometerSlider;
    [SerializeField] private Slider boostSlider;
    [SerializeField] private Color blockedBooster;
    [SerializeField] private Color unblockedBooster;
    

    private void Start()
    {
        speedometerSlider.maxValue = player.MaxSpeed;
        boostSlider.maxValue = player.BoostMaxCount;
        boostSlider.value = 0;
    }

    private void Update()
    {
        speedText.text = (int)player.CurrentSpeed + "\n<size=40%>km/h</size>";
        speedometerSlider.value = player.CurrentSpeed;
        boostSlider.value = player.BoostCount;
        boostSlider.image.color = player.BoostAvailability? unblockedBooster : blockedBooster;
    }
}
