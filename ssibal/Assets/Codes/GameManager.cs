using System.Collections;
using System.Collections.Generic;
using UnityEditor.ProjectWindowCallback;
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
        if(other.gameObject.tag == "Player")
        {
            _HpDown();
            other.attachedRigidbody.velocity = Vector2.zero;
            other.transform.position = new Vector3(-16, -2.5f, -1);
        }
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
            StartCoroutine(End());
            IEnumerator End()
            {
                mainPlayer._OnDie();
                yield return new WaitForSeconds(2f);
                Time.timeScale = 0;

                yield break;
            }
        }
            
    }

    #endregion
}
