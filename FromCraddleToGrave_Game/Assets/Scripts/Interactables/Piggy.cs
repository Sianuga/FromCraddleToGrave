using UnityEngine;

public class Piggy : MonoBehaviour, IInteractable
{
    public string windowName;
    public void Interact(PlayerController3D player)
    {
        Debug.Log($"Interacted with {name}");
        // Your door logic here (open/close, toggle, etc.)
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        
    UIManager.Instance.ShowUI(windowName);
        }
}