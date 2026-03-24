using Microsoft.Data.SqlClient;
using SailClubLibrary.Models;
using SailClubLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class MainMenu
{
    //Lav selv flere menupunkter til at vælge funktioner for Rooms, bookings m.m.
    //og ligeledes for at kalde async metoder
    public static void showOptions()
    {
        Console.Clear();
        Console.WriteLine("\tVælg et menupunkt\n");
        Console.WriteLine("\t1)\t List Members asynkront");
        Console.WriteLine("\t2)\t Tilføj et Member asynkront");
        Console.WriteLine("\t3)\t Find et Member asynkront");
        Console.WriteLine("\t4)\t Count");
        Console.WriteLine("\tQ)\t Afslut");
        Console.Write("\tIndtast valg: ");
    }



    
    public static bool Menu()
    {
        showOptions();
        switch (Console.ReadLine())
        {
            case "1":
                ShowMembersAsync();
                DoSomething(); /*for at demonstrere foreskellen ml synkron og async kald*/
                return true;
            case "2":
                CreateMemberAsync();
                DoSomething();
                return true;
            case "3":
                GetMemberByIdAsync();
                DoSomething();
                return true;
            case "4":
                ShowCountAsync();
                DoSomething();
                return true;
            case "Q":
            case "q": return false;
            default: return true;
        }

    }
    private async static Task GetMemberByIdAsync()
    {
        Console.Clear();
        Console.WriteLine("Indlæs Telefon Nr.");
        string phoneNumber = Console.ReadLine();
        MemberRepositoryAsync mRepo = new MemberRepositoryAsync();
        Member foundMember = await mRepo.SearchMember(phoneNumber);
        if (foundMember != null)
        {
            Console.WriteLine($"Medlem fundet : {foundMember} ");
        }
        else
        {
            Console.WriteLine("Medlem findes ikke");
        }
        Console.ReadKey();
    }
    private async static Task ShowMembersAsync()
    {
        Console.Clear();
        List<Member> members = [];
        MemberRepositoryAsync mRepo = new MemberRepositoryAsync();
        members = await mRepo.GetAllMembers();
        foreach (Member member in members)
        {
            Console.WriteLine(member + "\n");
        }
        Console.ReadKey();
    }

    private async static Task ShowCountAsync()
    {
        Console.Clear();
        MemberRepositoryAsync mRepo = new MemberRepositoryAsync();
        Console.WriteLine("Count for members is: " + await mRepo.Count);
        Console.ReadKey();
    }
    private async static Task UpdateMemberAsync()
    {
        //Console.Clear();
        //Console.WriteLine("Indlæs tlf");
        ////int memberPhoneNumber = Convert.ToInt32(Console.ReadLine());
        //string memberPhoneNumber = Console.ReadLine();
        //Console.WriteLine("Indlæs by");
        //string city = Console.ReadLine();
        //Console.WriteLine("Indlæs by");
        //string city = Console.ReadLine();
        //Console.WriteLine("Indlæs by");
        //string city = Console.ReadLine();
        //Console.WriteLine("Indlæs by");
        //string city = Console.ReadLine();
        //Console.WriteLine("Indlæs by");
        //string city = Console.ReadLine();
        //Console.WriteLine("Indlæs by");
        //string city = Console.ReadLine();
        //Console.WriteLine("Indlæs by");
        //string city = Console.ReadLine();
        ////Console.WriteLine("Indlæs ny hotel adresse");
        ////string adresse = Console.ReadLine();

        //MemberRepositoryAsync ms = new MemberRepositoryAsync();
        //Member foundMember = await ms.SearchMember(memberPhoneNumber);
        //if (foundMember == null)
        //{
        //    Console.WriteLine("Member findes ikke");
        //}
        //else
        //{
        //    foundMember.FirstName =  ;
        //    foundMember.City = adresse;
        //    bool ok = await hs.UpdateHotelAsync(foundHotel, hotelnr);
        //    if (ok)
        //    {
        //        Console.WriteLine("Hotellet blev opdateret!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Fejl. Hotellet blev ikke opdateret!");
        //    }
        //}
        //Console.ReadKey();
    }

    private async static Task CreateMemberAsync()
    {
        //Indlæs data
        Console.Clear();
        Console.WriteLine("Indlæs Nr");
        int memberID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Indlæs Navn");
        string firstName = Console.ReadLine();
        Console.WriteLine("Indlæs Efternavn");
        string surName = Console.ReadLine();
        Console.WriteLine("Indlæs Telefon Nr.");
        string phoneNumber = Console.ReadLine();
        Console.WriteLine("Indlæs Adresse");
        string address = Console.ReadLine();
        Console.WriteLine("Indlæs By");
        string city = Console.ReadLine();
        Console.WriteLine("Indlæs Mail");
        string mail = Console.ReadLine();
        Console.WriteLine("Indlæs Medlem Type");
        MemberType theMemberType = (MemberType)Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Indlæs Medlem Role");
        MemberRole theMemberRole = (MemberRole)Convert.ToInt32(Console.ReadLine());

        //Kald hotelservice og vis resultatet
        //HotelServiceAsync hs = new HotelServiceAsync();
        MemberRepositoryAsync mRepo = new MemberRepositoryAsync();
        await mRepo.AddMember(new Member(memberID, firstName, surName, phoneNumber, address, city, mail, theMemberType, theMemberRole));
        Member memberToAdd = await mRepo.SearchMember(phoneNumber);
        bool ok = memberToAdd != null;
        if (ok)
        {
            Console.WriteLine("Medlem blev oprettet!");
        }
        else
        {
            Console.WriteLine("Fejl. Hotellet blev ikke oprettet!");
        }
        Console.ReadKey();
    }

    private static void DoSomething()
    {
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(100);
            Console.WriteLine(i + " i GUI i main thread");
        }
        Console.ReadLine();
    }

}