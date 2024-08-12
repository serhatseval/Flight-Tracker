using System.Globalization;
using NetworkSourceSimulator;

namespace Project1;

class MessagetoStringClass
{
    /// <summary>
    /// This dictionary designed to get data from byte array and convert it to string array to use it with CreateFileToFactories dictionary. Source: https://learn.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-8.0
    /// </summary>
    static public Dictionary<string, Func<byte[], string[]>> MessagetoString = new Dictionary<
        string,
        Func<byte[], string[]>
    >()
    {
        {
            "NAI",
            message =>
            {
                string[] values;
                using (var stream = new MemoryStream(message))
                using (var reader = new BinaryReader(stream))
                {
                    string Type = new string(reader.ReadChars(3));
                    UInt32 Length = reader.ReadUInt32();
                    UInt64 Id = reader.ReadUInt64();
                    UInt16 NameLength = reader.ReadUInt16();
                    string Name = new string(reader.ReadChars(NameLength));
                    string Code = new string(reader.ReadChars(3));
                    Single Longitude = reader.ReadSingle();
                    Single Latitude = reader.ReadSingle();
                    Single AMSL = reader.ReadSingle();
                    string ISO = new string(reader.ReadChars(3));
                    values = new string[]
                    {
                        "AI",
                        Id.ToString(),
                        Name,
                        Code,
                        Longitude.ToString(CultureInfo.InvariantCulture),
                        Latitude.ToString(CultureInfo.InvariantCulture),
                        AMSL.ToString(CultureInfo.InvariantCulture),
                        ISO
                    };
                }

                return StringFixer(values);
            }
        },
        {
            "NPA",
            message =>
            {
                string[] values;
                using (var stream = new MemoryStream(message))
                using (var reader = new BinaryReader(stream))
                {
                    string Type = new string(reader.ReadChars(3));
                    UInt32 Length = reader.ReadUInt32();
                    UInt64 Id = reader.ReadUInt64();
                    UInt16 NameLength = reader.ReadUInt16();
                    string Name = new string(reader.ReadChars(NameLength));
                    UInt16 Age = reader.ReadUInt16();
                    string PhoneNumber = new string(reader.ReadChars(12));
                    UInt16 EmailLength = reader.ReadUInt16();
                    string Email = new string(reader.ReadChars(EmailLength));
                    char Class = reader.ReadChar();
                    UInt64 Miles = reader.ReadUInt64();
                    values = new string[]
                    {
                        "P",
                        Id.ToString(),
                        Name,
                        Age.ToString(),
                        PhoneNumber,
                        Email,
                        Class.ToString(),
                        Miles.ToString()
                    };
                }

                return StringFixer(values);
            }
        },
        {
            "NCP",
            message =>
            {
                string[] values;
                using (var stream = new MemoryStream(message))
                using (var reader = new BinaryReader(stream))
                {
                    string Type = new string(reader.ReadChars(3));
                    UInt32 Length = reader.ReadUInt32();
                    UInt64 Id = reader.ReadUInt64();
                    string Serial = new string(reader.ReadChars(10));
                    string CountryIso = new string(reader.ReadChars(3));
                    UInt16 ModelLength = reader.ReadUInt16();
                    string Model = new string(reader.ReadChars(ModelLength));
                    Single MaxLoad = reader.ReadSingle();
                    values = new string[]
                    {
                        "CP",
                        Id.ToString(),
                        Serial,
                        CountryIso,
                        Model,
                        MaxLoad.ToString(CultureInfo.InvariantCulture)
                    };
                }

                return StringFixer(values);
            }
        },
        {
            "NCA",
            message =>
            {
                string[] values;
                using (var stream = new MemoryStream(message))
                using (var reader = new BinaryReader(stream))
                {
                    string Type = new string(reader.ReadChars(3));
                    UInt32 Length = reader.ReadUInt32();
                    UInt64 Id = reader.ReadUInt64();
                    Single Weight = reader.ReadSingle();
                    string Code = new string(reader.ReadChars(6));
                    UInt16 DescriptionLength = reader.ReadUInt16();
                    string Description = new string(reader.ReadChars(DescriptionLength));
                    values = new string[]
                    {
                        "CA",
                        Id.ToString(),
                        Weight.ToString(CultureInfo.InvariantCulture),
                        Code,
                        Description
                    };
                }

                return StringFixer(values);
            }
        },
        {
            "NPP",
            message =>
            {
                string[] values;
                using (var stream = new MemoryStream(message))
                using (var reader = new BinaryReader(stream))
                {
                    string Type = new string(reader.ReadChars(3));
                    UInt32 Length = reader.ReadUInt32();
                    UInt64 Id = reader.ReadUInt64();
                    string Serial = new string(reader.ReadChars(10));
                    string CountryIso = new string(reader.ReadChars(3));
                    UInt16 ModelLength = reader.ReadUInt16();
                    string Model = new string(reader.ReadChars(ModelLength));
                    UInt16 FirstClassSize = reader.ReadUInt16();
                    UInt16 BusinessClassSize = reader.ReadUInt16();
                    UInt16 EconomyClassSize = reader.ReadUInt16();
                    values = new string[]
                    {
                        "PP",
                        Id.ToString(),
                        Serial,
                        CountryIso,
                        Model,
                        FirstClassSize.ToString(),
                        BusinessClassSize.ToString(),
                        EconomyClassSize.ToString()
                    };
                }

                return StringFixer(values);
            }
        },
        {
            "NFL",
            message =>
            {
                string[] values;
                using (var stream = new MemoryStream(message))
                using (var reader = new BinaryReader(stream))
                {
                    string Type = new string(reader.ReadChars(3));
                    UInt32 Length = reader.ReadUInt32();
                    UInt64 Id = reader.ReadUInt64();
                    UInt64 OriginId = reader.ReadUInt64();
                    UInt64 TargetId = reader.ReadUInt64();
                    Int64 TakeOffTime = reader.ReadInt64();
                    Int64 LandingTime = reader.ReadInt64();
                    UInt64 PlaneId = reader.ReadUInt64();
                    UInt16 CrewNumber = reader.ReadUInt16();
                    UInt64[] CrewId = new UInt64[CrewNumber];
                    for (UInt64 i = 0; i < CrewNumber; i++)
                    {
                        CrewId[i] = reader.ReadUInt64();
                    }

                    UInt16 LoadNumber = reader.ReadUInt16();
                    UInt64[] LoadId = new UInt64[LoadNumber];
                    for (UInt64 i = 0; i < LoadNumber; i++)
                    {
                        LoadId[i] = reader.ReadUInt64();
                    }

                    values = new string[]
                    {
                        "FL",
                        Id.ToString(),
                        OriginId.ToString(),
                        TargetId.ToString(),
                        EPOCHtoString(TakeOffTime),
                        EPOCHtoString(LandingTime),
                        0.ToString(),
                        0.ToString(),
                        0.ToString(),
                        PlaneId.ToString(),
                        string.Join(";", CrewId),
                        string.Join(";", LoadId)
                    };
                }

                return StringFixer(values);
            }
        },
        {
            "NCR",
            message =>
            {
                string[] values;
                using (var stream = new MemoryStream(message))
                using (var reader = new BinaryReader(stream))
                {
                    string Type = new string(reader.ReadChars(3));
                    UInt32 Length = reader.ReadUInt32();
                    UInt64 Id = reader.ReadUInt64();
                    UInt16 NameLength = reader.ReadUInt16();
                    string Name = new string(reader.ReadChars(NameLength));
                    UInt16 Age = reader.ReadUInt16();
                    string PhoneNumber = new string(reader.ReadChars(12));
                    UInt16 EmailLength = reader.ReadUInt16();
                    string Email = new string(reader.ReadChars(EmailLength));
                    UInt16 Practice = reader.ReadUInt16();
                    char Role = reader.ReadChar();
                    values = new string[]
                    {
                        "C",
                        Id.ToString(),
                        Name,
                        Age.ToString(),
                        PhoneNumber,
                        Email,
                        Practice.ToString(),
                        Role.ToString()
                    };
                }

                return StringFixer(values);
            }
        }
    };


    /// <summary>
    /// Function to remove null characters from the string array
    /// </summary>
    /// <param name="values">String array</param>
    /// <returns></returns>
    private static string[] StringFixer(string[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = values[i].Replace("\0", string.Empty);
        }
    
        return values;
    }

    /// <summary>
    /// Function to convert EPOCH time to string. Source: https://stackoverflow.com/questions/2883576/how-do-you-convert-epoch-time-in-c
    /// </summary>
    /// <param name="time">Milliseconds from EPOCH</param>
    /// <returns></returns>
    private static string EPOCHtoString(Int64 time)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(time).DateTime.ToString("HH:mm");
    }
}
