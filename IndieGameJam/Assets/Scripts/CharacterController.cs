using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    //proyectil
    public GameObject prefab;
    public Transform shootPoint;
    public float bulletForce;

    //rotacion del cañon
    private float angle;

    void Update()
    {
        //determino la posicion del cursor
        Vector3 mouse = Input.mousePosition;
        //Vector3 cannon = Camera.main.WorldToScreenPoint(this.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(mouse);


        //el cañon rota segun la posicion del cursor
        angle = Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(-15, -angle, 0));

        //disparar
        if (Input.GetButtonDown("Fire1"))
        {
            //instancio el proyectil
            GameObject bullet = Instantiate(prefab, shootPoint.position, Quaternion.identity);
            //disparo el proyectil en la direccion del cañon
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.AddForce(shootPoint.forward * bulletForce);
        }
    }
}