namespace Server.DAL.Models.Enums
{
    [Flags]
    public enum Legs
    {
        First =     0b000000001,
        Second =    0b000000010,
        Third =     0b000000100,
        Fourth =    0b000001000,
        Fifth =     0b000010000,
        Sixth =     0b000100000,
        Seventh =   0b001000000,
        Eighth =    0b010000000,
        Air =       0b100000000
    }
}
