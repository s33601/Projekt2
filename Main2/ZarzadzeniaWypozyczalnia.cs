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
     public List<string> wyswyietlanieCalejlisty()
    {
        List<string> raport = new List<string>();
        foreach(Sprzet sprzet in listaSprzetu){
            string informacje = "Sprzet ID: " +sprzet.ID +" | Stan: " + sprzet.statusDostepnosci;
            if (sprzet.statusDostepnosci == EDostepnosc.WYPOZYCZONE)
            {
                foreach(Wypozyczenia w in listaWypozyczen)
                {
                    if (sprzet.ID == w.sprzet.ID && w.kiedyOddany == null)
                    {
                       informacje += " | Osoba wypozyczajaca: " + w.osoba.ID + " | Od: " + w.odkiedy.ToString("dd.MM.yyyy") + " | Do: " + w.doKiedy.ToString("dd.MM.yyyy");
                        break;
                    }
                }
                
            }
            raport.Add(informacje);
        }
        return raport;

    }

    public List<string> wyswietlanieDOSTEPNYCH()
    {
        List<string> raport = new List<string>();
        
        foreach (Sprzet sprzet in listaSprzetu)
        {
            if (sprzet.statusDostepnosci == EDostepnosc.DOSTEPNE)
            {
                string typSprzetu = sprzet.GetType().Name;
                string informacje = "Sprzet: " + typSprzetu +" | Id sprzetu: " + sprzet.ID + " | Status: " + sprzet.statusDostepnosci;
                
                 raport.Add(informacje);
            }
           

        }
        return raport;
    }
    
    public List<string> wyswietelnieWYPOZYCZONYCH()
    {
        List<string> raport = new List<string>();
        foreach(Sprzet sprzet in listaSprzetu){
            
            if (sprzet.statusDostepnosci == EDostepnosc.WYPOZYCZONE)
            {
                foreach(Wypozyczenia w in listaWypozyczen)
                {
                    if (sprzet.ID == w.sprzet.ID && w.kiedyOddany == null)
                    {
                        string typSprzetu = sprzet.GetType().Name;
                        string informacje = "Sprzet: " + typSprzetu + " | Id sprzetu: " +sprzet.ID + " | Osoba wypozyczajaca: " + w.osoba.ID + " | Od: " + w.odkiedy.ToString("dd.MM.yyyy") + " | Do: " + w.doKiedy.ToString("dd.MM.yyyy");

                        if (DateTime.Now > w.doKiedy)
                        {
                            TimeSpan ts = DateTime.Now - w.doKiedy;
                            informacje += " | Termin minal: " + ts.Days + " dni temu";
                        }
                        else
                        {
                            informacje += " | Termin oddania: " + w.doKiedy.ToString("dd.MM.yyyy");
                        }
                        
                        raport.Add(informacje);
                        break;
                    }

                }
            }
        }
        return raport;
    }
    
    public List<string> wyswietlanieNAPRAWA()
    {
        List<string> raport = new List<string>();
        
        foreach (Sprzet sprzet in listaSprzetu)
        {
            if (sprzet.statusDostepnosci == EDostepnosc.NAPRAWA)
            {
                string typSprzetu = sprzet.GetType().Name;
                string informacje = "Sprzet: " + typSprzetu +" | Id sprzetu: " + sprzet.ID + " | Status: " + sprzet.statusDostepnosci;
                
                raport.Add(informacje);
            }
           

        }
        return raport;
    }
    
      public List<string> wyswietlWypozyczeniaDanegoID(int OsobaID)
    {
        List<string> raport = new List<string>();
        foreach (Sprzet sprzet in listaSprzetu)
        {


            foreach (Wypozyczenia w in listaWypozyczen)
            {
                if (sprzet.ID == w.sprzet.ID && w.kiedyOddany == null && w.osoba.ID == OsobaID)
                {
                    string typSprzetu = sprzet.GetType().Name;
                    string informacje = "Sprzet: " + typSprzetu + " | Id sprzetu: " + sprzet.ID +
                                        " | Osoba wypozyczajaca: " + w.osoba.ID + " | Od: " +
                                        w.odkiedy.ToString("dd.MM.yyyy") + " | Do: " + w.doKiedy.ToString("dd.MM.yyyy");

                    if (DateTime.Now > w.doKiedy)
                    {
                        TimeSpan ts = DateTime.Now - w.doKiedy;
                        informacje += " | Termin minal: " + ts.Days + " dni temu";
                    }
                    else
                    {
                        informacje += " | Termin oddania: " + w.doKiedy.ToString("dd.MM.yyyy");
                    }

                    raport.Add(informacje);

                }
            }
        }

        if (raport.Count == 0)
                {
                    raport.Add("Brak aktywnych wypozyczen");
                }
     return raport;
        }

    public List<string> wyswietlListeZALEGAJACYCHzeZwrotem()
    {
        List<string> raport = new List<string>();
        foreach (Wypozyczenia w in listaWypozyczen)
        {
            if (w.kiedyOddany == null && DateTime.Now > w.doKiedy)
            {
                TimeSpan ts = DateTime.Now - w.doKiedy;
                string informacje = "Osoba o id: " + w.osoba.ID + " zalega z przedmiotem o id: " + w.sprzet.ID +
                                    " od " + ts.Days + " dni";
                raport.Add(informacje);

            }
            
        }
         if (raport.Count == 0)
                    {
                        raport.Add("Brak zaleglych zwrotow ");
                    }
        return raport;
    }

    
}