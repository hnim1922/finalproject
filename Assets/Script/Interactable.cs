using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("The tag of the player object")]
    public string playerTag = "Player";
    
    [Tooltip("Maximum distance for interaction")]
    public float interactionDistance = 3f;
    
    [Tooltip("Angle threshold for facing check (in degrees)")]
    [Range(0f, 180f)]
    public float facingAngleThreshold = 60f;
    
    [Header("UI Settings")]
    [Tooltip("UI GameObject to show/hide (should contain the interact button)")]
    public GameObject interactUI;
    
    [Header("Events")]
    [Tooltip("Called when player interacts with this object")]
    public UnityEvent OnInteract;
    
    [Tooltip("Called when player enters interaction range")]
    public UnityEvent OnEnterRange;
    
    [Tooltip("Called when player exits interaction range")]
    public UnityEvent OnExitRange;
    
    private bool isPlayerInRange = false;
    private bool isPlayerFacing = false;
    private GameObject playerObject;
    private Collider triggerCollider;
    
    void Start()
    {
        // Get or add collider
        triggerCollider = GetComponent<Collider>();
        if (triggerCollider == null)
        {
            triggerCollider = gameObject.AddComponent<SphereCollider>();
            Debug.Log($"[Interactable] {gameObject.name}: Added SphereCollider automatically");
        }
        
        // Ensure it's a trigger
        triggerCollider.isTrigger = true;
        Debug.Log($"[Interactable] {gameObject.name}: Collider set as trigger. Player Tag: '{playerTag}'");
        
        // Hide UI initially
        if (interactUI != null)
        {
            interactUI.SetActive(false);
            Debug.Log($"[Interactable] {gameObject.name}: UI initialized and hidden");
        }
        else
        {
            Debug.LogWarning($"[Interactable] {gameObject.name}: No Interact UI assigned!");
        }
    }
    
    void Update()
    {
        if (isPlayerInRange && playerObject != null)
        {
            // Check if player is facing the object
            CheckPlayerFacing();
            
            // Check for interaction input
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isPlayerFacing)
                {
                    Interact();
                }
                else
                {
                    Debug.Log($"[Interactable] {gameObject.name}: Player pressed E but is not facing the object. Turn towards it to interact.");
                }
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = true;
            playerObject = other.gameObject;
            Debug.Log($"[Interactable] {gameObject.name}: Player '{playerObject.name}' entered interaction range");
            OnEnterRange?.Invoke();
        }
        else
        {
            Debug.Log($"[Interactable] {gameObject.name}: Object '{other.name}' (Tag: '{other.tag}') entered trigger, but doesn't match Player tag '{playerTag}'");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = false;
            isPlayerFacing = false;
            Debug.Log($"[Interactable] {gameObject.name}: Player '{other.name}' exited interaction range");
            playerObject = null;
            
            // Hide UI
            if (interactUI != null)
            {
                interactUI.SetActive(false);
            }
            
            OnExitRange?.Invoke();
        }
    }
    
    void CheckPlayerFacing()
    {
        if (playerObject == null) return;
        
        // Get direction from player to this object
        Vector3 directionToObject = (transform.position - playerObject.transform.position).normalized;
        
        // Get player's forward direction
        Vector3 playerForward = playerObject.transform.forward;
        
        // Calculate angle between player forward and direction to object
        float angle = Vector3.Angle(playerForward, directionToObject);
        
        // Check if player is facing the object (within threshold)
        bool wasFacing = isPlayerFacing;
        isPlayerFacing = angle <= facingAngleThreshold;
        
        // Debug when facing state changes
        if (wasFacing != isPlayerFacing)
        {
            if (isPlayerFacing)
            {
                Debug.Log($"[Interactable] {gameObject.name}: Player is now facing the object (Angle: {angle:F1}°, Threshold: {facingAngleThreshold}°)");
            }
            else
            {
                Debug.Log($"[Interactable] {gameObject.name}: Player is no longer facing the object (Angle: {angle:F1}°, Threshold: {facingAngleThreshold}°)");
            }
        }
        
        // Update UI visibility
        if (interactUI != null)
        {
            interactUI.SetActive(isPlayerFacing);
        }
        
        // Optional: Rotate UI to face camera
        if (isPlayerFacing && Camera.main != null)
        {
            interactUI.transform.LookAt(Camera.main.transform);
            interactUI.transform.Rotate(0, 180, 0); // Flip to face camera properly
        }
    }
    
    public void Interact()
    {
        Debug.Log($"[Interactable] {gameObject.name}: INTERACTION TRIGGERED by player '{playerObject?.name ?? "Unknown"}'");
        OnInteract?.Invoke();
    }
    
    // Helper method to enable/disable the interactable
    public void SetInteractable(bool enabled)
    {
        this.enabled = enabled;
        if (triggerCollider != null)
        {
            triggerCollider.enabled = enabled;
        }
        if (interactUI != null && !enabled)
        {
            interactUI.SetActive(false);
        }
        Debug.Log($"[Interactable] {gameObject.name}: SetInteractable({enabled})");
    }
}

