# Couples Journal

A simple journaling app for couples to share their messages of love. This is my first Raspberry Pi project in that
this app will be served from a Raspberry Pi for my wife and I to share endearing messages between each other.

The technology stack chosen:

- .NET 5 (RC 1)
- Blazor (Server)
- Entity Framework Core (SQLite)
- Raspberry Pi 3B+ (hopefully, or 4 if push comes to shove)

## Requirements

The requirements are very simple so that the project will actually succeed.

Minimum Viable Product: 

- User accounts (provided by the Individual Accounts option when creating the project, SQLite instead of MSLocalDB)
- As a user I should be able to login and submit a new journal entry
- As a user I should be have a rich text WYSIWYG style text area to enter my journal entry or reply
- As a user I should be able to view my journal entries as well as my partners in a list with newest first
- As a user I should be able to edit my journal entries
- As a user I should be able to reply to a journal entries written by my partner
- As a user I should be able to delete a journal entry that I wrote
- As a user I should be able to delete a reply that I wrote

Above and Beyond:

- As a user I should be able to export journal entries as PDF for easy backup, printing my entries as well as the ones shared with me
- As a user I want to be notified by email when a person submits or updates a journal entry or replies to a journal

## Status

Just getting off the ground! No UI has been created yet. I'm working on the DB entities and API. Will transition to UI very
soon.

## Author(s)

Frank Hale &lt;frankhaledevelops@gmail.com&gt;

## Date

4 October 2020
