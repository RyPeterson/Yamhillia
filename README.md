# Yamhillia

## An open source farm mangement software

---

### Goals

* Fast at scale but also capable of running on a potato<sup>[1]</sup>.

* Full support for PostgreSQL (see "fast at scale") and SQLite (see "running on a potato").

* Easily deployed internally to a farm ("intranet"). This also includes the ability to run without connecting to an outside network (i.e. no remote services, like AWS, Google, etc.)

* Simple UI: A joke is bad if you have to explain it. Same will go for the client UI.

##### Code goals

* Close to 100% test coverage on server code (little obsessive to write unit test for an object with only properties). Same with the client if I can wrap my head around it.
* Adhere to the MIT license compatible libraries except for when there is not a reasonable alternative.
* Easy to set up. No need for manually writting to the desk with a magnetic needle.
* Sane SQL.
* No jQuery or Bootstrap for client components!

---

<sup>[1]</sup> potato: a lower end system. Initial goal will be a [Nanode](https://www.linode.com/pricing), or similar (1 GB RAM, 1 CPU core, 25gb storage), and would be a single server for client and server components.
