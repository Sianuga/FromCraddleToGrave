using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Canvas[] uiWindows;
    private PlayerController3D player;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (var c in uiWindows)
            if (c) c.gameObject.SetActive(false);
    }

    public void RegisterPlayer(PlayerController3D p)
    {
        player = p;
    }

    public void ShowUI(string name)
    {
        foreach (var c in uiWindows)
        {
            if (c && c.name == name)
            {
                c.gameObject.SetActive(true);
                if (player) player.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void HideAllUI()
    {
        foreach (var c in uiWindows)
            if (c) c.gameObject.SetActive(false);
        if (player) player.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
