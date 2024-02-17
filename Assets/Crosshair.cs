using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    bool seguirMouse = true;
    [SerializeField]
    bool mostrarCursor = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Exibir cursor do computador
        if(mostrarCursor)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }


    }


    public void CrosshairSeguirMouse()
    {
        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

    }
}
