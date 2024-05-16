using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offsetx, offsety, camspeed;
    private float travelling = 0;

    void Update()
    {
        //suit le joueur
        transform.position = new Vector3(player.position.x + travelling, player.position.y + offsety, transform.position.z);
        //Fait bouger la caméra dans la direction que le joueur regarde
        travelling = Mathf.Lerp(travelling, (offsetx * player.localScale.x), Time.deltaTime * camspeed);
    }
}
