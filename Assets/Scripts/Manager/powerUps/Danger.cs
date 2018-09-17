public class Danger : PowerUp
{ 
    public int damage;

    public Danger(string name, int damage)
    {
        this.damage = damage;
        this.name = name;
        this.color = "red";
        this.grouping = "Danger";
    }
}
