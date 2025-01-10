using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ChartAndGraph;
using UnityEngine.UI; 
using System; 

public class VueController : MonoBehaviour
{ 
     public static VueController Instance { get; private set;}

    [SerializeField]
    public TextMeshProUGUI puissanceText; 
    public TextMeshProUGUI windSpeedText; 
    public TextMeshProUGUI temperatureText; 

    public TextMeshProUGUI titreText; 

    public TextMeshProUGUI rotorspeedText; 

    public TextMeshProUGUI TempsText; 

    public GraphChart chart; 

    public Slider slider; 

    public float SliderValue = 0.0f;
    public Light directionalLight;

    
    // Start is called before the first frame update
    void Start()
    {
   
    }

    private void Awake(){       
        if (Instance != null && Instance != this){
            Destroy(this);
        }
        else {
            Instance = this; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setWindPower(string power){
        puissanceText.text = power;
    }

    public void setWindSpeed(string windspeed){
        windSpeedText.text = windspeed;
    }

    public void setTemperature(string temperture){
        temperatureText.text = temperture;
    }


    public void setRotorSpeed(string rotorspeed){
        rotorspeedText.text = rotorspeed;
    }

    
    public void setTime(string time){
        TempsText.text = time;
    }



     public void setTitre(string titre){
        titreText.text = titre;
    }




    public void setGraphChart (  List <DateTime> temps, List <float> power){
        chart.DataSource.StartBatch();
        chart.DataSource.ClearCategory("TUTO"); 
        // chart.DataSource.AddCategory("TUTO");

        for (int i = 0; i<temps.Count; i++)
        {
            
            chart.DataSource.AddPointToCategory("TUTO",temps[i],power[i]); 
        }

        chart.DataSource.EndBatch();
    }

    public void setGraphChartAverage (  List <DateTime> temps, List <float> average){
        chart.DataSource.StartBatch();
        chart.DataSource.ClearCategory("Average"); 
        //  chart.DataSource.AddCategory("Average");

        for (int i = 0; i<temps.Count; i++)
        {
           
            chart.DataSource.AddPointToCategory("Average",temps[i],average[i]); 
        }

        chart.DataSource.EndBatch();
    }


    public void getTime (float time){
        TempsText.text = convertIntToTime(time);
    } 

    public void getSliderValueChanged(float Iwant)
{
    SliderValue = Iwant;
    float heurenuit = 19f;
    float heurenuit2 = 24f;
    float leverSoleil = 8f;
    float milieuJournee = 12f;

    if (Iwant >= 19f)
    {
        // Transition du coucher du soleil
        float t = Mathf.InverseLerp(heurenuit, heurenuit2, Iwant);
        directionalLight.intensity = Mathf.Lerp(1.0f, 0.0f, t);
        directionalLight.color = Color.Lerp(Color.white, new Color(1f, 0.5f, 0.2f), t); // Blanc vers orange
    }
    else if (Iwant >= 8f && Iwant < 12f)
    {
        // Transition du lever du soleil
        float t = Mathf.InverseLerp(leverSoleil, milieuJournee, Iwant);
        directionalLight.intensity = Mathf.Lerp(0.0f, 1.0f, t);
        directionalLight.color = Color.Lerp(new Color(1f, 0.5f, 0.2f), Color.white, t); // Orange vers blanc
    }
    else if (Iwant < 8f)
    {
        // Avant 8h, lumière sombre
        directionalLight.intensity = 0.0f;
        directionalLight.color = Color.black;
    }
    else
    {
        // Lumière normale en journée
        directionalLight.intensity = 1.0f;
        directionalLight.color = Color.white;
    }
}


    public void getTitre (string titre){
        titreText.text = titre.ToString();
    }

    public string convertIntToTime(float t ){  
        string a = ""+System.Math.Floor(((t%1)*60)/10)*10;  
        string s = (int)t + ":" +System.Math.Floor(((t%1)*60)/10)*10;
        if ((""+(int)t).Length == 1) // 01:xx
            s = "0" + s;

        if (a.Length == 1){  
            s =  s + "0";
        }
        return s; 
    }

    public void clickButton1(){
        MainController.Instance.setTurbineId("T98");
    }

    public void clickButton2(){
         MainController.Instance.setTurbineId("T99");
        
    }

    public void clickButton3(){
         MainController.Instance.setTurbineId("T100");
        
    }

    public void clickButton4(){
         MainController.Instance.setTurbineId("T101");
        
    }

    public void clickButton5(){
         MainController.Instance.setTurbineId("T102");
    }

    public void clickButton6(){
         MainController.Instance.setTurbineId("T103");
    }

    public void clickButton7(){
         MainController.Instance.setTurbineId("T104");
    }

    public void clickButton8(){
         MainController.Instance.setTurbineId("T105");
    }

    public void clickButton9(){
         MainController.Instance.setTurbineId("T106");
    }

    public void clickButton10(){
        MainController.Instance.setTurbineId("T107");
    }

    public void clickButton11(){
         MainController.Instance.setTurbineId("T108");
    }

    public void clickButton12(){
         MainController.Instance.setTurbineId("T109");
    }

    public void clickButtonTout(){
         MainController.Instance.setTurbineId("T00");
    }


}
