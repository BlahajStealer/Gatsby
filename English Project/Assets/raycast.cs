using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class raycast : MonoBehaviour
{
    public GameObject First;
    public GameObject Second;
    public GameObject TheGreatestGatsbyThatEverLived;
    public GameObject BlackPanel;
    private SpriteRenderer sr;
    public Color startColor;
    public Color startGatz;
    public Color endColor;
    public Color endGatz;
    public float duration;
    bool LookingAtButton = false;
    bool done = false;
    bool SecondAct;
    float Timer = 0;
    float EndTimer = 0;
    bool end;
    bool endGatzB;
    public GameObject GatsbyText;
    bool inTable;
    Transform childt;
    Transform childt2;
    public GameObject tsObj;
    TextSystem ts;
    public bool p1inter = false;
    float sulkTimer = 0;
    public GameObject ObjectivesObj;
    Objectives ob;
    int TablesActive = 0;
    bool barTrue = false;
    bool phoneTrue = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ObjectivesObj != null)
        {
            ob = ObjectivesObj.GetComponent<Objectives>();
        }
        if (tsObj != null)
        {
            ts = tsObj.GetComponent<TextSystem>();

        }
        if (BlackPanel != null)
        {
            sr = BlackPanel.GetComponent<SpriteRenderer>();
            sr.material.color = startColor;
            GatsbyText.SetActive(false);
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (BlackPanel != null)
        {
            if (end)
            {
                EndTimer += Time.deltaTime;
                SecondAct = false;
                done = false;

                if (EndTimer > 3)
                {
                    GatsbyText.SetActive(true);
                    sr = GatsbyText.GetComponent<SpriteRenderer>();
                    endGatzB = true;
                    startColor = startGatz;
                    endColor = endGatz;
                    StartCoroutine(ChangeColor(false));
                }
            }
            if (LookingAtButton && Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("Pressed!");
                StartCoroutine(ChangeColor(true));

                LookingAtButton = false;
            }
            if (done)
            {
                done = false;
                Color start = startColor;
                startColor = endColor;
                endColor = start;

                StartCoroutine(ChangeColor(false));

            }
            if (SecondAct)
            {
                Timer += Time.deltaTime;
                if (Timer >= 8)
                {
                    end = true;
                    Color start = startColor;
                    startColor = endColor;
                    endColor = start;
                    SecondAct = false;
                    StartCoroutine(ChangeColor(false));
                }
            }
            if (endGatzB)
            {
                sulkTimer += Time.deltaTime;
                if (sulkTimer >= 4)
                {
                    SceneManager.LoadScene(2);
                }
            }

        }
        if (barTrue && Keyboard.current.eKey.wasPressedThisFrame)
        {
            GameObject child = childt2.gameObject;
            child.SetActive(true);
            barTrue = false;
            ob.BarDone = true;
        }
        if (p1inter && Keyboard.current.eKey.wasPressedThisFrame)
        {
            ts.Interaction("The band will be arriving shortly! I'll direct them to the corner of the room!", 0);

            ob.BandDone = true;
            
        }
        if (inTable && Keyboard.current.eKey.wasPressedThisFrame)
        {
            GameObject child = childt.gameObject;
            if (!child.activeSelf)
            {
                child.SetActive(true);
                TablesActive++;
                if (TablesActive == 12)
                {
                    ob.TableDone = true;
                }
            }
        }

        if (phoneTrue && ob.objectivesFinished == 3 && Keyboard.current.eKey.wasPressedThisFrame)
        {
            ts.Interaction("It's WolfSheim. You need to get down to the Pharmacy. Now.", 1);
        }
    }
    
    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Button")) {
            LookingAtButton = true;
        } else if (other.gameObject.CompareTag("Table"))
        {
            inTable = true;
            childt = other.gameObject.transform.GetChild(0);

        } else if (other.gameObject.CompareTag("Loser1"))
        {
            p1inter = true;
        } else if (other.gameObject.CompareTag("Bar"))
        {
            barTrue = true;
            childt2 = other.gameObject.transform.GetChild(0);
        }
        else if (other.gameObject.CompareTag("Phone"))
        {
            phoneTrue = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Button")) {
            LookingAtButton = false;
        }
        else if (other.gameObject.CompareTag("Table"))
        {
            inTable = false;


        } else if (other.gameObject.CompareTag("Loser1"))
        {
            p1inter = false;

            ts.off();
        } else if (other.gameObject.CompareTag("Bar"))
        {
            barTrue = false;

        } else if (other.gameObject.CompareTag("Phone"))
        {
            phoneTrue = false;
        }
    }

    public IEnumerator ChangeColor(bool first)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t/duration);
            sr.material.color = Color.Lerp (startColor, endColor, blend);

            yield return null;
        }
        sr.material.color = endColor;

        if (first)
        {
            done = true;

        } else
        {
            SecondAct = true;
        }
        if (endGatzB)
        {
            done = false;
            SecondAct = false;
            end = false;
        }


            TheGreatestGatsbyThatEverLived.GetComponent<SpriteRenderer>().enabled = false;

        First.SetActive(false);
        Second.SetActive(false);
    }
}
