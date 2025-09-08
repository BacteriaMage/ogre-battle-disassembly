// github.com/BacteriaMage

namespace BacteriaMage.OgreBattle.Disassembler.Utilities;

public static class ConvertUtilities
{
    public static int ToInt(byte value) => value;
    public static int ToInt(ushort value) => value;

    public static byte ToByte(int value) => (byte)value;
    public static ushort ToWord(int value) => (ushort)value;
    public static ushort ToWord(int high, int low) => ToWord(high << 8 | (low & 0xff));

    public static void ToBytes(int value, out int high, out int low)
    {
        high = (value >> 8) & 0xff;
        low = value & 0xff;
    }

    public static void ToBytes(int value, out byte high, out byte low)
    {
        high = ToByte(value >> 8);
        low = ToByte(value);
    }
    
    public static void ToBytes(int value, out int high, out int middle, out int low)
    {
        high = (value >> 16) & 0xff;
        middle = (value >> 8) & 0xff;
        low = value & 0xff;
    }
    
    public static void ToBytes(int value, out byte high, out byte middle, out byte low)
    {
        high = ToByte(value >> 16);
        middle = ToByte(value >> 8);
        low = ToByte(value);
    }

    public static void ToBytes(int value, out int byte3, out int byte2, out int byte1, out int byte0)
    {
        byte3 = (value >> 24) & 0xff;
        byte2 = (value >> 16) & 0xff;
        byte1 = (value >> 8) & 0xff;
        byte0 = value & 0xff;
    }
    
    public static void ToBytes(int value, out byte byte3, out byte byte2, out byte byte1, out byte byte0)
    {
        byte3 = ToByte(value >> 24);
        byte2 = ToByte(value >> 16);
        byte1 = ToByte(value >> 8);
        byte0 = ToByte(value);
    }
    
    public static void ToWords(int value, out int high, out int low)
    {
        high = (value >> 16) & 0xffff;
        low = value & 0xffff;
    }
    
    public static void ToWords(int value, out ushort high, out ushort low)
    {
        high = ToWord(value >> 16);
        low = ToWord(value);
    }
    
    public static void ToBankAndWord(int value, out int bank, out int word)
    {
        bank = (value >> 16) & 0xff;
        word = value & 0xffff;
    }
    
    public static void ToBankAndWord(int value, out byte bank, out ushort word)
    {
        bank = ToByte(value >> 16);
        word = ToWord(value);
    }
    
    public static int FromBytes(byte high, byte low)
    {
        return ToInt(high) << 8 | ToInt(low);
    }
    
    public static int FromBytes(int high, int low)
    {
        return (high & 0xff) << 16 | (low & 0xff);
    }
    
    public static int FromBytes(byte high, byte middle, byte low)
    {
        return ToInt(high) << 16 | ToInt(middle) << 8 | ToInt(low);
    }
    
    public static int FromBytes(int high, int middle, int low)
    {
        return (high & 0xff) << 16 | (middle & 0xff) << 8 | (low & 0xff);
    }
    
    public static int FromBytes(byte byte3, byte byte2, byte byte1, byte byte0)
    {
        return ToInt(byte3) << 24 | ToInt(byte2) << 16 | ToInt(byte1) << 8 | ToInt(byte0);
    }
    
    public static int FromBytes(int byte3, int byte2, int byte1, int byte0)
    {
        return (byte3 & 0xff) << 24 | (byte2 & 0xff) << 16 | (byte1 & 0xff) << 8 | (byte0 & 0xff);
    }
    
    public static int FromWords(ushort high, ushort low)
    {
        return ToInt(high) << 16 | ToInt(low);
    }
    
    public static int FromWords(int high, int low)
    {
        return (high & 0xffff) << 16 | (low & 0xffff);
    }
    
    public static int FromBankAndWord(byte bank, ushort word)
    {
        return ToInt(bank) << 16 | ToInt(word);
    }
    
    public static int FromBankAndWord(int bank, int word)
    {
        return (bank & 0xff) << 16 | (word & 0xffff);
    }
}