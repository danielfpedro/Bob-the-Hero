using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableToggle : MonoBehaviour {

    public TouchTrigger trigger;
    public bool invert = false;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update () {
        bool flag = trigger.isOn;

        if (invert)
            flag = !flag;

        boxCollider.enabled = flag;
        spriteRenderer.enabled = flag;
    }
}
