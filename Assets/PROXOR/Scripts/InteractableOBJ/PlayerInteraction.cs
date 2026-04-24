using UnityEngine;
using TMPro;
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 3f;
    [SerializeField] private LayerMask _interactionLayer;

    [SerializeField] private TextMeshProUGUI _interactionText;
    private void Update()
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
            if (interactable != null)
            {
                UpdateUI(interactable.GetDescription());
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(0);
                    UpdateUI(interactable.GetDescription());
                }
                return;
            }
        }
        ClearUI();
    }

    private void UpdateUI(string description)
    {
        _interactionText.text = description;

        if (!_interactionText.gameObject.activeSelf)
            _interactionText.gameObject.SetActive(true);
    }

    public void ClearUI()
    {
        if (_interactionText.gameObject.activeSelf)
            _interactionText.gameObject.SetActive(false);
    }
}