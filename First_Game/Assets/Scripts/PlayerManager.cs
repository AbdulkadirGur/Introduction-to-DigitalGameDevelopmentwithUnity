using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float health , bulletSpeed;

    bool dead = false;

    public Slider slider; 

    public Transform muzzle;

    public Transform bullet, floatingtext , bloodParticle;
    public GameObject EndUI;
     

    bool mouseIsNotOverUI;

    void Start()
    {
        muzzle = transform.GetChild(1); 
        slider.maxValue = health;
        slider.value = health;
        
    }


    void Update()
    {
        mouseIsNotOverUI = EventSystem.current.currentSelectedGameObject == null;

        if (Input.GetMouseButtonDown(0) &&  mouseIsNotOverUI)
        {
            ShootBullet();  
        }
    }


    public void GetDamage(float damage)   // Hasar alma 
    {
        Instantiate(floatingtext,transform.position , Quaternion.identity).GetComponent<TextMesh>().text = damage.ToString();

        if ((health - damage) >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        slider.value = health;
        AmIDead();
    }

    public void AmIDead()  // Olu mu cank� m� 
    {
        if(health <= 0)
        {
            
            Destroy(Instantiate(bloodParticle, transform.position, Quaternion.identity), 3f); // kan efekkti 
            
            //DataManager.Instance.LoseProcess(); 
            dead = true;

            Destroy(gameObject);
            EndUI.SetActive(true);
            
        }
    }

    public void ShootBullet()
    {
        Transform tempBullet;
        tempBullet = Instantiate(bullet , muzzle.position , Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
        //DataManager.Instance.ShotBullet++;
    }
}
