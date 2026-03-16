using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    //8.67 -1.23, 10.3 -26.86
    // Update is called once per frame
    void Update()
    {
        Vector3 PositionToBe = Player.transform.position;
        PositionToBe.z = -10;
        if (PositionToBe.x >= 8.67f && PositionToBe.x <= 10.3f)
        {
            PositionToBe.x = Player.transform.position.x;
        } else
        {
            PositionToBe.x = transform.position.x;
        }
        if (PositionToBe.y <= -1.23f && PositionToBe.y >= -26.86)
        {
            PositionToBe.y = Player.transform.position.y;
        } else
        {
            PositionToBe.y = transform.position.y;

        }
        transform.position = PositionToBe;
    }
}
