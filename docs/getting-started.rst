.. _getting-started:

===============
Getting started
===============

Install and get started with the :ref:`CrateDB Npgsql Plugin <index>`.

.. CAUTION::

    For CrateDB versions 4.2 and above, we recommend that you use the `stock
    Npgsql driver`_ instead of this one.

    This software is a legacy plugin for older versions of CrateDB that lacked
    full compatibility with Npgsql.

.. rubric:: Table of contents

.. contents::
   :local:

Install
=======

The Npgsql plugin is made available as a NuGet package `Npgsql.CrateDb`_. Follow
the appropriate NuGet instructions to get the plugin installed.

.. NOTE::

    This plugin depends on a `fork`_ of the upstream Npgsql project to work.
    (Nuget will handle this for you.)

.. SEEALSO::

    An `introduction to NuGet`_.

    NuGet instructions for `dotnet CLI`_ or `Visual Studio`_.

.. NOTE::

    If you're using a generic database program that can work with any ADO.NET
    provider but doesn't come with Npgsql or references it directly, you can
    install Npgsql with the CrateDB plugin into your `Global Assembly Cache`_
    (GAC) with our custom `Npgsql Installer`_.

Next steps
==========

Once the plugin is installed, you probably want to :ref:`connect to CrateDB
<connect>`.


.. _dotnet CLI: https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-using-the-dotnet-cli
.. _fork: https://github.com/crate/npgsql
.. _Global Assembly Cache: https://docs.microsoft.com/en-us/dotnet/framework/app-domains/gac
.. _introduction to NuGet: https://docs.microsoft.com/en-us/nuget/what-is-nuget
.. _Npgsql.CrateDb: https://www.nuget.org/packages/Npgsql.CrateDb/
.. _Npgsql Installer: https://cdn.crate.io/downloads/releases/npgsql/
.. _Npgsql project: https://github.com/npgsql/npgsql
.. _stock Npgsql driver: https://www.npgsql.org/
.. _Visual Studio: https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio
