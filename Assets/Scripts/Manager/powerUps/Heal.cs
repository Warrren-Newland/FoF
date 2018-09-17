public class Heal : PowerUp {
    public int health;

    public Heal(string name, int health)
    {
        this.name = name;
        this.health = health;
        this.color = "green";
        this.grouping = "Heal";
    }

}
