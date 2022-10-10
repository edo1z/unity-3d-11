using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float shoot_interval_sec = 0.25f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;

    private GameObject _cam;
    private float prev_shoot_time;

    private void Awake()
    {
        _cam = GameObject.Find("Main Camera");
    }

    public void Shoot()
    {
        if (Time.time >= prev_shoot_time + shoot_interval_sec)
        {
            RaycastHit hit;
            muzzleFlash.Play();
            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, range))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null) enemy.TakeDamage(damage);

                GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(effect, 2f);
            }
            prev_shoot_time = Time.time;
        }
    }
}
