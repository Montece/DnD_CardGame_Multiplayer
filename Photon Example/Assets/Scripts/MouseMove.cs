using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float offset;

    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.rotation = Camera.main.transform.rotation;
        transform.LookAt(new Vector3(Input.mousePosition.x, -offset, Input.mousePosition.y));/*mouseRay.origin + mouseRay.direction * offset*/

        /*float mid = (transform.position - Camera.main.transform.position).magnitude * 0.5F;
        Vector3 dir = mouseRay.origin + mouseRay.direction * mid;
        transform.LookAt(new Vector3(dir.x, dir.y + 180, dir.z));*/
    }
}
