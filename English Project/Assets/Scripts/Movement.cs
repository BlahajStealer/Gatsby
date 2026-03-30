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
    raycast rc;

    public GameObject obObj;
    public GameObject NickCarryAway;
    Animator ani;
    Animator wAni;
    
    public GameObject NickTrans;
    public GameObject MeyerTrans;
    public GameObject Meyer;
    bool stopAnims = true;
    void Start()
    {
        if (obObj != null)
        {
            ob = obObj.GetComponent<Objectives>();

        }
        if (NickCarryAway != null)
        {
            wAni = Meyer.GetComponent<Animator>();
            ani = NickCarryAway.GetComponent<Animator>();

        }
        if (Ray != null)
        {
            rc = Ray.GetComponent<raycast>();

        }
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (NickCarryAway != null && stopAnims)
        {
            NickCarryAway.transform.position = NickTrans.transform.position;
            
        }
        if (rc != null)
        {
            if (rc.DoneWithMeyer && Meyer != null && stopAnims)
            {
                Debug.Log("Hai");
                Meyer.transform.position = MeyerTrans.transform.position;
            }
        }
        Vector2 position = (Vector2)rb.position + move * speed * Time.deltaTime;
        rb.MovePosition(position);
    }

    void Update()
    {
        if (!rc.DoneWithMeyer)
        {
            SetWolfFalse();
            wAni.SetBool("Front", true);
        }
        if (rc != null)
        {
            if (rc.tableSeated)
            {
                stopAnims = false;
                if (rc.chosentable.transform.position.x < 8)
                {
                    Meyer.transform.position = new Vector2(rc.chosentable.transform.position.x - 1.5f, rc.chosentable.transform.position.y);
                    
                } else
                {
                    Meyer.transform.position = new Vector2(rc.chosentable.transform.position.x + 1.5f, rc.chosentable.transform.position.y);
                    
                }
                
                NickCarryAway.transform.position =  new Vector2(rc.chosentable.transform.position.x, rc.chosentable.transform.position.y - 1.5f);
                SetNickFalse();
                ani.SetBool("Back", true);
                if (rc.DoneWithMeyer)
                {
                    SetWolfFalse();
                    ani.SetBool("Side", true);
                }
                
            }
        }
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
            if (NickCarryAway != null && stopAnims)
            {
                SetNickFalse();
                ani.SetBool(walkingB ? "BackW" : "Back", true);
                if (rc.DoneWithMeyer)
                {
                    SetWolfFalse();
                    wAni.SetBool(walkingB ? "BackW" : "Back", true);
                }
                
            }
            Ray.transform.localPosition = new Vector2(0, 2.08f);
            Ray.transform.rotation = new Quaternion(0, 0, 0, 90);
        }
        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            walkingB = false;
            SetAllFalse();
            anim.SetBool("BackIdle", true);
            if (NickCarryAway != null && stopAnims)
            {
                SetNickFalse();
                ani.SetBool("Back", true);
                if (rc.DoneWithMeyer)
                {
                    SetWolfFalse();
                    wAni.SetBool("Back", true);
                }
                
            }
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
            if (NickCarryAway != null && stopAnims)
            {
                SetNickFalse();
                ani.SetBool(walkingF ? "FrontW" : "Front", true);
                if (rc.DoneWithMeyer)
                {
                    SetWolfFalse();
                    wAni.SetBool(walkingF ? "FrontW" : "Front", true);
                }

            }
            Ray.transform.localPosition = new Vector2(0, -2.08f);
            Ray.transform.rotation = new Quaternion(0, 0, 0, 90);
        }
        if (Keyboard.current.sKey.wasReleasedThisFrame)
        {
            walkingF = false;
            SetAllFalse();
            if (NickCarryAway != null && stopAnims)
            {
                SetNickFalse();
                ani.SetBool("Front", true);
                SetWolfFalse();
                wAni.SetBool("Front", true);
            }
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
            if (NickCarryAway != null && stopAnims)
            {
                SetNickFalse();
                ani.SetBool(walkingS ? "SideW" : "Side", true);
                NickCarryAway.GetComponent<SpriteRenderer>().flipX = false;
                SetWolfFalse();
                wAni.SetBool(walkingS ? "SideW" : "Side", true);
                Meyer.GetComponent<SpriteRenderer>().flipX = false;
            }
            Ray.transform.rotation = new Quaternion(0, 0, -90, 90);
            Ray.transform.localPosition = new Vector2(-2.08f, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Keyboard.current.aKey.wasReleasedThisFrame)
        {
            walkingS = false;
            SetAllFalse();
            if (NickCarryAway != null && stopAnims)
            {
                SetNickFalse();
                ani.SetBool("Side", true);
                SetWolfFalse();
                wAni.SetBool("Side", true);
            }
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
            if (NickCarryAway != null && stopAnims)
            {
                SetNickFalse();
                ani.SetBool(walkingS ? "SideW" : "Side", true);
                NickCarryAway.GetComponent<SpriteRenderer>().flipX = true;
                SetWolfFalse();
                wAni.SetBool(walkingS ? "SideW" : "Side", true);
                Meyer.GetComponent<SpriteRenderer>().flipX = true;
            }
            Ray.transform.rotation = new Quaternion(0, 0, -90, 90);
            Ray.transform.localPosition = new Vector2(2.08f, 0);
            GetComponent<SpriteRenderer>().flipX = true;

        }
        if (Keyboard.current.dKey.wasReleasedThisFrame)
        {
            walkingS = false;
            SetAllFalse();
            if (NickCarryAway != null && stopAnims)
            {
                SetNickFalse();
                ani.SetBool("Side", true);
                SetWolfFalse();
                wAni.SetBool("Side", true);
            }
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
    void SetNickFalse()
    {
        ani.SetBool("Front", false);
        ani.SetBool("FrontW", false);
        ani.SetBool("Back", false);
        ani.SetBool("BackW", false);
        ani.SetBool("Side", false);
        ani.SetBool("SideW", false);
    }
        void SetWolfFalse()
    {
        wAni.SetBool("Front", false);
        wAni.SetBool("FrontW", false);
        wAni.SetBool("Back", false);
        wAni.SetBool("BackW", false);
        wAni.SetBool("Side", false);
        wAni.SetBool("SideW", false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Exit") && ob.DoneScene)
        {
            SceneManager.LoadScene(3);
        }
    }
}