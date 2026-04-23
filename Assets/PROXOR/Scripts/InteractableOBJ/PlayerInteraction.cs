using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float _interactionDistance = 3f;
    [SerializeField] LayerMask _interactionLayer;
    [SerializeField] TextMeshProUGUI _interactionText;
    [SerializeField] TMP_Text _stateText;
    void Update()
    {
        InteractionRay();
    }
    private void InteractionRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance, _interactionLayer))
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable is DoorBehaviour door && door.IsOpen)
            {
                ClearUI();
                return;
            }
            UpdateUI(interactable.GetDescription(), interactable.GetState());

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(0);
            }
            return;

        }
        ClearUI();
    }
    private void UpdateUI(string description, string state)
    {
        _interactionText.text = description;
        _stateText.text = state;

        if (!_interactionText.gameObject.activeSelf) _interactionText.gameObject.SetActive(true);
        bool hasState = !string.IsNullOrWhiteSpace(state);
        if (_stateText.gameObject.activeSelf != hasState)
        {
            _stateText.gameObject.SetActive(hasState);
        }
    }

    public void ClearUI()
    {
        if (_interactionText.gameObject.activeSelf) _interactionText.gameObject.SetActive(false);
        if (_stateText.gameObject.activeSelf) _stateText.gameObject.SetActive(false);
    }
}