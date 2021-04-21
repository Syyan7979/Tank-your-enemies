using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrack : MonoBehaviour
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
    }
}
