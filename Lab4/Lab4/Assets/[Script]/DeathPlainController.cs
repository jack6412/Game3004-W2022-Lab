using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlainController : MonoBehaviour
{

    public Transform SpawnPoint;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject;
            var characterController = player.GetComponent<CharacterController>();

            characterController.SimpleMove(Vector3.zero);
            
            characterController.enabled = false;
            player.transform.position = SpawnPoint.transform.position;
            characterController.enabled = true;
        }
    }
}
