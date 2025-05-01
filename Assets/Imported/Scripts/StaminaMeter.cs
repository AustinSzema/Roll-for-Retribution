using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaMeter : MonoBehaviour{
    public Slider slider;
    public TextMeshProUGUI staminaText;
    public int stamina { get; private set; } = 100;
    public int maxStamina = 100;


    public static StaminaMeter Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start(){
        slider.maxValue = maxStamina;
        stamina = maxStamina;
    }

    public void ReduceStamina(int amount){
        stamina -= amount;
        if (stamina <= 0)
        {
            stamina = 0;
        }
    }

    public void Update(){
        slider.value = stamina;
        staminaText.text = "Stamina: " + (float)stamina / maxStamina * 100 + "%";
    }
}




