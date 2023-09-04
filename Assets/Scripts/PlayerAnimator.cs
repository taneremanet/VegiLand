using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator playerAC;

    void Start()
    {

    }


    void Update()
    {

    }

    public void ManageAnimations(Vector3 move)
    {
        if (move.magnitude > 0)
        {
            PlayRunAnimation();
            playerAC.transform.forward = move.normalized; // player turns or rotates with the direction
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    private void PlayRunAnimation()
    {
        playerAC.Play("Run");
    }

    private void PlayIdleAnimation()
    {
        playerAC.Play("Idle");
    }
}
