using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] bool drawGizmos;
    [SerializeField] float dropRadius = 10f;
    [SerializeField] int numberOfDrops = 1;
    [SerializeField] ObjectToDrop[] objectsToDrop;

    public void DropStuffUponDeath()
    {
        for (int i = 0; i < numberOfDrops; i++)
        {
            float randomRoll = Random.Range(0f, 100f);
            foreach (var drop in objectsToDrop)
            {
                if (drop.dropChancePercent >= randomRoll)
                {
                    Instantiate(drop.DropObject, GetDropPosition(), Quaternion.identity);
                    break;
                }
                randomRoll -= drop.dropChancePercent;
            }
        }

    }

    Vector3 GetDropPosition()
    {
        return transform.position + GetRandomDirection() * dropRadius;
    }

    Vector3 GetRandomDirection()
    {
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        return new Vector3(randX, randY).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos)
        {
            return;
        }
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, dropRadius);
    }
}
