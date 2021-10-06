using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MapManager : SingletonGameObject<MapManager>
    {
        public List<GameObject> _maps;
        private void Start()
        {
            LoadStage();
        }

        private void LoadStage()
        {
            var stageNum = GameManager.Instance.stageNum;
            //리소스 로드 로직 만들기
            
            //멥 리소스 
            //플레이어 스폰포인트에다가 플레이어 스폰 > 플레이어 매니져
            Instantiate(_maps[stageNum - 1], transform);
        }
    }
}