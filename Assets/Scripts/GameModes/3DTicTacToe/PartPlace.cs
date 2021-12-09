using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamemode0
{
    public class PartPlace : MonoBehaviour
    {
        [SerializeField] private GameMode3DTicTacToe gameMode;
        [SerializeField] private GameObject crossDummyPrefab;
        [SerializeField] private GameObject circleDummyPrefab;

        private int x;
        private int y;
        private int z;

        private void Start()
        {
            x = Mathf.RoundToInt(transform.localPosition.x);
            y = Mathf.RoundToInt(transform.localPosition.y);
            z = Mathf.RoundToInt(transform.localPosition.z);
        }

        public void Place(int playerId)
        {
            gameMode.Place(playerId, x, y, z);

            Instantiate(playerId == 1 ? crossDummyPrefab : circleDummyPrefab, transform.position, Quaternion.Euler(-45, -45, 45));
        }
    }
}
