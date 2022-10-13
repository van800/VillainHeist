using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // Yo! If you want to make an item selectable you have to set its tag to selectable.
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;

    [SerializeField] private Material defaultMaterial;
    // Start is called before the first frame update

    private Transform _selection;
    private bool firstSelectionMade = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If item is no longer in the selection, calls deselect();
        if (_selection != null)
        {
            _selection.BroadcastMessage("deselect");
            _selection = null;
        }
        
        // Cast ray, if hits a selectable item, calls select() method within the item.
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                if (!firstSelectionMade)
                {
                    firstSelectionMade = true;
                }
                // Make Selection
                selection.BroadcastMessage("select");
                _selection = selection;
            }
        }
    }
}
