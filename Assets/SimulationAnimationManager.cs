using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System.Globalization;
using System.Reflection;

public class SimulationAnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public SimulationManager simulationManager;
    public UnityEngine.UI.Image recycledImage;
    public RectTransform recycledScale;
    public UnityEngine.UI.Image recyclableImage;
    public RectTransform recyclableScale;
    public float percentage;
    public float time;
    public float maxRunTime;
    private float timerTime;
    public float runTimer;
    internal float yearTime;
    private float currentYear;
    public TMP_Text timerText;
    public TMP_Text yearText;
    private float x, y;
    private Vector2 baseScale;
    public TMP_Text unrecycledWasteText;
    private float unrecycledWaste;
    public TMP_Text recycledWasteText;
    private float recycledWaste;
    private float waste;
    public TMP_Text wasteText;
    private float recyclableWaste;
    public TMP_Text recyclableWasteText;
    public bool running { get; set; }
    private RectTransform unrecycledScale;
    public GameObject blockingPanel;

    void Start()
    {
        running = false;
        timerTime = time;
        runTimer = maxRunTime;
        currentYear = 1;
        x = 0f;
        y = 0f;
        baseScale = gameObject.GetComponent<RectTransform>().localScale;
        gameObject.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
        unrecycledScale = gameObject.GetComponent<RectTransform>();

    }



    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            yearText.text = "Year " + currentYear;
            yearTime -= Time.deltaTime;
            if (yearTime <= 0)
            {
                currentYear++;
                yearTime = maxRunTime / simulationManager.years;
            }
            runTimer -= Time.deltaTime;
            if (runTimer <= 0)
            {
                running = false;
                unrecycledWasteText.text = ((unrecycledWaste / waste) * simulationManager.waste).ToString("##,#", CultureInfo.CurrentCulture) + " t";
                recycledWasteText.text = ((recycledWaste / waste)*simulationManager.waste).ToString("##,#", CultureInfo.CurrentCulture) + " t";
                wasteText.text = simulationManager.waste.ToString("##,#", CultureInfo.CurrentCulture) + " t";
                recyclableWasteText.text = ((recyclableWaste / waste) * simulationManager.waste).ToString("##,#", CultureInfo.CurrentCulture) + " t";
                blockingPanel.SetActive(true);
                return;
            }       
            if (timerTime <= 0)
            {
                timerTime = time;
                unrecycledScale.localScale = new Vector2(x, y);
                if (unrecycledScale.localScale.x < baseScale.x)
                    x += baseScale.x/(maxRunTime/time);
                if (unrecycledScale.localScale.y < baseScale.y)
                    y += baseScale.y / (maxRunTime / time);
                unrecycledWaste += simulationManager.unrecycledWaste / (maxRunTime / time);
                recycledWaste +=  simulationManager.recycledWaste / (maxRunTime / time);
                recyclableWaste += simulationManager.recyclableWaste / (maxRunTime / time);
                waste += simulationManager.waste / (maxRunTime / time);
                unrecycledWasteText.text = Mathf.Round(unrecycledWaste).ToString("##,#", CultureInfo.CurrentCulture) + " t";
                recycledWasteText.text = Mathf.Round(recycledWaste).ToString("##,#", CultureInfo.CurrentCulture) + " t";
                recyclableWasteText.text = Mathf.Round(recyclableWaste).ToString("##,#", CultureInfo.CurrentCulture) + " t";
                wasteText.text = Mathf.Round(waste).ToString("##,#", CultureInfo.CurrentCulture) + " t";
                recycledScale.localScale = new Vector2(unrecycledScale.localScale.x, unrecycledScale.localScale.y);
                recycledImage.fillAmount = recycledWaste / waste;
                recyclableScale.localScale = new Vector2(unrecycledScale.localScale.x, unrecycledScale.localScale.y);
                recyclableImage.fillAmount = recycledWaste / waste + recyclableWaste / waste;
                timerText.text = "Time left: " + Mathf.Round(runTimer);
            }

                
            timerTime -= Time.deltaTime;

        }
    }

}
