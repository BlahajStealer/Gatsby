using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
public class CarScript : MonoBehaviour
{
    public float speed;
    public float MaxSpeed;
    public float speedUpMult;
    public Rigidbody2D rb;
    Vector2 move;
    public bool Finishing = false;
    public float duration = 0.5f;
    public Vector2 end;
    public GameObject Car;
    bool NotDestroyed = true;
    public float slowDownMult;
    bool startInt = false;
    public string[] Police;
    public int[] PeopleSpeaking;
    public GameObject tsObj;
    TextSystem ts;
    public GameObject animObj;
    Animator anim;
    int currentOne = 0;
    float Timer = 0;
    public GameObject Panel;
    public bool goingLeft = false;
    public GameObject Myrtle;
    public Sprite MyrtleAlive;
    public Sprite MyrtleDead;
    public float SpeedOfCrash;
    bool startThis;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = animObj.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ts = tsObj.GetComponent<TextSystem>();
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("Start") == true)
        {
            Timer += Time.deltaTime;
            if (Timer > 0.6f)
            {
                anim.SetBool("Start", false);
            }
        }
        if ((speed < MaxSpeed && speed > -9) && !Finishing)
        {
            speed += Time.deltaTime * speedUpMult;

        } else if ((speed > MaxSpeed && speed <= -10) && !Finishing)
        {
            speed -= Time.deltaTime * speedUpMult;

        }
        if (Finishing && speed >= 0)
        {
            Debug.Log("Still here");
            speed -= Time.deltaTime * slowDownMult;
        }
        if (speed < 0 && speed > -9)
        {
            speed = 0;
        }
        if (speed == 0)
        {
            if (!startInt)
            {
                startInt = true;
                ts.Interaction(Police[currentOne], currentOne);
                currentOne++;
                anim.SetBool("Start", true);

            }
        }

        if (startInt && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (currentOne < Police.Length)
            {
                ts.Interaction(Police[currentOne], PeopleSpeaking[currentOne]);
                currentOne++;
            } else
            {
                ts.off();
            }


        }
        if (currentOne >= 4)
        {
            Finishing = false;
        }
        if (startInt && currentOne >= 3)
        {
            speed += Time.deltaTime * (currentOne-2);
        }
    }
    private void FixedUpdate()
    {

        move = GetComponent<PlayerInput>().actions["Move"].ReadValue<Vector2>();
        Debug.Log(move);
        if (Finishing && Car.transform.position.y <= 2.366f)
        {
            move.y = .3f;
        } if (startThis)
        {
            Debug.Log("Hello");
            move.y = .5f;
        }
        move.x = goingLeft ? -1 : 1;
        Vector2 position = (Vector2)rb.position + move * Mathf.Abs(speed) * Time.deltaTime;

        rb.MovePosition(position);
        if (NotDestroyed && Finishing)
        {
            NotDestroyed = false;
            GameObject[] Cars = GameObject.FindGameObjectsWithTag("Car");
            for (int i = 0; i < Cars.Length; i++)
            {
                    Destroy(Cars[i]);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            
            Panel.SetActive(true);
            Time.timeScale = .1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EndTrigger"))
        {
            Finishing = true;
        } else if (other.gameObject.CompareTag("DoneTrig"))
        {
            if (currentOne >= 7)
            {
                SceneManager.LoadScene(8);
            }
            else
            {
                Car.transform.position = new Vector2(261, Car.transform.position.y);
            }
        } if (other.gameObject.CompareTag("Time To die Myrtle"))
        {
            Debug.Log("MyrtleDeath");

            startThis = true;
        } if (other.gameObject.CompareTag("Myrtle"))
        {
            Myrtle.GetComponent<SpriteRenderer>().sprite = MyrtleDead;
        }
    }
    public void Retry()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Skip()
    {
        Time.timeScale = 1;

        Debug.Log("Next Scene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
