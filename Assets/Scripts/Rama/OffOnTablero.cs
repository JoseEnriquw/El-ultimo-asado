using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableroLuz : MonoBehaviour
{
    public GameObject fuse1; // Fusible 1
    public GameObject fuse2; // Fusible 2
    public GameObject electricalPanel; // Panel al que deben a�adirse los fusibles
    public GameObject offOn; // Objeto "OffOn" que cambiar� de material
    public Material rojoMaterial; // Material rojo
    public Material verdeMaterial; // Material verde
    private Renderer offOnRenderer;

    void Start()
    {
        // Obt�n el componente Renderer del objeto OffOn para cambiar su material
        if (offOn != null)
        {
            offOnRenderer = offOn.GetComponent<Renderer>();
            offOnRenderer.material = rojoMaterial; // Material inicial en rojo
        }
    }

    void Update()
    {
        // Verifica si ambos fusibles est�n activados y son hijos del panel el�ctrico
        if (IsFuseInElectricalPanel(fuse1) && IsFuseInElectricalPanel(fuse2))
        {
            CambiarMaterialOffOn(true); // Cambia a material verde
        }
        else
        {
            CambiarMaterialOffOn(false); // Cambia a material rojo
        }
    }

    private bool IsFuseInElectricalPanel(GameObject fuse)
    {
        // Comprueba si el fusible est� activo y es hijo de "electricalPanel"
        return fuse.activeInHierarchy && fuse.transform.parent == electricalPanel.transform;
    }

    private void CambiarMaterialOffOn(bool activarVerde)
    {
        // Cambia el material seg�n el estado de los fusibles
        if (offOnRenderer != null)
        {
            offOnRenderer.material = activarVerde ? verdeMaterial : rojoMaterial;
        }
    }
}
