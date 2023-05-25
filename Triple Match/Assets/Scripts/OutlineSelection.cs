using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    private bool isMouseButtonDown = false;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform.CompareTag("Cube") || raycastHit.transform.CompareTag("Sphere") || raycastHit.transform.CompareTag("Capsule"))
            {
                if (highlight != null && highlight != raycastHit.transform)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = false;
                }
                highlight = raycastHit.transform;
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = 6.0f;
                }
            }
            else // Mouse on Plane
            {
                if (highlight != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = false;
                    highlight = null;
                }
            }
        }

        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            isMouseButtonDown = true;
            if (highlight != null)
            {
                if (selection != null && selection != highlight)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = highlight;
                highlight = null;
            }
            else
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonDown = false;
        }
        else if (!isMouseButtonDown && highlight != null && highlight != selection)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = true;
        }
    }
}

