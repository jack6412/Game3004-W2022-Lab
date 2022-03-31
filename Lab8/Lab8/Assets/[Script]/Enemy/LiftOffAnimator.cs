using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LiftOffAnimator : MonoBehaviour
{
    private Animator animator;
    private AIController AIc;
    private Transform player;

    void Start()
    {
        animator = GetComponent<Animator>();
        AIc = transform.parent.GetComponent<AIController>();
        player = AIc.Player;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);

        if (Vector3.Distance(player.position, transform.position) < 2.1f)
            animator.SetInteger("Animationchose", 2);//punch
        else
            animator.SetInteger("Animationchose", 1);//walk
    }
}
