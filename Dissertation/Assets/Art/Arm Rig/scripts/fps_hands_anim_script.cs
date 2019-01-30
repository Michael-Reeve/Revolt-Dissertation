using UnityEngine;
using System.Collections;

public class fps_hands_anim_script : MonoBehaviour {

	private Animator anim;
    private float timer;
    private float length;
    private Animation idleBreak;

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
        idleBreak = GetComponent<Animation>();
        length = idleBreak.clip.length;
    }
    
    // Update is called once per frame
    void Update () {

        if (Input.anyKeyDown == false)      // Starts timer if you not press a buttons   

        {
            timer += Time.deltaTime;
        }

        if (timer > 5)      // If you don't press buttons after 5 seconds will play 'idle break' animation 

        {
            anim.SetBool("idle_break", true);
        }

        else

        {  
            anim.SetBool("idle_break", false);
        }

        if (timer > 5 + length || Input.anyKeyDown == true || Input.anyKey == true)     // If timer have value more then 5 seconds + lenght of 'idle break' animation or you press any button, timer will take value '0' 

        {
            timer = 0;
        }

        if (Input.GetButton("Fire1") & Input.GetButton("Vertical"))     // Your character is able to punch when he walks (play animations)

        {
            anim.SetBool("punch", true);
        }

        else

        {
            anim.SetBool("punch", false);
        }

        if (Input.GetButton("Fire1") & Input.GetButton("Fire2"))    // Your character is able to punch when he holds block (play animations)

        {
            anim.SetBool("punch", true);
        }

        else

        {
            anim.SetBool("punch", false);
        }

        if (Input.GetButton("Fire1"))       // Your character is able to punch (play animations)

        {
            anim.SetBool("punch", true);
        }

        else

        {
            anim.SetBool("punch", false);
        }

        if (Input.GetButton("Vertical") & Input.GetButton("Fire2"))     // Your character is able to walk when he holds block (play animations)

        {
            anim.SetBool("block", true);
        }

        else

        {
            anim.SetBool("block", false);
        }

        if (Input.GetButton("Fire2"))       // Your character is able to block (play animations)

        {
            anim.SetBool("block", true);
        }

        else

        {
            anim.SetBool("block", false);
        }

        if (Input.GetButton("Vertical") & Input.GetButtonDown("Jump"))      // Your character is able to jump forward or backward (play animations)

        { 
            anim.SetBool("jump", true);
        }

        else

        { 
            anim.SetBool("jump", false);
        }
   
        if (anim.GetBool("run") == true & Input.GetButtonDown("Jump"))      // Your character is able to running jump (play animations)

        {
            anim.SetBool("jump", true);
        }

        else

        {
            anim.SetBool("jump", false);
        }

        if (Input.GetKey(KeyCode.W) & Input.GetButton("Run"))      // Your character is able to run (play animations)

        {
            anim.SetBool("run", true);
        }

        else

        {
            anim.SetBool("run", false);
        }

        if (Input.GetButton("Vertical"))        // Your character is able to walk (play animations)

        {
            anim.SetBool("walk", true);
        }

        else

        {
            anim.SetBool("walk", false);
        }

        if (Input.GetButtonDown("Jump"))        // Your character is able to jump (play animations)

        {
            anim.SetBool("jump", true);
        }

        else

        {
            anim.SetBool("jump", false);
        }

   
    }
}
