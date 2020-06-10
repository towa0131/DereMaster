using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class TitleKeyControl : KeyControl
{
    private bool displayPressed;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        displayPressed = false;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0) && !displayPressed)
        {
            displayPressed = true;
            PlaySounds sound = gameObject.GetComponent<PlaySounds>();
            sound.Play(0, "se_title_start");

            Animator animator = this.gameObject.GetComponentInParent<Animator>();
            animator.Play("TouchFlash");

            StartCoroutine(base.DelayMethod(0.75f, () =>
            {
                animator.Play("TitleFade", 1);
            }));
            StartCoroutine(base.DelayMethod(2.0f, () =>
            {
                DontDestroyOnLoad(GameObject.Find("Sounds"));
                SceneManager.LoadScene("Loading");
            }));

        }
    }
}
