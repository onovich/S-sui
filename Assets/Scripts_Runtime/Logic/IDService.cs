public class IDService {

    int cardEntityID;

    public IDService() {
        cardEntityID = 0;
    }

    public int PickNextCardEntityID() {
        return ++cardEntityID;
    }

    public void Reset() {
        cardEntityID = 0;
    }

}