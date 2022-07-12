using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TextScene;

public class TextSceneIconController : MonoBehaviour
{
    public CurrentActor thisActor;
    public Animator animator;
    public SpriteRenderer renderer;

    // Start is called before the first frame update
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(thisActor == CurrentActor.Rapier)
        {
            //Animation logic
            if (TextScene.currentActor == CurrentActor.Rapier && TextScene.currentActorState == CurrentActorState.Talking)
            {
                animator.enabled = true;
            }
            else
            {
                animator.playbackTime = 0f;
                animator.enabled = false;
            }

            //Actor logic
            if (TextScene.currentActor == CurrentActor.Rapier)
                renderer.enabled = true;
            else
                renderer.enabled = false;
        }
        else if (thisActor == CurrentActor.Witch)
        {
            //Actor logic
            if (TextScene.currentActor == CurrentActor.Witch)
                renderer.enabled = true;
            else
                renderer.enabled = false;
        }
    }
}
