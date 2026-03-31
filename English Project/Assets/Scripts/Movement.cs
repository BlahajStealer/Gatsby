using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
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
    public float speedOfMovement;
    bool MoveMeyer = false;
    bool setAnims = false;
    public GameObject Transform1;
    public GameObject Transform2;
    bool nextOne = false;
    public GameObject Trans;
    public float speedOfSecond;
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
            NickCarryAway.transform.position = Vector2.MoveTowards(
                NickCarryAway.transform.position,
                NickTrans.transform.position,
                speedOfMovement * Time.deltaTime
            );
        }

        if (rc != null && rc.DoneWithMeyer && Meyer != null && stopAnims)
        {
            Meyer.transform.position = Vector2.MoveTowards(
                Meyer.transform.position,
                MeyerTrans.transform.position,
                speedOfMovement * Time.deltaTime
            );
        }
        Vector2 position = (Vector2)rb.position + move * speed * Time.deltaTime;
        rb.MovePosition(position);
    }

    void Update()
    {
        if (rc.MeyersDepart)
        {
            Meyer.transform.position = Vector2.MoveTowards(
            Meyer.transform.position,
            Transform1.transform.position,
            speedOfSecond * Time.deltaTime
           );
            SetWolfFalse();
            wAni.SetBool("FrontW", true);
            if (Meyer.transform.position == Transform1.transform.position)
            {
                nextOne = true;
                rc.MeyersDepart = false;
            }
        }
        if (nextOne)
        {
            Meyer.transform.position = Vector2.MoveTowards(
            Meyer.transform.position,
            Transform2.transform.position,
            speedOfSecond * Time.deltaTime
            );
            SetWolfFalse();
            wAni.SetBool("FrontW", true);
            if (Meyer.transform.position == Transform2.transform.position)
            {
                Meyer.SetActive( false );
                nextOne = false;
            }
        }
        if (stopAnims)
        {
            rc.endThisThing = false;
        }
        if (rc.MeyersDepart)
        {
            MoveMeyer = false;
        }
        if (!rc.DoneWithMeyer && wAni != null)
        {
            SetWolfFalse();
            wAni.SetBool("Front", true);
        }
        if (rc.NickRun)
        {
            rc.ReadyForTom = true;
            Vector2 nickTarget = Trans.transform.position;
            Vector2 nickDelta = nickTarget - (Vector2)NickCarryAway.transform.position;
            NickCarryAway.transform.position = Vector2.MoveTowards(
            NickCarryAway.transform.position,
            nickTarget,
            speedOfSecond * Time.deltaTime
            );


            if (nickDelta.magnitude > 0.05f) // only animate if actually moving
            {
                SetNickFalse();
                ani.SetBool("BackW", true);

            } else
            {
                SetNickFalse();
                ani.SetBool("Back", true);
            }



        }
        if (rc != null)
        {
            if (rc.tableSeated)
            {
                MoveMeyer = true;

                stopAnims = false;

                
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

        if (MoveMeyer)
        {
            if (rc.chosentable.transform.position.x < 8)
            {

                NickCarryAway.transform.position = Vector2.MoveTowards(
                    NickCarryAway.transform.position,
                    new Vector2(rc.chosentable.transform.position.x, rc.chosentable.transform.position.y - 1.5f),
                    speedOfMovement * Time.deltaTime
                );



                Meyer.transform.position = Vector2.MoveTowards(
                    Meyer.transform.position,
                    new Vector2(rc.chosentable.transform.position.x - 1.5f, rc.chosentable.transform.position.y),
                    speedOfMovement * Time.deltaTime
                );
                if (!setAnims)
                {
                    SetNickFalse();
                    ani.SetBool("Back", true);
                    SetWolfFalse();
                    wAni.SetBool("Side", true);
                    setAnims = true;
                    Meyer.GetComponent<SpriteRenderer>().flipX = true;
                }



            }
            else
            {
                NickCarryAway.transform.position = Vector2.MoveTowards(
                    NickCarryAway.transform.position,
                    new Vector2(rc.chosentable.transform.position.x, rc.chosentable.transform.position.y - 1.5f),
                    speedOfMovement * Time.deltaTime
                );


                Meyer.transform.position = Vector2.MoveTowards(
                    Meyer.transform.position,
                    new Vector2(rc.chosentable.transform.position.x + 1.5f, rc.chosentable.transform.position.y),
                    speedOfMovement * Time.deltaTime
                );
                if (!setAnims)
                {
                    SetNickFalse();
                    ani.SetBool("Back", true);
                    SetWolfFalse();
                    wAni.SetBool("Side", true);
                    setAnims = true;
                    Meyer.GetComponent<SpriteRenderer>().flipX = false;
                }

            }
        }
        move = GetComponent<PlayerInput>().actions["Move"].ReadValue<Vector2>();

        // W Key - Back walk
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            if (NickCarryAway != null && stopAnims)
            {
                NickTrans.transform.localPosition = new Vector2(0, -2.82f);
                MeyerTrans.transform.localPosition = new Vector2(0, -5.87f);

            }
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
            if (NickCarryAway != null && stopAnims)
            {
                NickTrans.transform.localPosition = new Vector2(0, 2.82f);
                MeyerTrans.transform.localPosition = new Vector2(0, 5.87f);

            }
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
                if (rc.DoneWithMeyer)
                {
                    SetWolfFalse();
                    wAni.SetBool("Front", true);
                }
                
            }
            anim.SetBool("Idle", true);
        }

        // A Key - Side walk left
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            if (NickCarryAway != null && stopAnims)
            {
                NickTrans.transform.localPosition = new Vector2(2.82f, 0);
                MeyerTrans.transform.localPosition = new Vector2(5.87f, 0);

            }
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
                if (rc.DoneWithMeyer)
                {
                    SetWolfFalse();
                    wAni.SetBool(walkingS ? "SideW" : "Side", true);
                    Meyer.GetComponent<SpriteRenderer>().flipX = false;
                }

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
                if (rc.DoneWithMeyer)
                {
                    SetWolfFalse();
                    wAni.SetBool("Side", true);
                }
                
            }
            anim.SetBool("IdleSide", true);
        }

        // D Key - Side walk right
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            if (NickCarryAway != null && stopAnims)
            {
                NickTrans.transform.localPosition = new Vector2(-2.82f, 0);
                MeyerTrans.transform.localPosition = new Vector2(-5.87f, 0);

            }
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
                if (rc.DoneWithMeyer)
                {
                    SetWolfFalse();
                    wAni.SetBool(walkingS ? "SideW" : "Side", true);
                    Meyer.GetComponent<SpriteRenderer>().flipX = true;
                }

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
                if (rc.DoneWithMeyer)
                {
                    SetWolfFalse();
                    wAni.SetBool("Side", true);
                }
                
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