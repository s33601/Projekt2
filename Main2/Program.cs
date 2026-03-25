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


    }
}