using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            CheckWin();
        }

        private void CheckWin()
        {
            if (_enemies.Count == 0)
                GameManager.Instance.isWin = true;

            if (GameManager.Instance.isWin)
            {
                GameManager.Instance.WinningCeremony();
            }
        }
    }
}