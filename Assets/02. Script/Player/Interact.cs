using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [SerializeField] private float maxRayDistance;
    [SerializeField] private float checkRate;
    [SerializeField] private float lastCheckTime;
    [SerializeField] private GameObject curInteractGameObject;
    [SerializeField] private IInteractable interactable;
    [SerializeField] private TextMeshProUGUI promptText;

    public LayerMask layerMask;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime < checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRayDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    interactable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                interactable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = interactable.GetInteractPrompt();
    }
}
