using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapier : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public AudioSource switchMode;

    static public Rapier refToCurrentRapier;

    private bool IsNearEdge;
    private int NearEdgeLastPx;
    private const int NearEdgeThresholdPx = 10;

    static public Vector3 RapierVelocity; // => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    private Vector3 oldRapPos;
    private Vector3 newPos => this.gameObject.transform.position;

    private int minXpos => Screen.width / 2;
    private int maxXpos => Screen.width;
    private int minYpos => 0;
    private int maxYpos => Screen.height - 16;

    private Vector3 mPosOld = new Vector3();
    private TouchPhase oldTouchPhase;

    private bool firstCycle;

    // Start is called before the first frame update
    private void Start()
    {
        refToCurrentRapier = gameObject.GetComponent<Rapier>();

        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        sprite = GetComponent<SpriteRenderer>();

        switchMode = GetComponents<AudioSource>()[0];

        if (Input.touchCount > 0)
        {
            mPosOld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            oldTouchPhase = Input.GetTouch(0).phase;
        }

        this.gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(minXpos + NearEdgeThresholdPx, 0, 0)).x, 0, this.gameObject.transform.position.z);
        firstCycle = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))// || (TouchUtils.IsUniqueDoubleTap())) ----> Not as convenient
            RotateRapier();

        UpdateRapierTransparency();
    }

    private void UpdateRapierTransparency()
    {
        var alpha = 0.1f + (1f * (float)((float)NearEdgeLastPx / (float)NearEdgeThresholdPx));
        if (IsNearEdge)
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        else
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
    }

    public void RotateRapier()
    {
        this.gameObject.transform.Rotate(0, 0, 90);
        switchMode.Play();
    }

    // High-perfomance Update
    private void FixedUpdate()
    {
        if (Input.mousePresent)
            UpdateRapierPosByMouse();
        else if (Input.touchSupported)
            UpdateRapierPosByTouch();

        //Resets rapier position after first update cycle, because it's prone to flying offscreen due to accident extreme touch deltas
        if (firstCycle)
        {
            firstCycle = false;
            this.gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint( new Vector3(minXpos + NearEdgeThresholdPx,0,0)).x, 0, this.gameObject.transform.position.z);
        }
    }

    /// <summary>
    /// Uses touch logic to update by delta pos on screen (doesn't matter where finger is + cut out finger "teleports")
    /// </summary>
    private void UpdateRapierPosByTouch()
    {
        var touch = Input.GetTouch(0);
        bool isSuddenTouch = touch.phase == TouchPhase.Began;

        //Don't update deltas on sudden touches
        if (!isSuddenTouch)
        {
            //Old rapier in-game position
            this.oldRapPos = this.gameObject.transform.position;

            //Get mouse (touch) position delta
            var mPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            var mPosDelta = mPos - mPosOld;

            //Not needed 99% of the cases, but sometimes user can graze the finger or use two, and TouchPhase won't be ended, but finger will be "teleported"
            if (Mathf.Abs(mPosDelta.x) >= 0.3f || Mathf.Abs(mPosDelta.y) >= 0.3f)
                mPosDelta = new Vector3(0, 0, 0);

            //Add position delta to rapier
            this.gameObject.transform.position += mPosDelta;
        }

        //Limit the pos so that it doesn't come into witch field, but have to convert it from world to screen units and back first (for ease)
        var rapPos = this.gameObject.transform.position;
        rapPos = Camera.main.WorldToScreenPoint(rapPos);
        rapPos.x = Mathf.Clamp(rapPos.x, minXpos, maxXpos);
        rapPos.y = Mathf.Clamp(rapPos.y, minYpos, maxYpos);
        rapPos = Camera.main.ScreenToWorldPoint(rapPos);

        //Update actual position
        this.gameObject.transform.position = rapPos;

        //Update velocity
        RapierVelocity = newPos - oldRapPos;

        //See if rapier is near the edge. Needed for a visual effect
        IsNearEdge = Mathf.Abs(Camera.main.WorldToScreenPoint(rapPos).x - minXpos) < NearEdgeThresholdPx;
        NearEdgeLastPx = (int)Mathf.Abs(Camera.main.WorldToScreenPoint(rapPos).x - minXpos);

        //Update old mouse position
        mPosOld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        oldTouchPhase = Input.GetTouch(0).phase;
    }

    /// <summary>
    /// Use "snap rapier to mouse" rapier position update logic
    /// </summary>
    private void UpdateRapierPosByMouse()
    {
        //Old rapier in-game position
        this.oldRapPos = this.gameObject.transform.position;

        //Get new mouse position & limit it, so that it doesn't come into the witch territory
        var mPos = Input.mousePosition;
        mPos.x = Mathf.Clamp(mPos.x, minXpos, maxXpos);
        mPos.y = Mathf.Clamp(mPos.y, minYpos, maxYpos);

        //Adapt mouse position to world position
        var camPos = Camera.main.ScreenToWorldPoint(new Vector3(mPos.x, mPos.y, Camera.main.nearClipPlane));

        //Update rapier position
        this.gameObject.transform.position = new Vector3(camPos.x, camPos.y, this.gameObject.transform.position.z);

        //See if rapier is near the edge. Needed for a visual effect
        IsNearEdge = Mathf.Abs(mPos.x - minXpos) < NearEdgeThresholdPx;
        NearEdgeLastPx = (int)Mathf.Abs(mPos.x - minXpos);

        //Update velocity
        RapierVelocity = newPos - oldRapPos;
    }
}
