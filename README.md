Keywielder
==========

Simple token, unique key generator. It's one of my favorite library to generate formatted primary key.
Such as Document number in (LOB) apps.

# How to - 1

```csharp
String key = Keywielder
        .New()
        .AddString("KEY", 3, "-")
        .AddLongYear()
        .AddNumericMonth()
        .AddDate("-")
        .AddCounter(12, 10, 4)
        .BuildKey();

Console.WriteLine(key);
```

Above code will generate key like this (below), result may vary as I use date
*KEY-20150125-0022*

  
# How to - 2

```csharp
String simpleKey = Keywielder
        .New()
        .AddString("SIMPLE", 5, "-")
        .AddGUIDString("-")
        .AddLongYear("-")
        .AddRandomAlphaNumeric(10, "-")
        .AddRandomString(5, "-")
        .AddNumericDay()
        .BuildKey();

Console.WriteLine(complexKey);
```

Above code will generate key like this (below), result may vary as I use year and combination random and GUID
*SIMPL-3d5968351a0645dda7bfd3bbeb4ad972-2015-CRFWSJWZ3B-SPRHG-01*
