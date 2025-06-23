using UnityEngine;
using System;

namespace Core.EventBas
{
    public static class GameEventBas
    {
        public static event Action OnBurgerTook;
        public static event Action OnSetTotalScore;
        public static event Action OnGameOver;
        public static event Action OnSetBestScore;

        public static void TakeBurger()
        {
            OnBurgerTook?.Invoke();
        }

        public static void SetTotalScore()
        {
            OnSetTotalScore?.Invoke();
        }

        public static void GameOver()
        {
            OnGameOver?.Invoke();
        }

        public static void SetBestScore()
        {
            OnSetBestScore?.Invoke();
        }
    }
}
