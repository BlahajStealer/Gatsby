using UnityEngine;
using UnityEngine.Video;
public class VideoEnder : MonoBehaviour
{
    VideoPlayer vp;
    public GameObject Killit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        vp.loopPointReached += EndReached;
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
}
