using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    public GameObject Ray;
    public float speed;
    public Rigidbody2D rb;
    Vector2 move;
    private Animator anim;
    bool walkingB = false;
    bool walkingF = false;
    bool walkingS = false;
    Objectives ob;
    public GameObject obObj;

    void Start()
    {
        if (obObj != null)
        {
            ob = obObj.GetComponent<Objectives>();

        }
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + move * speed * Time.deltaTime;
        rb.MovePosition(position);
    }

    void Update()
    {
        move = GetComponent<PlayerInput>().actions["Move"].ReadValue<Vector2>();

        // W Key - Back walk
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            walkingB = true;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            SetAllFalse();
            anim.SetBool(walkingB ? "BackWalk" : "BackIdle", true);

            Ray.transform.localPosition = new Vector2(0, 2.08f);
            Ray.transform.rotation = new Quaternion(0, 0, 0, 90);
        }
        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            walkingB = false;
            SetAllFalse();
            anim.SetBool("BackIdle", true);
        }

        // S Key - Forward walk
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            walkingF = true;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            SetAllFalse();
            anim.SetBool(walkingF ? "WalkForward" : "Idle", true);

            Ray.transform.localPosition = new Vector2(0, -2.08f);
            Ray.transform.rotation = new Quaternion(0, 0, 0, 90);
        }
        if (Keyboard.current.sKey.wasReleasedThisFrame)
        {
            walkingF = false;
            SetAllFalse();
            anim.SetBool("Idle", true);
        }

        // A Key - Side walk left
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            walkingS = true;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            SetAllFalse();
            anim.SetBool(walkingS ? "SideWalk" : "IdleSide", true);

            Ray.transform.rotation = new Quaternion(0, 0, -90, 90);
            Ray.transform.localPosition = new Vector2(-2.08f, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Keyboard.current.aKey.wasReleasedThisFrame)
        {
            walkingS = false;
            SetAllFalse();
            anim.SetBool("IdleSide", true);
        }

        // D Key - Side walk right
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            walkingS = true;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            SetAllFalse();
            anim.SetBool(walkingS ? "SideWalk" : "IdleSide", true);

            Ray.transform.rotation = new Quaternion(0, 0, -90, 90);
            Ray.transform.localPosition = new Vector2(2.08f, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Keyboard.current.dKey.wasReleasedThisFrame)
        {
            walkingS = false;
            SetAllFalse();
            anim.SetBool("IdleSide", true);
        }
    }

    void SetAllFalse()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("IdleSide", false);
        anim.SetBool("SideWalk", false);
        anim.SetBool("WalkForward", false);
        anim.SetBool("BackIdle", false);
        anim.SetBool("BackWalk", false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Exit") && ob.DoneScene)
        {
            SceneManager.LoadScene(3);
        }
    }
}