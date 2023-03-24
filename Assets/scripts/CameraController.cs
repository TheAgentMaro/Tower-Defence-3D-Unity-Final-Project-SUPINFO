using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorder = 0.05f * Screen.height;
    public float scrollspd = 5f;
    public float miny = 10f;
    public float maxy = 80f;

    
    void Update()
    {
        if(GameManager.end)
        {
            this.enabled = false;
            return;
        }
        if(Input.GetKey(KeyCode.Z)||Input.mousePosition.y >= Screen.height-panBorder)// HAUT 
        {
            transform.Translate(Vector3.forward * panSpeed*Time.deltaTime,Space.World);
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y < panBorder)//BAS
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <  panBorder)//GAUCHE
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x > Screen.width - panBorder)//DROITE
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll*1000 * scrollspd * Time.deltaTime;
        pos.y=Mathf.Clamp(pos.y, miny, maxy);
        transform.position = pos;
    }
}
