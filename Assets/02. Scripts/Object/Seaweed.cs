using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seaweed : MonoBehaviour
{
    GameObject player;
    public Transform Seatrap;  // 묶이는 지점
    public Transform SeaEscape;  // 탈출 지점

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Inseon");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inseon"))
        {
            Inseon.isTrapped = true;
            player.GetComponent<Inseon>().trap = Seatrap;
            player.GetComponent<Inseon>().Escape = SeaEscape;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Inseon"))
        {
            Inseon.isTrapped = false;
            
        }
    }
}

