using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButtonRotateSword : MonoBehaviour
{
    public SpriteRenderer rend;

    public Sprite normal;
    public Sprite pressed;

    private const float maxTouchDelay = 0.1f;
    private float currentTouchDelay = 0f;

    public bool pressable => currentTouchDelay <= 0f;

    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        currentTouchDelay = 0f;

        if (Input.mousePresent)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentTouchDelay > 0f)
        {
            currentTouchDelay -= Time.deltaTime;
            if (currentTouchDelay <= 0f)
                rend.sprite = normal;
        }
    }

    private void OnMouseOver()
    {
        //Currently only can process first touch on device
        if (!pressable)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            PressRapierButton();
        }
    }

    /// <summary>
    /// Currently isn't used, because I didn't yet had the chance to debug multi-touch. Should be placed in Update()
    /// </summary>
    private void CheckMultitouchAnyFingerButtonPress()
    {
        if (!pressable)
            return;

        var buttonCollider = gameObject.GetComponent<Collider2D>();
        foreach (var touch in Input.touches)
        {
            var touchInBox = buttonCollider.bounds.Contains(Camera.main.ScreenToWorldPoint(touch.position));
            var touchJustPressed = touch.phase == TouchPhase.Began;
            if (touchInBox && touchJustPressed)
                PressRapierButton();
        }
    }

    private void PressRapierButton()
    {
        currentTouchDelay = maxTouchDelay;
        rend.sprite = pressed;
        Rapier.refToCurrentRapier.RotateRapier();
    }
}
