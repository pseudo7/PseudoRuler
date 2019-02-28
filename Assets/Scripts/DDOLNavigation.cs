using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DDOLNavigation : MonoBehaviour
{
    public static DDOLNavigation Instance;

    [Header("Ruler")]
    public float initSize = 10.0f;
    public GameObject rulerPrefab;

    [Header("UI Elements")]
    public GameObject gSensor;
    public Transform rulerTransform;
    public InputField sizeIF;
    public Slider sizeSlider;
    public Button nextSceneButton;
    public Text currentDistanceText;

    static float lastTouch;
    static float lastSliderValue = 10;

    const float RULER_WIDTH = .0254f;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        else Destroy(gameObject);

        UpdateScale(initSize);
        UpdateSlider(initSize);
        sizeIF.text = initSize.ToString("#.0");
    }

    private void OnSceneUnloaded(Scene scene)
    {
        //FindObjectOfType<StreamManager>().webCam.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0: CheckGSensor(); break;
            case 1: CheckForNewRuler(); break;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        {
            Camera.main.transform.localPosition = new Vector3(0, .3f * (10 / lastSliderValue), 0);
            currentDistanceText = FindObjectOfType<Text>();
        }
    }

    void CheckForNewRuler()
    {
        if (lastTouch == 0) lastTouch = Time.timeSinceLevelLoad;
        else if (Time.timeSinceLevelLoad - lastTouch > 1)
        {
            if (Input.touchCount == 4) Instantiate(rulerPrefab);
            lastTouch = 0;
        }
    }

    public void UpdateScale()
    {
        float size = float.Parse(sizeIF.text);
        if (size > 10) { sizeIF.text = 10.ToString("0.0"); return; }
        lastSliderValue = size;
        rulerTransform.localScale = new Vector3(size / 100, RULER_WIDTH, 1);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        FindObjectOfType<StreamManager>().webCam.Stop();
        yield return new WaitForSeconds(1);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void UpdateSlider()
    {
        if (sizeSlider.value > 10) { sizeSlider.value = 10; return; }
        UpdateScale(sizeSlider.value);
        sizeIF.text = sizeSlider.value.ToString("0.0");
    }

    void CheckGSensor()
    {
        Vector3 acc = Input.acceleration;
        float accX = Mathf.Round(acc.x * 10);
        float accY = Mathf.Round(acc.y * 10);
        gSensor.SetActive(accY + accX == 0);
        nextSceneButton.interactable = accY + accX == 0;
    }

    void UpdateScale(float size)
    {
        rulerTransform.localScale = new Vector3(size / 100, RULER_WIDTH, 1);
    }

    void UpdateSlider(float size)
    {
        sizeSlider.maxValue = size;
        sizeSlider.value = size;
        UpdateScale(size);
    }
}
