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

    public TextMeshProUGUI titreText; 

    public TextMeshProUGUI rotorspeedText; 

    public TextMeshProUGUI TempsText; 

    public GraphChart chart; 

    public Slider slider; 

    public float SliderValue = 0.0f;

    
    // Start is called before the first frame update
    void Start()
    {
   
    }

    private void Awake(){        //Des que l'objet est instancié
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

        for (int i = 0; i<temps.Count; i++)
        {
            chart.DataSource.AddPointToCategory("TUTO",temps[i],power[i]); 
        }

        chart.DataSource.EndBatch();
    }

    public void setGraphChartAverage (  List <DateTime> temps, List <float> average){
        chart.DataSource.StartBatch();
        chart.DataSource.ClearCategory("Average"); 

        for (int i = 0; i<temps.Count; i++)
        {
            chart.DataSource.AddPointToCategory("Average",temps[i],average[i]); 
        }

        chart.DataSource.EndBatch();
    }


    public void getTime (float time){
        TempsText.text = convertIntToTime(time);
    }

    public void getSliderValueChanged (float Iwant){ 
        SliderValue = Iwant; 
    }

    public void getTitre (string titre){
        titreText.text = titre.ToString();
    }

    public string convertIntToTime(float t ){  //Fonction qui convertie les valeurs du slider
        string a = ""+System.Math.Floor(((t%1)*60)/10)*10;  // "" permet de convertir en String
        string s = (int)t + ":" +System.Math.Floor(((t%1)*60)/10)*10;
        if ((""+(int)t).Length == 1) // 01:xx
            s = "0" + s;

        if (a.Length == 1){  //regarder la taille du string 
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
