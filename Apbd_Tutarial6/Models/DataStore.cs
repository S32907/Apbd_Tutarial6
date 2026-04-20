namespace Apbd_Tutarial6.Models;

public static class DataStore
{
    public static List<Room> Rooms = new()
    {
        new Room(0,"Luxury3L","20l",8,4,true,false),
        new Room(1,"Basic1B","15b",2,2,false,true),
        new Room(2,"Advanced2A","15b",5,3,true,true)
    };

    public static List<Reservation> Reservations = new();
}