using UnityEngine;
using System.Collections;

public class Pistol : MonoBehaviour
{
    public float Cooldown = 1f;
    public GameObject ProjectilePrefab;

    private float _cooldown;

    void FixedUpdate()
    {
        _cooldown -= Time.fixedDeltaTime;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float lenght;
        plane.Raycast(ray, out lenght);
        Vector3 mouseWorldPos = ray.origin + ray.direction * lenght;
        Vector3 aimDirection = (mouseWorldPos - transform.position).normalized;

        bool shoot = Input.GetMouseButton(0);
        if (shoot && _cooldown <= 0)
        {
            SpawnShot(transform.position + Vector3.up * 1f, aimDirection);
        }
    }

    void SpawnShot(Vector3 position, Vector3 direction)
    {
        Instantiate(ProjectilePrefab, position, Quaternion.LookRotation(direction));
        _cooldown = Cooldown;
    }
}
