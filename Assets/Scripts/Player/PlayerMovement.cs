using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //---------------------------- Vesa lisäämät  +  [ Header ( " " ) ]     +     [ SerializeField ]    lisäykset------------------------------------------------------------------------------------------------------------
    // [SerializeField] float gh;
    [Header("Movement Direction -   Left -1  Right +1   Up +1   Down -1")]
    [SerializeField] private float vertical;
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    [SerializeField] private float horizontal; 
    [SerializeField] private bool isFacingRight = true;

    [Header("Movement Adjustment")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;


    [Header("Components")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;



    //------------------------------------------------------Tuija lisäykset---------------------------------------------------------------------------------------------------------------
    public bool IsFacingRight { get; set; } //Tuija
  
    private float _fallSpeedYDampingChangeThreshold; //Tuija


    private void Start()//Tuija 
    {
        _fallSpeedYDampingChangeThreshold = CameraManager.instance._fallSpeedYDampingChangeThreshold;
      
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Update()
    { //Pieni hyppy näpäyttämällä ja iso hyppy space pohjassa
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");   //Vesa lisäys

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
        //------------------------------------------------------Tuija lisäykset-------------------------------------------------------------------------------------------------------------------------------------
        //Tuija: if we are falling past a certain speed threshold
        if (rb.velocity.y < _fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        //Tuija: if we are standing still or moving up
        if (rb.velocity.y >= 0f && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        {
            //reset so it can be called again
            CameraManager.instance.LerpedFromPlayerFalling = false;

            CameraManager.instance.LerpYDamping(false);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    }

    private void FixedUpdate()
    {

        // Liikkuminen vasen ja oikea
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    #region DontTouch
    //------------------------------------------------------------------Vesa häsellystä---Grapler lisäystä-----------------------------------------------------------------------------------

    //clamp the player's fall speed in the Y (I set a super high upper limit to ensure we can have a fast jump speed if we want)
    //private void FixedUpdate()
    //{
    
    //rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -_maxFallSpeed, _maxFallSpeed * 5));

    //if (moveInput > 0 || moveInput < 0)
    //{
    //    TurnCheck();
    //}
    //--------------------------------------------------------------------

    //if (!gh.retracting)
    //{
    //    rb.velocity = new Vector2(horizontal, vecrtical).normalized * speed;
    //}
    //else
    //{
    //    rb.velocity = Vector2.zero;
    //}
    //-----------------------------------------------------------------------------------------------
    //}
    #endregion

    private bool IsGrounded() // Groundcheck
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private void Flip()    // Pelihahmon kääntyminen
    {                                               
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }

}


