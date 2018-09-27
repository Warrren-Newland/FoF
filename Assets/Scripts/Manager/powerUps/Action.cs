public class Action : PowerUp {
    public int damage;
    public int span;
    public string direction;

    public Action(string name, int damage, int span, string direction)
    {
        this.name = name;
        this.damage = damage;
        this.span = span;
        this.direction = direction;
        this.color = "orange";
        this.grouping = "Action";
    }

}
