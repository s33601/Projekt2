using Main2.Sprzety;
using Main2.Transakcje;
using Main2.Uzytkownicy;

namespace Main2;

public class ZarzadzeniaWypozyczalnia
{
    private List<Sprzet> listaSprzetu = new List<Sprzet>();
    private List<Osoba> listaOsob = new List<Osoba>();
    private List<Wypozyczenia> listaWypozyczen = new List<Wypozyczenia>();

    public void dodawnieSprzetu(Sprzet sprzet)
    {
        sprzet.ID = listaSprzetu.Count + 1;
        listaSprzetu.Add(sprzet);
    }

    public void dodawnieOsoba(Osoba osoba)
    {
        osoba.ID = listaOsob.Count + 1;
        listaOsob.Add(osoba);
    }
}