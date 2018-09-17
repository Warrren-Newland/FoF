using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupGenerator : MonoBehaviour {

    public List<PowerUp> allPowerups;

    private Danger mine;

    private void instatiateVariables()
    {
        allPowerups = new List<PowerUp>();
    }

    private void fillPowerUpList()
    {
        instatiateVariables();
        allPowerups.Add(new Danger("Mine", 20));
        allPowerups.Add(new Weapon("Light Saber", 40, 1, "around"));
        allPowerups.Add(new Weapon("Gun", 40, 100, "straight"));
        allPowerups.Add(new Heal("Small Heal", 30));
    }

    public PowerUp generateRandomPowerUp()
    {
        System.Random random = new System.Random();
        fillPowerUpList();
        int ran = random.Next(0, allPowerups.Count);
        return allPowerups[ran];
    }

	public PowerUp generateMine()
    {
        return new Danger("Mine", 20);
    }

    public PowerUp generateLightSaber()
    {
        return new Weapon("Light Saber", 40, 1, "around");
    }

    public PowerUp generateGun()
    {
        return new Weapon("Gun", 40, 100, "straight");
    }

    public PowerUp generateHeal()
    {
        return new Heal("Small Heal", 30);
    }
}
