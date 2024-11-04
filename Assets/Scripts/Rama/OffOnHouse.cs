using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLightsByLayer : MonoBehaviour
{
    public string lightLayerName = "Light"; // Nombre del layer que contiene las luces
    private bool lightsOn = true; // Estado de las luces, true indica que est�n encendidas

    void Update()
    {
        // Detecta la tecla "L" para alternar el estado de las luces
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLights();
        }
    }

    void ToggleLights()
    {
        // Convierte el nombre del layer a su �ndice
        int layer = LayerMask.NameToLayer(lightLayerName);

        // Encuentra todos los objetos en la escena y filtra los que pertenecen al layer "Light"
        Light[] allLights = FindObjectsOfType<Light>();

        foreach (Light light in allLights)
        {
            // Verifica si el objeto tiene el layer deseado
            if (light.gameObject.layer == layer)
            {
                // Cambia el estado de la luz
                light.enabled = lightsOn;
            }
        }

        // Invierte el estado para la pr�xima vez que se llame ToggleLights
        lightsOn = !lightsOn;
    }
}
