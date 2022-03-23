using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapier : MonoBehaviour
{
    public Rigidbody2D rb;
    public AudioSource switchMode;

    static public Vector3 RapierVelocity; // => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    private Vector3 oldPos;
    private Vector3 newPos => this.gameObject.transform.position;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        switchMode = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            this.gameObject.transform.Rotate(0, 0, 90);
            switchMode.Play();
        }
    }

    // High-perfomance Update
    private void FixedUpdate()
    {
        var mPos = Input.mousePosition;
        var camPos = Camera.main.ScreenToWorldPoint(new Vector3(mPos.x, mPos.y, Camera.main.nearClipPlane));

        this.oldPos = this.gameObject.transform.position;
        this.gameObject.transform.position = new Vector3(camPos.x, camPos.y, this.gameObject.transform.position.z);

        RapierVelocity = newPos - oldPos;
    }
}
