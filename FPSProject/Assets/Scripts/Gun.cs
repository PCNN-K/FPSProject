using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Ammo _ammo;
        
        //ammo 의 오브젝트 풀
        //힌트 : 총알 담든 자료구조는 Queue로 구현!
        public void Fire()
        {
            
        }

        private void CreateAmmo()
        {
            
        }

        private void Shot()
        {
            
        }

        private void Awake()
        {
            //초기화 로직   
        }
    }
}