using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player mainPlayer;

    #region Unity_Function
    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") Debug.Log("111");
    }
    #endregion

    #region Function

    public void _HpDown()
    {
        if(mainPlayer.Hp > 1) 
        {
            mainPlayer.Hp --;
        }
        else
        {
            mainPlayer.Hp --;
            mainPlayer._OnDie();
        }
            
    }
    #endregion
}
