using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public void Awake()
        {
            if (Instance ==null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(Restart);
        }

        public Button _button;
        public TextMeshProUGUI CoinText;

        public void SetCoin(int coin)
        {
            CoinText.text = coin.ToString();
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _button.gameObject.SetActive(false);
        }
    }
}