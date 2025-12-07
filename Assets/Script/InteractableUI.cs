using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI Component for interactable prompts
/// Attach this to a UI GameObject that will show the interact button
/// </summary>
public class InteractableUI : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("The button or image to show")]
    public GameObject interactButton;
    
    [Tooltip("Optional text to display (e.g., 'Press E to interact')")]
    public TextMeshProUGUI interactText;
    
    [Tooltip("Optional: Key to display in the text")]
    public KeyCode interactKey = KeyCode.E;
    
    void Start()
    {
        // Hide by default
        if (interactButton != null)
        {
            interactButton.SetActive(false);
        }
        
        // Set up text if available
        if (interactText != null)
        {
            interactText.text = $"Press {interactKey} to interact";
        }
    }
    
    public void Show()
    {
        if (interactButton != null)
        {
            interactButton.SetActive(true);
        }
    }
    
    public void Hide()
    {
        if (interactButton != null)
        {
            interactButton.SetActive(false);
        }
    }
}

