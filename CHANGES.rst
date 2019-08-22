==================================
Changes in Npgsql-Plugin for Crate
==================================

Unreleased
==========

-  Added a type mapping from ``geo_point`` (oid 600) to ``NpgsqlPoint``, to be
   able to handle ``geo_point`` values with CrateDB >= 4.1. There the streaming
   changed, so that ``geo_point`` values are no longer streamed as double array
   but as ``geo_point``.

2018/10/15 1.2.3
================

- initial release
