using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderVolume : MonoBehaviour
{
    public Slider vSlider;
    public float sliderValue;


    // Start is called before the first frame update
    void Start()
    {
        vSlider.value = PlayerPrefs.GetFloat("volumenAudio", 1f);
        AudioListener.volume = vSlider.value;
    }
    public void ChangeSlider(float valor)
    {
        vSlider.value = valor;
        PlayerPrefs.SetFloat("volumeAudio", sliderValue);
        AudioListener.volume = vSlider.value;
    }
}
