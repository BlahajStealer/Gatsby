using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
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
    public string[] LinesForParty;
    bool startTalking = false;
    int currentLineForNick = 0;
    public int[] currentPersonSpeaking;
    public string[] BandLines;
    public bool SpeakingToBand;
    int Lines = 0;
    public string[] MeyerNYCLines;
    public int[] MeyerNYCProfiles;
    public bool SpeakingWithLoser = false;
    public string[] LosersFinalLines;
    public int currentLines = 0;
    public bool Scene5 = false;
    bool wait = false;
    float timer;
    bool obupalreadydone = false;
    bool talkingToWaiter = false;
    public bool DoneWithMeyer = false;
    
    public string[] waiterLines;
    public int[] WaiterProfs;
    int currentLine = 0;
    public bool tableSeated = false;
    public GameObject chosentable;
    bool lookingAtTable;

    public string[] PhoneLines;
    public int[] TalkingTo;
    bool SecondPhone;
    int currentLinePhone = 0;
    bool SecondWave = false;
    bool PhoneDone = false;
    public string[] SecondWaveLines;
    public int[] SecondWaveProfs;
    int secondWaveLines;
    public bool MeyersDepart;
    public bool NickRun;
    bool LookingAtTom;
    public string[] TomLines;
    public int[] TomProfs;
    int currentTom = 0;
    public bool ReadyForTom = false;
    public bool endThisThing = true;
    bool TomDead;
    bool NicksCarHere = false;
    public string[] NickCarDialogue;
    public int[] NickCarProf;
    int nickCar = 0;
    public bool nickLeave = false;
    bool nickLeft = false;
    bool NextDaisy = false;
    bool LeaveScene = false;
    
    public string[] HotelLines;
    public int[] HotelProfs;
    int HotelInt = 0;
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
        ts.Interaction(HotelLines[HotelInt], HotelProfs[HotelInt]);
        HotelInt++;



    }

    // Update is called once per frame
    void Update()
    {
        if (HotelInt < HotelLines.Length && Keyboard.current.eKey.wasPressedThisFrame)
        {
            ts.Interaction(HotelLines[HotelInt], HotelProfs[HotelInt]);
            HotelInt++;
        } else if (HotelInt >= HotelLines.Length)
        {
            ts.off();
        }
        if (NextDaisy && LeaveScene)
        {
            Debug.Log("Next Scene");
        }
        if (NicksCarHere && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (nickCar < NickCarDialogue.Length - 1)
            {
                ts.Interaction(NickCarDialogue[nickCar], NickCarProf[nickCar]);
                nickCar++;
            } else
            {
                if (!nickLeft)
                {
                    nickLeave = true;
                    nickLeft = true;
                }
                LeaveScene = true;
                ts.off();
            }
        }
        if (LookingAtTom && ReadyForTom && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (currentTom >= TomLines.Length)
            {
                TomDead = true;
                ts.off();
            } else
            {
                ts.Interaction(TomLines[currentTom], TomProfs[currentTom]);
                currentTom++;
            }
        }
        if (PhoneDone && lookingAtTable && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (secondWaveLines == 5)
            {
                MeyersDepart = true;
            }
            if (secondWaveLines < SecondWaveLines.Length)
            {
                ts.Interaction(SecondWaveLines[secondWaveLines], SecondWaveProfs[secondWaveLines]);
                secondWaveLines++;
            } else
            {
                NickRun = true;
                ts.off();
            }
        }
        if (SecondPhone && Keyboard.current.eKey.wasPressedThisFrame && SecondWave)
        {
            if (currentLinePhone < PhoneLines.Length)
            {
                ts.Interaction(PhoneLines[currentLinePhone], TalkingTo[currentLinePhone]);
                currentLinePhone++;
            } else
            {
                PhoneDone = true;
                ts.off();
            }
        }
        if (lookingAtTable && Keyboard.current.eKey.wasPressedThisFrame && DoneWithMeyer && !NickRun)
        {
            tableSeated = true;
        } else
        {
            tableSeated = false;
        }
        if (talkingToWaiter && Keyboard.current.eKey.wasPressedThisFrame && DoneWithMeyer)
        {
            ts.Interaction("Hello, please seat yourself anywhere you wish", 3);

        }
        if (lookingAtTable && Keyboard.current.eKey.wasPressedThisFrame && DoneWithMeyer && !PhoneDone)
        {
            if (currentLine < waiterLines.Length)
            {
                ts.Interaction(waiterLines[currentLine], WaiterProfs[currentLine]);
                currentLine++;
            } else
            {
                ts.off();
                SecondWave = true;
                
                tableSeated = false;
            }
        }



        if (wait)
        {
            timer += Time.deltaTime;
            if ( timer > 4)
            {
                SceneManager.LoadScene(6);

            }
        }
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
        if (SpeakingWithLoser && Keyboard.current.eKey.wasPressedThisFrame && ob.doneWithBand && ob.DoneTalkingAtTable)
        {
            if (CurrentLine >= 2 && !obupalreadydone)
            {
                obupalreadydone = true;
                SpeakingWithLoser = false;
                ts.off();

                ob.Up = true;
            }
            else if (CurrentLine < 2) 
            {
                int prof;
                if (CurrentLine == 1)
                {
                    prof = 4;
                }
                else
                {
                    prof = 2;
                }
                if (currentLines < LosersFinalLines.Length)
                {
                    ts.Interaction(LosersFinalLines[CurrentLine], prof);

                }
                CurrentLine++;
            }

        }
        if (SpeakingToBand && Keyboard.current.eKey.wasPressedThisFrame && ob.DoneTalkingAtTable)
        {
            if (Lines < 2)
            {
                print("Interaction");
                ts.Interaction(BandLines[Lines], Lines + 2);
                Lines++;

            } else
            {
                ts.off();
                ob.TalkingToBand = true;
                SpeakingToBand = false;
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
            if (SceneManager.GetActiveScene().buildIndex == 8 && !DoneWithMeyer)
            {
                if (CurrentLine >= MeyerNYCLines.Length - 1)
                {
                    ts.off();
                    DoneWithMeyer = true;
                }
                else
                {

                    ts.Interaction(MeyerNYCLines[CurrentLine], MeyerNYCProfiles[CurrentLine]);
                    CurrentLine++;

                }
            }
            else if(SceneManager.GetActiveScene().buildIndex != 8)
            {
                if (CurrentLine >= MeyerLines.Length - 1)
                {
                    ts.off();
                    ob.newObj = true;
                }
                else
                {
                    if (currentProf == 2)
                    {
                        currentProf--;

                    }
                    else if (currentProf == 1)
                    {
                        currentProf++;
                    }
                    ts.Interaction(MeyerLines[CurrentLine], currentProf);
                    CurrentLine++;

                }
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
        if (startTalking && Keyboard.current.eKey.wasPressedThisFrame && !ob.DoneTalkingAtTable)
        {
            if (currentLineForNick <= LinesForParty.Length - 1)
            {
                ts.Interaction(LinesForParty[currentLineForNick], currentPersonSpeaking[currentLineForNick]);
                currentLineForNick++;
            } else
            {
                ts.off();
                ob.DoneTalkingAtTable = true;
            }

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
        else if (other.gameObject.CompareTag("Band"))
        {
            SpeakingToBand = true;
        }
        else if (other.gameObject.CompareTag("LoserTres"))
        {
            SpeakingWithLoser = true;
        } else if (other.gameObject.CompareTag("Next Scene") && ob.GoUpstairs)
        {
            startThis = true;
        }
        if (other.gameObject.CompareTag("Nick's table"))
        {
            startTalking = true;

        } else if (other.gameObject.CompareTag("Waiter"))
        {
            talkingToWaiter = true;
        } else if (other.gameObject.CompareTag("TableNYC"))
        {
            if (!endThisThing)
            {
                chosentable = other.gameObject;

                lookingAtTable = true;
            }

            
        } else if (other.gameObject.CompareTag("Phone2"))
        {
            SecondPhone = true;
        } else if (other.gameObject.CompareTag("TomBuch"))
        {
            LookingAtTom = true;
        } else if (other.gameObject.CompareTag("YellowCar"))
        {
            if (TomDead)
            {
                SceneManager.LoadScene(9);
            }
        } else if(other.gameObject.CompareTag("NicksCarryCar"))
        {
            NicksCarHere = true;
        } if (other.gameObject.CompareTag("NextSceneDaisy"))
        {
            NextDaisy = true;
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
        } else if (other.gameObject.CompareTag("Band"))
        {
            SpeakingToBand = false;
        }
        else if (other.gameObject.CompareTag("LoserTres"))
        {
            SpeakingWithLoser = false;
        } else if (other.gameObject.CompareTag("TableNYC"))
        {
            lookingAtTable = false;
        } else if (other.gameObject.CompareTag("Waiter"))
        {
            ts.off();
            talkingToWaiter = false;
        } else if (other.gameObject.CompareTag("Phone2"))
        {
            SecondPhone = false;
        }


    }
    
    public IEnumerator ChangeColor(bool first)
    {
        if (Scene5)
        {
            StartCoroutine(ob.MoveAway(ob.Upstairs));
        }
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
        } else if (!Scene5)
        {
            Scene4 = true;
        } else if (Scene5)
        {
            wait = true;

        }

    }
}
