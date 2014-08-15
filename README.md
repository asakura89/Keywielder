Keywielder
==========

Simple token, unique key generator. It's one of my favorite library to generate formatted primary key.  
Such as Document number in business project.

# How to

1. Config your key format  
```csharp
KeyWielder.KeyWielderConfig
        keyParts1 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.STRING, value = "KEY", valueLength = 3, backSeparator = "-" },
        keyParts2 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.YEAR, valueLength = 4 },
        keyParts3 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.MONTH, valueLength = 2 },
        keyParts4 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.DATE, valueLength = 2, backSeparator = "-" },
        keyParts5 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.COUNTER, valueLength = 4, replacementChar = "0", currentCounterValue = 12, counterIncrement = 10 };
```

2. Add all your config parts to list  
```csharp
var keyConfigList = new List<KeyWielder.KeyWielderConfig> { keyParts1, keyParts2, keyParts3, keyParts4, keyParts5 };
```

3. Generate key  
```csharp
String key = KeyWielder.BuildKey(keyConfigList);
Console.WriteLine(key);
```

Above code will generate key like this (below), result may vary as I use date in my config parts  
*KEY-20131023-0022*  



# KeyBlaster

Similar to KeyWielder but more simple

# How to

1. Set it's length and type then you're ready to go  
```csharp
String simpleKey = KeyBlaster.BuildSimpleKey(8, Keywielder.KeyBlaster.SimpleKeyType.ALPHANUMERIC);
Console.WriteLine(simpleKey);
```

2. Or you just lazy  
```csharp
String simpleKey = KeyBlaster.BuildSimpleKey();
Console.WriteLine(simpleKey);

String complexKey = KeyBlaster.BuildComplexKey();
Console.WriteLine(complexKey);
```

Above code will generate key like this (below), result may vary as simple key is combination random and complex key is GUID  
simple key: *M611IX3X*  
complex key: *ba9b43bd2dd04a548720d688e3980472*  
