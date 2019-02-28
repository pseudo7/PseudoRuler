using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DDOLNavigation : MonoBehaviour
{
    public static DDOLNavigation Instance;

    [Header("Functionality")]
    public float initSize = 10.0f;

    [Header("UI Elements")]
    public InputField sizeIF;
    //public InputField maxSizeIF;
    public Slider sizeSlider;
    public Transform rulerTransform;

    const float RULER_WIDTH = .0254f;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        UpdateScale(initSize);
        UpdateSlider(initSize);
        //UpdateMaxSize(initSize);
        sizeIF.text = initSize.ToString("#.0");
    }

    public void UpdateScale()
    {
        float size = float.Parse(sizeIF.text);
        if (size > 10) { sizeIF.text = 10.ToString("0.0"); return; }
        rulerTransform.localScale = new Vector3(size / 100, RULER_WIDTH, 1);
    }

    void UpdateScale(float size)
    {
        rulerTransform.localScale = new Vector3(size / 100, RULER_WIDTH, 1);
    }

    public void UpdateSlider()
    {
        if (sizeSlider.value > 10) { sizeSlider.value = 10; return; }
        UpdateScale(sizeSlider.value);
        sizeIF.text = sizeSlider.value.ToString("0.0");
    }

    void UpdateSlider(float size)
    {
        if (rulerTransform.localScale.x * 100 < size)
        {
            sizeSlider.maxValue = size;
            sizeSlider.value = size;
            //maxSizeIF.text = size.ToString("0.0");
            UpdateScale(size);
        }
    }

    //public void UpdateMaxSize()
    //{
    //    sizeSlider.maxValue = float.Parse(maxSizeIF.text);
    //}

    //void UpdateMaxSize(float size)
    //{
    //    if (float.Parse(maxSizeIF.text) < size)
    //    {
    //        sizeSlider.maxValue = size;
    //        sizeSlider.value = size;
    //        maxSizeIF.text = size.ToString("0.0");
    //    }
    //}

}
