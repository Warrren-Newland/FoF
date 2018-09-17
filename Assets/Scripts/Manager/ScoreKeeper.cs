using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    GameObject unitFrame;

    public void updatePlayerHealth(string playerName, int amount)
    {
        string name = "unitframe " + playerName;
        unitFrame = GameObject.Find(name);
        Text[] texts = unitFrame.GetComponentsInChildren<Text>();
        Text health = texts[1];
        Slider slider = unitFrame.GetComponentInChildren<Slider>();
        string text = health.text.Replace(" / 100", "");
        int currentHealth = System.Convert.ToInt32(text);
        int newHealth = currentHealth + amount;
        if(newHealth > 100)
        {
            newHealth = 100;
        }
        else if(newHealth < 0)
        {
            newHealth = 0;
        }

        slider.value = newHealth;

        health.text = newHealth + " / 100";
    }
}
