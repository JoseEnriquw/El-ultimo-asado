using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJodyAnimation : MonoBehaviour
{
    [SerializeField] private bool isActive;
    private JodyAnimatorControllerState jodyController;
    // Start is called before the first frame update
    void Start()
    {
        isActive= false;
        jodyController= FindObjectOfType<JodyAnimatorControllerState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        isActive= true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            jodyController.IsWaiting();
            jodyController.Gritar();
        }
    }
}
