using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private bool isDown;
    [SerializeField] private float speed;
    [SerializeField] private Transform up, down, player, elevatorSwitch;
    [SerializeField] private SpriteRenderer sprite;

    private void Update()
    {
        StartElevator();
        DisplayColor();
    }

    //Initialise la position de l'ascenseur et sa direction de mouvement
    void StartElevator()
    {
        if(Vector2.Distance(player.position,elevatorSwitch.position)<1f && Input.GetKeyDown(KeyCode.E))
        {
            if (transform.position.y <= down.position.y)
            {
                isDown = true;
            }
            else if (transform.position.y >= up.position.y)
            {
                isDown = false;
            }
        }

        if (isDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, up.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, down.position, speed * Time.deltaTime);
        }
    }

    // change la couleur de vert à rouge lorsque l'ascenseur est utilisé 
    void DisplayColor()
    {
        if(transform.position.y<=down.position.y || transform.position.y >= up.position.y)
            sprite.color = Color.green;
        else
            sprite.color = Color.red;
    }
}
