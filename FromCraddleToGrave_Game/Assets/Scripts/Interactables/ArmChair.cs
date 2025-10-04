using UnityEngine;

public class ArmChair : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController3D player)
    {
        Debug.Log($"Interacted with {name}");
        // Your door logic here (open/close, toggle, etc.)
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}