using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.GameManager;

public class UIManager : MonoBehaviour
{
    static UIManager current;

    [SerializeField] private TextMeshProUGUI Nombretxt;
    [SerializeField] private Image NombreImg;
    [SerializeField] private Image Billetera;
    [SerializeField] private TextMeshProUGUI TareaTxt;
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image backGroundLoadingImage;
    [Header("LoadingSprites")]
    [SerializeField] private List<Sprite> loadingSprites;

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

    public void UpdateNombre(string name)
    {
        if(current==null)
            return;
        current.Nombretxt.text = name;  
    } 
    
    public void UpdateNombreImg(Image img)
    {
        if (current == null)
            return;
        current.NombreImg = img;
    }
    public void SetTarea(string task)
    {
        if(current==null)
            return;
        current.TareaTxt.text = task;  
    }

    public void ShowTaskPanel()
    {
        if(!taskPanel.activeSelf) taskPanel.SetActive(true);
    }   

    public void HideTaskPanel()
    {
        if(taskPanel.activeSelf) taskPanel.SetActive(false);
    } 

    public void ShowLoadingPanel()
    {
        if(!loadingPanel.activeSelf) loadingPanel.SetActive(true);
    }   

    public void HideLoadingPanel()
    {
        if(loadingPanel.activeSelf) loadingPanel.SetActive(false);
    }

    public static UIManager GetUIManager()=>current;

    public void ChangeLoadingBackGround(int currentScene)
    {
        if(currentScene> 0 && currentScene<=3)
        { 
           backGroundLoadingImage.sprite = loadingSprites[currentScene-1];
           ShowLoadingPanel();
        }
    }
}
