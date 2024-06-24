using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UXManager : MonoBehaviour
{

    bool isSoundOff = false;
    [SerializeField] GameObject volumeToggleButton;
    Image volumeButtonImage;
    [SerializeField] Sprite onImage;
    [SerializeField] Sprite offImage;
    // Start is called before the first frame update
    void Start()
    {
        volumeButtonImage = volumeToggleButton.GetComponent<Image>();
    }

    public void ToggleVolume()
    {
        isSoundOff = !isSoundOff;
        AudioListener.pause = isSoundOff;

        if (AudioListener.pause == true)
        {
            volumeButtonImage.sprite = offImage;
        }
        else
            volumeButtonImage.sprite = onImage;
    }
}
