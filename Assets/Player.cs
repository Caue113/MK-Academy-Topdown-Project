using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    Crosshair crosshair;

    [SerializeField]
    Personagem personagemControlado;

    Rigidbody2D rigidBodyPersonagem;

    public float velocidade = 1f;

    Vector2 direcaoHorizontal;
    Vector2 direcaoVertical;
    Vector2 direcaoAndar;

    void Start()
    {
        rigidBodyPersonagem = personagemControlado.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Movimentação Horizontal
        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            direcaoHorizontal = Vector2.zero;
        }
        else if(Input.GetAxisRaw("Horizontal") == 1)
        {
            direcaoHorizontal = Vector2.right;
        }
        else if(Input.GetAxisRaw("Horizontal") == -1)
        {
            direcaoHorizontal = Vector2.left;
        }
        
        //Movimentação Vertical
        if(Input.GetAxisRaw("Vertical") == 0)
        {
            direcaoVertical = Vector2.zero;
        }
        else if(Input.GetAxisRaw("Vertical") == 1)
        {
            direcaoVertical = Vector2.up;
        }
        else if(Input.GetAxisRaw("Vertical") == -1)
        {
            direcaoVertical = Vector2.down;
        }


        crosshair.CrosshairSeguirMouse();
        Mover();
    }


    private void Mover()
    {
        float distance = Vector2.Distance(direcaoHorizontal + direcaoVertical, Vector2.zero);
        Vector2 horizontalNormal;
        Vector2 verticalNormal;

        if (distance == 0f)
        {
            horizontalNormal = Vector2.zero;
            verticalNormal = Vector2.zero;
        }
        else
        {
            horizontalNormal = direcaoHorizontal / distance;
            verticalNormal = direcaoVertical / distance;
        }
        

        direcaoAndar = (horizontalNormal + verticalNormal) * velocidade;
        rigidBodyPersonagem.velocity = direcaoAndar;
    }
}
