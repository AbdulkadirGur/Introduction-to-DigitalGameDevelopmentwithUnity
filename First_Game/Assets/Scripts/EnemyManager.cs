using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyManager : MonoBehaviour
{
    public float health;
    public float damage;

    bool colliderBusy = false;
   
    public Slider slider;
    void Start()
    {
        slider.maxValue= health;
        slider.value = health;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) // icine giriyor
    {
        if (other.tag == "Player" && !colliderBusy)
        {
            colliderBusy = true;
            other.GetComponent<PlayerManager>().GetDamage(damage);  // olmayan bir objenn kcollideri deyinc null hatsi verir.
        }
        else if(other.tag == "Bullet")
        {
            GetDamage(other.GetComponent<BulletManager>().bulletDamage);
            Destroy(other.gameObject);
        }
        
    }
  

    private void OnTriggerExit2D(Collider2D other)// icine giriyor cikiyor
    {
        if(other.tag == "Player")
        {
            colliderBusy=false;
        }
    }


    public void GetDamage(float damage)   // Hasar alma 
    {
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

    public void AmIDead()  // Olu mu canlý mý 
    {
        if (health <= 0)
        {
            DataManager.Instance.EnemyKilled++;
            Destroy(gameObject);
        }
    }
}
