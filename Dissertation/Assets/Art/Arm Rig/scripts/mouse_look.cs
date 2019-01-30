using UnityEngine;
using System.Collections;

public class mouse_look : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0F;
    public float smoothing = 2.0F;

    GameObject character;

    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1F / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1F / smoothing);
        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}