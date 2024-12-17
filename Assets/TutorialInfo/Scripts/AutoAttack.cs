using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public Transform target; // Cible de l'attaque
    public float attackRange = 5f; // Distance d'attaque
    public float attackSpeed = 1f;  // Vitesse de d�placement
    public float attackCooldown = 1f; // Temps entre les attaques
    public float moveInterval = 2f;  // Intervalle de d�placement (toutes les 2 secondes)
    private float timer = 0f;        // Timer pour contr�ler le d�lai
    public int damage = 10; // D�g�ts inflig�s
    private bool isAttacking = false;

    private float lastAttackTime;

    void Update()
    {
        timer += Time.deltaTime;

        if (target != null)
        {
            if (timer >= moveInterval)
            {
                isAttacking = true;
                timer = 0f;      // R�initialiser le timer
            }
        }

         // D�placer le cube vers la cible s'il attaque
        if (isAttacking)
        {
                MoveTowardsTarget();
        }

    }

    void MoveTowardsTarget()
    {
        // Calculer la distance actuelle � la cible
        float distance = Vector3.Distance(transform.position, target.position);

        // Si le cube est encore hors de port�e, il se d�place
        if (distance > attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, attackSpeed * Time.deltaTime);
            Debug.DrawLine(transform.position, target.position, Color.red);

        }
        else
        {
            // Si la cible est dans la port�e d'attaque, on arr�te de bouger et on attaque
            isAttacking = false;
            Attack();
        }
    }


    void Attack()
    {
        if (target != null)
        {
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
        }
        // Impl�mentez les effets de l'attaque ici (ex. r�duire la sant� de la cible)
        Debug.Log($"{gameObject.name} attaque {target.name} et inflige {damage} points de d�g�ts !");
    }
}
