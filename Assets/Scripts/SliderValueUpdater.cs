using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueUpdater : MonoBehaviour
{
    public TextMeshProUGUI sliderValueText;
    [SerializeField] GameObject costManager;
    [SerializeField] Slider slider;
    [SerializeField] float minimumValue, maximumValue, normalValue;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = minimumValue;
        slider.maxValue = maximumValue;
        

        sliderValueText.text = slider.value.ToString("");
    }

    public void UpdateSliderValue()
    {
        float newValue = slider.value;
        sliderValueText.text = newValue.ToString(); // Format the value as desired

        costManager.GetComponent<TwoWheelCostManager>().UpdateChart();
    }
}