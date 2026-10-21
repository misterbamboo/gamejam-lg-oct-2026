using UnityEngine;
public class ThreeDView : MonoBehaviour
{
    public void Update()
    {
        
        while(Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.D))
                transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime, Space.Self);
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime, Space.Self);
            if (Input.GetKey(KeyCode.W))
                transform.Rotate(new Vector3(1, 0,0) * Time.deltaTime, Space.Self);
            if (Input.GetKey(KeyCode.S))
                transform.Rotate(new Vector3(-1, 0, 0) * Time.deltaTime, Space.Self);
            if (Input.GetKey(KeyCode.Q))
                transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime, Space.Self);
            if (Input.GetKey(KeyCode.E))
                transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime, Space.Self);
        }
    }
}