using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MapManager : SingletonGameObject<MapManager>
    {
        public List<GameObject> _maps;

        private void Awake()
        {
            SettingMapsList();
        }
        private void Start()
        {

            LoadStage();
        }

        private void SettingMapsList()
        {
            var map1 = Resources.Load<GameObject>("Prefabs/map/Stage1");
            var map2 = Resources.Load<GameObject>("Prefabs/map/Stage2");
            var map3 = Resources.Load<GameObject>("Prefabs/map/Stage3");
            var map4 = Resources.Load<GameObject>("Prefabs/map/Stage4");
            var map5 = Resources.Load<GameObject>("Prefabs/map/Stage5");

            _maps.Add(map1);
            _maps.Add(map2);
            _maps.Add(map3);
            _maps.Add(map4);
            _maps.Add(map5);

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