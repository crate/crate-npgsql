=====================
CrateDB Npgsql Plugin
=====================

Archived plugin
===============

The CrateDB Npgsql Plugin is archived and is not maintained. The plugin is not
required for connecting to CrateDB version 4.2 and later. 

CrateDB version 4.1 was released in January 2020, and it has reached end-of-life
and it has been deprecated.

For `.NET`_ developers, we encourage you to use the stock `Npgsql`_ drivers.
You can learn more by studying our `example`_ or our `csharp docs`_.


A `.NET`_ plugin for `Npgsql`_ that provides backwards compatibility support
for `CrateDB`_ versions 4.1.x and earlier. CrateDB versions 4.2 and later work
with Npgsql without the need for a plugin.

This plugin depends on a `fork`_ of the upstream Npgsql project to work. (Nuget
will handle this for you.)

Contributing
============

This project is primarily maintained by `Crate.io`_, but we welcome community
contributions!

See the `developer docs`_ and the `contribution docs`_ for more information.


Help
====

Looking for more help?

- Read the `project docs`_
- Check out our `support channels`_


.. _.NET: https://www.microsoft.com/net
.. _contribution docs: CONTRIBUTING.rst
.. _Crate.io: http://crate.io/
.. _CrateDB: https://github.com/crate/crate
.. _developer docs: DEVELOP.rst
.. _fork: https://github.com/crate/npgsql
.. _Npgsql: https://www.npgsql.org/
.. _project docs: https://crate.io/docs/clients/npgsql/en/latest/
.. _support channels: https://crate.io/support/
.. _example: https://github.com/crate/cratedb-examples/tree/main/by-language/csharp-npgsql
.. _csharp docs: https://cratedb.com/docs/guide/connect/csharp/index.html
