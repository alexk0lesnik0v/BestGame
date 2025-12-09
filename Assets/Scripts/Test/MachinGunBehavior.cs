using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MachinGunBehavior : MonoBehaviour
{
    public float damage = 21f;

    public float fireRate = 10f;

    public float force = 155f;

    public float range = 100f;

    public ParticleSystem muzzleFlash;

    public Transform bulletSpawn;

    public AudioClip shotSFX;

    public AudioSource _audioSource;

    public GameObject hitEffect;

    public Camera _camera;

    private float nextFire = 0f;

    public bool _isGet = false;


    private void Update()
    {

        if (this.name == "MachineGun(Clone)")
        {
            _isGet = true;



        }

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {

            if (_isGet)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

      
    }



    void Shoot()
    {

        // Instantiate(muzzleFlash, bulletSpawn.position, bulletSpawn.rotation);


        _audioSource.PlayOneShot(shotSFX);

        muzzleFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range))
        {
            Debug.Log("I got you!!!" + hit.collider);

            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 0.1f);


            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {



            Destroy(this.transform.gameObject);

            Debug.Log("Now you have a GUN!!!");


        }
    }

   
}
