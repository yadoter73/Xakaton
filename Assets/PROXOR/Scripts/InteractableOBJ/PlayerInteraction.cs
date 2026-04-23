using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float _interactionDistance = 10f;

    public GameObject interactionUI;
    [SerializeField] TextMeshProUGUI _interactionText;
    [SerializeField] TMP_Text _stateText;
    void Update()
    {
        InteractionRay();
    }
    public Interactable InteractionRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSMTH = false;
        Interactable interactable = null;
        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                hitSMTH = true;
                _interactionText.text = interactable.GetDescription();
                if (interactable is DoorBehaviour DoorState)
                {
                    _stateText.text = DoorState.GetState();
                }
                else
                {
                    _stateText.text = "";
                }
                if (Input.GetButtonDown("UseItem"))
                {
                    interactable.Interact(0);
                }
            }

        }
        interactionUI.SetActive(hitSMTH);
        _stateText.gameObject.SetActive(hitSMTH);
        return interactable;
    }
}

public interface Interactable
{
    public void Interact(int id) { }
    string GetDescription(); 
}