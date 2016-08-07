using UnityEngine;
using System.Collections;

public class Pistol : MonoBehaviour
{
    public LayerMask BoundsMask;
    public float EnergyCost;
    public int MouseButton;
    public float Cooldown = 1f;
    public GameObject ProjectilePrefab;
    public AudioClip ProjectileSpawnSound;

    private float _cooldown;

    void FixedUpdate()
    {
        _cooldown -= Time.fixedDeltaTime;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float lenght;
        plane.Raycast(ray, out lenght);
        Vector3 mouseWorldPos = ray.origin + ray.direction * lenght;


        Vector3 playerToTargetVector = mouseWorldPos - transform.position;
        var playerToTargetRay = new Ray(transform.position, playerToTargetVector.normalized);

        RaycastHit hit;
        if (Physics.Raycast(playerToTargetRay, out hit, playerToTargetVector.magnitude, BoundsMask))
        {
            mouseWorldPos = playerToTargetRay.origin + playerToTargetRay.direction * hit.distance * 0.8f;
        }

        bool shoot = Input.GetMouseButton(MouseButton);
        if (shoot && _cooldown <= 0)
        {
            SpawnShot(transform.position + Vector3.up * 1f, mouseWorldPos);
            if (ProjectileSpawnSound != null)
            {
                AudioSource.PlayClipAtPoint(ProjectileSpawnSound, transform.position);
            }
        }
    }

    void SpawnShot(Vector3 position, Vector3 targetPosition)
    {
        var instance = Instantiate(ProjectilePrefab) as GameObject;
        var shot = instance.GetComponent<IShot>();
        shot.Spawn(position, targetPosition);
        _cooldown = Cooldown;
    }    
}
