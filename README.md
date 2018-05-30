# CrateDB Npgsql Plugin
A plugin that provides extensions to Npgsql which enable usage of Npgsql as a .NET data provider for CrateDB.

This plugin is based on the official [Npgsql](https://github.com/npgsql/npgsql) project.

To use Npgsql with CrateDB, a special DatabaseInfoFactory-class has to be registered with a call to `NpgsqlDatabaseInfo.RegisterFactory`:

```c#
NpgsqlDatabaseInfo.RegisterFactory(new CrateDbDatabaseInfoFactory());
```

The factory has to be registered before opening connections to CrateDB.

Further, the type mappings of Npgsql have to be adapted for CrateDB with a call to `CrateDbDatabaseInfo.AddCrateDbSpecificTypeMappings`. This can either be done globally in `NpgsqlConnection.GlobalTypeMapper` or per connection:

```c#
using Npgsql.CrateDb;

...

using (var con = OpenConnection())
{
    CrateDbDatabaseInfo.AddCrateDbSpecificTypeMappings(con.TypeMapper);

    ...
}
```

We are working on changes to get rid of this step.

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

## Known Issues

There is a problem with the length parameter of row messages for Byte-Arrays and GeoShape-Arrays that is currently under investigation. This can lead to exceptions when closing a corresponding data reader. To bypass this problem, open data readers with `CommandBehavior.SequentialAccess` when working with those data types:

```c#
...

using (var command = new NpgsqlCommand("select my_geo_shape_array_column from my_table", con))
using (var reader = command.ExecuteReader(CommandBehavior.SequentialAccess))
{
    ...
}
```
