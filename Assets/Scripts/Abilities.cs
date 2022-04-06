using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : PlayerMovement
{
    private CharacterController controller;
    
    // === ABILITIES === //
    private bool canDash = true;
    // dash
    private float dashDistance = 7f;
    private float dashCooldown = 2f;
    
    private bool canShoot = true;
    private bool isShooting = false;
    
    
    private Image dashCooldownCover;

    private Text debugText;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        dashCooldownCover = GameObject.Find("Dash BW").GetComponent<Image>();
        dashCooldownCover.fillAmount = 0f;
        
        debugText = GameObject.Find("Debug Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = "canDash: " + canDash;
        
        Dash();
    }


    void Dash()
    {
        if (canDash && Input.GetButton("Dash"))
        {
            canDash = false;
            dashCooldownCover.fillAmount = 1f;
            
            Debug.Log("Dash");
            controller.Move(transform.forward * dashDistance);

            StartCoroutine(DashCooldown());
        }
        else if (!canDash)
        {
            dashCooldownCover.fillAmount -= 1f / dashCooldown * Time.deltaTime;
            
            if (dashCooldownCover.fillAmount <= 0f)
            {
                canDash = true;
                dashCooldownCover.fillAmount = 0f;
            }
        }
    }
    
    
    // === COROUTINES ===
    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}