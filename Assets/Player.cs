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

    Vector2 direcaoMirar;


    public GameObject projetil;

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


        //Atirar
        if(Input.GetMouseButtonDown(0))
        {
            Atirar();
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

    private void Atirar() 
    {
        GameObject instance = Instantiate(projetil, personagemControlado.transform.position, Quaternion.identity);
        Rigidbody2D projetilRb = instance.GetComponent<Rigidbody2D>();

        Vector2 delta = new Vector2(crosshair.transform.position.x - personagemControlado.transform.position.x, crosshair.transform.position.y - personagemControlado.transform.position.y);

        //Cálculo prático
        float aimDist2 = Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y);
        Vector2 norm = new Vector2(delta.x / aimDist2, delta.y / aimDist2);

        //A maneira simples
        //float distanciaMira = Vector2.Distance(personagemControlado.transform.position, crosshair.transform.position);
        //Vector2 norm2 = new Vector2(delta.x / distanciaMira, delta.y / distanciaMira);

        //O mais simples
        //Vector2 normal = delta.normalized;

        Debug.DrawLine(personagemControlado.transform.position, crosshair.transform.position, Color.green, 2f);
        Debug.DrawRay(personagemControlado.transform.position, norm, Color.cyan, 2f);
        //Debug.Log("Dist: " + distanciaMira + " Normal:" + norm);

        projetilRb.velocity = norm;
    }
}
