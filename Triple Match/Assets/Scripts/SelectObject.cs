using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SelectObject : MonoBehaviour
{
    [SerializeField] private Collection collection;

    private bool isMouseHeld = false; // Track if mouse is held
    private GameObject clickedObject; // Track the object clicked
    private GameObject hoveredObject; // Track the object currently being hovered

    void Update()
    {
        // Check if mouse button is being held down
        if (Input.GetMouseButtonDown(0))
        {
            isMouseHeld = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickedObject = hit.collider.gameObject;
                hoveredObject = clickedObject;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseHeld = false;

            if (hoveredObject != null)
            {
                if (hoveredObject.CompareTag("Cube") || hoveredObject.CompareTag("Capsule") || hoveredObject.CompareTag("Sphere"))
                {
                    ICollectionItem item = hoveredObject.GetComponent<ICollectionItem>();
                    if (item != null)
                    {
                        collection.AddItem(item);
                        AudioManager.instance.PlaySFX("Place", 1f);
                    }
                }
            }

            clickedObject = null;
            hoveredObject = null;
        }
        else if (isMouseHeld)
        {
            // Update the hovered object while the mouse is held
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                hoveredObject = hit.collider.gameObject;
            }
            else
            {
                hoveredObject = null;
            }
        }
    }
}


