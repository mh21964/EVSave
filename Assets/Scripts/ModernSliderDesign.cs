using UnityEngine;
using UnityEngine.UI;

public class ModernSliderDesign : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;
    public Image handleImage;

    public Color backgroundColor;
    public Color fillColor;
    public Color handleColor;

    public float fillTransitionDuration = 0.2f;
    public float handleTransitionDuration = 0.2f;

    private Color handleOriginalColor;

    private void Start()
    {
        slider = GetComponent<Slider>();

        // Store the original handle color
        handleOriginalColor = handleImage.color;

        // Set the initial colors
        SetColors();
    }

    private void SetColors()
    {
        // Set the colors of the slider handle and fill images
        fillImage.color = fillColor;
        handleImage.color = handleColor;

        // Set the background color of the slider
        slider.transform.Find("Background").GetComponent<Image>().color = backgroundColor;
    }

    public void OnSliderValueChanged(float value)
    {
        // Do something with the slider value
        Debug.Log("Slider value: " + value);
    }

    public void OnSliderPointerEnter()
    {
        // Animate the fill and handle on pointer enter
        AnimateColor(fillImage, fillColor, Color.white, fillTransitionDuration);
        AnimateColor(handleImage, handleColor, Color.white, handleTransitionDuration);
    }

    public void OnSliderPointerExit()
    {
        // Animate the fill and handle on pointer exit
        AnimateColor(fillImage, Color.white, fillColor, fillTransitionDuration);
        AnimateColor(handleImage, Color.white, handleColor, handleTransitionDuration);
    }

    private void AnimateColor(Image image, Color startColor, Color endColor, float duration)
    {
        // Start a color animation on the image
        image.CrossFadeColor(endColor, duration, true, true);
    }
}