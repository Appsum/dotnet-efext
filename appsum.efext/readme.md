# Commands

## migrations | migration | m
- list | ls: List all the migrations
- add | a: Add a new migration by name
- remove | rm: Removes the last migration or all migrations to the NAME
- remove-interactive | rmi: Removes all migrations to the one chosen from the list. This is powered by the ls command running first.
- script | s: Create a script FROM and TO a migration, FROM defaults to first, TO defaults to last. FROM can be later than TO to have a rollback script generated
- script-interactive | si: Create a script by getting the FROM and TO as a selection list to choose from. This is powered by the ls command running first and being parsed

## database | db

- update | u: Updates to the latest migration, a NAME or 0 to revert all
- update-interactive | ui: Updates to a migration that can be chosen from a selection list. This is powered by the ls command running first.
- drop | d: drop the database