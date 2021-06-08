.. _index:

======================
CrateDB Npgsql Plug-In
======================


.. CAUTION::

    Please don't use this driver with recent versions of CrateDB.

    CrateDB versions prior to 4.2 needed a custom fork of the official `Npgsql`_
    `.NET`_ data provider for PostgreSQL, `Npgsql.CrateDb`_. CrateDB versions
    4.2 and later work with the vanilla Npgsql driver without the need for a
    plugin.

For general help using Npgsql, please consult the standard `Npgsql
documentation`_. For quickly getting started, please also consult the `basic
demonstration program for using CrateDB with vanilla Npgsql`_.


.. rubric:: Table of contents

.. toctree::
   :maxdepth: 2

   getting-started
   connect
   appendices/index

.. SEEALSO::

    The CrateDB Npgsql Plugin is an open source project and it is hosted on
    GitHub at `crate-npgsql`_.

.. _basic demonstration program for using CrateDB with vanilla Npgsql: https://github.com/crate/cratedb-examples/tree/main/spikes/npgsql-vanilla
.. _Crate.io: http://crate.io/
.. _CrateDB: https://crate.io/docs/crate/reference/
.. _crate-npgsql: https://github.com/crate/crate-npgsql
.. _.NET: https://www.microsoft.com/net
.. _Npgsql: https://www.npgsql.org/
.. _Npgsql documentation: https://www.npgsql.org/doc/index.html
.. _Npgsql.CrateDb: https://www.nuget.org/packages/Npgsql.CrateDb/
