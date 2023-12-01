using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] Slider _slider;
    // Update is called once per frame
    void Update()
    {
        _textMeshProUGUI.text = (1f / Time.deltaTime).ToString();
        
    }

    public void SetFPS()
    {
        Application.targetFrameRate = (int)_slider.value;
    }
}
