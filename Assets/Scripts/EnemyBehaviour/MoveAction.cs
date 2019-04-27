using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Actions/Move"), System.Serializable]
public class MoveAction : EnemyActions
{
    [SerializeField]
    private float moveDistance;
    private Vector3 startPosition;
    private bool startPositionSet = false;
    public override void Execute(Enemy enemy)
    {
        if (startPositionSet == false)
        {
            startPosition = enemy.transform.position;
            startPositionSet = true;
        }
        if (Vector3.Distance(startPosition, enemy.transform.position) < moveDistance)
        {
            enemy.transform.position += enemy.transform.forward * Time.deltaTime * enemy.MovementSpeed;
        }
        else
        {
            startPositionSet = false;
            enemy.ActionCompleted();
        }
    }
}
