using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class EnemyManager : SingletonGameObject<EnemyManager>
    {
        [SerializeField] private List<Enemy> _enemies;

        private void Start()
        {
            var enemyList = FindObjectsOfType<Enemy>();
            _enemies.AddRange(enemyList);
        }

        public void Killed(Enemy killed)
        {
            _enemies.Remove(killed);
        }
    }
}