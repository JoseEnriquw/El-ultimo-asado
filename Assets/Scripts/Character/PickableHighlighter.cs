using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PickableHighlighter : MonoBehaviour
    {
        public float rayDistance = 5f; // Distancia máxima del raycast
        public Color outlineColor = Color.yellow;
        public float outlineWidth = 5f;

        private GameObject currentObject;
        [SerializeField] private Camera mainCamera;

        void Update()
        {
            // Lanza un raycast desde la cámara hacia adelante
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag(Tags.ObjetoPickeable))
                {
                    // Si es un nuevo objeto, actualiza el resaltado
                    if (currentObject != hitObject)
                    {
                        ClearHighlight();
                        HighlightObject(hitObject);
                    }
                }
                else
                {
                    ClearHighlight();
                }
            }
            else
            {
                ClearHighlight();
            }
        }

        void HighlightObject(GameObject obj)
        {
            if (!obj.TryGetComponent<Outline>(out var outline))
            {
                outline = obj.AddComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = outlineColor;
                outline.OutlineWidth = outlineWidth;
            }
            else
            {
                outline.enabled = true;
            }

            currentObject = obj;
        }

        void ClearHighlight()
        {
            if (currentObject != null)
            {
                Outline outline = currentObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = false;
                }
                currentObject = null;
            }
        }
    }

}
