using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PiratesGame.Entity;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private BaseShip _playerShip;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(vertical > 0.2f)
        {
            _playerShip.IncreaseCurrentSpeed();
        }else if(vertical < -0.2f)
        {
            _playerShip.DecreaseCurrentSpeed();
        }

        if (horizontal > 0.2f)
        {
            _playerShip.IncreaseTurnAngle();
        }
        else if (horizontal < -0.2f)
        {
            _playerShip.DecreaseTurnAngle();
        }
    }
}
