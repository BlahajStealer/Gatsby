using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TextSystem : MonoBehaviour
{
    public Texture[] Characters;
    public TextMeshProUGUI textBox;
    public RawImage Talker;
    public GameObject txt;
    public GameObject ch;
    public GameObject pnl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interaction(string text, int character)
    {
        textBox.text = text;
        Talker.texture = Characters[character];
        txt.SetActive(true);
        ch.SetActive(true);
        pnl.SetActive(true);
    }
    public void off()
    {
        txt.SetActive(false);
        ch.SetActive(false);
        pnl.SetActive(false);
    }
}
