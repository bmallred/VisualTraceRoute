Author:			Bryan M. Allred
Last update:	6/5/2011

The intentions of the Parser is to allow users to change the data structure
without needing to recompile the source code. This may also aid in the
flexibility to allow swapping of graphing utilities.

Available tokens are:

{destination}
{address}
{hop}
{hostname}
{roundtrip}

Also, understanding there will be multiple replies on any given trace there
has to be a way to mark repeating text. This is done with the following
text:

{routes}
<!-- Insert repeating text here -->

{hops}
<!-- Insert more repeating text here -->
{/hops}
{/routes}

Some times there needs to be a separator between iterations:

{routes}
<!-- Something -->
{/routes},

or

{routes}
{hops}
<!-- Something -->
{/hops},
{/routes}