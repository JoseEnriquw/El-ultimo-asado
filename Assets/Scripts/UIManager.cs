using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager current;

    [SerializeField] private TextMeshPro Nombretxt;
    [SerializeField] private Image NombreImg;
    [SerializeField] private Image Billetera;

    private void Awake()
    {
        if(current != null && current!=this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        
    }

    public static void UpdateNombre(string name)
    {
        if(current==null)
            return;
        current.Nombretxt.text = name;  
    }
    public static void UpdateNombreImg(Image img)
    {
        if (current == null)
            return;
        current.NombreImg = img;
    }
}
