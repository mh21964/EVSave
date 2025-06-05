//using UnityEngine;
//using UnityEngine.UI;



//public class SwitchToggle : MonoBehaviour
//{
//    [SerializeField] RectTransform uiHandleRectTransform;
//    [SerializeField] Color backgroundActiveColor;
//    [SerializeField] Color handleActiveColor;

//    Image backgroundImage, handleImage;

//    Color backgroundDefaultColor, handleDefaultColor;

//    Toggle toggle;

//    Vector2 handlePosition;

//    void Awake()
//    {
//        toggle = GetComponent<Toggle>();

//        handlePosition = uiHandleRectTransform.anchoredPosition;

//        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
//        handleImage = uiHandleRectTransform.GetComponent<Image>();

//        backgroundDefaultColor = backgroundImage.color;
//        handleDefaultColor = handleImage.color;

//        toggle.onValueChanged.AddListener(OnSwitch);

//        if (toggle.isOn)
//            OnSwitch(true);
//    }

//    void OnSwitch(bool on)
//    {
//        // Use LeanTween for animations
//        LeanTween.moveLocalX(uiHandleRectTransform, handlePosition.x * (on ? -1 : 1), 0.4f).setEase(LeanTween.Ease.InOutBack);
//        LeanTween.color(backgroundImage, on ? backgroundActiveColor : backgroundDefaultColor, 0.6f);
//        LeanTween.color(handleImage, on ? handleActiveColor : handleDefaultColor, 0.4f);
//    }

//    void OnDestroy()
//    {
//        toggle.onValueChanged.RemoveListener(OnSwitch);
//    }
//}