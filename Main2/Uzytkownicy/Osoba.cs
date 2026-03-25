namespace Main2.Uzytkownicy;

public abstract class Osoba
{
    public int ID { get; set; }
    public String Imie { get; set; }
    public String Nazwisko { get; set; }
    public virtual int limitWypozyczenia { get; }
}