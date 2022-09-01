using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb; // fizik degerleri olusturmak icin
    public float move_Speed = 1f ; // x ekseninde hareket edebilmek icin 
    public float jumpSpeed = 1f ; // y keseninde harket edip edemedigimizi kontrol ediyoruz 
    bool facingRight = true;   // sag sola dondugumuzde yuzumuzun de donup donemdigi kontolu 
    public bool isGrounded  = false;  // yere temas edip etmedigimizi belirtiyor
    public float JumpFrequency = 1f; // ziplamama sikligi 
    public float nextJumpTime = 1f; // bir sonraki ziplama zamani 
    public Transform groundCheckPosition; // Yere degýp degmedýgýmýzý kontrol edencek daýrenýn pozýsyon býlgýsý degýskený
    public float groundcheckRadious;// daýrenýn capý ne kadar oolacak 
    public LayerMask groundCheckLayer;// game objesýnde yere degýp degmedýgýný kontrolunu yapacagimiz layer degiskeni 

    Animator playerAnimator; // Sprite 2d goruntu animasyonu 
    private void Awake() 
    {
        
    }
    void Start() // Oyun basltildiginda  degiskenlerin tanimlanmasi saglnair
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();    
    }

    
    void Update() // Fps kavrami her frame yenilenir fiziksel degiskenler buraya konulmaz 
    {
        HorizontalMove();
        onGroundCheck();

        if (playerRb.velocity.x<0 && facingRight)
        {
            FlipFace(); 
        }
        else if (playerRb.velocity.x>0 && !facingRight)
        {
            FlipFace();
        }


        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad)) // bir sonraki zipladisiyimdaki zamani oyun basladigindan berisdirki zamandakinden kiucukse

        {
            nextJumpTime = Time.timeSinceLevelLoad + JumpFrequency;  //   bir sonraki ziplayisimin zamani arti suanki zamanla toplayip esotliyoruz.
            Jump();
           
        }
    }
    private void FixedUpdate()// Fix yani sabit olan kavramlari kullaniyoruz.Fizik kavramlari ve deegerleri 
    {
        
    }
    void HorizontalMove ()
    {
        playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * move_Speed, playerRb.velocity.y); // hizla x ekseninde basilan harkeet yapiyor y ise default kaliyor.
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRb.velocity.x)); // x duzlemdeki hizla paramatereyi guncelleiyoruz.
    }

    void FlipFace()
    {
        facingRight = !facingRight; // toggle deniyor
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }


    void Jump()
    {
        playerRb.AddForce(new Vector2(0f, jumpSpeed));
    }


    void onGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundcheckRadious, groundCheckLayer);
        playerAnimator.SetBool("isGroundedAnim",isGrounded);
        
    }
}
