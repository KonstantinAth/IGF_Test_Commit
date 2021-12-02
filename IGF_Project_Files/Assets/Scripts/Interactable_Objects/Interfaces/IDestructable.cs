//IDestuctable inteface contains all the needed methods
//for an object to be able to receive damage...
public interface IDestructable {
    public void TakeDamage(int damageReceived);
    public int GetStatePoints();
}