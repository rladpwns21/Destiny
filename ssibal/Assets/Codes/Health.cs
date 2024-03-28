using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int numOfHearts;
    public List<Image> hearts = new List<Image>();
    public Sprite fullHeart;
    public Sprite emptyHeart;



    void Update()
    {
        int hp = GameManager.instance.mainPlayer.Hp;
        if (hp > numOfHearts)
        {
            hp = numOfHearts;
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < hp)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
