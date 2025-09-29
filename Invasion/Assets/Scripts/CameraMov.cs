using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public float smoothSpeed = 5.0f;
    private Vector3 CameraDefaultPos;
    private Vector3 NewCameraPos;

    void Awake()
    {
        CameraDefaultPos = transform.position;
        NewCameraPos = CameraDefaultPos;
        Debug.Log("Default pos " + CameraDefaultPos.ToString());
    }

    void Start()
    {

    }


    void Update()
    {
        if (hover_script.HoverSciptObj == null) { return; }
        NewCameraPos = hover_script.HoverSciptObj.WorldPosition;
        NewCameraPos.z = CameraDefaultPos.z;
        if (transform.position != NewCameraPos)
        {
            Debug.Log("NewPos " + NewCameraPos.ToString());
            transform.position = Vector3.Lerp(transform.position, NewCameraPos, smoothSpeed * Time.deltaTime);
        }

    }
}
