.. _connect:

==================
Connect to CrateDB
==================

Connect to CrateDB using the :ref:`CrateDB Npgsql Plugin <index>`.

.. CAUTION::

    For CrateDB versions 4.2 and above, we recommend that you use the `stock
    Npgsql driver`_ instead of this one.

    This software is a legacy plugin for older versions of CrateDB that lacked
    full compatibility with Npgsql.


Setup
=====

Before connecting to CrateDB, you must set up the plugin by registering a
special CrateDB `DatabaseInfoFactory`_ subclass, like so:

.. code-block:: csharp

    NpgsqlDatabaseInfo.RegisterFactory(new CrateDbDatabaseInfoFactory());


The basics
==========

Connect to CrateDB using a standard `NpgsqlConnection`_ object, like so:

.. code-block:: csharp

    var connString = "Host=127.0.0.1;Username=crate;SSL Mode=Prefer";

    using (var conn = new NpgsqlConnection(connString))
    {
        conn.Open();
    }

Here, we are connecting as the ``crate`` user to a CrateDB node listening on
``127.0.0.1`` (localhost).

.. SEEALSO::

    A definitive `connection string parameters`_ reference.

.. NOTE::

    The default CrateDB schema is ``doc``, and if you do not specify a schema
    (using the ``Database`` connection parameter) this is what will be used.

    However, once connected, you can query any schema you like by specifying it
    in the query.

Next steps
==========

Use the standard `Npgsql documentation`_ for the rest of your setup process.

.. SEEALSO::

    The plugin :ref:`data-types`.

.. _connection string parameters: https://www.npgsql.org/doc/connection-string-parameters.html
.. _DatabaseInfoFactory: https://www.npgsql.org/doc/api/Npgsql.Internal.NpgsqlDatabaseInfo.html
.. _NpgsqlConnection: https://www.npgsql.org/doc/api/Npgsql.NpgsqlConnection.html
.. _Npgsql documentation: https://www.npgsql.org/doc/index.html
.. _stock Npgsql driver: https://www.npgsql.org/
.. _usual Npgsql way: https://www.npgsql.org/doc/index.html
