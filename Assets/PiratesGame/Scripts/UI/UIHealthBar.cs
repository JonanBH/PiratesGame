using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PiratesGame.UI
{
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _fillImage;

        public void UpdateHealthBar(float percent)
        {
            _fillImage.fillAmount = percent;
        }
    }
}