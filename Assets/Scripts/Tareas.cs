using Assets.Scripts.GameManager;
using UnityEngine;

public class Tareas : MonoBehaviour
{
    private int Scenenumber;
    void Start()
    {
        Scenenumber= GameManager.GetGameManager().GetSceneNumber();
        AsingTask(Scenenumber);
    }

    private void AsingTask(int scenenumber)
    {
        Text textComponent = this.GetComponent<Text>();
        var text="";
        switch (scenenumber)
        {
            case 1: text = "Hablar con Remy";
                textComponent.UpdateTextBasedOnInteraction(true, text, false);
                break;
            case 2:
                text = "Buscar la linterna";
                textComponent.UpdateTextBasedOnInteraction(true, text, false);
                break;
            case 3:
                text = "Buscar el botiquin";
                textComponent.UpdateTextBasedOnInteraction(true, text, false);
                break;
            case 4:
                text = "Encuentra la llave y escapa";
                textComponent.UpdateTextBasedOnInteraction(true, text, false);
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
