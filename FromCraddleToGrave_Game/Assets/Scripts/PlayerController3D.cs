using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3D : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public InputActionReference interactAction;
    public InputActionReference sitAction;

    public float moveSpeed = 4f;
    public float mouseSensitivity = 120f;
    public float maxPitch = 80f;

    public float interactDistance = 2f;
    public LayerMask interactMask = ~0;
    public Camera cam;

    public Animator animator;
    public string isWalkingParam = "IsWalking";
    public string sitParam = "IsSitting";
    public string grabTrigger = "Grab";

    private CharacterController cc;
    private float yaw;
    private float pitch;
    private bool isSitting;
    private bool isWalking;
    private bool controlBlocked;
    private float blockTimer;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (!cam) cam = Camera.main;
        yaw = transform.eulerAngles.y;
        UIManager.Instance.RegisterPlayer(this);
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        lookAction.action.Enable();
        interactAction.action.Enable();
        sitAction.action.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        lookAction.action.Disable();
        interactAction.action.Disable();
        sitAction.action.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (controlBlocked)
        {
            blockTimer -= Time.deltaTime;
            if (blockTimer <= 0f) controlBlocked = false;
        }

        if (!controlBlocked)
        {
            HandleLook();
            HandleSit();
            HandleMove();
        }

        HandleInteract();
    }

    private void HandleLook()
    {
        Vector2 look = lookAction.action.ReadValue<Vector2>();
        float dt = Time.deltaTime;
        yaw += look.x * mouseSensitivity * dt;
        pitch -= look.y * mouseSensitivity * dt;
        pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        if (cam) cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void HandleMove()
    {
        if (isSitting)
        {
            cc.SimpleMove(Vector3.zero);
            animator.SetBool(isWalkingParam, false);
            isWalking = false;
            return;
        }

        Vector2 m = moveAction.action.ReadValue<Vector2>();
        bool isMovingNow = m.sqrMagnitude > 0.1f;

        if (isMovingNow && !isWalking)
        {
            isWalking = true;
            animator.SetBool(isWalkingParam, true);
        }
        else if (!isMovingNow && isWalking)
        {
            isWalking = false;
            animator.SetBool(isWalkingParam, false);
        }

        Vector3 local = new Vector3(m.x, 0f, m.y);
        Vector3 world = transform.TransformDirection(local);
        cc.SimpleMove(world * moveSpeed);
    }

    private void HandleInteract()
    {
        if (!interactAction.action.WasPressedThisFrame()) return;
        if (animator) animator.SetTrigger(grabTrigger);

        controlBlocked = true;
        blockTimer = 3f;

        Collider[] nearby = Physics.OverlapSphere(transform.position, interactDistance, interactMask);
        float closestDist = float.MaxValue;
        IInteractable closest = null;
        foreach (Collider c in nearby)
        {
            var interactable = c.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float dist = Vector3.Distance(transform.position, c.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = interactable;
                }
            }
        }
        if (closest != null) closest.Interact(this);
    }

    private void HandleSit()
    {
        if (sitAction.action.WasPressedThisFrame())
        {
            isSitting = !isSitting;
            animator.SetBool(sitParam, isSitting);
        }
        if (isSitting)
        {
            Vector2 m = moveAction.action.ReadValue<Vector2>();
            if (m.sqrMagnitude > 0.001f)
            {
                isSitting = false;
                animator.SetBool(sitParam, false);
            }
        }
    }
}
