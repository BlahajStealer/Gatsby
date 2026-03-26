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
    public bool p2inter = false;
    float sulkTimer = 0;
    public GameObject ObjectivesObj;
    Objectives ob;
    int TablesActive = 0;
    bool barTrue = false;
    bool phoneTrue = false;
    public string[] MeyerLines;
    bool InteractingWithMeyer;
    int CurrentLine = 0;
    int currentProf = 2;
    bool Customer = false;
    MinigameManager mm;
    public GameObject mmObj;
    public string[] DialogueP2;
    int current = 0;
    int currentLoser = 2;
    public bool firstScene = true;
    bool startThis = false;
    public bool Scene4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mmObj != null)
        {
            mm = mmObj.GetComponent<MinigameManager>();
        }
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
            if (firstScene)
            {
                GatsbyText.SetActive(false);
            
            }
                
            
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (BlackPanel != null)
        {
            if (!firstScene && startThis)
            {
                startThis = false;
                StartCoroutine(ChangeColor(false));
            } 
            else if (firstScene)
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
                    if (firstScene)
                    {
                        Color start = startColor;
                        startColor = endColor;
                        endColor = start;

                        StartCoroutine(ChangeColor(false));
                    }


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
        if (p2inter && Keyboard.current.eKey.wasPressedThisFrame)
        {
            ob.TableDone = true;
            if (current == DialogueP2.Length)
            {
                startThis = true;
                ts.off();
            } else
            {
                ts.Interaction(DialogueP2[current], currentLoser);
                current++;
            }
            if (currentLoser == 2) currentLoser = 0; else currentLoser = 2;

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
        if (InteractingWithMeyer && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (CurrentLine == MeyerLines.Length - 1)
            {
                ts.off();
                ob.newObj = true;
            } else
            {
                if (currentProf == 2)
                {
                    currentProf--;

                } else if (currentProf == 1)
                {
                    currentProf++;
                }
                ts.Interaction(MeyerLines[CurrentLine], currentProf);


                CurrentLine++;
                
            }
            
        }
        if (phoneTrue && ob.objectivesFinished == 3 && Keyboard.current.eKey.wasPressedThisFrame)
        {
            ts.Interaction("It's WolfSheim. You need to get down to the Pharmacy. Now.", 1);
            ob.phoneDone = true;
        }
        if (Customer && Keyboard.current.eKey.wasPressedThisFrame)
        {
            mm.nextFunc();
            Customer=false;
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
        } else if (other.gameObject.CompareTag("Meyer"))
        {
            InteractingWithMeyer = true;
        } if (other.gameObject.CompareTag("Customer"))
        {
            Customer = true;
        }
        if (mmObj != null)
        {
            if (other.gameObject.CompareTag("ExitBar") && mm.CanLeave)
            {
                SceneManager.LoadScene(4);
            }
        }else if (other.gameObject.CompareTag("Loser2"))
        {
            p2inter = true;
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
            ts.off();
        }else if (other.gameObject.CompareTag("Meyer"))
        {
            InteractingWithMeyer = false;
        }if (other.gameObject.CompareTag("Customer"))
        {
            Customer = false;
        }else if (other.gameObject.CompareTag("Loser2"))
        {
            p2inter = false;
            ts.off();
        }

    }

    public IEnumerator ChangeColor(bool first)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t/duration);
            sr.material.color = Color.Lerp(startColor, endColor, blend);

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
        if (firstScene)
        {
            First.SetActive(false);
            Second.SetActive(false);
        } else
        {
            Scene4 = true;
        }
        
    }
}
