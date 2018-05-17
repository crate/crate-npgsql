# CrateDB Npgsql Plugin
A plugin that provides extensions to Npgsql which enable usage of Npgsql as a .NET data provider for CrateDB.

This plugin depends on a [fork](https://github.com/crate/npgsql) of the official [Npgsql](https://github.com/npgsql/npgsql) project. That fork contains the necessary changes to Npgsql that make this plugin work. We are working to get those changes merged upstream.

To use Npgsql with CrateDB, a special DatabaseInfoFactory-class has to be registered with a call to `NpgsqlDatabaseInfo.RegisterFactory`.
The factory has to be registered before opening connections to CrateDB:

```c#
NpgsqlDatabaseInfo.RegisterFactory(new CrateDbDatabaseInfoFactory());
```

## CrateDB object handler

The CrateDB object handler enables reading and/or writing of arbitrary CLR objects from/to CrateDB columns with a data type of json or json array.

To use this feature, import the `Npgsql.CrateDb` namespace and call the `UseCrateDbObjectHandler` extension method for the TypeMapper of your `NpgsqlConnection`:


```c#
using Npgsql.CrateDb;

...

using (var con = OpenConnection())
{
    con.TypeMapper.UseCrateDbObjectHandler();

    ...
}
```

Single objects can be read with the use of the `GetObject<T>(NpgsqlDataReader, int)` extension method.

```c#
...

using (var command = new NpgsqlCommand("select my_json_column from my_table", con))
using (var reader = command.ExecuteReader())
{
    if (reader.Read())
    {
        // Returns an instance of type MyClrObject
        var obj = reader.GetObject<MyClrObject>(0);

        ...
    }
}
```

Object arrays can be read with the `GetObjectArray<T>(NpgsqlDataReader, int)` extension method.

```c#
...

using (var command = new NpgsqlCommand("select my_json_array_column from my_table", con))
using (var reader = command.ExecuteReader())
{
    if (reader.Read())
    {
        // Returns an instance of type MyClrObject[]
        var arr = reader.GetObjectArray<MyClrObject>(0);

        ...
    }
}
```

The `GetObject<T>(NpgsqlDataReader, int)` and `GetObjectArray<T>(NpgsqlDataReader, int)` extension methods are implemented in the `Npgsql.CrateDb` namespace.

With the help of the CrateDB object handler you can also set arbitrary CLR objects and arrays as parameter values for parameters referencing columns with a data type of json or json array.

```c#
...

command.Parameters.AddWithValue("@obj_field", NpgsqlTypes.NpgsqlDbType.Json,
                                new { inner = "Zoon" });
command.Parameters.AddWithValue("@obj_array", NpgsqlTypes.NpgsqlDbType.Json,
                                new object[] {
                                    new { inner = "Zoon1" },
                                    new { inner = "Zoon2" }
                                });
...
```

Internally Newtonsoft.Json is used to serialize objects to json strings and vice versa.

## GetBytes in CrateDB

In CrateDB a byte array is returned as an array of int2 or `char[]` in the .NET world respectively. So you would have to use something like the following to read those values and cast them to `byte[]` by yourself:

```c#
char[] array = reader.GetFieldValue<char[]>(ordinal);
return array.Select(c => Convert.ToByte(c)).ToArray();
```

And that is pretty much what the `GetBytes(NpgsqlDataReader, int)` extension method is doing. So you can read a byte array like this:

```c#
...

using (var command = new NpgsqlCommand("select my_byte_array_column from my_table", con))
using (var reader = command.ExecuteReader())
{
    if (reader.Read())
    {
        // Returns an array of bytes
        var arr = reader.GetBytes(0);

        ...
    }
}
```
