using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using System; 

public class MainController : MonoBehaviour
{
    public static MainController Instance { get; private set;}

    public string turbineId; 
    
    


    List<List<string>> data= new List<List<string>>();

    // int turbineId = -1;
    // Start is called before the first frame update
    void Start()
    {
        readCVSFile();
         turbineId = "T00";
         drawGraphAverage();
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
        VueController.Instance.setWindPower(AveragePower(VueController.Instance.SliderValue).ToString());
        VueController.Instance.setWindSpeed(AverageWindSpeed(VueController.Instance.SliderValue).ToString());
        VueController.Instance.setRotorSpeed(AverageRotorSpeed(VueController.Instance.SliderValue).ToString());
        drawGraph();
        if (turbineId == "T00"){
            VueController.Instance.setTitre("Toutes les éoliennes");
        }
        else {
            VueController.Instance.setTitre("L'éolienne " + turbineId);
        }
        
    }

    void readCVSFile(){
        StreamReader strReader = new StreamReader(".\\Assets\\Data\\data.csv");  //.. reviens en arrière
        bool endOfFile = false; 

        for (int i = 0; i< 8; i++){
            data.Add(new List<string>());    //Création des 8 listes 
        }
        while (!endOfFile){

            string data_String = strReader.ReadLine();
            
            if (data_String == null)
            {
                endOfFile = true;
                break; 
            }


            var data_values = data_String.Split(','); // Séparation du fichier CSV 
            

            for (int i = 0; i <data_values.Length; i++){  //remplissage des colonnes avec les datas 
                data[i].Add(data_values[i]);
            }

        }
        for (int i = 0; i<data.Count; i ++){  //Delete la première ligne
                data[i].RemoveAt(0);
               
        }
       
    }
    
    // T98, 00:00-0:10, 100, OK, 7.14, 0.324, 10.561, 1106.4532
    public float AveragePower(float time){
        if (time == 0.0f ){
            List <float> l = new List<float>();
            
            if (turbineId  != "T00" )
            { 
                for (int i = 0; i < data[0].Count ; i++ ){
                    if (data[0][i]==turbineId){
                        var a = float.Parse(data[7][i],CultureInfo.InvariantCulture.NumberFormat);   // Mettre une condition pour filtrer par l'id ; attention pas de filtre si id = -1
                        l.Add(a);
                    }
                }
            }
            else 
            {
                for (int i = 0; i< data[7].Count; i ++){               
                    var a = float.Parse(data[7][i],CultureInfo.InvariantCulture.NumberFormat);   // Mettre une condition pour filtrer par l'id ; attention pas de filtre si id = -1
                    l.Add(a);
                }
            }
            
            
            return CalculAverage(l); 
        }

        else {
            var t = " " + convertIntToTime(time); // TEMPS DE SLIDER EN STRING    
            
            List <float> l = new List<float>();
            if (turbineId  != "T00" )
            { 
                for (int i = 0; i <data[7].Count; i++){

                    var inter = data[1][i].Split("-")[0]; // On récupère la première intervale de temps 
                    if (string.Equals(inter, t) && data[0][i] == turbineId) 
                    {   
                    
                        l.Add(float.Parse(data[7][i],CultureInfo.InvariantCulture.NumberFormat));
                    }
                }
            }
            else 
            {
                for (int i = 0; i <data[7].Count; i++){

                    var inter = data[1][i].Split("-")[0]; // On récupère la premier interval de temps 
                    if (string.Equals(inter, t)) 
                    {   
                    
                        l.Add(float.Parse(data[7][i],CultureInfo.InvariantCulture.NumberFormat));
                    }
                }
            }
            
            return CalculAverage(l); 
        }
        return -1f; 
    }

    public float CalculAverage (List<float> l){
        float total = 0;
        foreach (float a in l){
            total = total + a;
            
        }
        
        return total/l.Count; 
    }

    public string convertIntToTime(float t ){  //Fonction qui convertie les valeurs du slider
        string a = ""+System.Math.Floor(((t%1)*60)/10)*10;  // "" permet de convertir en String
        string s = (int)t + ":" +System.Math.Floor(((t%1)*60)/10)*10;
        if ((""+(int)t).Length == 1)  // 01:xx
            s = "0" + s;

        if (a.Length == 1){  //regarder la taille du string 
            s =  s + "0";
        }
        return s; 
    }

    public void setTurbineId(string id)
    {
        this.turbineId = id;
    }

    public void drawGraph (){
        List <DateTime> temps = new List<DateTime>();
        List <float> power = new List<float>();
        if (turbineId  != "T00" )
            { 
                for (int i = 0; i <data[7].Count; i++){

                    if ( data[0][i] == turbineId) 
                    {   
                    
                        power.Add(float.Parse(data[7][i],CultureInfo.InvariantCulture.NumberFormat));

                        var a = data[1][i].Split("-")[0];
                        a  = a.Substring(1, a.Length-1);
                        var inter = DateTime.ParseExact(a,"HH:mm",CultureInfo.InvariantCulture); // On récupère la première intervale de temps 
                        

                        temps.Add(inter);
                    }
                }
            }
        VueController.Instance.setGraphChart(temps, power); 

    }

    public void drawGraphAverage (){
        List <DateTime> temps = new List<DateTime>();
        List <float> average = new List<float>(); 
        var draw = new Dictionary <string, List<float>>(); //creation d'un dico de string et de list de float
            
        for (int i = 0; i <data[7].Count; i++){
            

            var interString = data[1][i].Split("-")[0];
            interString  = interString.Substring(1, interString.Length-1);

            if (!draw.ContainsKey(interString)){  //Si le temps n'existe pas 
                var inter = DateTime.ParseExact(interString,"HH:mm",CultureInfo.InvariantCulture); // On récupère la première interval de temps  
                temps.Add(inter);  
                draw.Add(interString, new List<float>());  //Creation d'une nouvelle ligne à la dico
            }
            draw[interString].Add(float.Parse(data[7][i],CultureInfo.InvariantCulture.NumberFormat));  //ajoute la valeur à la ligne  - le interString c'est comme un i 
        }
        foreach (KeyValuePair<string, List <float> > line in draw)  
        {  
            average.Add(CalculAverage(line.Value)); 
        } 
        
        VueController.Instance.setGraphChartAverage(temps, average); 
    }

    public float AverageWindSpeed(float time){
            if (time == 0.0f ){
                List <float> l = new List<float>();
                
                if (turbineId  != "T00" )
                { 
                    for (int i = 0; i < data[0].Count ; i++ ){
                        if (data[0][i]==turbineId){
                            var a = float.Parse(data[4][i],CultureInfo.InvariantCulture.NumberFormat);   // Mettre une condition pour filtrer par l'id ; attention pas de filtre si id = -1
                            l.Add(a);
                        }
                    }
                }
                else 
                {
                    for (int i = 0; i< data[7].Count; i ++){               
                        var a = float.Parse(data[4][i],CultureInfo.InvariantCulture.NumberFormat);   // Mettre une condition pour filtrer par l'id ; attention pas de filtre si id = -1
                        l.Add(a);
                    }
                }
                
                
                return CalculAverage(l); 
            }

            else {
                var t = " " + convertIntToTime(time); // TEMPS DE SLIDER EN STRING    
                
                List <float> l = new List<float>();
                if (turbineId  != "T00" )
                { 
                    for (int i = 0; i <data[7].Count; i++){

                        var inter = data[1][i].Split("-")[0]; // On récupère la premier interval de temps 
                        if (string.Equals(inter, t) && data[0][i] == turbineId) 
                        {   
                        
                            l.Add(float.Parse(data[4][i],CultureInfo.InvariantCulture.NumberFormat));
                        }
                    }
                }
                else 
                {
                    for (int i = 0; i <data[7].Count; i++){

                        var inter = data[1][i].Split("-")[0]; // On récupère la premier interval de temps 
                        if (string.Equals(inter, t)) 
                        {   
                        
                            l.Add(float.Parse(data[4][i],CultureInfo.InvariantCulture.NumberFormat));
                        }
                    }
                }
                
                return CalculAverage(l); 
            }
            return -1f; 
        }



        public float AverageRotorSpeed(float time){
            if (time == 0.0f ){
                List <float> l = new List<float>();
                
                if (turbineId  != "T00" )
                { 
                    for (int i = 0; i < data[0].Count ; i++ ){
                        if (data[0][i]==turbineId){
                            var a = float.Parse(data[6][i],CultureInfo.InvariantCulture.NumberFormat);   // Mettre une condition pour filtrer par l'id ; attention pas de filtre si id = -1
                            l.Add(a);
                        }
                    }
                }
                else 
                {
                    for (int i = 0; i< data[7].Count; i ++){               
                        var a = float.Parse(data[6][i],CultureInfo.InvariantCulture.NumberFormat);   // Mettre une condition pour filtrer par l'id ; attention pas de filtre si id = -1
                        l.Add(a);
                    }
                }
                
                
                return CalculAverage(l); 
            }

            else {
                var t = " " + convertIntToTime(time); // TEMPS DE SLIDER EN STRING    
                
                List <float> l = new List<float>();
                if (turbineId  != "T00" )
                { 
                    for (int i = 0; i <data[7].Count; i++){

                        var inter = data[1][i].Split("-")[0]; // On récupère la premier interval de temps 
                        if (string.Equals(inter, t) && data[0][i] == turbineId) 
                        {   
                        
                            l.Add(float.Parse(data[6][i],CultureInfo.InvariantCulture.NumberFormat));
                        }
                    }
                }
                else 
                {
                    for (int i = 0; i <data[7].Count; i++){

                        var inter = data[1][i].Split("-")[0]; // On récupère la premier interval de temps 
                        if (string.Equals(inter, t)) 
                        {   
                        
                            l.Add(float.Parse(data[6][i],CultureInfo.InvariantCulture.NumberFormat));
                        }
                    }
                }
                
                return CalculAverage(l); 
            }
            return -1f; 
        }

}
