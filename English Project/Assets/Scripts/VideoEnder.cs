using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class VideoEnder : MonoBehaviour
{
    VideoPlayer vp;
    public GameObject Killit;
    public bool next;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        if (!next)
        {
            vp.loopPointReached += EndReached;

        } else
        {
            vp.loopPointReached += OnVideoEnd;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EndReached(VideoPlayer vp)
    {
        Destroy(gameObject);
        Destroy(Killit);
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(7);

    }

}
