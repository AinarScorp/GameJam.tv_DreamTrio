using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] float dropRadius = 10f;
    [SerializeField] ObjectToDrop[] objectsToDrop;

    public void DropStuffUponDeath()
    {
        float randomRoll = Random.Range(0f, 100f);
        foreach (var drop in objectsToDrop)
        {
            if (drop.dropChancePercent >= randomRoll)
            {
                Debug.Log(drop.dropObject.name);
                Instantiate(drop.dropObject, GetDropPosition(), Quaternion.identity);
                return;
            }
            randomRoll -= drop.dropChancePercent;
        }
        Debug.Log("drop nothing");

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
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, dropRadius);
    }
}
