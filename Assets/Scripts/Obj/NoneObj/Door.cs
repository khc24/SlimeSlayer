using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public BoxCollider2D coll2D;

    private void Start()
    {
         animator = GetComponentInChildren<Animator>(true);
         coll2D = GetComponent<BoxCollider2D>();
    }
}

