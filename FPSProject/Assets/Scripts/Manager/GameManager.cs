using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : SingletonGameObject<GameManager>
    {
        public int stageNum = 1;

        public bool isWin = false;
        public bool isDefeat = false;

        public void WinningCeremony()
        {
            stageNum++;
            SceneManager.LoadScene("InGame");
        }
    }
}