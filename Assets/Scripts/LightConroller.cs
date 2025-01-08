using UnityEngine;
using UnityEngine.UI;
 
public class LightController : MonoBehaviour
{
    [SerializeField] private Slider timeSlider; // Référence au Slider
    [SerializeField] private Light directionalLight; // Référence à la Directional Light
    [SerializeField] private float maxLightIntensity = 1.0f; // Intensité maximale de la lumière
    [SerializeField] private float minLightIntensity = 0.0f; // Intensité minimale de la lumière
 
    void Start()
    {
        if (timeSlider == null || directionalLight == null)
        {
            Debug.LogError("Assurez-vous que le Slider et la Directional Light sont assignés dans l'Inspector !");
            return;
        }

 
        // Ajouter un listener au slider pour détecter les changements
        timeSlider.onValueChanged.AddListener(OnTimeSliderChanged);
        UpdateLightIntensity(); // Mettre à jour une première fois
    }
 
    private void OnTimeSliderChanged(float value)
    {
        UpdateLightIntensity();
    }
 
    private void UpdateLightIntensity()
    {
        float currentTime = timeSlider.value;
    
        if (currentTime >= 21f)
        {
            float t = Mathf.InverseLerp(21f, 24f, currentTime);
            directionalLight.intensity = Mathf.Lerp(maxLightIntensity, minLightIntensity, t);
            directionalLight.color = Color.Lerp(Color.white, new Color(1f, 0.5f, 0.2f), t); // Blanc vers orange
        }
        else
        {
            directionalLight.intensity = maxLightIntensity;
            directionalLight.color = Color.white; // Lumière normale en journée
        }
    }
}