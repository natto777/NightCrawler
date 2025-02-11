using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fire;
    public int maxmana = 150;
    public int currentmana;
    public Manabar manabar;
    public int usage = 2;
    public int damage = 5;

    public float fireforce = 20f;
    public AudioClip shooteffect;
    public AudioSource audioSource;
    // Update is called once per frame

    void Start()
    {
        resetManaStat();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (useMana(usage)){ shoot(); }
        }
    }

    public void resetManaStat()
    {
        currentmana = maxmana;
        manabar.SetMaxMana(maxmana);
    }

    void shoot()
    {
        GameObject bullet = Instantiate(fire, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * fireforce, ForceMode2D.Impulse);
        audioSource.clip = shooteffect;
        audioSource.Play();
    }

    bool useMana(int usage)
    {
        bool flag = true;
        currentmana -= usage;
        if (currentmana < 0) { currentmana = 0; flag = false; }
        manabar.SetMana(currentmana);
        return flag;
    }

    public void increasemana(int mana)
    {
        currentmana += mana;
        currentmana = (currentmana >= maxmana)? maxmana: currentmana;
        manabar.SetMana(currentmana);
    }
}
