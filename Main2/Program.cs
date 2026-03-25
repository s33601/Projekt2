using Main2;
using Main2.Sprzety;
using Main2.Uzytkownicy;

internal class Program
{
    public static void Main(string[] args)
    {
        Student student = new Student();
        student.Imie = "Adam";
        student.Nazwisko = "Wyrzykowski";

        Pracownik pracownik = new Pracownik();
        pracownik.Imie = "Filip";
        pracownik.Nazwisko = "Lipinski";
        Laptop laptop = new Laptop();
        laptop.iloscRam = 32;
        laptop.pojemnosc = 512;
        laptop.nazwa = "Laptop1";
        laptop.nazwaProducenta = "Lenovo";
        laptop.statusDostepnosci = EDostepnosc.DOSTEPNE;
        Projektor projektor = new Projektor();
        projektor.jasnosc = 1000;
        projektor.nazwa = "Projektor1";
        projektor.nazwaProducenta = "LG";
        projektor.statusDostepnosci = EDostepnosc.DOSTEPNE;
        projektor.rozdzielczosc = "1920x1080";
        Aparat aparat = new Aparat();
        aparat.kolor = "zielony";
        aparat.rozdzielczoscMatrycy = "36x24";
        aparat.nazwaProducenta = "Sony";

        ZarzadzeniaWypozyczalnia system = new ZarzadzeniaWypozyczalnia();
        system.dodawnieOsoba(student);
        system.dodawnieOsoba(pracownik);
        system.dodawnieSprzetu(laptop);
        system.dodawnieSprzetu(projektor);
        system.dodawnieSprzetu(aparat);


          while (true)
        {
            Console.WriteLine("---MENU WYPOZYCZALNI---");
            Console.WriteLine("0. EXIT");
            Console.WriteLine("1. Wypozyczenie sprzetu");
            Console.WriteLine("2. Oddanie sprzetu do wypozyczalni");
            Console.WriteLine("3. Oddaj sprzet do naprawy");
            Console.WriteLine("4. Powrot sprzetu z naprawy");
            Console.WriteLine("5. Wyswietl cala liste sprzetu");
            Console.WriteLine("6. Wyswietl dostepny sprzet");
            Console.WriteLine("7. Wyswietl wypozyczony sprzet");
            Console.WriteLine("8. Wyswietl sprzet w naprawie");
            Console.WriteLine("9. Wyswietl wypozyczenia dla danej osoby");
            Console.WriteLine("10. Wyswietl liste zalegajacych zwrotow");
            
            int wybranaopcja = int.Parse(Console.ReadLine());
            if (wybranaopcja == 0)
            {
                Console.WriteLine("Wychodze z programu");
                return;
            }
            if (wybranaopcja == 1)
            {
                Console.WriteLine("Podaj ID osoby wypozyczajacej sprzet: ");
                int podaneIdOsoby = int.Parse(Console.ReadLine());
                Console.WriteLine("Podaj ID wypozczanego sprzetu: ");
                int podaneIdSprzetu = int.Parse(Console.ReadLine());    
                Console.WriteLine("Podaj na ile dni klienta chce wypozyczyc sprzet: ");
               int ileDniPodal = int.Parse(Console.ReadLine());
                string wynik = system.WypozyczSprzet(podaneIdOsoby,podaneIdSprzetu,ileDniPodal);
                Console.WriteLine(wynik);
            }
            else if (wybranaopcja == 2)
            {
                Console.WriteLine("Jakie ID sprzetu chcesz zwrocic: ");
                int ileOddawanego = int.Parse(Console.ReadLine());
                
                Console.WriteLine("Podaj bazową stawkę kary (naliczenie tylko w przypadku opoznionego oddana): ");
                double stawkaKary = double.Parse(Console.ReadLine());
                
                string wynikZwrotu = system.oddajSprzet(ileOddawanego,stawkaKary);
                Console.WriteLine(wynikZwrotu);
            }
            else if (wybranaopcja == 3)
            {
                Console.WriteLine("Jakie ID sprzetu chcesz dać do naprawy: ");
                int cooddajemy = int.Parse(Console.ReadLine());
                string wynikNaprawy = system.OddajDoNaprawy(cooddajemy);
                Console.WriteLine(wynikNaprawy);
            }
            else if(wybranaopcja ==4)
                        {
                            Console.WriteLine("Jakie ID sprzetu wraca z naprawy: ");
                            int  podaneIdSprzetu = int.Parse(Console.ReadLine());
                            string wynik = system.PowrotZnaprawy(podaneIdSprzetu);
                            Console.WriteLine(wynik);
                        }
            else if (wybranaopcja == 5)
            {
                Console.WriteLine("---CALY SPRZET W MAGAZYNIE---");
                List<string> lista = system.wyswyietlanieCalejlisty();
                foreach (string item in lista)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("-----------------------\n");
            }
            else if (wybranaopcja == 6)
            {
                Console.WriteLine("---DOSTEPNY SPRZET---");
                List<string> lista = system.wyswietlanieDOSTEPNYCH();
                foreach (string item in lista)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("-----------------------\n");
            }
            else if (wybranaopcja == 7)
            {
                Console.WriteLine("---WYPOZYCZONY SPRZET---");
                List<string> lista = system.wyswietelnieWYPOZYCZONYCH();
                foreach (string item in lista)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("-----------------------\n");
            }
            else if (wybranaopcja == 8)
            {
                Console.WriteLine("---SPRZET W NAPRAWIE---");
                List<string> lista = system.wyswietlanieNAPRAWA();
                foreach (string item in lista)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("-----------------------\n");
            }
            else if (wybranaopcja == 9)
            {
                Console.WriteLine("Podaj id osoby do sprawdzenia: ");
                int podaneIdOsoby = int.Parse(Console.ReadLine());
                Console.WriteLine("---WYPOZYCZONY SPRZET OSOBY O ID: "+podaneIdOsoby+"---");
                
                List<string> lista = system.wyswietlWypozyczeniaDanegoID(podaneIdOsoby);
                foreach (string item in lista)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("-----------------------\n");
            }
            else if (wybranaopcja == 10)
            {
                Console.WriteLine("---ZALEGAJĄCY ZE ZWROTEM---");
                List<string> lista = system.wyswietlListeZALEGAJACYCHzeZwrotem();
                foreach (string item in lista)
                    {
                    Console.WriteLine(item);
                    }
                Console.WriteLine("-----------------------");
            }

        }
        
    }
}