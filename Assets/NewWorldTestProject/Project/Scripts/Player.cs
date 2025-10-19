using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // variables for players
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float swimSpeed;
    [SerializeField]
    private float swimRunSpeed;
    private float currentSpeed;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float swimJumpForce;

    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private float cameraRotationXMin;
    [SerializeField]
    private float cameraRotationXMax;
    private float mouseX;
    private float mouseY;

    [SerializeField]
    private float maxDrag;

    [SerializeField]
    private bool visibleCursor;

    // State variable
    private bool isRun;
    private bool isJump;
    private bool isSwim;
    private bool isEnergyLack;

    private float swimTimer;
    private float timer;

    // Required components
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private PlayerState state;

    private void Awake()
    {
        if (visibleCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private IEnumerator Start()
    {
        while (true)
        {
            StartCoroutine(Swim());
            FollowCamera();
            Rotation();
            Jump();
            Run();
            Move();
            StateControl();

            yield return null;
        }
    }

    private IEnumerator Swim()
    {
        bool canSwim = isSwim && !isEnergyLack;

        if (!canSwim)
        {
            swimTimer = 0f;

            rigid.useGravity = true;
            rigid.linearDamping = 0;

            yield break;
        }

        while (swimTimer < 1f)
        {
            swimTimer += Time.deltaTime * 0.025f;

            rigid.linearDamping = Mathf.Lerp(0f, maxDrag, swimTimer);

            yield return null;
        }

        rigid.useGravity = false;

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * swimJumpForce * Time.deltaTime;
        }
        else
        {
            rigid.useGravity = true;
        }
    }

    private void FollowCamera()
    {
        cam.transform.position = transform.position + Vector3.up * 0.95f;

        cam.transform.rotation = Quaternion.Euler(mouseY, mouseX, cam.transform.rotation.z);
    }

    private void Rotation()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * sensitivity;
        mouseY -= Input.GetAxisRaw("Mouse Y") * sensitivity;

        mouseY = Mathf.Clamp(mouseY, cameraRotationXMin, cameraRotationXMax);

        transform.rotation = Quaternion.Euler(transform.rotation.x, mouseX, transform.rotation.z);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump && !isEnergyLack)
        {
            isJump = true;

            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Run()
    {
        if (!isEnergyLack)
        {
            isRun = Input.GetKey(KeyCode.LeftControl);
        }
    }

    private void Move()
    {
        currentSpeed = isRun ? isSwim ? swimRunSpeed : runSpeed : isSwim ? swimSpeed : walkSpeed;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    private void StateControl()
    {
        timer += Time.deltaTime;

        if (state.CurrentEnergy <= 0f && !isEnergyLack)
        {
            isEnergyLack = true;
        }

        if (isRun || isJump)
        {
            state.Descrease(PlayerState.StateType.ENERGY, Time.deltaTime * 8f);
        }

        if (isEnergyLack)
        {
            isRun = false;
            isJump = false;
        }

        state.Increase(PlayerState.StateType.ENERGY, Time.deltaTime * 3f);

        if (state.CurrentEnergy >= 20f && isEnergyLack)
        {
            isEnergyLack = false;
        }

        if (timer > 300f)
        {
            timer -= 300f;

            state.Descrease(PlayerState.StateType.HUNGRY, 1f);
            state.Descrease(PlayerState.StateType.THIRST, 2.5f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Map
        if (collision.gameObject.layer == 9 && !isEnergyLack)
        {
            isJump = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Water
        if (other.gameObject.layer == 10 && !isEnergyLack)
        {
            isSwim = true;
        }
        // Explosion
        if (other.gameObject.layer == 12)
        {
            rigid.AddForce(Vector3.up * 10f + (other.transform.position - transform.position).normalized * 2.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isEnergyLack)
        {
            isSwim = false;
        }
    }
}
