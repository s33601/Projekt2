namespace Main2.Uzytkownicy;

public class Student : Osoba
{
    public override int limitWypozyczenia { get; } = 2;
}