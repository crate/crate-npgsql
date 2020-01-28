===============
Developer Guide
===============

Documentation
=============

Prerequisites
-------------

Python 3 is required.

Setup
-----

To install the documentation project dependencies, run::

    $ ./bootstrap.sh

Writing Documentation
---------------------

The docs live under the ``docs`` directory.

The docs are written written with ReStructuredText_ and processed with Sphinx_.

Build the docs by running::

    $ bin/sphinx

The output can then be found in the ``out/html`` directory.

If you would like to live-reload the docs as you edit them, you can run::

    $ bin/sphinx dev

The docs are automatically built from Git by `Read the Docs`_ and there is
nothing special you need to do to get the live docs to update.

To make changes to the RTD configuration (e.g., to activate or deactivate a
release version), please contact the `@crate/docs`_ team.

.. _@crate/docs: https://github.com/orgs/crate/teams/docs
.. _Read the Docs: http://readthedocs.org
.. _ReStructuredText: http://docutils.sourceforge.net/rst.html
.. _Sphinx: http://sphinx-doc.org/
