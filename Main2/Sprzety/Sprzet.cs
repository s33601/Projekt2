namespace Main2.Sprzety;

public abstract class Sprzet
{
    public int ID { get; set; }
    public String nazwa {get; set; }
    public EDostepnosc statusDostepnosci { get; set; }
    public String nazwaProducenta { get; set; }
}