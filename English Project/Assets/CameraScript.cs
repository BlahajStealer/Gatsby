using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    public Vector3 TopLeft;
    public Vector3 BottomRight;
    // 8.67 10.3
    //-1.23f -26.86


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
        if (PositionToBe.x >= TopLeft.x && PositionToBe.x <= BottomRight.x)
        {
            PositionToBe.x = Player.transform.position.x;
        } else
        {
            PositionToBe.x = transform.position.x;
        }
        if (PositionToBe.y <= TopLeft.y && PositionToBe.y >= BottomRight.y)
        {
            PositionToBe.y = Player.transform.position.y;
        } else
        {
            PositionToBe.y = transform.position.y;

        }
        transform.position = PositionToBe;
    }
}
