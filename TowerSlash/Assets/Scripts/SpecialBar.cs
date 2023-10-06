using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void Start()
    {
        slider.value = 0;
    }

    public void SetMaxSpecial(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetSpecial(float value)
    {
        slider.value = value;
    }
}
