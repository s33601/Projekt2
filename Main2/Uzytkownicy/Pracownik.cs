namespace Main2.Uzytkownicy;

public class Pracownik : Osoba
{
    public override int limitWypozyczenia { get; } = 5;
}