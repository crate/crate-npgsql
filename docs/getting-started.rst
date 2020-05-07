.. _getting-started:

===============
Getting started
===============

Install and get started with the :ref:`CrateDB Npgsql Plugin <index>`.

.. rubric:: Table of contents

.. contents::
   :local:

Install
=======

The Npgsql plugin is made available as `Npgsql.CrateDB Nuget package`_. Follow
the appropriate Nuget instructions to get the plugin installed.

.. CAUTION::

    For now, this plugin depends on a `fork`_ of the official `Npgsql
    project`_ to work. We are, however, working to get the necessary changes
    merged in upstream.

    Nuget will handle this for you.

.. SEEALSO::

    An `introduction to Nuget`_.

    Nuget instructions for `dotnet CLI`_ or `Visual Studio`_.

.. NOTE::

    If you're using a generic database program that can work with any ADO.NET
    provider but doesn't come with Npgsql or reference it directly, you can
    install Npgsql with the CrateDB plugin into your `Global Assembly Cache`_
    (GAC) with our custom `Npgsql Installer`_.

Next steps
==========

Once the plugin is installed, you probably want to :ref:`connect to CrateDB
<connect>`.

.. _Npgsql.CrateDB Nuget package: https://www.nuget.org/packages/Npgsql.CrateDb/
.. _dotnet CLI: https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-using-the-dotnet-cli
.. _fork: https://github.com/crate/npgsql
.. _Global Assembly Cache: https://docs.microsoft.com/en-us/dotnet/framework/app-domains/gac
.. _introduction to Nuget: https://docs.microsoft.com/en-us/nuget/what-is-nuget
.. _Npgsql Installer: https://cdn.crate.io/downloads/releases/npgsql/
.. _Npgsql project: https://github.com/npgsql/npgsql
.. _Visual Studio: https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio
