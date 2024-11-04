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
        string text;
        switch (scenenumber)
        {
            case 1: text = "Hablar con Remy";
                UIManager.GetUIManager().SetTarea(text);
                break;
            case 2:
                text = "Buscar la linterna";
                UIManager.GetUIManager().SetTarea(text);
                break;
            case 3:
                text = "Buscar el botiquin";
                UIManager.GetUIManager().SetTarea(text);
                break;
            case 4:
                text = "Encuentra la llave y escapa";
                UIManager.GetUIManager().SetTarea(text);
                //textComponent.UpdateTextBasedOnInteraction(true, text, false);
                break;

            default:
                break;
        }
    }
}
