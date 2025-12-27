using UnityEngine;
using Unity.Cinemachine;
using UnityEditor.Build;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    public InputReader input;
    public Transform attackPoint; // Point from which the attack is made

    [Header("Settings")]
    public float attackRange = 1.5f; // Radius of the attack area
    public LayerMask enemyLayers;
    public float freezeDuration = 0.15f; // Duration of hit stop in seconds

    [Header("Juice")]
    public GameObject hitEffectPrefab;
    private CinemachineImpulseSource _impulseSource; // Reference to the Cinemachine impulse source

    void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>(); // Get the impulse source component
    }

    private void Start()
    {
        input.AttackEvent += HandleAttack; // Subscribe to attack event
    }

    private void OnDestroy()
    {
        input.AttackEvent -= HandleAttack; // Unsubscribe from attack event
    }

    private void HandleAttack()
    {
        Debug.Log("Swing!"); // Placeholder for attack animation trigger
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        if (hitEnemies.Length > 0)
        {
            if (_impulseSource != null)
                _impulseSource.GenerateImpulseWithForce(0.5f);

            HitStop.Instance.Freeze(freezeDuration);

            foreach (Collider enemy in hitEnemies)
            {
                Vector3 hitPos = enemy.ClosestPoint(attackPoint.position);

                Instantiate(hitEffectPrefab, hitPos, Quaternion.identity);
            }
        }
    }
}
