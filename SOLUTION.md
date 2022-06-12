# Solution comments

## Adding items directly on list screen

I implemnted the simplest solution could be further improved by not requiring reload of the page and just refreshing the list.

## Rank api

It has URL /TodoItem/rank.
To update rank a post request needs to be sent that has the form { todoItemId: 12, rank: 10 }.
I have disabled authentication so it can be tested without having a login. Usualy APIs use some form of client outthentication when called form othere services.

## Order by rank

There are two links one orders with reload from the initial task and the other with JS in the description orders using JavaScript.
