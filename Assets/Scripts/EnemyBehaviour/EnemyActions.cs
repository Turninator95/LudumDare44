using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyActions : ScriptableObject
{
    public abstract void Execute(Enemy enemy);
}
