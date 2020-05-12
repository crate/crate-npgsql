.. _data-types:

==========
Data types
==========

.. rubric:: Table of contents

.. contents::
   :local:

.. _objects:

Objects
=======

The CrateDB object handler
--------------------------

This plugin provides a CrateDB object handler that allows you to read and write
arbitrary `CLR objects`_ from and CrateDB columns.

To use this feature, import the ``Npgsql.CrateDb`` namespace and call the
``UseCrateDbObjectHandler`` extension method on the `TypeMapper`_ of your
`NpgsqlConnection`_, like so:

.. code-block:: csharp

    using Npgsql.CrateDb;

    ...

    using (var con = OpenConnection())
    {
        con.TypeMapper.UseCrateDbObjectHandler();

        ...
    }

Reading
-------

Reading objects
...............

Objects can be read with the ``GetObject<T>(NpgsqlDataReader, int)`` extension
method, like so:

.. code-block:: csharp

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

.. NOTE::

    The ``GetObject<T>(NpgsqlDataReader, int)`` extension method is in the
    ``Npgsql.CrateDb`` namespace.

Reading object arrays
.....................

Object arrays can be read with the ``GetObjectArray<T>(NpgsqlDataReader, int)``
extension method, like so:

.. code-block:: csharp

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

.. NOTE::

    The ``GetObjectArray<T>(NpgsqlDataReader, int)`` extension method is in the
    ``Npgsql.CrateDb`` namespace.

Writing objects and object arrays
---------------------------------

You can set arbitrary CLR objects as values for parameters referencing columns
with the JSON `data type`_. For example:

.. code-block:: csharp

    ...

    command.Parameters.AddWithValue("@obj_field", NpgsqlTypes.NpgsqlDbType.Json,
                                    new { inner = "Zoon" });
    ...

Internally, `Newtonsoft.Json`_ is used to serialize objects to JSON strings and
vice versa.

An array of objects can be inserted into a table by setting the parameter
value of the object array column to a string array of JSON strings that
represent the objects. For example:

.. code-block:: csharp

    ...

    command.Parameters.AddWithValue("@obj_array", new string[] {
                                        "{\"inner\": \"Zoon1\"}",
                                        "{\"inner\": \"Zoon2\"}"
                                    });
    ...

.. _byte-arrays:

Byte arrays
===========

A CrateDB `byte array`_ is read into .NET as an array of either ``int2`` or
``char[]`` objects. Normally, to read those values into a ``byte[]`` object,
you could do something like this:

.. code-block:: csharp

    char[] array = reader.GetFieldValue<char[]>(ordinal);
    return array.Select(c => Convert.ToByte(c)).ToArray();

This plugin provides the ``GetBytes(NpgsqlDataReader, int)`` extension method,
which achieves the same result.

You can read a byte array like this:

.. code-block:: csharp

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

.. _byte array: https://crate.io/docs/crate/reference/en/latest/general/ddl/data-types.html#array
.. _CLR objects: https://en.wikipedia.org/wiki/Plain_old_CLR_object
.. _data type: https://www.npgsql.org/doc/types/basic.html
.. _Newtonsoft.Json: https://www.newtonsoft.com/json
.. _NpgsqlConnection: https://www.npgsql.org/doc/api/Npgsql.NpgsqlConnection.html
.. _TypeMapper: https://www.npgsql.org/doc/api/Npgsql.NpgsqlConnection.html#Npgsql_NpgsqlConnection_TypeMapper
