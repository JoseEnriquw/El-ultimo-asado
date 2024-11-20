using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager current;

    [SerializeField] private TextMeshProUGUI Nombretxt;
    [SerializeField] private Image NombreImg;
    [SerializeField] private Image Billetera;
    [SerializeField] private TextMeshProUGUI TareaTxt;
    [SerializeField] private GameObject taskPanel;

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

    public static UIManager GetUIManager()=>current;
}
