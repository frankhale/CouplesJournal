# Couples Journal

A simple journaling app for couples to share their messages of love. This is my
first Raspberry Pi project in that this app will be served from a Raspberry Pi
for my wife and I to share endearing messages between each other.

The technology stack chosen:

- .NET 5 (RC 1)
- Blazor (Server)
- Entity Framework Core (SQLite)
- Raspberry Pi 3B+ (hopefully, or 4 if push comes to shove)

## Requirements

The requirements are very simple so that the project will actually succeed.

Minimum Viable Product:

- [x] User accounts (provided by the Individual Accounts option when creating
      the project, SQLite instead of MSLocalDB)
- [x] As a user I should be able to login
- [x] As a user I should be able to submit a new journal entry
- [x] As a user I should be able to edit my journal entries
- [x] As a user I should be able to view my journal entries as well as my
      partners in a list with newest first
- [x] As a user I should be able to reply to a journal entries written by my partner
- [x] As a user I should be able to delete a journal entry that I wrote
- [x] As a user I should be able to delete a reply that I wrote
- [x] As a user I should be able to page journal results

Above and Beyond:

- As a user I should be able to export journal entries as PDF for easy backup,
  printing my entries as well as the ones shared with me
- As a user I want to be notified by email when a person submits or updates a
  journal entry or replies to a journal
- As a user I should be able to confirm my account via email (currently there is a
  button to fake this) when registering

## Status

All features I marked in MVP are done. Next up is code cleanup and to test the
hell out of this before I put it in production!!!

## Author(s)

Frank Hale &lt;frankhaledevelops@gmail.com&gt;

## Date

19 October 2020
