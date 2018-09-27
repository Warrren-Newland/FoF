using System.Collections.Generic;
using UnityEngine;

public class PowerupGenerator : MonoBehaviour {

    public List<PowerUp> allPowerups;
    int ran;
    System.Random random;

    private void instatiateVariables()
    {
        random = new System.Random(); 
        allPowerups = new List<PowerUp>();
    }

    public void fillPowerUpList()
    {
        instatiateVariables();
        allPowerups.Add(new Danger("Mine", 20));
        allPowerups.Add(new Weapon("Light Saber", 40, 1, "around"));
        allPowerups.Add(new Weapon("Gun", 40, 100, "straight"));
        allPowerups.Add(new Heal("Small Heal", 30));
        allPowerups.Add(new Action("Air Bounce", 0, 2, "around"));
        allPowerups.Add(new Buff("Invisibility", 5));
        allPowerups.Add(new Action("Plummet", 0, 0, "random"));
        allPowerups.Add(new Action("Energy Wave", 60, 100, "straight"));
    }

    public PowerUp generateRandomPowerUp()
    {
        ran = random.Next(0, allPowerups.Count);
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

    public PowerUp generateAirBounce()
    {
        return new Action("Air Bounce", 0, 2, "around");
    }

    public PowerUp generateInvisibility()
    {
        return new Buff("Invisibility", 5);
    }

    public PowerUp generatePlummet()
    {
        return new Action("Plummet", 0, 0, "random");
    }

    public PowerUp generateEnergyWave()
    {
        return new Action("Energy Wave", 60, 100, "straight");
    }
}
