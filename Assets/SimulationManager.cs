using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using TMPro;

public class SimulationManager : MonoBehaviour
{
    public SimulationAnimationManager simulationAnimationManager;
    public float population;
    public TMP_Text populationText;
    public float years;
    public TMP_Text timeText;
    public float recyclingPercent;
    public TMP_Text recyclingPercentText;
    public float waste;
    public float unrecycledWaste;
    public float recycledWaste;
    public float recyclableWaste;
    

    public void adjustPopulation(float pop)
    {
        population = (int)pop;
        populationText.text = "Population : " + pop;
    }

    public void adjustTime(float yr)
    {
        years = (int)yr;
        timeText.text = "Time(Years) : " + years;
    }

    public void adjustRecyclingPercentage(float prc)
    {
        recyclingPercent = Mathf.Round(prc) / 100;
        recyclingPercentText.text = "People, who recycle : " + Mathf.Round(prc) + "%";
    }

    // Start is called before the first frame update
    void Start()
    {
        population = 10000;
        years = 1;
        recyclingPercent = 0;

    }



    public void InitFormulas()
    {
        recycledWaste = Mathf.Round(years * ((0.407f * 0.31f) * (population * recyclingPercent)));
        recyclableWaste = Mathf.Round(years * ((0.407f * 0.31f) * (population * (1 - recyclingPercent))));
        unrecycledWaste = Mathf.Round(years * (0.407f * population) * (1-0.31f));
        waste = unrecycledWaste + recycledWaste + recyclableWaste;

    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
