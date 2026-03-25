using Main2.Sprzety;
using Main2.Uzytkownicy;

namespace Main2.Transakcje;

public class Wypozyczenia
{
    public Osoba osoba{set;get;}
    public Sprzet sprzet{set;get;}
    public DateTime odkiedy { get; set; }
    public DateTime doKiedy { get; set; }
    public DateTime? kiedyOddany { get; set; }
    public double kwotaKary { get; set; }
    
    public bool czyZwrotTerminowosc { get; set; }
}