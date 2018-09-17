public class Weapon : PowerUp {
    public int damage;
    public int span;
    public string direction;

    public Weapon(string name, int damage, int span, string direction)
    {
        this.name = name;
        this.damage = damage;
        this.span = span;
        this.direction = direction;
        this.color = "purple";
        this.grouping = "Weapon";
    }
}
