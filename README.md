# Couples Journal

A simple journaling app for couples to share their messages of love. This is my
first Raspberry Pi project in that this app will be served from a Raspberry Pi
for my wife and I to share endearing messages between each other.

The technology stack chosen:

- .NET 5 (RC 2)
- Blazor (Server)
- Entity Framework Core (SQLite)
- Raspberry Pi 3B+ (Ubuntu 20.10 Server)

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
- [x] As a user I should be able to reply to a journal entries written by my 
      partner
- [x] As a user I should be able to delete a journal entry that I wrote
- [x] As a user I should be able to delete a reply that I wrote
- [x] As a user I should be able to page journal results
- [x] As a user I want to be notified by email when my partner submits a 
      journal entry
- [x] As a user I want to be notified by email when my partner updates a 
      journal entry
- [x] As a user I want to be notified by email when my partner replies to one 
      of my journal entries
- [x] As a user I'd like to know how many replies a journal has when viewing 
      the journal list page
- [x] As a user I'd like to have a page that provides my analytics 
        - number of journals posted
        - number of replies

Other:

- [x] Hide register user on login page
- [ ] As a user I'd like to preview my journal before I submit (eg for 
      Markdown transformation)
- [ ] As a user I should be able to export journal entries as PDF for easy 
      backup, printing my entries as well as the ones shared with me
- [ ] As a user I should be able to confirm my account via email (currently 
      there is a button to fake this) when registering

Raspberry Pi Setup:

- [x] Set up SD card with Ubuntu 20.10 server
- [x] Configure OS, get latest updates, etc...
- [x] Enable SSH
- [x] Install .NET 5
- [x] Install Nginx
- [x] Open firewall to allow Nginx
- [ ] Configure Nginx to proxy to app
- [ ] Figure out how to run web app forever (if it crashes, restart it)

## Status

All features I marked in MVP are done. I'm working on some nice to haves and 
then finish the remaining server admin work.

.NET 5 runs well on the Pi 3B+ but it's a little slower than I'd want. I may 
end up moving this app to a Pi 4. That being said I'm okay with the performance 
of the Pi 3B+ right now.

The user authentication uses the individual user accounts option that you can
click on during project creation. I've tweaked it a little to change how users
are created. Instead of users being created with a username of an email 
address, you can now create a user with a proper username and displayname. Even
though I have the register page still showing I plan to turn that off and just
use the scripted accounts via the appsettings.json that I implemented. The app
should have only two users.

## Author(s)

Frank Hale &lt;frankhaledevelops@gmail.com&gt;

## Date

31 October 2020
