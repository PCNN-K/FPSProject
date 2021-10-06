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
            var isWin = true;
            //조건 체크

            if (isWin)
            {
                GameManager.Instance.stageNum++;
                SceneManager.LoadScene("InGame");
            }
        }
    }
}