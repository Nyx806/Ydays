using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public Transform target; // Cible de l'attaque
    public float attackRange = 5f; // Distance d'attaque
    public float attackSpeed = 1f;  // Vitesse de déplacement
    public float attackCooldown = 1f; // Temps entre les attaques
    public float moveInterval = 2f;  // Intervalle de déplacement (toutes les 2 secondes)
    private float timer = 0f;        // Timer pour contrôler le délai
    public int damage = 10; // Dégâts infligés
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
                timer = 0f;      // Réinitialiser le timer
            }
        }

         // Déplacer le cube vers la cible s'il attaque
        if (isAttacking)
        {
                MoveTowardsTarget();
        }

    }

    void MoveTowardsTarget()
    {
        // Calculer la distance actuelle à la cible
        float distance = Vector3.Distance(transform.position, target.position);

        // Si le cube est encore hors de portée, il se déplace
        if (distance > attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, attackSpeed * Time.deltaTime);
            Debug.DrawLine(transform.position, target.position, Color.red);

        }
        else
        {
            // Si la cible est dans la portée d'attaque, on arrête de bouger et on attaque
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
        // Implémentez les effets de l'attaque ici (ex. réduire la santé de la cible)
        Debug.Log($"{gameObject.name} attaque {target.name} et inflige {damage} points de dégâts !");
    }
}
