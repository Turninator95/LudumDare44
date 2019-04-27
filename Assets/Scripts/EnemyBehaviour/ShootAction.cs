﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Actions/Shoot"), System.Serializable]
public class ShootAction : EnemyActions
{
    private Player player; 
    public override void Execute(Enemy enemy)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        RaycastHit raycastHit;

        if (Physics.Raycast(enemy.transform.position, player.transform.position - enemy.transform.position, out raycastHit))
        {
            if (raycastHit.collider.tag != "Player")
            {
                Debug.Log("Can't see the sheriff!");
            }
            else
            {
                Debug.Log("I'd like to shoot the sheriff!");

                enemy.Gun.transform.LookAt(player.transform);

                if (enemy.CurrentAmmo - enemy.EquippedGun.CostPerShot > 0)
                {
                    Instantiate(enemy.EquippedGun.Projectile, enemy.Gun.transform.GetChild(1).transform.position, enemy.Gun.transform.rotation).GetComponent<Bullet>().speed = enemy.EquippedGun.ProjectileSpeed;
                    enemy.CurrentAmmo -= enemy.EquippedGun.CostPerShot;
                    Debug.Log($"{enemy.name} has {enemy.CurrentAmmo} ammo left.");
                }

                if (enemy.Gun.transform.eulerAngles.y > 0 && enemy.Gun.transform.eulerAngles.y < 180)
                {
                    enemy.Gun.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    enemy.Gun.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
        enemy.ActionCompleted();
    }
}
