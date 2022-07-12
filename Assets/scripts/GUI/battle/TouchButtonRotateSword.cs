using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NOTE: This used to be a button that works from collider.
/// But! It so happened that in touch devices, here in particular it's easier to simply check for zone, rather than work with collisions
/// (that don't account for multitouches as clearly)
/// So don't mind the collider, it doesn't do anything (anymore)
/// </summary>
public class TouchButtonRotateSword : MonoBehaviour
{
    public SpriteRenderer renderer;

    public Sprite normal;
    public Sprite pressed;

    private const float maxTouchDelay = 0.1f;
    private float currentTouchDelay = 0f;

    public bool pressable => currentTouchDelay <= 0f;

    // Start is called before the first frame update
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false; //Currently this works the same, but instead of button, there's half of screen
        currentTouchDelay = 0f;

        if (Input.mousePresent)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_ANDROID

        if (currentTouchDelay > 0f)
        {
            currentTouchDelay -= Time.deltaTime;
            if (currentTouchDelay <= 0f)
                ;//renderer.sprite = normal; //Currently this works the same, but instead of button, there's half of screen
        }

        //all touches that both X < ScreenWidth / 2 and TouchPhase.Began
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && TouchUtils.IsXLeftPart(touch.position.x))
                PressRapierButton();
        }
#endif
    }

    private void OnMouseOver()
    {
#if !UNITY_ANDROID
        return;
#endif

        // NOTE: This used to be a button that works from collider.
        // But! It so happened that in touch devices, here in particular it's easier to simply check for zone, rather than work with collisions
        // (that don't account for multitouches as clearly)
        // So don't mind the collider, it doesn't do anything (anymore)

        ////all touches that both <X/2 and TouchPhase.Began
        //foreach (var touch in Input.touches)
        //{
        //    if (touch.phase == TouchPhase.Began && TouchUtils.IsXLeftPart(touch.position.x))
        //        PressRapierButton();
        //}
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
        if (!pressable)
            return;

        currentTouchDelay = maxTouchDelay;
        ;//renderer.sprite = pressed; //Currently this works the same, but instead of button, there's half of screen
        Rapier.refToCurrentRapier.RotateRapier();
    }
}
