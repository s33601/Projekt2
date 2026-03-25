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
    
     public string WypozyczSprzet(int idOsoba, int idSprzetu, int naIleDni)
    {
        Osoba osoba = null;
        foreach (Osoba o in listaOsob)
        {
            if (o.ID == idOsoba)
            {
                osoba = o;
                break;
            }

        }
        Sprzet sprzet = null;
        foreach (Sprzet s in listaSprzetu)
        {
            if (s.ID == idSprzetu)
            {
                sprzet = s;
                break;
            }

        }
       
       
        if (osoba == null || sprzet == null)
        {
            return "Nie ma takiego rekordu";
        }

        if (sprzet.statusDostepnosci != EDostepnosc.DOSTEPNE)
        {
           
            return "Sprzet jest niedostepny";
        }

        int iloscAktualnieWypozyczonych = 0;
        foreach (Wypozyczenia w in listaWypozyczen)
        {
            if (w.osoba.ID == osoba.ID && w.kiedyOddany == null)
            {
                iloscAktualnieWypozyczonych++;
            }
        }
        
            
        
        if (iloscAktualnieWypozyczonych >= osoba.limitWypozyczenia)
        {
            
            return "Osiagnieto limit wypozyczonych sprzetow";
        }
        // przeszlo ifa
            Wypozyczenia daneWypozyczenie = new Wypozyczenia();
            daneWypozyczenie.osoba = osoba;
            daneWypozyczenie.sprzet = sprzet;
            daneWypozyczenie.odkiedy = DateTime.Now;
            daneWypozyczenie.doKiedy = DateTime.Now.AddDays(naIleDni);
            
            listaWypozyczen.Add(daneWypozyczenie);
            sprzet.statusDostepnosci = EDostepnosc.WYPOZYCZONE;
            return "Wypozyczono sprzet";
            
    }

    public string oddajSprzet(int idSprzetu, double stawkaKary)
    {
        foreach (Wypozyczenia daneWypozyczenie in listaWypozyczen)
        {
            if (daneWypozyczenie.sprzet.ID == idSprzetu && daneWypozyczenie.kiedyOddany == null)
            {
                daneWypozyczenie.kiedyOddany = DateTime.Now;
                daneWypozyczenie.sprzet.statusDostepnosci = EDostepnosc.DOSTEPNE;
                daneWypozyczenie.czyZwrotTerminowosc = true;
                if (DateTime.Now > daneWypozyczenie.doKiedy)
                {
                    daneWypozyczenie.czyZwrotTerminowosc = false;
                    double ilewiecejdni = (DateTime.Now - daneWypozyczenie.doKiedy).TotalDays;
                    int dniOpoznienia = (int)Math.Ceiling(ilewiecejdni);
                    
                    double ileKary = dniOpoznienia * stawkaKary; // kara 10zl za dzien
                    daneWypozyczenie.kwotaKary = ileKary;
                    return "Naliczono: " + ileKary + " za opóźnienie: " + ilewiecejdni+" dni";

                }

                return "Oddano sprzet w terminie";

            }
        }
        return "Nie znaleziono aktywnego wypozyczenia dla tego sprzetu";
        
        
    }

    public string OddajDoNaprawy(int idSprzetu)
    {
        foreach (Sprzet s in listaSprzetu)
        {
            if (s.ID == idSprzetu)
            {
                if (s.statusDostepnosci == EDostepnosc.WYPOZYCZONE)
                {
                    return "Sprzet jest wypozyczony, nie mozna go oddac do naprawy";
                }

                s.statusDostepnosci = EDostepnosc.NAPRAWA;
                return "Oddany sprzet o ID: " + s.ID + " do naprawy";
            }
        }

        return "Nie ma sprzetu o takim ID";
    }

    public string PowrotZnaprawy(int idSprzetu)
    {
        foreach (Sprzet s in listaSprzetu)
        {
            if (s.ID == idSprzetu)
            {
                if (s.statusDostepnosci == EDostepnosc.NAPRAWA)
                {
                    s.statusDostepnosci = EDostepnosc.DOSTEPNE;
                    return "Sprzet o id: " + s.ID + " wrócił z naprawy";
                }
                
            }
        }

        return "Sprzet o takim ID nie jest w naprawie";
    }
}